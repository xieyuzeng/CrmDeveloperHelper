using Microsoft.Xrm.Sdk.Metadata;
using Nav.Common.VSPackages.CrmDeveloperHelper.Controllers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.SolutionComponentDescription;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using Nav.Common.VSPackages.CrmDeveloperHelper.UserControls;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Views
{
    public partial class WindowExplorerEntityRelationshipOneToMany : WindowWithConnectionList
    {
        private readonly Popup _popupEntityMetadataFilter;
        private readonly EntityMetadataFilter _entityMetadataFilter;

        private readonly ObservableCollection<EntityMetadataListViewItem> _itemsSourceEntityList;

        private readonly ObservableCollection<OneToManyRelationshipMetadataViewItem> _itemsSourceEntityRelationshipList;

        private readonly Dictionary<Guid, IEnumerable<EntityMetadata>> _cacheEntityMetadata = new Dictionary<Guid, IEnumerable<EntityMetadata>>();

        private readonly Dictionary<Guid, ConcurrentDictionary<string, Task>> _cacheMetadataTask = new Dictionary<Guid, ConcurrentDictionary<string, Task>>();

        private readonly Dictionary<Guid, ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>>> _cacheEntityOneToMany = new Dictionary<Guid, ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>>>();

        private readonly Dictionary<Guid, ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>>> _cacheEntityManyToOne = new Dictionary<Guid, ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>>>();

        public WindowExplorerEntityRelationshipOneToMany(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , IOrganizationServiceExtented service
            , string filterEntity
        ) : base(iWriteToOutput, commonConfig, service)
        {
            this.IncreaseInit();

            InputLanguageManager.SetInputLanguage(this, CultureInfo.CreateSpecificCulture("en-US"));

            InitializeComponent();

            _entityMetadataFilter = new EntityMetadataFilter();
            _entityMetadataFilter.CloseClicked += this._entityMetadataFilter_CloseClicked;
            this._popupEntityMetadataFilter = new Popup
            {
                Child = _entityMetadataFilter,

                PlacementTarget = lblEntitiesList,
                Placement = PlacementMode.Bottom,
                StaysOpen = false,
                Focusable = true,
            };
            _popupEntityMetadataFilter.Closed += this._popupEntityMetadataFilter_Closed;

            FillRoleEditorLayoutTabs();

            LoadFromConfig();

            txtBFilterEnitity.Text = filterEntity;
            txtBFilterEnitity.SelectionLength = 0;
            txtBFilterEnitity.SelectionStart = txtBFilterEnitity.Text.Length;

            txtBFilterEnitity.Focus();

            _itemsSourceEntityList = new ObservableCollection<EntityMetadataListViewItem>();
            lstVwEntities.ItemsSource = _itemsSourceEntityList;

            _itemsSourceEntityRelationshipList = new ObservableCollection<OneToManyRelationshipMetadataViewItem>();
            lstVwEntityRelationships.ItemsSource = _itemsSourceEntityRelationshipList;

            UpdateButtonsEnable();

            cmBCurrentConnection.ItemsSource = service.ConnectionData.ConnectionConfiguration.Connections;
            cmBCurrentConnection.SelectedItem = service.ConnectionData;

            FillExplorersMenuItems();

            this.DecreaseInit();

            ShowExistingEntities();
        }

        private void FillExplorersMenuItems()
        {
            var explorersHelper = new ExplorersHelper(_iWriteToOutput, _commonConfig, GetService
                , getEntityName: GetEntityName
                , getEntityMetadataList: GetEntityMetadataList
            );

            var compareWindowsHelper = new CompareWindowsHelper(_iWriteToOutput, _commonConfig, () => Tuple.Create(GetSelectedConnection(), GetSelectedConnection())
                , getEntityName: GetEntityName
            );

            explorersHelper.FillExplorers(miExplorers);
            compareWindowsHelper.FillCompareWindows(miCompareOrganizations);

            if (this.Resources.Contains("listContextMenu")
                && this.Resources["listContextMenu"] is ContextMenu listContextMenu
            )
            {
                explorersHelper.FillExplorers(listContextMenu, nameof(miExplorers));

                compareWindowsHelper.FillCompareWindows(listContextMenu, nameof(miCompareOrganizations));
            }
        }

        private string GetEntityName()
        {
            var entity = GetSelectedEntity();

            return entity?.LogicalName;
        }

        private IEnumerable<EntityMetadata> GetEntityMetadataList(Guid connectionId)
        {
            if (_cacheEntityMetadata.ContainsKey(connectionId))
            {
                return _cacheEntityMetadata[connectionId];
            }

            return null;
        }

        private void LoadFromConfig()
        {
            WindowSettings winConfig = GetWindowsSettings();

            LoadFormSettings(winConfig);
        }

        private void btnEntityMetadataFilter_Click(object sender, RoutedEventArgs e)
        {
            _popupEntityMetadataFilter.IsOpen = true;
            _popupEntityMetadataFilter.Child.Focus();
        }

        private void _popupEntityMetadataFilter_Closed(object sender, EventArgs e)
        {
            if (_entityMetadataFilter.FilterChanged)
            {
                ShowExistingEntities();
            }
        }

        private void _entityMetadataFilter_CloseClicked(object sender, EventArgs e)
        {
            if (_popupEntityMetadataFilter.IsOpen)
            {
                _popupEntityMetadataFilter.IsOpen = false;
                this.Focus();
            }
        }

        private void FillRoleEditorLayoutTabs()
        {
            cmBRoleEditorLayoutTabs.Items.Clear();

            cmBRoleEditorLayoutTabs.Items.Add("All");

            var tabs = RoleEditorLayoutTab.GetTabs();

            foreach (var tab in tabs)
            {
                cmBRoleEditorLayoutTabs.Items.Add(tab);
            }

            cmBRoleEditorLayoutTabs.SelectedIndex = 0;
        }

        protected override void LoadConfigurationInternal(WindowSettings winConfig)
        {
            base.LoadConfigurationInternal(winConfig);

            LoadFormSettings(winConfig);
        }

        private const string paramColumnEntityWidth = "ColumnEntityWidth";
        private const string paramRelationType = "RelationType";

        private void LoadFormSettings(WindowSettings winConfig)
        {
            if (winConfig.DictDouble.ContainsKey(paramColumnEntityWidth))
            {
                columnEntity.Width = new GridLength(winConfig.DictDouble[paramColumnEntityWidth]);
            }

            {
                var categoryValue = winConfig.GetValueInt(paramRelationType);

                if (categoryValue.HasValue && 0 <= categoryValue && categoryValue < cmBRelationType.Items.Count)
                {
                    cmBRelationType.SelectedIndex = categoryValue.Value;
                }
            }
        }

        protected override void SaveConfigurationInternal(WindowSettings winConfig)
        {
            base.SaveConfigurationInternal(winConfig);

            winConfig.DictDouble[paramColumnEntityWidth] = columnEntity.Width.Value;
            winConfig.DictInt[paramRelationType] = cmBRelationType.SelectedIndex;
        }

        protected override void OnClosed(EventArgs e)
        {
            _commonConfig.Save();

            BindingOperations.ClearAllBindings(cmBCurrentConnection);

            cmBCurrentConnection.Items.DetachFromSourceCollection();

            cmBCurrentConnection.DataContext = null;
            cmBCurrentConnection.ItemsSource = null;

            base.OnClosed(e);
        }

        private ConnectionData GetSelectedConnection()
        {
            ConnectionData connectionData = null;

            cmBCurrentConnection.Dispatcher.Invoke(() =>
            {
                connectionData = cmBCurrentConnection.SelectedItem as ConnectionData;
            });

            return connectionData;
        }

        private Task<IOrganizationServiceExtented> GetService()
        {
            return GetOrganizationService(GetSelectedConnection());
        }

        private enum RelationType
        {
            All = 0,
            OneToMany = 1,
            ManyToOne = 2,
        }

        private RelationType GetRelationType()
        {
            var result = RelationType.All;

            cmBRelationType.Dispatcher.Invoke(() =>
            {
                if (cmBRelationType.SelectedIndex != -1)
                {
                    result = (RelationType)cmBRelationType.SelectedIndex;
                }
            });

            return result;
        }

        private async Task ShowExistingEntities()
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            var service = await GetService();

            ToggleControls(service.ConnectionData, false, Properties.OutputStrings.LoadingEntities);

            _itemsSourceEntityList.Clear();
            _itemsSourceEntityRelationshipList.Clear();

            IEnumerable<EntityMetadataListViewItem> list = Enumerable.Empty<EntityMetadataListViewItem>();

            try
            {
                if (service != null)
                {
                    if (!_cacheEntityMetadata.ContainsKey(service.ConnectionData.ConnectionId))
                    {
                        EntityMetadataRepository repository = new EntityMetadataRepository(service);

                        var temp = await repository.GetEntitiesForEntityAttributeExplorerAsync(EntityFilters.Entity);

                        _cacheEntityMetadata.Add(service.ConnectionData.ConnectionId, temp);
                    }

                    list = _cacheEntityMetadata[service.ConnectionData.ConnectionId].Select(e => new EntityMetadataListViewItem(e)).ToList();
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(service.ConnectionData, ex);
            }

            string textName = string.Empty;
            RoleEditorLayoutTab selectedTab = null;

            this.Dispatcher.Invoke(() =>
            {
                textName = txtBFilterEnitity.Text.Trim().ToLower();
                selectedTab = cmBRoleEditorLayoutTabs.SelectedItem as RoleEditorLayoutTab;
            });

            list = FilterEntityList(list, textName, selectedTab);

            LoadEntities(list);

            ToggleControls(service.ConnectionData, true, Properties.OutputStrings.LoadingEntitiesCompletedFormat1, list.Count());

            ShowExistingEntityRelationships();
        }

        private IEnumerable<EntityMetadataListViewItem> FilterEntityList(IEnumerable<EntityMetadataListViewItem> list, string textName, RoleEditorLayoutTab selectedTab)
        {
            list = _entityMetadataFilter.FilterList(list, i => i.EntityMetadata);

            if (selectedTab != null)
            {
                list = list.Where(ent => ent.EntityMetadata.ObjectTypeCode.HasValue && selectedTab.EntitiesHash.Contains(ent.EntityMetadata.ObjectTypeCode.Value));
            }

            if (!string.IsNullOrEmpty(textName))
            {
                textName = textName.ToLower();

                if (int.TryParse(textName, out int tempInt))
                {
                    list = list.Where(ent => ent.EntityMetadata.ObjectTypeCode == tempInt);
                }
                else
                {
                    if (Guid.TryParse(textName, out Guid tempGuid))
                    {
                        list = list.Where(ent => ent.EntityMetadata.MetadataId == tempGuid);
                    }
                    else
                    {
                        list = list
                        .Where(ent =>
                            ent.LogicalName.IndexOf(textName, StringComparison.InvariantCultureIgnoreCase) > -1
                            ||
                            (
                                ent.DisplayName != null
                                && ent.EntityMetadata.DisplayName.LocalizedLabels
                                    .Where(l => !string.IsNullOrEmpty(l.Label))
                                    .Any(lbl => lbl.Label.IndexOf(textName, StringComparison.InvariantCultureIgnoreCase) > -1)
                            )

                        //|| (ent.Description != null && ent.Description.LocalizedLabels
                        //    .Where(l => !string.IsNullOrEmpty(l.Label))
                        //    .Any(lbl => lbl.Label.ToLower().Contains(textName)))

                        //|| (ent.DisplayCollectionName != null && ent.DisplayCollectionName.LocalizedLabels
                        //    .Where(l => !string.IsNullOrEmpty(l.Label))
                        //    .Any(lbl => lbl.Label.ToLower().Contains(textName)))
                        );
                    }
                }
            }

            return list;
        }

        private void LoadEntities(IEnumerable<EntityMetadataListViewItem> results)
        {
            this.lstVwEntities.Dispatcher.Invoke(() =>
            {
                foreach (var entity in results
                    .OrderBy(s => s.IsIntersect)
                    .ThenBy(s => s.LogicalName)
                )
                {
                    _itemsSourceEntityList.Add(entity);
                }

                if (this.lstVwEntities.Items.Count == 1)
                {
                    this.lstVwEntities.SelectedItem = this.lstVwEntities.Items[0];
                }
            });
        }

        private async Task ShowExistingEntityRelationships()
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            var service = await GetService();

            ToggleControls(service.ConnectionData, false, Properties.OutputStrings.LoadingOneToManyRelationships);

            string entityLogicalName = string.Empty;

            this.Dispatcher.Invoke(() =>
            {
                _itemsSourceEntityRelationshipList.Clear();

                entityLogicalName = GetSelectedEntity()?.LogicalName;
            });

            IEnumerable<OneToManyRelationshipMetadataViewItem> list = Enumerable.Empty<OneToManyRelationshipMetadataViewItem>();

            if (!string.IsNullOrEmpty(entityLogicalName))
            {
                try
                {
                    if (service != null)
                    {
                        if (!_cacheEntityOneToMany.ContainsKey(service.ConnectionData.ConnectionId))
                        {
                            _cacheEntityOneToMany.Add(service.ConnectionData.ConnectionId, new ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>>(StringComparer.InvariantCultureIgnoreCase));
                        }

                        if (!_cacheEntityManyToOne.ContainsKey(service.ConnectionData.ConnectionId))
                        {
                            _cacheEntityManyToOne.Add(service.ConnectionData.ConnectionId, new ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>>(StringComparer.InvariantCultureIgnoreCase));
                        }

                        if (!_cacheMetadataTask.ContainsKey(service.ConnectionData.ConnectionId))
                        {
                            _cacheMetadataTask.Add(service.ConnectionData.ConnectionId, new ConcurrentDictionary<string, Task>(StringComparer.InvariantCultureIgnoreCase));
                        }

                        var connectionEntityOneToMany = _cacheEntityOneToMany[service.ConnectionData.ConnectionId];
                        var connectionEntityManyToOne = _cacheEntityManyToOne[service.ConnectionData.ConnectionId];
                        var cacheMetadataTask = _cacheMetadataTask[service.ConnectionData.ConnectionId];

                        if (!connectionEntityOneToMany.ContainsKey(entityLogicalName)
                            || !connectionEntityManyToOne.ContainsKey(entityLogicalName)
                            )
                        {
                            if (cacheMetadataTask.ContainsKey(entityLogicalName))
                            {
                                if (cacheMetadataTask.TryGetValue(entityLogicalName, out var task))
                                {
                                    await task;
                                }
                            }
                            else
                            {
                                var task = GetEntityMetadataInformationAsync(service, entityLogicalName, connectionEntityOneToMany, connectionEntityManyToOne, cacheMetadataTask);

                                cacheMetadataTask.TryAdd(entityLogicalName, task);

                                await task;
                            }
                        }

                        HashSet<Guid> hashGuid = new HashSet<Guid>();
                        var coll = new List<OneToManyRelationshipMetadataViewItem>();

                        var relType = GetRelationType();

                        if (relType == RelationType.All || relType == RelationType.OneToMany)
                        {
                            foreach (var item in connectionEntityOneToMany[entityLogicalName])
                            {
                                if (hashGuid.Add(item.OneToManyRelationshipMetadata.MetadataId.Value))
                                {
                                    coll.Add(item);
                                }
                            }
                        }

                        if (relType == RelationType.All || relType == RelationType.ManyToOne)
                        {
                            foreach (var item in connectionEntityManyToOne[entityLogicalName])
                            {
                                if (hashGuid.Add(item.OneToManyRelationshipMetadata.MetadataId.Value))
                                {
                                    coll.Add(item);
                                }
                            }
                        }

                        list = coll;
                    }
                }
                catch (Exception ex)
                {
                    this._iWriteToOutput.WriteErrorToOutput(service.ConnectionData, ex);
                }
            }

            string textName = string.Empty;

            txtBFilterEntityRelationship.Dispatcher.Invoke(() =>
            {
                textName = txtBFilterEntityRelationship.Text.Trim().ToLower();
            });

            list = FilterEntityRelationshipList(list, textName);

            LoadEntityRelationships(list);

            ToggleControls(service.ConnectionData, true, Properties.OutputStrings.LoadingOneToManyRelationshipsCompletedFormat1, list.Count());
        }

        private static async Task GetEntityMetadataInformationAsync(IOrganizationServiceExtented service
            , string entityLogicalName
            , ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>> connectionEntityOneToMany
            , ConcurrentDictionary<string, IEnumerable<OneToManyRelationshipMetadataViewItem>> connectionEntityManyToOne
            , ConcurrentDictionary<string, Task> cacheMetadataTask
        )
        {
            var repository = new EntityMetadataRepository(service);

            var metadata = await repository.GetEntityMetadataAttributesAsync(entityLogicalName, EntityFilters.Relationships);

            if (metadata != null)
            {
                if (metadata.OneToManyRelationships != null && !connectionEntityOneToMany.ContainsKey(entityLogicalName))
                {
                    connectionEntityOneToMany.TryAdd(entityLogicalName, metadata.OneToManyRelationships.Select(e => new OneToManyRelationshipMetadataViewItem(e)).ToList());
                }

                if (metadata.ManyToOneRelationships != null && !connectionEntityManyToOne.ContainsKey(entityLogicalName))
                {
                    connectionEntityManyToOne.TryAdd(entityLogicalName, metadata.ManyToOneRelationships.Select(e => new OneToManyRelationshipMetadataViewItem(e)).ToList());
                }
            }

            if (cacheMetadataTask.ContainsKey(entityLogicalName))
            {
                cacheMetadataTask.TryRemove(entityLogicalName, out _);
            }
        }

        private static IEnumerable<OneToManyRelationshipMetadataViewItem> FilterEntityRelationshipList(IEnumerable<OneToManyRelationshipMetadataViewItem> list, string textName)
        {
            if (!string.IsNullOrEmpty(textName))
            {
                textName = textName.ToLower();

                if (Guid.TryParse(textName, out Guid tempGuid))
                {
                    list = list.Where(ent => ent.OneToManyRelationshipMetadata.MetadataId == tempGuid);
                }
                else
                {
                    list = list
                    .Where(ent =>
                        ent.SchemaName.IndexOf(textName, StringComparison.InvariantCultureIgnoreCase) > -1
                        ||
                        (
                            ent.OneToManyRelationshipMetadata.AssociatedMenuConfiguration != null
                            && ent.OneToManyRelationshipMetadata.AssociatedMenuConfiguration.Label.LocalizedLabels
                                .Where(l => !string.IsNullOrEmpty(l.Label))
                                .Any(lbl => lbl.Label.IndexOf(textName, StringComparison.InvariantCultureIgnoreCase) > -1)
                        )

                    //|| (ent.Description != null && ent.Description.LocalizedLabels
                    //    .Where(l => !string.IsNullOrEmpty(l.Label))
                    //    .Any(lbl => lbl.Label.ToLower().Contains(textName)))

                    //|| (ent.DisplayCollectionName != null && ent.DisplayCollectionName.LocalizedLabels
                    //    .Where(l => !string.IsNullOrEmpty(l.Label))
                    //    .Any(lbl => lbl.Label.ToLower().Contains(textName)))
                    );
                }
            }

            return list;
        }

        private void LoadEntityRelationships(IEnumerable<OneToManyRelationshipMetadataViewItem> results)
        {
            this.lstVwEntityRelationships.Dispatcher.Invoke(() =>
            {
                foreach (var entity in results
                    .OrderBy(s => s.ReferencedEntity)
                    .ThenBy(s => s.ReferencedAttribute)
                    .ThenBy(s => s.ReferencingEntity)
                    .ThenBy(s => s.ReferencingAttribute)
                    .ThenBy(s => s.SchemaName)
                )
                {
                    _itemsSourceEntityRelationshipList.Add(entity);
                }

                if (this.lstVwEntityRelationships.Items.Count == 1)
                {
                    this.lstVwEntityRelationships.SelectedItem = this.lstVwEntityRelationships.Items[0];
                }
            });
        }


        private void UpdateStatus(ConnectionData connectionData, string format, params object[] args)
        {
            string message = format;

            if (args != null && args.Length > 0)
            {
                message = string.Format(format, args);
            }

            _iWriteToOutput.WriteToOutput(connectionData, message);

            this.stBIStatus.Dispatcher.Invoke(() =>
            {
                this.stBIStatus.Content = message;
            });
        }

        protected override void ToggleControls(ConnectionData connectionData, bool enabled, string statusFormat, params object[] args)
        {
            this.ChangeInitByEnabled(enabled);

            UpdateStatus(connectionData, statusFormat, args);

            ToggleControl(this.tSProgressBar, cmBCurrentConnection, btnSetCurrentConnection, cmBRelationType, mIClearEntityCacheAndRefresh, mIClearEntityRelationshipCacheAndRefresh);

            UpdateButtonsEnable();
        }

        private void UpdateButtonsEnable()
        {
            this.lstVwEntities.Dispatcher.Invoke(() =>
            {
                try
                {
                    bool enabled = this.IsControlsEnabled && this.lstVwEntities != null && this.lstVwEntities.SelectedItems.Count > 0;

                    UIElement[] list = { };

                    foreach (var button in list)
                    {
                        button.IsEnabled = enabled;
                    }
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
                ShowExistingEntities();
            }
        }

        private void cmBRoleEditorLayoutTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowExistingEntities();
        }

        private void txtBFilterEntityRelationship_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowExistingEntityRelationships();
            }
        }

        private EntityMetadataListViewItem GetSelectedEntity()
        {
            return this.lstVwEntities.SelectedItems.OfType<EntityMetadataListViewItem>().Count() == 1
                ? this.lstVwEntities.SelectedItems.OfType<EntityMetadataListViewItem>().SingleOrDefault() : null;
        }

        private List<EntityMetadataListViewItem> GetSelectedEntities()
        {
            List<EntityMetadataListViewItem> result = this.lstVwEntities.SelectedItems.OfType<EntityMetadataListViewItem>().ToList();

            return result;
        }

        private OneToManyRelationshipMetadataViewItem GetSelectedEntityRelationship()
        {
            return this.lstVwEntityRelationships.SelectedItems.OfType<OneToManyRelationshipMetadataViewItem>().Count() == 1
                ? this.lstVwEntityRelationships.SelectedItems.OfType<OneToManyRelationshipMetadataViewItem>().SingleOrDefault() : null;
        }

        private List<OneToManyRelationshipMetadataViewItem> GetSelectedEntityRelationships()
        {
            List<OneToManyRelationshipMetadataViewItem> result = this.lstVwEntityRelationships.SelectedItems.OfType<OneToManyRelationshipMetadataViewItem>().ToList();

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
                EntityMetadataListViewItem entity = GetItemFromRoutedDataContext<EntityMetadataListViewItem>(e);
                ConnectionData connectionData = GetSelectedConnection();

                if (connectionData != null && entity != null)
                {
                    connectionData.OpenEntityMetadataInWeb(entity.EntityMetadata.MetadataId.Value);
                }
            }
        }

        private void lstVwEntityRelationships_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ConnectionData connectionData = GetSelectedConnection();

                var entity = GetSelectedEntity();

                OneToManyRelationshipMetadataViewItem entityRelationship = GetItemFromRoutedDataContext<OneToManyRelationshipMetadataViewItem>(e);

                if (connectionData != null
                    && entity != null
                    && entityRelationship != null
                )
                {
                    connectionData.OpenRelationshipMetadataInWeb(entity.EntityMetadata.MetadataId.Value, entityRelationship.OneToManyRelationshipMetadata.MetadataId.Value);
                }
            }
        }

        private async void btnPublishEntity_Click(object sender, RoutedEventArgs e)
        {
            var entityList = GetSelectedEntities();

            if (entityList == null || !entityList.Any())
            {
                return;
            }

            await PublishEntityAsync(GetSelectedConnection(), entityList.Select(item => item.LogicalName).ToList());
        }

        private void lstVwEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowExistingEntityRelationships();

            UpdateButtonsEnable();
        }

        protected override void OnRefreshList(ExecutedRoutedEventArgs e)
        {
            e.Handled = true;

            ShowExistingEntities();
        }

        private void mIOpenEntityInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                connectionData.OpenEntityMetadataInWeb(entity.EntityMetadata.MetadataId.Value);
            }
        }

        private void mIOpenEntityRelationshipInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            var entityRelationship = GetSelectedEntityRelationship();

            if (entity == null || entityRelationship == null)
            {
                return;
            }

            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                connectionData.OpenRelationshipMetadataInWeb(entity.EntityMetadata.MetadataId.Value, entityRelationship.OneToManyRelationshipMetadata.MetadataId.Value);
            }
        }

        private void mIOpenEntityListInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                connectionData.OpenEntityInstanceListInWeb(entity.LogicalName);
            }
        }

        private async void AddToCrmSolutionIncludeSubcomponents_Click(object sender, RoutedEventArgs e)
        {
            await AddToSolution(true, null, SolutionComponent.Schema.OptionSets.rootcomponentbehavior.Include_Subcomponents_0);
        }

        private async void AddToCrmSolutionDoNotIncludeSubcomponents_Click(object sender, RoutedEventArgs e)
        {
            await AddToSolution(true, null, SolutionComponent.Schema.OptionSets.rootcomponentbehavior.Do_not_include_subcomponents_1);
        }

        private async void AddToCrmSolutionIncludeAsShellOnly_Click(object sender, RoutedEventArgs e)
        {
            await AddToSolution(true, null, SolutionComponent.Schema.OptionSets.rootcomponentbehavior.Include_As_Shell_Only_2);
        }

        private async void AddToCrmSolutionLastIncludeSubcomponents_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem
               && menuItem.Tag != null
               && menuItem.Tag is string solutionUniqueName
               )
            {
                await AddToSolution(false, solutionUniqueName, SolutionComponent.Schema.OptionSets.rootcomponentbehavior.Include_Subcomponents_0);
            }
        }

        private async void AddToCrmSolutionLastDoNotIncludeSubcomponents_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem
               && menuItem.Tag != null
               && menuItem.Tag is string solutionUniqueName
               )
            {
                await AddToSolution(false, solutionUniqueName, SolutionComponent.Schema.OptionSets.rootcomponentbehavior.Do_not_include_subcomponents_1);
            }
        }

        private async void AddToCrmSolutionLastIncludeAsShellOnly_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem
               && menuItem.Tag != null
               && menuItem.Tag is string solutionUniqueName
               )
            {
                await AddToSolution(false, solutionUniqueName, SolutionComponent.Schema.OptionSets.rootcomponentbehavior.Include_As_Shell_Only_2);
            }
        }

        private async Task AddToSolution(bool withSelect, string solutionUniqueName, SolutionComponent.Schema.OptionSets.rootcomponentbehavior rootComponentBehavior)
        {
            var entityList = GetSelectedEntities();

            if (entityList == null || !entityList.Any())
            {
                return;
            }

            await AddEntityMetadataToSolution(
                GetSelectedConnection()
                , entityList.Select(item => item.EntityMetadata.MetadataId.Value)
                , withSelect
                , solutionUniqueName
                , rootComponentBehavior
            );
        }

        private async void AddEntityRelationshipToCrmSolution_Click(object sender, RoutedEventArgs e)
        {
            await AddEntityRelationshipToSolution(true, null);
        }

        private async void AddEntityRelationshipToCrmSolutionLast_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem
               && menuItem.Tag != null
               && menuItem.Tag is string solutionUniqueName
               )
            {
                await AddEntityRelationshipToSolution(false, solutionUniqueName);
            }
        }

        private async Task AddEntityRelationshipToSolution(bool withSelect, string solutionUniqueName)
        {
            var entityRelationshipList = GetSelectedEntityRelationships();

            if (entityRelationshipList == null || !entityRelationshipList.Any())
            {
                return;
            }

            _commonConfig.Save();

            var service = await GetService();

            try
            {
                this._iWriteToOutput.ActivateOutputWindow(service.ConnectionData);

                await SolutionController.AddSolutionComponentsGroupToSolution(_iWriteToOutput, service, null, _commonConfig, solutionUniqueName, ComponentType.EntityRelationship, entityRelationshipList.Select(item => item.OneToManyRelationshipMetadata.MetadataId.Value).ToList(), null, withSelect);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(service.ConnectionData, ex);
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (sender is ContextMenu contextMenu)
            {
                var items = contextMenu.Items.OfType<Control>();

                ConnectionData connectionData = GetSelectedConnection();

                FillLastSolutionItems(connectionData, items, true, AddToCrmSolutionLastIncludeSubcomponents_Click, "contMnAddToSolutionLastIncludeSubcomponents");

                FillLastSolutionItems(connectionData, items, true, AddToCrmSolutionLastDoNotIncludeSubcomponents_Click, "contMnAddToSolutionLastDoNotIncludeSubcomponents");

                FillLastSolutionItems(connectionData, items, true, AddToCrmSolutionLastIncludeAsShellOnly_Click, "contMnAddToSolutionLastIncludeAsShellOnly");

                ActivateControls(items, connectionData.LastSelectedSolutionsUniqueName != null && connectionData.LastSelectedSolutionsUniqueName.Any(), "contMnAddToSolutionLast");
            }
        }

        private void ContextMenuEntityRelationship_Opened(object sender, RoutedEventArgs e)
        {
            if (sender is ContextMenu contextMenu)
            {
                var items = contextMenu.Items.OfType<Control>();

                ConnectionData connectionData = GetSelectedConnection();

                FillLastSolutionItems(connectionData, items, true, AddEntityRelationshipToCrmSolutionLast_Click, "contMnAddToSolutionLast");
            }
        }

        private void mIOpenDependentComponentsInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                connectionData.OpenSolutionComponentDependentComponentsInWeb(ComponentType.Entity, entity.EntityMetadata.MetadataId.Value);
            }
        }

        private async void mIOpenDependentComponentsInExplorer_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            _commonConfig.Save();

            var service = await GetService();

            WindowHelper.OpenSolutionComponentDependenciesExplorer(_iWriteToOutput, service, null, _commonConfig, (int)ComponentType.Entity, entity.EntityMetadata.MetadataId.Value, null);
        }

        private async void mIOpenSolutionsContainingComponentInExplorer_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            _commonConfig.Save();

            var service = await GetService();

            WindowHelper.OpenExplorerSolutionExplorer(
                _iWriteToOutput
                , service
                , _commonConfig
                , (int)ComponentType.Entity
                , entity.EntityMetadata.MetadataId.Value
                , null
            );
        }

        private void mIEntityRelationshipOpenDependentComponentsInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entityRelationship = GetSelectedEntityRelationship();

            if (entityRelationship == null)
            {
                return;
            }

            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                connectionData.OpenSolutionComponentDependentComponentsInWeb(ComponentType.EntityRelationship, entityRelationship.OneToManyRelationshipMetadata.MetadataId.Value);
            }
        }

        private async void mIEntityRelationshipOpenDependentComponentsInExplorer_Click(object sender, RoutedEventArgs e)
        {
            var entityRelationship = GetSelectedEntityRelationship();

            if (entityRelationship == null)
            {
                return;
            }

            _commonConfig.Save();

            var service = await GetService();

            WindowHelper.OpenSolutionComponentDependenciesExplorer(_iWriteToOutput, service, null, _commonConfig, (int)ComponentType.EntityRelationship, entityRelationship.OneToManyRelationshipMetadata.MetadataId.Value, null);
        }

        private async void mIEntityRelationshipOpenSolutionsContainingComponentInExplorer_Click(object sender, RoutedEventArgs e)
        {
            var entityRelationship = GetSelectedEntityRelationship();

            if (entityRelationship == null)
            {
                return;
            }

            _commonConfig.Save();

            var service = await GetService();

            WindowHelper.OpenExplorerSolutionExplorer(
                _iWriteToOutput
                , service
                , _commonConfig
                , (int)ComponentType.EntityRelationship
                , entityRelationship.OneToManyRelationshipMetadata.MetadataId.Value
                , null
            );
        }

        private void cmBCurrentConnection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this._itemsSourceEntityList?.Clear();
                this._itemsSourceEntityRelationshipList?.Clear();
            });

            if (!this.IsControlsEnabled)
            {
                return;
            }

            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                UpdateButtonsEnable();

                ShowExistingEntities();
            }
        }

        private void mIClearEntityCacheAndRefresh_Click(object sender, RoutedEventArgs e)
        {
            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                _cacheEntityMetadata.Remove(connectionData.ConnectionId);

                UpdateButtonsEnable();

                ShowExistingEntities();
            }
        }

        private void mIClearEntityRelationshipCacheAndRefresh_Click(object sender, RoutedEventArgs e)
        {
            ConnectionData connectionData = GetSelectedConnection();

            if (connectionData != null)
            {
                _cacheEntityOneToMany.Remove(connectionData.ConnectionId);

                UpdateButtonsEnable();

                ShowExistingEntityRelationships();
            }
        }

        private void cmBRelationType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            UpdateButtonsEnable();

            ShowExistingEntityRelationships();
        }

        private void btnSetCurrentConnection_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentConnection(_iWriteToOutput, GetSelectedConnection());
        }

        private async void hyperlinkPublishEntity_Click(object sender, RoutedEventArgs e)
        {
            EntityMetadataListViewItem entity = GetItemFromRoutedDataContext<EntityMetadataListViewItem>(e);

            if (entity == null)
            {
                return;
            }

            await PublishEntityAsync(GetSelectedConnection(), new[] { entity.LogicalName });
        }
    }
}