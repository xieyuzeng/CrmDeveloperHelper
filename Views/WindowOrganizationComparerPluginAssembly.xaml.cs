﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Controllers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Views
{
    public partial class WindowOrganizationComparerPluginAssembly : WindowBase
    {
        private readonly object sysObjectConnections = new object();

        private IWriteToOutput _iWriteToOutput;

        private Dictionary<Guid, IOrganizationServiceExtented> _cacheService = new Dictionary<Guid, IOrganizationServiceExtented>();

        private CommonConfiguration _commonConfig;
        private ConnectionConfiguration _connectionConfig;

        private ObservableCollection<EntityViewItem> _itemsSource;

        private bool _controlsEnabled = true;

        private int _init = 0;

        public WindowOrganizationComparerPluginAssembly(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            )
        {
            _init++;

            InputLanguageManager.SetInputLanguage(this, CultureInfo.CreateSpecificCulture("en-US"));

            this._iWriteToOutput = iWriteToOutput;
            this._commonConfig = commonConfig;
            this._connectionConfig = connection1.ConnectionConfiguration;

            BindingOperations.EnableCollectionSynchronization(_connectionConfig.Connections, sysObjectConnections);

            InitializeComponent();

            tSDDBConnection1.Header = string.Format("Export from {0}", connection1.Name);
            tSDDBConnection2.Header = string.Format("Export from {0}", connection2.Name);

            this.Resources["ConnectionName1"] = string.Format("Create from {0}", connection1.Name);
            this.Resources["ConnectionName2"] = string.Format("Create from {0}", connection2.Name);

            LoadFromConfig();

            txtBFilter.SelectionLength = 0;
            txtBFilter.SelectionStart = txtBFilter.Text.Length;

            txtBFilter.Focus();

            this._itemsSource = new ObservableCollection<EntityViewItem>();

            this.lstVwPluginAssemblies.ItemsSource = _itemsSource;

            cmBConnection1.ItemsSource = _connectionConfig.Connections;
            cmBConnection1.SelectedItem = connection1;

            cmBConnection2.ItemsSource = _connectionConfig.Connections;
            cmBConnection2.SelectedItem = connection2;

            _init--;

            ShowExistingPluginAssemblies();
        }

        private void LoadFromConfig()
        {
            cmBFileAction.DataContext = _commonConfig;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _commonConfig.Save();
            _connectionConfig.Save();

            BindingOperations.ClearAllBindings(cmBConnection1);
            cmBConnection1.Items.DetachFromSourceCollection();
            cmBConnection1.DataContext = null;
            cmBConnection1.ItemsSource = null;

            BindingOperations.ClearAllBindings(cmBConnection2);
            cmBConnection2.Items.DetachFromSourceCollection();
            cmBConnection2.DataContext = null;
            cmBConnection2.ItemsSource = null;
        }

        private async Task<IOrganizationServiceExtented> GetService1()
        {
            ConnectionData connectionData = null;

            cmBConnection1.Dispatcher.Invoke(() =>
            {
                connectionData = cmBConnection1.SelectedItem as ConnectionData;
            });

            if (connectionData != null)
            {
                if (!_cacheService.ContainsKey(connectionData.ConnectionId))
                {
                    _iWriteToOutput.WriteToOutput("Connection to CRM.");
                    _iWriteToOutput.WriteToOutput(connectionData.GetConnectionDescription());
                    var service = await QuickConnection.ConnectAsync(connectionData);
                    _iWriteToOutput.WriteToOutput("Current Service Endpoint: {0}", service.CurrentServiceEndpoint);

                    _cacheService[connectionData.ConnectionId] = service;
                }

                return _cacheService[connectionData.ConnectionId];
            }

            return null;
        }

        private async Task<IOrganizationServiceExtented> GetService2()
        {
            ConnectionData connectionData = null;

            cmBConnection2.Dispatcher.Invoke(() =>
            {
                connectionData = cmBConnection2.SelectedItem as ConnectionData;
            });

            if (connectionData != null)
            {
                if (!_cacheService.ContainsKey(connectionData.ConnectionId))
                {
                    _iWriteToOutput.WriteToOutput("Connection to CRM.");
                    _iWriteToOutput.WriteToOutput(connectionData.GetConnectionDescription());
                    var service = await QuickConnection.ConnectAsync(connectionData);
                    _iWriteToOutput.WriteToOutput("Current Service Endpoint: {0}", service.CurrentServiceEndpoint);

                    _cacheService[connectionData.ConnectionId] = service;
                }

                return _cacheService[connectionData.ConnectionId];
            }

            return null;
        }

        private async void ShowExistingPluginAssemblies()
        {
            if (!_controlsEnabled)
            {
                return;
            }

            ToggleControls(false);

            UpdateStatus("Loading Plugin Assemblies...");

            this._itemsSource.Clear();

            var textName = string.Empty;

            txtBFilter.Dispatcher.Invoke(() =>
            {
                textName = txtBFilter.Text.Trim().ToLower();
            });

            IEnumerable<LinkedEntities<PluginAssembly>> list = Enumerable.Empty<LinkedEntities<PluginAssembly>>();

            try
            {
                var service1 = await GetService1();
                var service2 = await GetService2();

                if (service1 != null && service2 != null)
                {
                    var columnSet = new ColumnSet(PluginAssembly.Schema.Attributes.pluginassemblyid, PluginAssembly.Schema.Attributes.name);

                    var temp = new List<LinkedEntities<PluginAssembly>>();

                    if (service1.ConnectionData.ConnectionId != service2.ConnectionData.ConnectionId)
                    {
                        var repository1 = new PluginAssemblyRepository(service1);
                        var repository2 = new PluginAssemblyRepository(service2);

                        var task1 = repository1.GetPluginAssembliesAsync(textName, columnSet);
                        var task2 = repository2.GetPluginAssembliesAsync(textName, columnSet);

                        var list1 = await task1;
                        var list2 = await task2;

                        foreach (var assembly1 in list1)
                        {
                            var assembly2 = list2.FirstOrDefault(c => c.Name == assembly1.Name);

                            if (assembly2 == null)
                            {
                                continue;
                            }

                            temp.Add(new LinkedEntities<PluginAssembly>(assembly1, assembly2));
                        }
                    }
                    else
                    {
                        var repository1 = new PluginAssemblyRepository(service1);

                        var task1 = repository1.GetPluginAssembliesAsync(textName, columnSet);

                        var list1 = await task1;

                        foreach (var assembly1 in list1)
                        {
                            temp.Add(new LinkedEntities<PluginAssembly>(assembly1, null));
                        }
                    }

                    list = temp;
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(ex);
            }

            LoadEntities(list);
        }

        private class EntityViewItem
        {
            public string AssemblyName { get; private set; }

            public LinkedEntities<PluginAssembly> Link { get; private set; }

            public EntityViewItem(string assemblyName, LinkedEntities<PluginAssembly> link)
            {
                this.AssemblyName = assemblyName;
                this.Link = link;
            }
        }

        private void LoadEntities(IEnumerable<LinkedEntities<PluginAssembly>> results)
        {
            this._iWriteToOutput.WriteToOutput("Found {0} Plugin Assemblies.", results.Count());

            this.lstVwPluginAssemblies.Dispatcher.Invoke(() =>
            {
                foreach (var link in results.OrderBy(ent => ent.Entity1.Name))
                {
                    var item = new EntityViewItem(link.Entity1.Name, link);

                    this._itemsSource.Add(item);
                }

                if (this.lstVwPluginAssemblies.Items.Count == 1)
                {
                    this.lstVwPluginAssemblies.SelectedItem = this.lstVwPluginAssemblies.Items[0];
                }
            });

            UpdateStatus(string.Format("{0} Plugin Assemblies loaded.", results.Count()));

            ToggleControls(true);
        }

        private void UpdateStatus(string msg)
        {
            this.statusBar.Dispatcher.Invoke(() =>
            {
                this.tSSLStatusMessage.Content = msg;
            });
        }

        private void ToggleControls(bool enabled)
        {
            this._controlsEnabled = enabled;

            ToggleControl(this.toolStrip, enabled);

            ToggleProgressBar(enabled);

            if (enabled)
            {
                UpdateButtonsEnable();
            }
        }

        private void ToggleProgressBar(bool enabled)
        {
            if (tSProgressBar == null)
            {
                return;
            }

            this.tSProgressBar.Dispatcher.Invoke(() =>
            {
                tSProgressBar.IsIndeterminate = !enabled;
            });
        }

        private void ToggleControl(Control c, bool enabled)
        {
            c.Dispatcher.Invoke(() =>
            {
                if (c is TextBox)
                {
                    ((TextBox)c).IsReadOnly = !enabled;
                }
                else
                {
                    c.IsEnabled = enabled;
                }
            });
        }

        private void UpdateButtonsEnable()
        {
            this.lstVwPluginAssemblies.Dispatcher.Invoke(() =>
            {
                try
                {
                    bool enabled = this.lstVwPluginAssemblies.SelectedItems.Count > 0;

                    var item = (this.lstVwPluginAssemblies.SelectedItems[0] as EntityViewItem);

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
                ShowExistingPluginAssemblies();
            }
        }

        private EntityViewItem GetSelectedEntity()
        {
            EntityViewItem result = null;

            if (this.lstVwPluginAssemblies.SelectedItems.Count == 1
                && this.lstVwPluginAssemblies.SelectedItems[0] != null
                && this.lstVwPluginAssemblies.SelectedItems[0] is EntityViewItem
                )
            {
                result = (this.lstVwPluginAssemblies.SelectedItems[0] as EntityViewItem);
            }

            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstVwEntities_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var item = ((FrameworkElement)e.OriginalSource).DataContext as EntityViewItem;

                if (item != null)
                {
                    ExecuteActionOnLinkedEntities(item.Link, false, PerformShowingDifferenceAllAsync);
                }
            }
        }

        private void lstVwEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonsEnable();
        }

        private void ExecuteActionOnLinkedEntities(LinkedEntities<PluginAssembly> linked, bool showAllways, Func<LinkedEntities<PluginAssembly>, bool, Task> action)
        {
            if (!_controlsEnabled)
            {
                return;
            }

            if (linked.Entity1 == null || linked.Entity2 == null || linked.Entity1 == linked.Entity2)
            {
                return;
            }

            if (string.IsNullOrEmpty(_commonConfig.FolderForExport))
            {
                return;
            }

            if (!Directory.Exists(_commonConfig.FolderForExport))
            {
                return;
            }

            action(linked, showAllways);
        }

        private void btnShowDifferenceAll_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteActionOnLinkedEntities(link.Link, false, PerformShowingDifferenceAllAsync);
        }

        private async Task PerformShowingDifferenceAllAsync(LinkedEntities<PluginAssembly> linked, bool showAllways)
        {
            await PerformShowingDifferenceAssemblyDescriptionAsync(linked, showAllways);

            await PerformShowingDifferenceEntityDescriptionAsync(linked, showAllways);
        }

        private void mIShowDifferenceAssemblyDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteActionOnLinkedEntities(link.Link, true, PerformShowingDifferenceAssemblyDescriptionAsync);
        }

        private async Task PerformShowingDifferenceAssemblyDescriptionAsync(LinkedEntities<PluginAssembly> linked, bool showAllways)
        {
            if (!_controlsEnabled)
            {
                return;
            }

            ToggleControls(false);

            var service1 = await GetService1();
            var service2 = await GetService2();

            if (service1 != null && service2 != null)
            {
                var handler1 = new PluginAssemblyDescriptionHandler(service1, service1.ConnectionData.GetConnectionInfo());
                var handler2 = new PluginAssemblyDescriptionHandler(service2, service2.ConnectionData.GetConnectionInfo());

                DateTime now = DateTime.Now;

                string desc1 = await handler1.CreateDescriptionAsync(linked.Entity1.Id, linked.Entity1.Name, now);
                string desc2 = await handler2.CreateDescriptionAsync(linked.Entity2.Id, linked.Entity2.Name, now);

                if (showAllways || desc1 != desc2)
                {
                    string filePath1 = await CreateDescriptionFileAsync(service1.ConnectionData.Name, linked.Entity1.Name, "Description", desc1);
                    string filePath2 = await CreateDescriptionFileAsync(service2.ConnectionData.Name, linked.Entity2.Name, "Description", desc2);

                    if (File.Exists(filePath1) && File.Exists(filePath2))
                    {
                        this._iWriteToOutput.ProcessStartProgramComparer(this._commonConfig, filePath1, filePath2, Path.GetFileName(filePath1), Path.GetFileName(filePath2));
                    }
                    else
                    {
                        this._iWriteToOutput.PerformAction(filePath1, _commonConfig);

                        this._iWriteToOutput.PerformAction(filePath2, _commonConfig);
                    }
                }
            }

            UpdateStatus("Operation is completed.");

            ToggleControls(true);
        }

        private void mIShowDifferenceEntityDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null)
            {
                return;
            }

            ExecuteActionOnLinkedEntities(link.Link, true, PerformShowingDifferenceEntityDescriptionAsync);
        }

        private async Task PerformShowingDifferenceEntityDescriptionAsync(LinkedEntities<PluginAssembly> linked, bool showAllways)
        {
            if (!_controlsEnabled)
            {
                return;
            }

            ToggleControls(false);

            var service1 = await GetService1();
            var service2 = await GetService2();

            if (service1 != null && service2 != null)
            {
                var repository1 = new PluginAssemblyRepository(service1);
                var repository2 = new PluginAssemblyRepository(service2);

                var assembly1 = await repository1.GetAssemblyByIdAsync(linked.Entity1.Id, new ColumnSet(true));
                var assembly2 = await repository2.GetAssemblyByIdAsync(linked.Entity2.Id, new ColumnSet(true));

                var desc1 = await EntityDescriptionHandler.GetEntityDescriptionAsync(assembly1, EntityFileNameFormatter.PluginAssemblyIgnoreFields);
                var desc2 = await EntityDescriptionHandler.GetEntityDescriptionAsync(assembly2, EntityFileNameFormatter.PluginAssemblyIgnoreFields);

                if (showAllways || desc1 != desc2)
                {
                    string filePath1 = await CreateDescriptionFileAsync(service1.ConnectionData.Name, assembly1.Name, "EntityDescription", desc1);
                    string filePath2 = await CreateDescriptionFileAsync(service2.ConnectionData.Name, assembly2.Name, "EntityDescription", desc2);

                    if (File.Exists(filePath1) && File.Exists(filePath2))
                    {
                        this._iWriteToOutput.ProcessStartProgramComparer(this._commonConfig, filePath1, filePath2, Path.GetFileName(filePath1), Path.GetFileName(filePath2));
                    }
                    else
                    {
                        this._iWriteToOutput.PerformAction(filePath1, _commonConfig);

                        this._iWriteToOutput.PerformAction(filePath2, _commonConfig);
                    }
                }
            }

            UpdateStatus("Operation is completed.");

            ToggleControls(true);
        }

        private void mIExportPluginAssembly1AssemblyDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity1 != null)
            {
                return;
            }

            ExecuteActionPluginAssemblyDescription(link.Link.Entity1.Id, GetService1, PerformExportAssemblyDescriptionToFileAsync);
        }

        private void mIExportPluginAssembly2AssemblyDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity2 != null)
            {
                return;
            }

            ExecuteActionPluginAssemblyDescription(link.Link.Entity2.Id, GetService2, PerformExportAssemblyDescriptionToFileAsync);
        }

        private void ExecuteActionPluginAssemblyDescription(
            Guid pluginAssemblyId
            , Func<Task<IOrganizationServiceExtented>> getService
            , Func<Guid, Func<Task<IOrganizationServiceExtented>>, Task> action)
        {
            if (!_controlsEnabled)
            {
                return;
            }

            if (string.IsNullOrEmpty(_commonConfig.FolderForExport))
            {
                return;
            }

            if (!Directory.Exists(_commonConfig.FolderForExport))
            {
                return;
            }

            action(pluginAssemblyId, getService);
        }

        private async Task PerformExportAssemblyDescriptionToFileAsync(Guid idAssembly, Func<Task<IOrganizationServiceExtented>> getService)
        {
            if (!_controlsEnabled)
            {
                return;
            }

            ToggleControls(false);

            var service = await getService();

            if (service != null)
            {
                var repository = new PluginAssemblyRepository(service);

                var assembly = await repository.GetAssemblyByIdAsync(idAssembly, new ColumnSet(PluginAssembly.Schema.Attributes.name));

                PluginAssemblyDescriptionHandler handler = new PluginAssemblyDescriptionHandler(service, service.ConnectionData.GetConnectionInfo());

                string description = await handler.CreateDescriptionAsync(assembly.Id, assembly.Name, DateTime.Now);

                string filePath = await CreateDescriptionFileAsync(service.ConnectionData.Name, assembly.Name, "Description", description);

                this._iWriteToOutput.PerformAction(filePath, _commonConfig);
            }

            UpdateStatus("Operation is completed.");

            ToggleControls(true);
        }

        private void mIExportPluginAssembly1EntityDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity1 != null)
            {
                return;
            }

            ExecuteActionPluginAssemblyDescription(link.Link.Entity1.Id, GetService1, PerformExportEntityDescriptionToFileAsync);
        }

        private void mIExportPluginAssembly2EntityDescription_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity2 != null)
            {
                return;
            }

            ExecuteActionPluginAssemblyDescription(link.Link.Entity2.Id, GetService2, PerformExportEntityDescriptionToFileAsync);
        }

        private void mIExportPluginAssembly1BinaryContent_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity1 != null)
            {
                return;
            }

            ExecuteActionPluginAssemblyDescription(link.Link.Entity1.Id, GetService1, PerformDownloadBinaryContent);
        }

        private void mIExportPluginAssembly2BinaryContent_Click(object sender, RoutedEventArgs e)
        {
            var link = GetSelectedEntity();

            if (link == null && link.Link.Entity2 != null)
            {
                return;
            }

            ExecuteActionPluginAssemblyDescription(link.Link.Entity2.Id, GetService2, PerformDownloadBinaryContent);
        }

        private async Task PerformDownloadBinaryContent(Guid pluginAssemblyId, Func<Task<IOrganizationServiceExtented>> getService)
        {
            ToggleControls(false);

            var service = await getService();

            if (service != null)
            {
                var repository = new PluginAssemblyRepository(service);

                var assembly = await repository.GetAssemblyByIdAsync(pluginAssemblyId, new ColumnSet(PluginAssembly.Schema.Attributes.content));

                string fileName = EntityFileNameFormatter.GetPluginAssemblyFileName(service.ConnectionData.Name, assembly.Name, "Content", "dll");
                string filePath = Path.Combine(_commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileName));

                var array = Convert.FromBase64String(assembly.Content);

                File.WriteAllBytes(filePath, array);

                this._iWriteToOutput.WriteToOutput("{0} Plugin Assembly {1} exported to {2}", service.ConnectionData.Name, assembly.Name, filePath);

                if (File.Exists(filePath))
                {
                    if (_commonConfig.AfterCreateFileAction != FileAction.None)
                    {
                        this._iWriteToOutput.SelectFileInFolder(filePath);
                    }
                }
            }

            UpdateStatus("Operation is completed.");

            ToggleControls(true);
        }

        private async Task PerformExportEntityDescriptionToFileAsync(Guid pluginAssemblyId, Func<Task<IOrganizationServiceExtented>> getService)
        {
            if (!_controlsEnabled)
            {
                return;
            }

            ToggleControls(false);

            var service = await getService();

            if (service != null)
            {
                var repository = new PluginAssemblyRepository(service);

                var assembly = await repository.GetAssemblyByIdAsync(pluginAssemblyId, new ColumnSet(true));

                var description = await EntityDescriptionHandler.GetEntityDescriptionAsync(assembly, EntityFileNameFormatter.PluginAssemblyIgnoreFields);

                string filePath = await CreateDescriptionFileAsync(service.ConnectionData.Name, assembly.Name, "EntityDescription", description);

                this._iWriteToOutput.PerformAction(filePath, _commonConfig);
            }

            UpdateStatus("Operation is completed.");

            ToggleControls(true);
        }

        private Task<string> CreateDescriptionFileAsync(string connectionName, string name, string fieldName, string description)
        {
            return Task.Run(() => CreateDescriptionFile(connectionName, name, fieldName, description));
        }

        private string CreateDescriptionFile(string connectionName, string name, string fieldName, string description)
        {
            string fileName = EntityFileNameFormatter.GetPluginAssemblyFileName(connectionName, name, fieldName, "txt");
            string filePath = Path.Combine(_commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileName));

            if (!string.IsNullOrEmpty(description))
            {
                try
                {
                    File.WriteAllText(filePath, description, Encoding.UTF8);

                    this._iWriteToOutput.WriteToOutput("{0} Plugin Assembly Entity Description {1} {2} exported to {3}", connectionName, name, fieldName, filePath);
                }
                catch (Exception ex)
                {
                    this._iWriteToOutput.WriteErrorToOutput(ex);
                }
            }
            else
            {
                filePath = string.Empty;
                this._iWriteToOutput.WriteToOutput("{0} Plugin Assembly Entity Description {1} {2} is empty.", connectionName, name, fieldName);
                this._iWriteToOutput.ActivateOutputWindow();
            }

            return filePath;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;

                ShowExistingPluginAssemblies();
            }

            base.OnKeyDown(e);
        }

        private void cmBCurrentConnection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_init > 0)
            {
                return;
            }

            this.Dispatcher.Invoke(() =>
            {
                this._itemsSource.Clear();

                if (!_controlsEnabled)
                {
                    return;
                }

                ConnectionData connection1 = cmBConnection1.SelectedItem as ConnectionData;
                ConnectionData connection2 = cmBConnection2.SelectedItem as ConnectionData;

                if (connection1 != null && connection2 != null)
                {
                    tSDDBConnection1.Header = string.Format("Export from {0}", connection1.Name);
                    tSDDBConnection2.Header = string.Format("Export from {0}", connection2.Name);

                    this.Resources["ConnectionName1"] = string.Format("Create from {0}", connection1.Name);
                    this.Resources["ConnectionName2"] = string.Format("Create from {0}", connection2.Name);

                    ShowExistingPluginAssemblies();
                }
            });
        }
        private async void btnOrganizationComparer_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service = await GetService1();

            WindowHelper.OpenOrganizationComparerWindow(this._iWriteToOutput, service.ConnectionData.ConnectionConfiguration, _commonConfig);
        }

        private async void btnComparePluginAssemblies_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service = await GetService1();

            WindowHelper.OpenOrganizationComparerPluginAssemblyWindow(
                _iWriteToOutput
                , _commonConfig
                , service.ConnectionData
                , service.ConnectionData
            );
        }

        private async void btnCompareMetadataFile_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service1 = await GetService1();
            var service2 = await GetService2();

            WindowHelper.OpenOrganizationComparerEntityMetadataWindow(this._iWriteToOutput, _commonConfig, service1.ConnectionData, service2.ConnectionData);
        }

        private async void btnCompareRibbon_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service1 = await GetService1();
            var service2 = await GetService2();

            WindowHelper.OpenOrganizationComparerRibbonWindow(this._iWriteToOutput, _commonConfig, service1.ConnectionData, service2.ConnectionData);
        }

        private async void btnCompareGlobalOptionSets_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service1 = await GetService1();
            var service2 = await GetService2();

            WindowHelper.OpenOrganizationComparerGlobalOptionSetsWindow(this._iWriteToOutput, _commonConfig, service1.ConnectionData, service2.ConnectionData);
        }

        private async void btnCompareSystemForms_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service1 = await GetService1();
            var service2 = await GetService2();

            WindowHelper.OpenOrganizationComparerSystemFormWindow(this._iWriteToOutput, _commonConfig, service1.ConnectionData, service2.ConnectionData, null);
        }

        private async void btnCompareSavedQuery_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service1 = await GetService1();
            var service2 = await GetService2();

            WindowHelper.OpenOrganizationComparerSavedQueryWindow(this._iWriteToOutput, _commonConfig, service1.ConnectionData, service2.ConnectionData, null);
        }

        private async void btnCompareSavedChart_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service1 = await GetService1();
            var service2 = await GetService2();

            WindowHelper.OpenOrganizationComparerSavedQueryVisualizationWindow(this._iWriteToOutput, _commonConfig, service1.ConnectionData, service2.ConnectionData, null);
        }

        private async void btnCompareWorkflows_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service1 = await GetService1();
            var service2 = await GetService2();

            WindowHelper.OpenOrganizationComparerWorkflowWindow(this._iWriteToOutput, _commonConfig, service1.ConnectionData, service2.ConnectionData, null);
        }

        private async void btnExportPluginAssembly1_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service = await GetService1();

            WindowHelper.OpenPluginAssemblyWindow(this._iWriteToOutput, service, _commonConfig, null);
        }

        private async void btnExportPluginAssembly2_Click(object sender, RoutedEventArgs e)
        {
            _commonConfig.Save();

            var service = await GetService2();

            WindowHelper.OpenPluginAssemblyWindow(this._iWriteToOutput, service, _commonConfig, null);
        }
    }
}
