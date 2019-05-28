﻿using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Views
{
    public partial class WindowOrganizationComparerWebResources : WindowBase
    {
        private readonly object sysObjectConnections = new object();

        private readonly IWriteToOutput _iWriteToOutput;

        private readonly Dictionary<Guid, IOrganizationServiceExtented> _cacheService = new Dictionary<Guid, IOrganizationServiceExtented>();

        private readonly CommonConfiguration _commonConfig;

        private readonly ObservableCollection<EntityViewItem> _itemsSource;

        public WindowOrganizationComparerWebResources(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filter
        )
        {
            this.IncreaseInit();

            InputLanguageManager.SetInputLanguage(this, CultureInfo.CreateSpecificCulture("en-US"));

            this._iWriteToOutput = iWriteToOutput;
            this._commonConfig = commonConfig;

            BindingOperations.EnableCollectionSynchronization(connection1.ConnectionConfiguration.Connections, sysObjectConnections);

            InitializeComponent();

            cmBType.ItemsSource = new EnumBindingSourceExtension(typeof(WebResource.Schema.OptionSets.webresourcetype?)).ProvideValue(null) as IEnumerable;

            tSDDBConnection1.Header = string.Format(Properties.OperationNames.ExportFromConnectionFormat1, connection1.Name);
            tSDDBConnection2.Header = string.Format(Properties.OperationNames.ExportFromConnectionFormat1, connection2.Name);

            this.Resources["ConnectionName1"] = string.Format(Properties.OperationNames.CreateFromConnectionFormat1, connection1.Name);
            this.Resources["ConnectionName2"] = string.Format(Properties.OperationNames.CreateFromConnectionFormat1, connection2.Name);

            LoadFromConfig();

            if (!string.IsNullOrEmpty(filter))
            {
                txtBFilter.Text = filter;
            }

            txtBFilter.SelectionLength = 0;
            txtBFilter.SelectionStart = txtBFilter.Text.Length;

            txtBFilter.Focus();

            this._itemsSource = new ObservableCollection<EntityViewItem>();

            this.lstVwWebResources.ItemsSource = _itemsSource;

            cmBConnection1.ItemsSource = connection1.ConnectionConfiguration.Connections;
            cmBConnection1.SelectedItem = connection1;

            cmBConnection2.ItemsSource = connection1.ConnectionConfiguration.Connections;
            cmBConnection2.SelectedItem = connection2;

            this.DecreaseInit();

            ShowExistingWebResources();
        }

        private void LoadFromConfig()
        {
            cmBFileAction.DataContext = _commonConfig;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _commonConfig.Save();
        }

        private ConnectionData GetConnection1()
        {
            ConnectionData connectionData = null;

            cmBConnection1.Dispatcher.Invoke(() =>
            {
                connectionData = cmBConnection1.SelectedItem as ConnectionData;
            });

            return connectionData;
        }

        private ConnectionData GetConnection2()
        {
            ConnectionData connectionData = null;

            cmBConnection1.Dispatcher.Invoke(() =>
            {
                connectionData = cmBConnection2.SelectedItem as ConnectionData;
            });

            return connectionData;
        }

        private async Task<IOrganizationServiceExtented> GetService1()
        {
            return await GetService(GetConnection1());
        }

        private async Task<IOrganizationServiceExtented> GetService2()
        {
            return await GetService(GetConnection2());
        }

        private async Task<IOrganizationServiceExtented> GetService(ConnectionData connectionData)
        {
            if (connectionData != null)
            {
                if (!_cacheService.ContainsKey(connectionData.ConnectionId))
                {
                    _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);
                    _iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());
                    var service = await QuickConnection.ConnectAsync(connectionData);
                    _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

                    _cacheService[connectionData.ConnectionId] = service;
                }

                return _cacheService[connectionData.ConnectionId];
            }

            return null;
        }

        private async Task ShowExistingWebResources()
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.LoadingWebResources);

            string textName = string.Empty;
            bool? hidden = null;
            bool? managed = null;
            int? webResourceType = null;

            this.Dispatcher.Invoke(() =>
            {
                textName = txtBFilter.Text.Trim().ToLower();

                if (cmBManaged.SelectedItem is ComboBoxItem comboBoxItemManaged
                    && comboBoxItemManaged.Tag != null
                    && comboBoxItemManaged.Tag is bool boolManaged
                )
                {
                    managed = boolManaged;
                }

                if (cmBHidden.SelectedItem is ComboBoxItem comboBoxItemHidden
                    && comboBoxItemHidden.Tag != null
                    && comboBoxItemHidden.Tag is bool boolHidden
                )
                {
                    hidden = boolHidden;
                }

                if (cmBType.SelectedItem is WebResource.Schema.OptionSets.webresourcetype webresourcetype)
                {
                    webResourceType = (int)webresourcetype;
                }
            });

            this._itemsSource.Clear();

            IEnumerable<LinkedEntities<WebResource>> list = Enumerable.Empty<LinkedEntities<WebResource>>();

            try
            {
                var service1 = await GetService1();
                var service2 = await GetService2();

                if (service1 != null && service2 != null)
                {
                    var columnSet = new ColumnSet
                    (
                        WebResource.Schema.Attributes.name
                        , WebResource.Schema.Attributes.displayname
                        , WebResource.Schema.Attributes.webresourcetype
                        , WebResource.Schema.Attributes.ismanaged
                        , WebResource.Schema.Attributes.ishidden
                    );

                    List<LinkedEntities<WebResource>> temp = new List<LinkedEntities<WebResource>>();

                    if (service1.ConnectionData.ConnectionId != service2.ConnectionData.ConnectionId)
                    {
                        var repository1 = new WebResourceRepository(service1);
                        var repository2 = new WebResourceRepository(service2);

                        var task1 = repository1.GetListSupportsTextAsync(textName, webResourceType, managed, hidden, columnSet);
                        var task2 = repository2.GetListSupportsTextAsync(textName, webResourceType, managed, hidden, columnSet);

                        var list1 = await task1;
                        var list2 = await task2;

                        foreach (var webresource1 in list1)
                        {
                            var webresource2 = list2.FirstOrDefault(c => string.Equals(c.Name, webresource1.Name, StringComparison.InvariantCultureIgnoreCase));

                            if (webresource2 == null)
                            {
                                continue;
                            }

                            temp.Add(new LinkedEntities<WebResource>(webresource1, webresource2));
                        }
                    }
                    else
                    {
                        var repository1 = new WebResourceRepository(service1);

                        var task1 = repository1.GetListSupportsTextAsync(textName, webResourceType, managed, hidden, columnSet);

                        var list1 = await task1;

                        foreach (var webresource1 in list1)
                        {
                            temp.Add(new LinkedEntities<WebResource>(webresource1, null));
                        }
                    }

                    list = temp;
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(null, ex);
            }

            list = FilterList(list, textName);

            LoadEntities(list);
        }

        private static IEnumerable<LinkedEntities<WebResource>> FilterList(IEnumerable<LinkedEntities<WebResource>> list, string textName)
        {
            if (!string.IsNullOrEmpty(textName))
            {
                textName = textName.ToLower();

                if (Guid.TryParse(textName, out Guid tempGuid))
                {
                    list = list.Where(ent =>
                        ent.Entity1?.Id == tempGuid
                        || ent.Entity2?.Id == tempGuid
                        || ent.Entity1?.WebResourceIdUnique == tempGuid
                        || ent.Entity2?.WebResourceIdUnique == tempGuid
                    );
                }
                else
                {
                    list = list.Where(ent =>
                    {
                        var name1 = ent.Entity1?.Name.ToLower() ?? string.Empty;
                        var name2 = ent.Entity2?.Name.ToLower() ?? string.Empty;

                        var displayname1 = ent.Entity1?.DisplayName.ToLower() ?? string.Empty;
                        var displayname2 = ent.Entity2?.DisplayName.ToLower() ?? string.Empty;

                        return name1.Contains(textName)
                            || name2.Contains(textName)
                            || displayname1.Contains(textName)
                            || displayname2.Contains(textName)
                            ;
                    });
                }
            }

            return list;
        }

        private class EntityViewItem
        {
            public string TypeName { get; private set; }

            public string WebResourceName { get; private set; }

            public string DisplayName1 { get; private set; }

            public string DisplayName2 { get; private set; }

            public LinkedEntities<WebResource> Link { get; private set; }

            public EntityViewItem(string webresourceName, string typeName, string displayName1, string displayName2, LinkedEntities<WebResource> link)
            {
                this.TypeName = typeName;
                this.WebResourceName = webresourceName;
                this.DisplayName1 = displayName1;
                this.DisplayName2 = displayName2;
                this.Link = link;
            }
        }

        private void LoadEntities(IEnumerable<LinkedEntities<WebResource>> results)
        {
            this.lstVwWebResources.Dispatcher.Invoke(() =>
            {
                foreach (var link in results
                      .OrderBy(ent => ent.Entity1.WebResourceType.Value)
                      .ThenBy(ent => ent.Entity1.Name)
                      .ThenBy(ent => ent.Entity1.DisplayName)
                      .ThenBy(ent => ent.Entity2?.Name)
                      .ThenBy(ent => ent.Entity2?.DisplayName)
                  )
                {
                    var item = new EntityViewItem(
                        link.Entity1.FormattedValues[WebResource.Schema.Attributes.webresourcetype]
                        , link.Entity1.Name
                        , link.Entity1.DisplayName
                        , link.Entity2?.DisplayName
                        , link);

                    this._itemsSource.Add(item);
                }

                if (this.lstVwWebResources.Items.Count == 1)
                {
                    this.lstVwWebResources.SelectedItem = this.lstVwWebResources.Items[0];
                }
            });

            ToggleControls(true, Properties.WindowStatusStrings.LoadingWebResourcesCompletedFormat1, results.Count());
        }

        private void UpdateStatus(string format, params object[] args)
        {
            string message = format;

            if (args != null && args.Length > 0)
            {
                message = string.Format(format, args);
            }

            _iWriteToOutput.WriteToOutput(null, message);

            this.stBIStatus.Dispatcher.Invoke(() =>
            {
                this.stBIStatus.Content = message;
            });
        }

        private void ToggleControls(bool enabled, string statusFormat, params object[] args)
        {
            this.ChangeInitByEnabled(enabled);

            UpdateStatus(statusFormat, args);

            ToggleControl(this.tSProgressBar
                , this.cmBConnection1
                , this.cmBConnection2
                , this.cmBType
                , this.cmBManaged
                , this.cmBHidden
            );

            UpdateButtonsEnable();
        }

        private void UpdateButtonsEnable()
        {
            this.lstVwWebResources.Dispatcher.Invoke(() =>
            {
                try
                {
                    bool enabled = this.IsControlsEnabled && this.lstVwWebResources.SelectedItems.Count > 0;

                    var item = (this.lstVwWebResources.SelectedItems[0] as EntityViewItem);

                    tSDDBShowDifference.IsEnabled = enabled && item.Link.Entity1 != null && item.Link.Entity2 != null;
                    tSDDBConnection1.IsEnabled = enabled && item.Link.Entity1 != null;
                    tSDDBConnection2.IsEnabled = enabled && item.Link.Entity2 != null;
                }
                catch (Exception)
                {
                }
            });
        }

        private void txtBFilterEnitity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowExistingWebResources();
            }
        }

        private EntityViewItem GetSelectedEntity()
        {
            return this.lstVwWebResources.SelectedItems.OfType<EntityViewItem>().Count() == 1
                ? this.lstVwWebResources.SelectedItems.OfType<EntityViewItem>().SingleOrDefault() : null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstVwEntities_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (((FrameworkElement)e.OriginalSource).DataContext is EntityViewItem item)
                {
                    ExecuteAction(item.Link, true, PerformShowingDifferenceContentAsync);
                }
            }
        }

        private void lstVwEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonsEnable();
        }

        private void ExecuteAction(LinkedEntities<WebResource> linked, bool showAllways, Func<LinkedEntities<WebResource>, bool, Task> action)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            if (linked.Entity1 == null || linked.Entity2 == null || linked.Entity1 == linked.Entity2)
            {
                return;
            }

            if (string.IsNullOrEmpty(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportIsEmpty);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }
            else if (!Directory.Exists(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportDoesNotExistsFormat1, _commonConfig.FolderForExport);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }

            action(linked, showAllways);
        }

        private Task<string> CreateDescriptionFileAsync(ConnectionData connectionData, string name, string fieldTitle, string description)
        {
            return Task.Run(() => CreateDescriptionFile(connectionData, name, fieldTitle, description));
        }

        private string CreateDescriptionFile(ConnectionData connectionData, string name, string fieldTitle, string description)
        {
            string fileName = EntityFileNameFormatter.GetWebResourceFileName(connectionData.Name, name, fieldTitle, "txt");
            string filePath = Path.Combine(_commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileName));

            if (!string.IsNullOrEmpty(description))
            {
                try
                {
                    File.WriteAllText(filePath, description, new UTF8Encoding(false));

                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, WebResource.Schema.EntityLogicalName, name, fieldTitle, filePath);
                }
                catch (Exception ex)
                {
                    this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                }
            }
            else
            {
                filePath = string.Empty;
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, WebResource.Schema.EntityLogicalName, name, fieldTitle);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
            }

            return filePath;
        }

        private void btnShowDifferenceAll_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteAction(link.Link, false, PerformShowingDifferenceAllAsync);
        }

        private async Task PerformShowingDifferenceAllAsync(LinkedEntities<WebResource> linked, bool showAllways)
        {
            await PerformShowingDifferenceContentAsync(linked, showAllways);

            await PerformShowingDifferenceDescriptionAsync(linked, showAllways);
        }

        private void ExecuteActionLinked(
            LinkedEntities<WebResource> linked
            , bool showAllways
            , string fieldName
            , string fieldTitle
            , string extension
            , Func<LinkedEntities<WebResource>, bool, string, string, string, Task> action
        )
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            if (string.IsNullOrEmpty(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportIsEmpty);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }
            else if (!Directory.Exists(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportDoesNotExistsFormat1, _commonConfig.FolderForExport);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }

            action(linked, showAllways, fieldName, fieldTitle, extension);
        }

        private void mIShowDifferenceEntityDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteAction(link.Link, true, PerformShowingDifferenceDescriptionAsync);
        }

        private async Task PerformShowingDifferenceDescriptionAsync(LinkedEntities<WebResource> linked, bool showAllways)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.ShowingDifferenceEntityDescription);

            var service1 = await GetService1();
            var service2 = await GetService2();

            if (service1 != null && service2 != null)
            {
                var repository1 = new WebResourceRepository(service1);
                var repository2 = new WebResourceRepository(service2);

                var webResource1 = await repository1.GetByIdAsync(linked.Entity1.Id, new ColumnSet(true));
                var webResource2 = await repository2.GetByIdAsync(linked.Entity2.Id, new ColumnSet(true));

                var desc1 = await EntityDescriptionHandler.GetEntityDescriptionAsync(webResource1, EntityFileNameFormatter.WebResourceIgnoreFields);
                var desc2 = await EntityDescriptionHandler.GetEntityDescriptionAsync(webResource2, EntityFileNameFormatter.WebResourceIgnoreFields);

                if (showAllways || desc1 != desc2)
                {
                    string filePath1 = await CreateDescriptionFileAsync(service1.ConnectionData, webResource1.Name, EntityFileNameFormatter.Headers.EntityDescription, desc1);
                    string filePath2 = await CreateDescriptionFileAsync(service2.ConnectionData, webResource2.Name, EntityFileNameFormatter.Headers.EntityDescription, desc2);

                    if (File.Exists(filePath1) && File.Exists(filePath2))
                    {
                        this._iWriteToOutput.ProcessStartProgramComparer(filePath1, filePath2, Path.GetFileName(filePath1), Path.GetFileName(filePath2));
                    }
                    else
                    {
                        this._iWriteToOutput.PerformAction(service1.ConnectionData, filePath1);

                        this._iWriteToOutput.PerformAction(service2.ConnectionData, filePath2);
                    }
                }
            }

            ToggleControls(true, Properties.WindowStatusStrings.ShowingDifferenceEntityDescriptionCompleted);
        }

        private void ExecuteActionDescription(Guid idWebResource, string name, Func<Task<IOrganizationServiceExtented>> getService, Func<Guid, string, Func<Task<IOrganizationServiceExtented>>, Task> action)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            if (string.IsNullOrEmpty(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportIsEmpty);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }
            else if (!Directory.Exists(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportDoesNotExistsFormat1, _commonConfig.FolderForExport);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }

            action(idWebResource, name, getService);
        }

        private async Task PerformExportDescriptionToFile(Guid idWebResource, string name, Func<Task<IOrganizationServiceExtented>> getService)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.CreatingEntityDescription);

            var service = await getService();

            if (service != null)
            {
                WebResourceRepository webResourceRepository = new WebResourceRepository(service);

                var webresource = await webResourceRepository.GetByIdAsync(idWebResource, new ColumnSet(true));

                var description = await EntityDescriptionHandler.GetEntityDescriptionAsync(webresource, EntityFileNameFormatter.WebResourceIgnoreFields, service.ConnectionData);

                string filePath = await CreateDescriptionFileAsync(service.ConnectionData, webresource.Name, EntityFileNameFormatter.Headers.EntityDescription, description);

                this._iWriteToOutput.PerformAction(service.ConnectionData, filePath);
            }

            ToggleControls(true, Properties.WindowStatusStrings.CreatingEntityDescriptionCompleted);
        }

        private void mIExportWebResource1EntityDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity1 != null)
            {
                return;
            }

            ExecuteActionDescription(link.Link.Entity1.Id, link.Link.Entity1.Name, GetService1, PerformExportDescriptionToFile);
        }

        private void mIExportWebResource2EntityDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity2 != null)
            {
                return;
            }

            ExecuteActionDescription(link.Link.Entity2.Id, link.Link.Entity2.Name, GetService2, PerformExportDescriptionToFile);
        }

        private void mIExportWebResource1Content_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity1 != null)
            {
                return;
            }

            ExecuteActionDescription(link.Link.Entity1.Id, link.Link.Entity1.Name, GetService1, PerformDownloadWebResourceAsync);
        }

        private void mIExportWebResource2Content_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity2 != null)
            {
                return;
            }

            ExecuteActionDescription(link.Link.Entity2.Id, link.Link.Entity2.Name, GetService2, PerformDownloadWebResourceAsync);
        }

        private async Task PerformDownloadWebResourceAsync(Guid idWebResource, string name, Func<Task<IOrganizationServiceExtented>> getService)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.ExportingWebResourceContentFormat1, name);

            var service = await getService();

            if (service != null)
            {
                WebResourceRepository webResourceRepository = new WebResourceRepository(service);

                var webresource = await webResourceRepository.GetByIdAsync(idWebResource, new ColumnSet(true));

                this._iWriteToOutput.WriteToOutput(service.ConnectionData, "Starting downloading {0}", webresource.Name);

                string filePath = await CreateFileWithContentAsync(service.ConnectionData.Name, webresource);

                this._iWriteToOutput.WriteToOutput(service.ConnectionData, "Web-resource '{0}' has downloaded to {1}.", webresource.Name, filePath);

                this._iWriteToOutput.PerformAction(service.ConnectionData, filePath);
            }

            ToggleControls(true, Properties.WindowStatusStrings.ExportingWebResourceContentCompletedFormat1, name);
        }

        private Task<string> CreateFileWithContentAsync(string connectionName, WebResource webresource)
        {
            return Task.Run(() => CreateFileWithContent(connectionName, webresource));
        }

        private string CreateFileWithContent(string connectionName, WebResource webresource)
        {
            var webResourceFileName = WebResourceRepository.GetWebResourceFileName(webresource);

            var contentWebResource = webresource.Content ?? string.Empty;

            if (!string.IsNullOrEmpty(contentWebResource))
            {
                var array = Convert.FromBase64String(contentWebResource);

                string localFileName = string.Format("{0}.{1}", connectionName, webResourceFileName);
                string filePath = Path.Combine(_commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(localFileName));

                File.WriteAllBytes(filePath, array);

                return filePath;
            }

            return string.Empty;
        }

        private void mIShowDifferenceContent_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteAction(link.Link, true, PerformShowingDifferenceContentAsync);
        }

        private async Task PerformShowingDifferenceContentAsync(LinkedEntities<WebResource> linked, bool showAllways)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.ShowingDifferenceWebResourcesFormat1, linked.Entity1.Name);

            var service1 = await GetService1();
            var service2 = await GetService2();

            if (service1 != null && service2 != null)
            {
                var repository1 = new WebResourceRepository(service1);
                var repository2 = new WebResourceRepository(service2);

                var webResource1 = await repository1.GetByIdAsync(linked.Entity1.Id, new ColumnSet(true));
                var webResource2 = await repository2.GetByIdAsync(linked.Entity2.Id, new ColumnSet(true));

                if (showAllways || webResource1.Content != webResource2.Content)
                {
                    string filePath1 = await CreateFileWithContentAsync(service1.ConnectionData.Name, webResource1);
                    string filePath2 = await CreateFileWithContentAsync(service2.ConnectionData.Name, webResource2);

                    if (File.Exists(filePath1) && File.Exists(filePath2))
                    {
                        this._iWriteToOutput.ProcessStartProgramComparer(filePath1, filePath2, Path.GetFileName(filePath1), Path.GetFileName(filePath2));
                    }
                    else
                    {
                        this._iWriteToOutput.PerformAction(service1.ConnectionData, filePath1);

                        this._iWriteToOutput.PerformAction(service2.ConnectionData, filePath2);
                    }
                }
            }

            ToggleControls(true, Properties.WindowStatusStrings.ShowingDifferenceWebResourcesCompletedFormat1, linked.Entity1.Name);
        }

        private void mIShowDifferenceContentJson_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteActionLinked(link.Link, true, WebResource.Schema.Attributes.contentjson, SavedQuery.Schema.Headers.fetchxml, "json", PerformShowingDifferenceSingleXmlAsync);
        }

        private void mIShowDifferenceDependencyXml_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteActionLinked(link.Link, true, WebResource.Schema.Attributes.dependencyxml, SavedQuery.Schema.Headers.fetchxml, "xml", PerformShowingDifferenceSingleXmlAsync);
        }

        private async Task PerformShowingDifferenceSingleXmlAsync(LinkedEntities<WebResource> linked, bool showAllways, string fieldName, string fieldTitle, string extension)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.ShowingDifferenceXmlForFieldFormat1, fieldName);

            try
            {
                var service1 = await GetService1();
                var service2 = await GetService2();

                if (service1 != null && service2 != null)
                {
                    var repository1 = new WebResourceRepository(service1);
                    var repository2 = new WebResourceRepository(service2);

                    var webResource1 = await repository1.GetByIdAsync(linked.Entity1.Id, new ColumnSet(true));
                    var webResource2 = await repository2.GetByIdAsync(linked.Entity2.Id, new ColumnSet(true));

                    string xml1 = webResource1.GetAttributeValue<string>(fieldName);
                    string xml2 = webResource2.GetAttributeValue<string>(fieldName);

                    if (showAllways || !ContentCoparerHelper.CompareXML(xml1, xml2, false).IsEqual)
                    {
                        string filePath1 = await CreateFileAsync(service1.ConnectionData, webResource1.Name, fieldTitle, extension, xml1);
                        string filePath2 = await CreateFileAsync(service2.ConnectionData, webResource2.Name, fieldTitle, extension, xml2);

                        if (File.Exists(filePath1) && File.Exists(filePath2))
                        {
                            this._iWriteToOutput.ProcessStartProgramComparer(filePath1, filePath2, Path.GetFileName(filePath1), Path.GetFileName(filePath2));
                        }
                        else
                        {
                            this._iWriteToOutput.PerformAction(service1.ConnectionData, filePath1);

                            this._iWriteToOutput.PerformAction(service2.ConnectionData, filePath2);
                        }
                    }
                }

                ToggleControls(true, Properties.WindowStatusStrings.ShowingDifferenceXmlForFieldCompletedFormat1, fieldName);
            }
            catch (Exception ex)
            {
                _iWriteToOutput.WriteErrorToOutput(null, ex);

                ToggleControls(true, Properties.WindowStatusStrings.ShowingDifferenceXmlForFieldFailedFormat1, fieldName);
            }
        }

        private Task<string> CreateFileAsync(ConnectionData connectionData, string name, string fieldTitle, string xmlContent, string extension)
        {
            return Task.Run(() => CreateFile(connectionData, name, fieldTitle, xmlContent, extension));
        }

        private string CreateFile(ConnectionData connectionData, string name, string fieldTitle, string xmlContent, string extension)
        {
            name = Path.GetFileName(name);

            if (connectionData == null)
            {
                return null;
            }

            string fileName = EntityFileNameFormatter.GetWebResourceFileName(connectionData.Name, name, fieldTitle, extension);
            string filePath = Path.Combine(_commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileName));

            if (!string.IsNullOrEmpty(xmlContent))
            {
                try
                {
                    if (ContentCoparerHelper.TryParseXml(xmlContent, out var doc))
                    {
                        xmlContent = doc.ToString();
                    }

                    File.WriteAllText(filePath, xmlContent, new UTF8Encoding(false));

                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, WebResource.Schema.EntityLogicalName, name, fieldTitle, filePath);
                }
                catch (Exception ex)
                {
                    this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                }
            }
            else
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, WebResource.Schema.EntityLogicalName, name, fieldTitle);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
            }

            return filePath;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;

                ShowExistingWebResources();
            }

            base.OnKeyDown(e);
        }

        private void cmBCurrentConnection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            this.Dispatcher.Invoke(() =>
            {
                this._itemsSource?.Clear();

                ConnectionData connection1 = cmBConnection1.SelectedItem as ConnectionData;
                ConnectionData connection2 = cmBConnection2.SelectedItem as ConnectionData;

                if (connection1 != null && connection2 != null)
                {
                    tSDDBConnection1.Header = string.Format(Properties.OperationNames.ExportFromConnectionFormat1, connection1.Name);
                    tSDDBConnection2.Header = string.Format(Properties.OperationNames.ExportFromConnectionFormat1, connection2.Name);

                    this.Resources["ConnectionName1"] = string.Format(Properties.OperationNames.CreateFromConnectionFormat1, connection1.Name);
                    this.Resources["ConnectionName2"] = string.Format(Properties.OperationNames.CreateFromConnectionFormat1, connection2.Name);

                    UpdateButtonsEnable();

                    ShowExistingWebResources();
                }
            });
        }

        private async void btnOrganizationComparer_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service = await GetService1();

            WindowHelper.OpenOrganizationComparerWindow(this._iWriteToOutput, service.ConnectionData.ConnectionConfiguration, _commonConfig);
        }

        private async void btnExportWebResources1_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            _commonConfig.Save();

            var service = await GetService1();

            WindowHelper.OpenWebResourceExplorerWindow(this._iWriteToOutput, service, _commonConfig, entity?.WebResourceName ?? txtBFilter.Text);
        }

        private async void btnExportWebResources2_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            _commonConfig.Save();

            var service = await GetService2();

            WindowHelper.OpenWebResourceExplorerWindow(this._iWriteToOutput, service, _commonConfig, entity?.WebResourceName ?? txtBFilter.Text);
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (!(sender is ContextMenu contextMenu))
            {
                return;
            }

            var linkedEntityMetadata = ((FrameworkElement)e.OriginalSource).DataContext as EntityViewItem;

            var items = contextMenu.Items.OfType<Control>();

            foreach (var menuContextDifference in items.Where(i => string.Equals(i.Uid, "menuContextDifference", StringComparison.InvariantCultureIgnoreCase)))
            {
                menuContextDifference.IsEnabled = false;
                menuContextDifference.Visibility = Visibility.Collapsed;

                if (linkedEntityMetadata != null
                     && linkedEntityMetadata.Link != null
                     && linkedEntityMetadata.Link.Entity1 != null
                     && linkedEntityMetadata.Link.Entity2 != null
                )
                {
                    menuContextDifference.IsEnabled = true;
                    menuContextDifference.Visibility = Visibility.Visible;
                }
            }

            foreach (var menuContextConnection2 in items.Where(i => string.Equals(i.Uid, "menuContextConnection2", StringComparison.InvariantCultureIgnoreCase)))
            {
                menuContextConnection2.IsEnabled = false;
                menuContextConnection2.Visibility = Visibility.Collapsed;

                if (linkedEntityMetadata != null
                    && linkedEntityMetadata.Link != null
                    && linkedEntityMetadata.Link.Entity2 != null
                )
                {
                    menuContextConnection2.IsEnabled = true;
                    menuContextConnection2.Visibility = Visibility.Visible;
                }
            }
        }

        private async void mIConnection1OpenSolutionComponentInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            var service = await GetService1();

            if (service != null)
            {
                service.UrlGenerator.OpenSolutionComponentInWeb(ComponentType.WebResource, entity.Link.Entity1.Id);
            }
        }

        private async void mIConnection2OpenSolutionComponentInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            var service = await GetService2();

            if (service != null)
            {
                service.UrlGenerator.OpenSolutionComponentInWeb(ComponentType.WebResource, entity.Link.Entity2.Id);
            }
        }

        private void ExecuteActionEntity(Guid idWebResource, Func<Task<IOrganizationServiceExtented>> getService, string fieldName, string fieldTitle, string extension, Func<Guid, Func<Task<IOrganizationServiceExtented>>, string, string, string, Task> action)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            if (string.IsNullOrEmpty(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportIsEmpty);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }
            else if (!Directory.Exists(_commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportDoesNotExistsFormat1, _commonConfig.FolderForExport);
                _commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }

            action(idWebResource, getService, fieldName, fieldTitle, extension);
        }

        private void mIExportWebResource1ContentJson_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity1 != null)
            {
                return;
            }

            ExecuteActionEntity(link.Link.Entity1.Id, GetService1, WebResource.Schema.Attributes.contentjson, WebResource.Schema.Headers.contentjson, "json", PerformExportXmlToFileAsync);
        }

        private void mIExportWebResource2ContentJson_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity2 != null)
            {
                return;
            }

            ExecuteActionEntity(link.Link.Entity2.Id, GetService2, WebResource.Schema.Attributes.contentjson, WebResource.Schema.Headers.contentjson, "json", PerformExportXmlToFileAsync);
        }

        private void mIExportWebResource1DependencyXml_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity1 != null)
            {
                return;
            }

            ExecuteActionEntity(link.Link.Entity1.Id, GetService1, WebResource.Schema.Attributes.dependencyxml, WebResource.Schema.Headers.dependencyxml, "xml", PerformExportXmlToFileAsync);
        }

        private void mIExportWebResource2DependencyXml_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity2 != null)
            {
                return;
            }

            ExecuteActionEntity(link.Link.Entity2.Id, GetService2, WebResource.Schema.Attributes.dependencyxml, WebResource.Schema.Headers.dependencyxml, "xml", PerformExportXmlToFileAsync);
        }

        private async Task PerformExportXmlToFileAsync(Guid idWebResource, Func<Task<IOrganizationServiceExtented>> getService, string fieldName, string fieldTitle, string extension)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.WindowStatusStrings.ExportingXmlFieldToFileFormat1, fieldTitle);

            var service = await getService();

            if (service != null)
            {
                var repository = new WebResourceRepository(service);

                var webResource = await repository.GetByIdAsync(idWebResource, new ColumnSet(true));

                string xmlContent = webResource.GetAttributeValue<string>(fieldName);

                string filePath = await CreateFileAsync(service.ConnectionData, webResource.Name, fieldTitle, extension, xmlContent);

                this._iWriteToOutput.PerformAction(service.ConnectionData, filePath);
            }

            ToggleControls(true, Properties.WindowStatusStrings.ExportingXmlFieldToFileCompletedFormat1, fieldName);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsControlsEnabled)
            {
                return;
            }

            ShowExistingWebResources();
        }
    }
}