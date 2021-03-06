using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Views
{
    public partial class WindowSolutionSelect : WindowBase
    {
        private readonly IWriteToOutput _iWriteToOutput;

        /// <summary>
        /// Сервис CRM
        /// </summary>
        private IOrganizationServiceExtented _service;

        private Solution _lastSolution;
        private readonly ObservableCollection<EntityViewItem> _itemsSource;

        private object _syncObject = new object();

        public Solution SelectedSolution { get; private set; }

        public bool ForAllOther => chBForAllOther.IsChecked.GetValueOrDefault();

        public WindowSolutionSelect(
            IWriteToOutput outputWindow
            , IOrganizationServiceExtented service
        )
        {
            InitializeComponent();

            InputLanguageManager.SetInputLanguage(this, CultureInfo.CreateSpecificCulture("en-US"));

            this._service = service;
            this._iWriteToOutput = outputWindow;

            this.tSSLblConnectionName.Content = this._service.ConnectionData.Name;

            BindingOperations.EnableCollectionSynchronization(service.ConnectionData.LastSelectedSolutionsUniqueName, _syncObject);

            this._itemsSource = new ObservableCollection<EntityViewItem>();

            this.lstVwSolutions.ItemsSource = _itemsSource;

            cmBFilter.DataContext = this._service.ConnectionData;

            FocusOnComboBoxTextBox(cmBFilter);

            if (_service != null)
            {
                ShowExistingSolutions(this._service.ConnectionData.LastSelectedSolutionsUniqueName.FirstOrDefault());
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            BindingOperations.ClearAllBindings(cmBFilter);

            cmBFilter.Items.DetachFromSourceCollection();
            cmBFilter.DataContext = null;
            cmBFilter.ItemsSource = null;

            base.OnClosed(e);
        }

        private async Task ShowExistingSolutions(string lastSolutionUniqueName = null)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            ToggleControls(false, Properties.OutputStrings.LoadingSolutions);

            this._itemsSource.Clear();

            List<Solution> list = null;

            try
            {
                SolutionRepository repository = new SolutionRepository(this._service);

                if (!string.IsNullOrEmpty(lastSolutionUniqueName) && this._lastSolution == null)
                {
                    this._lastSolution = await repository.GetSolutionByUniqueNameAsync(lastSolutionUniqueName);

                    if (this._lastSolution != null)
                    {
                        if (this._lastSolution.IsManaged.GetValueOrDefault() || !this._lastSolution.IsVisible.GetValueOrDefault())
                        {
                            this._lastSolution = null;
                        }
                    }

                    var name = this._lastSolution?.UniqueName;

                    var isEnabled = this._lastSolution != null;

                    var visibility = isEnabled ? Visibility.Visible : Visibility.Collapsed;

                    this.Dispatcher.Invoke(() =>
                    {
                        txtBLastLink.Text = name;

                        btnSelectLastSolution.IsEnabled = lblLastLink.IsEnabled = txtBLastLink.IsEnabled = sepLastLink.IsEnabled = isEnabled;
                        btnSelectLastSolution.Visibility = lblLastLink.Visibility = txtBLastLink.Visibility = sepLastLink.Visibility = visibility;

                        toolStrip.UpdateLayout();
                    });
                }

                string textName = string.Empty;

                cmBFilter.Dispatcher.Invoke(() =>
                {
                    textName = cmBFilter.Text?.Trim().ToLower();
                });

                list = await repository.FindSolutionsVisibleUnmanagedAsync(textName);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(_service.ConnectionData, ex);

                list = new List<Solution>();
            }

            LoadSolutions(list);

            ToggleControls(true, Properties.OutputStrings.LoadingSolutionsCompletedFormat1, list.Count());
        }

        private class EntityViewItem
        {
            public Solution Solution { get; private set; }

            public string SolutionName => Solution.UniqueName;

            public string DisplayName => Solution.FriendlyName;

            public string SolutionType => Solution.FormattedValues[Solution.Schema.Attributes.ismanaged];

            public string Visible => Solution.FormattedValues[Solution.Schema.Attributes.isvisible];

            public DateTime? InstalledOn => Solution.InstalledOn?.ToLocalTime();

            public string PublisherName => Solution.PublisherId?.Name;

            public string Prefix => Solution.PublisherCustomizationPrefix;

            public string Version => Solution.Version;

            public EntityViewItem(Solution Solution)
            {
                this.Solution = Solution;
            }
        }

        private void LoadSolutions(IEnumerable<Solution> results)
        {
            this.lstVwSolutions.Dispatcher.Invoke(() =>
            {
                foreach (var entity in results)
                {
                    _itemsSource.Add(new EntityViewItem(entity));
                }

                if (_itemsSource.Count == 1)
                {
                    this.lstVwSolutions.SelectedItem = this.lstVwSolutions.Items[0];
                }
            });
        }

        private void UpdateStatus(string format, params object[] args)
        {
            string message = format;

            if (args != null && args.Length > 0)
            {
                message = string.Format(format, args);
            }

            _iWriteToOutput.WriteToOutput(_service.ConnectionData, message);

            this.stBIStatus.Dispatcher.Invoke(() =>
            {
                this.stBIStatus.Content = message;
            });
        }

        private void ToggleControls(bool enabled, string statusFormat, params object[] args)
        {
            this.ChangeInitByEnabled(enabled);

            UpdateStatus(statusFormat, args);

            ToggleControl(this.tSProgressBar, this.btnSelectSolution, this.btnSelectLastSolution);

            UpdateButtonsEnable();
        }

        private void UpdateButtonsEnable()
        {
            this.lstVwSolutions.Dispatcher.Invoke(() =>
            {
                try
                {
                    bool enabled = this.lstVwSolutions.SelectedItems.Count > 0;

                    UIElement[] list = { btnSelectSolution, btnOpenSolutionInWeb };

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

        private void cmBFilterEnitity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowExistingSolutions();
            }
        }

        private Solution GetSelectedEntity()
        {
            return this.lstVwSolutions.SelectedItems.OfType<EntityViewItem>().Count() == 1
                ? this.lstVwSolutions.SelectedItems.OfType<EntityViewItem>().Select(e => e.Solution).SingleOrDefault() : null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstVwEntities_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                EntityViewItem item = GetItemFromRoutedDataContext<EntityViewItem>(e);

                if (item != null && item.Solution != null)
                {
                    SelectSolutioAction(item.Solution);
                }
            }
        }

        private void lstVwEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonsEnable();
        }

        private void SelectSolutioAction(Solution solution)
        {
            if (!this.IsControlsEnabled)
            {
                return;
            }

            if (solution == null)
            {
                return;
            }

            this.SelectedSolution = solution;

            var text = cmBFilter.Text;

            this._service.ConnectionData.AddLastSelectedSolution(solution.UniqueName);

            _iWriteToOutput.WriteToOutputSolutionUri(_service.ConnectionData, solution.UniqueName, solution.Id);

            cmBFilter.Text = text;

            this.DialogResult = true;

            this.Close();
        }

        private void btnSelectSolution_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            SelectSolutioAction(entity);
        }

        protected override void OnRefreshList(ExecutedRoutedEventArgs e)
        {
            e.Handled = true;

            ShowExistingSolutions();
        }

        private void btnSelectLastSolution_Click(object sender, RoutedEventArgs e)
        {
            if (this._lastSolution != null)
            {
                this.SelectedSolution = this._lastSolution;

                this._service.ConnectionData.AddLastSelectedSolution(this._lastSolution.UniqueName);

                _iWriteToOutput.WriteToOutputSolutionUri(_service.ConnectionData, _lastSolution.UniqueName, _lastSolution.Id);

                this.DialogResult = true;
            }
        }

        private void btnOpenSolutionInWeb_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            this._service.ConnectionData.OpenSolutionInWeb(entity.Id);
        }

        private void mIOpenSolutionListInWeb_Click(object sender, RoutedEventArgs e)
        {
            this._service.ConnectionData.OpenCrmWebSite(OpenCrmWebSiteType.Solutions);
        }

        private void mIOpenCustomizationInWeb_Click(object sender, RoutedEventArgs e)
        {
            this._service.ConnectionData.OpenCrmWebSite(OpenCrmWebSiteType.Customization);
        }

        private void btnOpenComponentsInExplorer_Click(object sender, RoutedEventArgs e)
        {
            var entity = GetSelectedEntity();

            if (entity == null)
            {
                return;
            }

            var commonConfig = CommonConfiguration.Get();

            WindowHelper.OpenSolutionComponentsExplorer(this._iWriteToOutput, _service, null, commonConfig, entity.UniqueName, null);
        }

        public void ShowForAllOther()
        {
            sepForAllOther.IsEnabled = chBForAllOther.IsEnabled = true;
            sepForAllOther.Visibility = chBForAllOther.Visibility = Visibility.Visible;
        }

        private void mICreateNewSolution_Click(object sender, RoutedEventArgs e)
        {
            this._service.ConnectionData.OpenSolutionCreateInWeb();
        }

        private async void ClearUnmanagedSolution_Click(object sender, RoutedEventArgs e)
        {
            var solution = GetSelectedEntity();

            if (solution == null)
            {
                return;
            }

            string question = string.Format(Properties.MessageBoxStrings.ClearSolutionFormat1, solution.UniqueName);

            if (MessageBox.Show(question, Properties.MessageBoxStrings.QuestionTitle, MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
            {
                return;
            }

            try
            {

                ToggleControls(false, Properties.OutputStrings.ClearingSolutionFormat2, _service.ConnectionData.Name, solution.UniqueName);

                var commonConfig = CommonConfiguration.Get();

                var descriptor = new SolutionComponentDescriptor(_service);
                descriptor.SetSettings(commonConfig);

                SolutionDescriptor solutionDescriptor = new SolutionDescriptor(_iWriteToOutput, _service, descriptor);

                this._iWriteToOutput.WriteToOutput(_service.ConnectionData, string.Empty);
                this._iWriteToOutput.WriteToOutput(_service.ConnectionData, "Creating backup Solution Components in '{0}'.", solution.UniqueName);

                {
                    string fileName = EntityFileNameFormatter.GetSolutionFileName(
                        _service.ConnectionData.Name
                        , solution.UniqueName
                        , "Components Backup"
                    );

                    string filePath = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileName));

                    await solutionDescriptor.CreateFileWithSolutionComponentsAsync(filePath, solution.Id);

                    this._iWriteToOutput.WriteToOutput(_service.ConnectionData, "Created backup Solution Components in '{0}': {1}", solution.UniqueName, filePath);
                    this._iWriteToOutput.WriteToOutputFilePathUri(_service.ConnectionData, filePath);
                }

                {
                    string fileName = EntityFileNameFormatter.GetSolutionFileName(
                        _service.ConnectionData.Name
                        , solution.UniqueName
                        , "SolutionImage Backup before Clearing"
                        , "xml"
                    );

                    string filePath = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileName));

                    SolutionImage solutionImage = await solutionDescriptor.CreateSolutionImageAsync(solution.Id, solution.UniqueName);

                    await solutionImage.SaveAsync(filePath);
                }

                SolutionComponentRepository repository = new SolutionComponentRepository(_service);
                await repository.ClearSolutionAsync(solution.UniqueName);

                ToggleControls(true, Properties.OutputStrings.ClearingSolutionCompletedFormat2, _service.ConnectionData.Name, solution.UniqueName);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(_service.ConnectionData, ex);

                ToggleControls(true, Properties.OutputStrings.ClearingSolutionFailedFormat2, _service.ConnectionData.Name, solution.UniqueName);
            }
        }
    }
}