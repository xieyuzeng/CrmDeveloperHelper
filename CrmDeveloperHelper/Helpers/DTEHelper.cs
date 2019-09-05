using EnvDTE;
using EnvDTE80;
using Nav.Common.VSPackages.CrmDeveloperHelper.Controllers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers
{
    public partial class DTEHelper
    {
        public DTE2 ApplicationObject { get; private set; }

        private readonly MainController Controller;

        private static DTEHelper _singleton;

        public static DTEHelper Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    var applicationObject = CrmDeveloperHelperPackage.Singleton?.ApplicationObject;

                    if (applicationObject != null)
                    {
                        _singleton = new DTEHelper(applicationObject);
                    }
                }

                return _singleton;
            }
        }

        private static readonly object syncObject = new object();

        public static DTEHelper Create(DTE2 applicationObject)
        {
            if (applicationObject == null)
            {
                throw new ArgumentNullException(nameof(applicationObject));
            }

            lock (syncObject)
            {
                if (_singleton != null)
                {
                    return _singleton;
                }

                _singleton = new DTEHelper(applicationObject);

                return _singleton;
            }
        }

        private DTEHelper(DTE2 applicationObject)
        {
            this.ApplicationObject = applicationObject ?? throw new ArgumentNullException(nameof(applicationObject));

            this.Controller = new MainController(this);

            System.Threading.Thread clearTempFiles = new System.Threading.Thread(ClearTemporaryFiles);
            clearTempFiles.Start();
        }

        private void ClearTemporaryFiles()
        {
            string directory = FileOperations.GetTempFileFolder();

            if (Directory.Exists(directory))
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(directory);

                    var files = dir.GetFiles();

                    foreach (var item in files)
                    {
                        try
                        {
                            item.Delete();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void CheckWishToChangeCurrentConnection(ConnectionData connectionData)
        {
            if (connectionData == null || connectionData.ConnectionConfiguration == null)
            {
                return;
            }

            if (connectionData.ConnectionConfiguration.CurrentConnectionData?.ConnectionId == connectionData.ConnectionId)
            {
                return;
            }

            if ((System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Shift) != 0)
            {
                connectionData.ConnectionConfiguration.SetCurrentConnection(connectionData.ConnectionId);
                connectionData.ConnectionConfiguration.Save();
                this.WriteToOutput(null, Properties.OutputStrings.CurrentConnectionFormat1, connectionData.Name);
                this.ActivateOutputWindow(null);
            }
        }

        public bool HasCurrentCrmConnection(out ConnectionConfiguration connectionConfig)
        {
            bool result = false;

            connectionConfig = Model.ConnectionConfiguration.Get();

            if (connectionConfig.CurrentConnectionData == null)
            {
                this.WriteToOutput(null, Properties.OutputStrings.CurrentCrmConnectionIsNotSelected);
                this.ActivateOutputWindow(null);

                var crmConnection = new WindowCrmConnectionList(this, connectionConfig, true);

                crmConnection.ShowDialog();

                connectionConfig.Save();
            }

            result = connectionConfig.CurrentConnectionData != null;

            return result;
        }

        public void HandleFileCompareCommand(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool withDetails)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartComparing(selectedFiles, connectionData, withDetails);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdateContentWebResourcesAndPublishCommand(ConnectionData connectionData, List<SelectedFile> selectedFiles)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                bool canPublish = false;

                if (commonConfig.DoNotPropmPublishMessage)
                {
                    canPublish = true;
                }
                else
                {
                    string message = string.Format(Properties.MessageBoxStrings.PublishWebResourcesFormat2, selectedFiles.Count, connectionData.GetDescriptionColumn());

                    var dialog = new WindowConfirmPublish(message);

                    if (dialog.ShowDialog().GetValueOrDefault())
                    {
                        commonConfig.DoNotPropmPublishMessage = dialog.DoNotPromtPublishMessage;

                        commonConfig.Save();

                        canPublish = true;
                    }
                }

                if (canPublish)
                {
                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartUpdateContentAndPublish(selectedFiles, connectionData);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleUpdateContentWebResourcesAndPublishEqualByTextCommand(ConnectionData connectionData, List<SelectedFile> selectedFiles)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                bool canPublish = false;

                if (commonConfig.DoNotPropmPublishMessage)
                {
                    canPublish = true;
                }
                else
                {
                    string message = string.Format(Properties.MessageBoxStrings.PublishWebResourcesEqualByTextFormat2, selectedFiles.Count, connectionData.GetDescription());

                    var dialog = new WindowConfirmPublish(message);

                    if (dialog.ShowDialog().GetValueOrDefault())
                    {
                        commonConfig.DoNotPropmPublishMessage = dialog.DoNotPromtPublishMessage;

                        commonConfig.Save();

                        canPublish = true;
                    }
                }

                if (canPublish)
                {
                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartUpdateContentAndPublishEqualByText(selectedFiles, connectionData);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleAddingWebResourcesToSolutionCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, List<SelectedFile> selectedFiles)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartAddingWebResourcesToSolution(connectionData, commonConfig, solutionUniqueName, selectedFiles, withSelect);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleAddingPluginAssemblyToSolutionByProjectCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, params string[] projectNames)
        {
            if (projectNames == null || !projectNames.Any())
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartAddingPluginAssemblyToSolution(connectionData, commonConfig, solutionUniqueName, projectNames, withSelect);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleAddingPluginAssemblyProcessingStepsByProjectCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, params string[] projectNames)
        {
            if (projectNames == null || !projectNames.Any())
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartAddingPluginAssemblyProcessingStepsToSolution(connectionData, commonConfig, solutionUniqueName, projectNames, withSelect);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleAddingPluginTypeProcessingStepsByProjectCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, params string[] pluginTypeNames)
        {
            if (pluginTypeNames == null || !pluginTypeNames.Any())
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartAddingPluginTypeProcessingStepsToSolution(connectionData, commonConfig, solutionUniqueName, pluginTypeNames, withSelect);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleAddingReportsToSolutionCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, List<SelectedFile> selectedFiles)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartAddingReportsToSolution(connectionData, commonConfig, solutionUniqueName, selectedFiles, withSelect);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleComparingPluginAssemblyAndLocalAssemblyCommand(ConnectionData connectionData, EnvDTE.Project project)
        {
            if (project == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(project.Name))
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                var defaultOutputFilePath = PropertiesHelper.GetOutputFilePath(project);

                try
                {
                    Controller.StartComparingPluginAssemblyAndLocalAssembly(connectionData, commonConfig, project.Name, defaultOutputFilePath);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdatingPluginAssemblyCommand(ConnectionData connectionData, params EnvDTE.Project[] projectList)
        {
            HandleUpdatingPluginAssemblyCommand(connectionData, projectList.ToList());
        }

        public void HandleUpdatingPluginAssemblyCommand(ConnectionData connectionData, List<EnvDTE.Project> projectList)
        {
            if (projectList == null || !projectList.Any(p => !string.IsNullOrEmpty(p.Name)))
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartUpdatingPluginAssembly(connectionData, commonConfig, projectList);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleRegisterPluginAssemblyCommand(ConnectionData connectionData, List<EnvDTE.Project> projectList)
        {
            if (projectList == null || !projectList.Any(p => !string.IsNullOrEmpty(p.Name)))
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartRegisterPluginAssembly(connectionData, commonConfig, projectList);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExecutingFetchXml(ConnectionData connectionData, SelectedFile selectedFile, bool strictConnection)
        {
            if (selectedFile == null || !File.Exists(selectedFile.FilePath))
            {
                return;
            }

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                CrmDeveloperHelperPackage.Singleton?.ExecuteFetchXmlQueryAsync(selectedFile.FilePath, connectionData, this, strictConnection);
            }
        }

        public void HandleUpdateEntityMetadataFileCSharpSchema(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

                try
                {
                    Controller.StartUpdatingFileWithEntityMetadataCSharpSchema(selectedFiles, connectionData, commonConfig, selectEntity, openOptions);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdateEntityMetadataFileCSharpProxyClassOrSchema(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

                try
                {
                    Controller.StartUpdatingFileWithEntityMetadataCSharpProxyClassOrSchema(selectedFiles, connectionData, commonConfig, selectEntity, openOptions);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdateEntityMetadataFileCSharpProxyClass(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

                try
                {
                    Controller.StartUpdatingFileWithEntityMetadataCSharpProxyClass(selectedFiles, connectionData, commonConfig, selectEntity, openOptions);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdateEntityMetadataFileJavaScript(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

                try
                {
                    Controller.StartUpdatingFileWithEntityMetadataJavaScript(selectedFiles, connectionData, commonConfig, selectEntity, openOptions);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdateGlobalOptionSetsFile(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool withSelect)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

                try
                {
                    Controller.StartUpdatingFileWithGlobalOptionSets(connectionData, commonConfig, selectedFiles, withSelect, openOptions);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdateGlobalOptionSetSingleFileJavaScript(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

                try
                {
                    Controller.StartUpdatingFileWithGlobalOptionSetSingleJavaScript(selectedFiles, connectionData, commonConfig, selectEntity, openOptions);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleUpdateGlobalOptionSetAllFileJavaScript(ConnectionData connectionData, SelectedFile selectedFile)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

                try
                {
                    Controller.StartUpdatingFileWithGlobalOptionSetAllJavaScript(connectionData, commonConfig, selectedFile, openOptions);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleReportDifferenceCommand(ConnectionData connectionData, string fieldName, string fieldTitle, bool isCustom)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartReportDifference(selectedFiles[0], fieldName, fieldTitle, isCustom, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleReportThreeFileDifferenceCommand(ConnectionData connectionData1, ConnectionData connectionData2, string fieldName, string fieldTitle, ShowDifferenceThreeFileType differenceType)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            if (connectionData1 != null && connectionData2 != null && connectionData1 != connectionData2 && commonConfig != null && selectedFiles.Count == 1)
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartReportThreeFileDifference(selectedFiles[0], fieldName, fieldTitle, connectionData1, connectionData2, differenceType, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandleReportUpdateCommand(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartReportUpdate(selectedFiles[0], connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenReportExplorerCommand()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                SelectedFile selectedFile = selectedFiles[0];

                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartOpenReportExplorer(connectionData, commonConfig, selectedFile.FileName);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenReportCommand(ConnectionData connectionData, ActionOpenComponent action)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                SelectedFile selectedFile = selectedFiles[0];

                var objectId = connectionData.GetLastLinkForFile(selectedFile.FriendlyFilePath);

                if (objectId.HasValue)
                {
                    switch (action)
                    {
                        case ActionOpenComponent.OpenInWeb:
                            connectionData.OpenEntityInstanceInWeb(Entities.Report.EntityLogicalName, objectId.Value);
                            return;

                        case ActionOpenComponent.OpenDependentComponentsInWeb:
                            connectionData.OpenSolutionComponentDependentComponentsInWeb(Entities.ComponentType.Report, objectId.Value);
                            return;
                    }
                }

                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartOpeningReport(commonConfig, connectionData, selectedFile, action);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenLastSelectedSolution(ConnectionData connectionData, string solutionUniqueName, ActionOpenComponent action)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && !string.IsNullOrEmpty(solutionUniqueName) && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpeningSolution(commonConfig, connectionData, solutionUniqueName, action);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCreateLaskLinkReportCommand(List<SelectedFile> selectedFiles)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                SelectedFile selectedFile = selectedFiles[0];

                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartCreatingLastLinkReport(selectedFile, connectionData);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleFileClearLink(List<SelectedFile> selectedFiles)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartClearingLastLink(selectedFiles, connectionData);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleWebResourceDifferenceCommand(ConnectionData connectionData, bool isCustom)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceTextType, false).Take(2).ToList();

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartWebResourceDifference(selectedFiles[0], isCustom, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleRibbonDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartRibbonDifference(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSiteMapDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSiteMapDifference(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSiteMapUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSiteMapUpdate(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSiteMapOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSiteMapOpenInWeb(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSystemFormDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSystemFormDifference(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSystemFormUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSystemFormUpdate(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSystemFormOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSystemFormOpenInWeb(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSavedQueryDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSavedQueryDifference(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSavedQueryUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSavedQueryUpdate(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSavedQueryOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartSavedQueryOpenInWeb(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleWorkflowDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartWorkflowDifference(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleWorkflowUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartWorkflowUpdate(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleWorkflowOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartWorkflowOpenInWeb(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleRibbonDiffXmlDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartRibbonDiffXmlDifference(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleRibbonDiffXmlUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                if (connectionData.IsReadOnly)
                {
                    this.WriteToOutput(null, Properties.OutputStrings.ConnectionIsReadOnlyFormat1, connectionData.Name);
                    return;
                }

                string message = string.Format(Properties.MessageBoxStrings.PublishRibbonDiffXmlFormat2, selectedFile.FileName, connectionData.GetDescription());

                var dialog = new WindowConfirmPublish(message, false);

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartRibbonDiffXmlUpdate(selectedFile, connectionData, commonConfig);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleEntityRibbonOpenInWeb(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartEntityRibbonOpenInWeb(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleRibbonExplorerCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartRibbonExplorer(selectedFile, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleWebResourceThreeFileDifferenceCommand(ConnectionData connectionData1, ConnectionData connectionData2, ShowDifferenceThreeFileType differenceType)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceTextType, false).Take(2).ToList();

            if (connectionData1 != null && connectionData2 != null && connectionData1 != connectionData2 && commonConfig != null && selectedFiles.Count == 1)
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartWebResourceThreeFileDifference(selectedFiles[0], connectionData1, connectionData2, differenceType, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandleCheckFileEncodingCommand(List<SelectedFile> selectedFiles)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartCheckFileEncoding(selectedFiles);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandleOpenWebResourceExplorerCommand()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceType, false).Take(2).ToList();

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                SelectedFile selectedFile = selectedFiles[0];

                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartOpenWebResourceExplorer(connectionData, commonConfig, selectedFile.FileName);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenWebResource(ConnectionData connectionData, ActionOpenComponent action)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceType, false).Take(2).ToList();

            if (connectionData != null && commonConfig != null && selectedFiles.Count == 1)
            {
                SelectedFile selectedFile = selectedFiles[0];

                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                var objectId = connectionData.GetLastLinkForFile(selectedFile.FriendlyFilePath);

                if (objectId.HasValue)
                {
                    switch (action)
                    {
                        case ActionOpenComponent.OpenInWeb:
                            connectionData.OpenEntityInstanceInWeb(Entities.WebResource.EntityLogicalName, objectId.Value);
                            return;

                        case ActionOpenComponent.OpenDependentComponentsInWeb:
                            connectionData.OpenSolutionComponentDependentComponentsInWeb(Entities.ComponentType.WebResource, objectId.Value);
                            return;
                    }
                }

                try
                {
                    Controller.StartOpeningWebResource(commonConfig, connectionData, selectedFile, action);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleShowingWebResourcesDependentComponents(List<SelectedFile> selectedFiles)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var form = new WindowSelectFolderForExport(connectionData, commonConfig.FolderForExport, commonConfig.DefaultFileAction);

                if (form.ShowDialog().GetValueOrDefault())
                {
                    commonConfig.FolderForExport = form.SelectedFolder;
                    commonConfig.DefaultFileAction = form.GetFileAction();

                    connectionData = form.GetConnectionData();

                    if (connectionData != null)
                    {
                        commonConfig.Save();

                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.ShowingWebResourcesDependentComponents(selectedFiles, connectionData, commonConfig);
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleCheckingWorkflowsUsedEntities()
        {
            HandleCheckingWorkflowsUsedEntities(null);
        }

        public void HandleCheckingWorkflowsUsedEntities(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var form = new WindowSelectFolderForExport(connectionData, commonConfig.FolderForExport, commonConfig.DefaultFileAction);

                if (form.ShowDialog().GetValueOrDefault())
                {
                    commonConfig.FolderForExport = form.SelectedFolder;
                    commonConfig.DefaultFileAction = form.GetFileAction();

                    connectionData = form.GetConnectionData();

                    if (connectionData != null)
                    {
                        commonConfig.Save();

                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.ExecuteCheckingWorkflowsUsedEntities(connectionData, commonConfig);
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleCheckingWorkflowsNotExistingUsedEntities()
        {
            HandleCheckingWorkflowsNotExistingUsedEntities(null);
        }

        public void HandleCheckingWorkflowsNotExistingUsedEntities(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var form = new WindowSelectFolderForExport(connectionData, commonConfig.FolderForExport, commonConfig.DefaultFileAction);

                if (form.ShowDialog().GetValueOrDefault())
                {
                    commonConfig.FolderForExport = form.SelectedFolder;
                    commonConfig.DefaultFileAction = form.GetFileAction();

                    connectionData = form.GetConnectionData();

                    if (connectionData != null)
                    {
                        commonConfig.Save();

                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.ExecuteCheckingWorkflowsNotExistingUsedEntities(connectionData, commonConfig);
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleOpenFilesCommand(List<SelectedFile> selectedFiles, OpenFilesType openFilesType, bool inTextEditor)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                if (inTextEditor && !File.Exists(commonConfig.TextEditorProgram))
                {
                    return;
                }

                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                Controller.StartOpeningFiles(selectedFiles, openFilesType, inTextEditor, connectionData, commonConfig);
            }
        }

        public void HandleFileCompareListForPublishCommand(ConnectionData connectionData, bool withDetails)
        {
            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            CheckWishToChangeCurrentConnection(connectionData);

            List<SelectedFile> selectedFiles = this.GetSelectedFilesFromListForPublish().ToList();

            if (selectedFiles.Count > 0)
            {
                this.ShowListForPublish(connectionData);

                this.HandleFileCompareCommand(connectionData, selectedFiles, withDetails);
            }
            else
            {
                this.WriteToOutput(connectionData, Properties.OutputStrings.PublishListIsEmpty);
                this.ActivateOutputWindow(connectionData);
            }
        }

        public void HandleMultiDifferenceFiles(List<SelectedFile> selectedFiles, OpenFilesType openFilesType)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartMultiDifferenceFiles(selectedFiles, openFilesType, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void OpenConnectionList()
        {
            ConnectionConfiguration connectionConfig = Model.ConnectionConfiguration.Get();

            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowCrmConnectionList(this, connectionConfig);

                    form.ShowDialog();

                    connectionConfig.Save();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public void TestConnection(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                QuickConnection.TestConnectAsync(connectionData, this, null);
            }
        }

        public void EditConnection(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                WindowHelper.OpenCrmConnectionCard(this, connectionData);
            }
        }

        public void OpenCommonConfiguration()
        {
            CommonConfiguration config = CommonConfiguration.Get();

            if (config != null)
            {
                var form = new WindowCommonConfiguration(config);

                form.ShowDialog();
            }
        }

        public ConnectionData GetCurrentConnection()
        {
            ConnectionConfiguration connectionConfig = Model.ConnectionConfiguration.Get();

            return connectionConfig?.CurrentConnectionData;
        }

        public string GetLastSolutionUniqueName()
        {
            ConnectionConfiguration connectionConfig = Model.ConnectionConfiguration.Get();

            return connectionConfig?.CurrentConnectionData?.LastSelectedSolutionsUniqueName?.FirstOrDefault();
        }

        #region Finds and Edits

        public void HandleFindEntityObjectsByPrefix()
        {
            HandleFindEntityObjectsByPrefix(null);
        }

        public void HandleFindEntityObjectsByPrefix(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select Entity Name Prefix", "Entity Name Prefix");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindEntityObjectsByPrefix(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindEntityObjectsByPrefixInExplorer()
        {
            HandleFindEntityObjectsByPrefixInExplorer(null);
        }

        public void HandleFindEntityObjectsByPrefixInExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select Entity Name Prefix", "Entity Name Prefix");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindEntityObjectsByPrefixInExplorer(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindEntityObjectsByPrefixAndShowDependentComponents()
        {
            HandleFindEntityObjectsByPrefixAndShowDependentComponents(null);
        }

        public void HandleFindEntityObjectsByPrefixAndShowDependentComponents(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select Entity Name Prefix", "Entity Name Prefix");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindEntityObjectsByPrefixAndShowDependentComponents(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindMarkedToDeleteInExplorer()
        {
            HandleFindMarkedToDeleteInExplorer(null);
        }

        public void HandleFindMarkedToDeleteInExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select mark to delete", "Mark to delete");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindMarkedToDeleteInExplorer(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindMarkedToDeleteAndShowDependentComponents()
        {
            HandleFindMarkedToDeleteAndShowDependentComponents(null);
        }

        public void HandleFindMarkedToDeleteAndShowDependentComponents(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select mark to delete", "Mark to delete");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindMarkedToDeleteAndShowDependentComponents(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindEntityObjectsByName()
        {
            HandleFindEntityObjectsByName(null);
        }

        public void HandleFindEntityObjectsByName(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select Element Name", "Element Name");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindEntityObjectsByName(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindEntityObjectsByNameInExplorer()
        {
            HandleFindEntityObjectsByNameInExplorer(null);
        }

        public void HandleFindEntityObjectsByNameInExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select Element Name", "Element Name");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindEntityObjectsByNameInExplorer(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindEntityObjectsContainsString()
        {
            HandleFindEntityObjectsContainsString(null);
        }

        public void HandleFindEntityObjectsContainsString(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select String for contain", "String for contain");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindEntityObjectsContainsString(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindEntityObjectsContainsStringInExplorer()
        {
            HandleFindEntityObjectsContainsStringInExplorer(null);
        }

        public void HandleFindEntityObjectsContainsStringInExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectFolderAndText(commonConfig, connectionData, "Select String for contain", "String for contain");

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    connectionData = dialog.GetConnectionData();

                    if (connectionData != null)
                    {
                        ActivateOutputWindow(connectionData);
                        WriteToOutputEmptyLines(connectionData, commonConfig);

                        CheckWishToChangeCurrentConnection(connectionData);

                        try
                        {
                            Controller.StartFindEntityObjectsContainsStringInExplorer(connectionData, commonConfig, dialog.GetText());
                        }
                        catch (Exception ex)
                        {
                            WriteErrorToOutput(connectionData, ex);
                        }
                    }
                }
            }
        }

        public void HandleFindEntityById()
        {
            HandleFindEntityById(null);
        }

        public void HandleFindEntityById(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectEntityIdToFind(commonConfig, connectionData, string.Format("Find Entity in {0} by Id", connectionData.Name));

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    string entityName = dialog.EntityTypeName;
                    int? entityTypeCode = dialog.EntityTypeCode;
                    Guid entityId = dialog.EntityId;

                    connectionData = dialog.GetConnectionData();

                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartFindEntityById(connectionData, commonConfig, entityName, entityTypeCode, entityId);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleFindEntityByUniqueidentifier()
        {
            HandleFindEntityByUniqueidentifier(null);
        }

        public void HandleFindEntityByUniqueidentifier(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectEntityIdToFind(commonConfig, connectionData, string.Format("Find Entity in {0} by Uniqueidentifier", connectionData.Name));

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    string entityName = dialog.EntityTypeName;
                    int? entityTypeCode = dialog.EntityTypeCode;
                    Guid entityId = dialog.EntityId;

                    connectionData = dialog.GetConnectionData();

                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartFindEntityByUniqueidentifier(connectionData, commonConfig, entityName, entityTypeCode, entityId);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleEditEntityById()
        {
            HandleEditEntityById(null);
        }

        public void HandleEditEntityById(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var dialog = new WindowSelectEntityIdToFind(commonConfig, connectionData, string.Format("Edit Entity in {0} by Id", connectionData.Name));

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    string entityName = dialog.EntityTypeName;
                    int? entityTypeCode = dialog.EntityTypeCode;
                    Guid entityId = dialog.EntityId;

                    connectionData = dialog.GetConnectionData();

                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartEditEntityById(connectionData, commonConfig, entityName, entityTypeCode, entityId);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        #endregion Finds and Edits

        public void HandleCheckEntitiesOwnership(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckEntitiesOwnership(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckGlobalOptionSetDuplicates(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckGlobalOptionSetDuplicates(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckComponentTypeEnum(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckComponentTypeEnum(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckUnknownFormControlTypes(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckUnknownFormControlTypes(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCreateAllDependencyNodesDescription(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCreateAllDependencyNodesDescription(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckManagedEntities(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckManagedEntities(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckPluginSteps(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckPluginSteps(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckPluginImages(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckPluginImages(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckPluginStepsRequiredComponents(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckPluginStepsRequiredComponents(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckPluginImagesRequiredComponents(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCheckPluginImagesRequiredComponents(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenPluginTree(string entityFilter, string pluginTypeFilter, string messageFilter)
        {
            HandleOpenPluginTree(null, entityFilter, pluginTypeFilter, messageFilter);
        }

        public void HandleOpenPluginTree(ConnectionData connectionData, string entityFilter, string pluginTypeFilter, string messageFilter)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartShowingPluginTree(connectionData, commonConfig, entityFilter, pluginTypeFilter, messageFilter);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSdkMessageTree()
        {
            HandleSdkMessageTree(null);
        }

        public void HandleSdkMessageTree(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartShowingSdkMessageTree(connectionData, commonConfig, selection, null);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenSystemUsersExplorer()
        {
            HandleOpenSystemUsersExplorer(null);
        }

        public void HandleOpenSystemUsersExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartShowingSystemUserExplorer(connectionData, commonConfig, selection);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenTeamsExplorer()
        {
            HandleOpenTeamsExplorer(null);
        }

        public void HandleOpenTeamsExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartShowingTeamsExplorer(connectionData, commonConfig, selection);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenSecurityRolesExplorer()
        {
            HandleOpenSecurityRolesExplorer(null);
        }

        public void HandleOpenSecurityRolesExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartShowingSecurityRolesExplorer(connectionData, commonConfig, selection);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandlePluginConfigurationCreate()
        {
            HandlePluginConfigurationCreate(null);
        }

        public void HandlePluginConfigurationCreate(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                var form = new WindowPluginConfiguration(commonConfig, connectionData, true);

                if (form.ShowDialog().GetValueOrDefault())
                {
                    connectionData = form.GetConnectionData();

                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartExportPluginConfiguration(connectionData, commonConfig);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandlePluginConfigurationPluginAssemblyDescription()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsXmlType, false).Take(2).ToList();

            string filePath = string.Empty;

            if (selectedFiles.Count == 1)
            {
                filePath = selectedFiles[0].FilePath;
            }

            if (commonConfig != null && Directory.Exists(commonConfig.FolderForExport))
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartShowingPluginConfigurationAssemblyDescription(commonConfig, filePath);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandlePluginConfigurationPluginTypeDescription()
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsXmlType, false).Take(2).ToList();

            string filePath = string.Empty;

            if (selectedFiles.Count == 1)
            {
                filePath = selectedFiles[0].FilePath;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (commonConfig != null && Directory.Exists(commonConfig.FolderForExport))
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartShowingPluginConfigurationTypeDescription(commonConfig, filePath);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandlePluginConfigurationTree()
        {
            HandlePluginConfigurationTree(null);
        }

        public void HandlePluginConfigurationTree(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsXmlType, false).Take(2).ToList();

            string filePath = string.Empty;

            if (selectedFiles.Count == 1)
            {
                filePath = selectedFiles[0].FilePath;
            }

            if (connectionData != null && commonConfig != null && Directory.Exists(commonConfig.FolderForExport))
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartShowingPluginConfigurationTree(connectionData, commonConfig, filePath);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandlePluginConfigurationComparerPluginAssembly()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            ConnectionConfiguration crmConfig = Model.ConnectionConfiguration.Get();

            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsXmlType, false).Take(2).ToList();

            string filePath = string.Empty;

            if (selectedFiles.Count == 1)
            {
                filePath = selectedFiles[0].FilePath;
            }

            if (commonConfig != null)
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartShowingPluginConfigurationComparer(commonConfig, filePath);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandleOrganizationComparer()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            ConnectionConfiguration crmConfig = Model.ConnectionConfiguration.Get();

            if (commonConfig != null)
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartOrganizationComparer(crmConfig, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandleTraceReaderWindow(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    this.Controller.StartTraceReaderWindow(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    this.WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportFileWithEntityMetadata()
        {
            HandleExportFileWithEntityMetadata(null, null);
        }

        public void HandleExportFileWithEntityMetadata(ConnectionData connectionData)
        {
            HandleExportFileWithEntityMetadata(connectionData, null);
        }

        public void HandleExportFileWithEntityMetadata(ConnectionData connectionData, SelectedItem selectedItem)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            string selection = string.Empty;

            if (selectedItem == null)
            {
                selection = GetSelectedText();
            }

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpeningEntityMetadataExplorer(selection, selectedItem, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleEntityMetadataFileGenerationOptions(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();
            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpeningEntityMetadataFileGenerationOptions(connectionData);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleGlobalOptionSetsMetadataFileGenerationOptions(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();
            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpeningGlobalOptionSetsMetadataFileGenerationOptions(connectionData);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleSdkMessageRequestTree()
        {
            HandleSdkMessageRequestTree(null, null);
        }

        public void HandleSdkMessageRequestTree(ConnectionData connectionData)
        {
            HandleSdkMessageRequestTree(connectionData, null);
        }

        public void HandleSdkMessageRequestTree(ConnectionData connectionData, SelectedItem selectedItem)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            string selection = string.Empty;

            if (selectedItem == null)
            {
                selection = GetSelectedText();
            }

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartShowingSdkMessageRequestTree(connectionData, commonConfig, selection, null, selectedItem);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenEntityAttributeExplorer()
        {
            HandleOpenEntityAttributeExplorer(null);
        }

        public void HandleOpenEntityAttributeExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartExplorerEntityAttribute(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenEntityKeyExplorer()
        {
            HandleOpenEntityKeyExplorer(null);
        }

        public void HandleOpenEntityKeyExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();
            string selection = GetSelectedText();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartExplorerEntityKey(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenEntityRelationshipOneToManyExplorer()
        {
            HandleOpenEntityRelationshipOneToManyExplorer(null);
        }

        public void HandleOpenEntityRelationshipOneToManyExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartExplorerEntityRelationshipOneToMany(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenEntityRelationshipManyToManyExplorer()
        {
            HandleOpenEntityRelationshipManyToManyExplorer(null);
        }

        public void HandleOpenEntityRelationshipManyToManyExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartExplorerEntityRelationshipManyToMany(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenEntityPrivilegesExplorer()
        {
            HandleOpenEntityPrivilegesExplorer(null);
        }

        public void HandleOpenEntityPrivilegesExplorer(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartExplorerEntityPrivileges(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportFormEvents(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                var form = new WindowExportFormEvents(commonConfig);

                if (form.ShowDialog().GetValueOrDefault())
                {
                    try
                    {
                        Controller.StartExportingFormEvents(connectionData, commonConfig);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleExportGlobalOptionSets()
        {
            HandleExportGlobalOptionSets(null, null);
        }

        public void HandleExportGlobalOptionSets(ConnectionData connectionData)
        {
            HandleExportGlobalOptionSets(connectionData, null);
        }

        public void HandleExportGlobalOptionSets(ConnectionData connectionData, SelectedItem selectedItem)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            string selection = string.Empty;

            if (selectedItem == null)
            {
                selection = GetSelectedText();
            }

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartCreatingFileWithGlobalOptionSets(connectionData, commonConfig, selection, selectedItem);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportOrganizationInformation()
        {
            HandleExportOrganizationInformation(null);
        }

        public void HandleExportOrganizationInformation(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartExplorerOrganizationInformation(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenPluginAssemblyExplorer()
        {
            HandleOpenPluginAssemblyExplorer(null, null);
        }

        public void HandleOpenPluginAssemblyExplorer(string selection)
        {
            HandleOpenPluginAssemblyExplorer(null, selection);
        }

        public void HandleOpenPluginAssemblyExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            HandleOpenPluginAssemblyExplorer(connectionData, selection);
        }

        public void HandleOpenPluginAssemblyExplorer(ConnectionData connectionData, string selection)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpenPluginAssemblyExplorer(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenPluginTypeExplorer(string selection)
        {
            HandleOpenPluginTypeExplorer(null, selection);
        }

        public void HandleOpenPluginTypeExplorer(ConnectionData connectionData, string selection)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpenPluginTypeExplorer(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleAddPluginStep(string pluginTypeName, ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartAddPluginStep(pluginTypeName, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportReport()
        {
            HandleExportReport(null);
        }

        public void HandleExportReport(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartOpenReportExplorer(connectionData, commonConfig, selection);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportRibbon()
        {
            HandleExportRibbon(null);
        }

        public void HandleExportRibbon(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartExportRibbonXml(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExplorerSitemap()
        {
            HandleExplorerSitemap(null, null);
        }

        public void HandleExplorerSitemap(string filter)
        {
            HandleExplorerSitemap(null, filter);
        }

        public void HandleExplorerSitemap(ConnectionData connectionData)
        {
            HandleExplorerSitemap(connectionData, null);
        }

        public void HandleExplorerSitemap(ConnectionData connectionData, string filter)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartExplorerSitemapXml(connectionData, commonConfig, filter);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenSolutionExplorerWindow(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpenSolutionExplorerWindow(null, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenImportJobExplorerWindow(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpenImportJobExplorerWindow(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenSolutionImageWindow()
        {
            HandleOpenSolutionImageWindow(null);
        }

        public void HandleOpenSolutionImageWindow(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartOpenSolutionImageWindow(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenSolutionDifferenceImageWindow()
        {
            HandleOpenSolutionDifferenceImageWindow(null);
        }

        public void HandleOpenSolutionDifferenceImageWindow(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartOpenSolutionDifferenceImageWindow(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenOrganizationDifferenceImageWindow()
        {
            HandleOpenOrganizationDifferenceImageWindow(null);
        }

        public void HandleOpenOrganizationDifferenceImageWindow(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpenOrganizationDifferenceImageWindow(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExplorerSystemForm()
        {
            string selection = GetSelectedText();

            HandleExplorerSystemForm(null, null, selection);
        }

        public void HandleExplorerSystemForm(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            HandleExplorerSystemForm(connectionData, null, selection);
        }

        public void HandleExplorerSystemForm(string selection)
        {
            HandleExplorerSystemForm(null, null, selection);
        }

        public void HandleExplorerSystemForm(ConnectionData connectionData, string selection)
        {
            HandleExplorerSystemForm(connectionData, null, selection);
        }

        public void HandleExplorerSystemForm(ConnectionData connectionData, SelectedItem selectedItem)
        {
            HandleExplorerSystemForm(connectionData, selectedItem, string.Empty);
        }

        private void HandleExplorerSystemForm(ConnectionData connectionData, SelectedItem selectedItem, string selection)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartExplorerSystemForm(connectionData, commonConfig, selection, selectedItem);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportCustomControl()
        {
            HandleExportCustomControl(null);
        }

        public void HandleExportCustomControl(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartExplorerCustomControl(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExplorerSystemSavedQuery()
        {
            string selection = GetSelectedText();

            HandleExplorerSystemSavedQuery(null, selection);
        }

        public void HandleExplorerSystemSavedQuery(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            HandleExplorerSystemSavedQuery(connectionData, selection);
        }

        public void HandleExplorerSystemSavedQuery(string selection)
        {
            HandleExplorerSystemSavedQuery(null, selection);
        }

        public void HandleExplorerSystemSavedQuery(ConnectionData connectionData, string selection)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartExplorerSystemSavedQueryXml(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportSystemSavedQueryVisualization()
        {
            HandleExportSystemSavedQueryVisualization(null);
        }

        public void HandleExportSystemSavedQueryVisualization(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                string selection = GetSelectedText();

                try
                {
                    Controller.StartExplorerSystemSavedQueryVisualization(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportWebResource()
        {
            HandleExportWebResource(null);
        }

        public void HandleExportWebResource(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();
            string selection = GetSelectedText();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartOpenWebResourceExplorer(connectionData, commonConfig, selection);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExplorerWorkflows()
        {
            string selection = GetSelectedText();

            HandleExplorerWorkflows(null, selection);
        }

        public void HandleExplorerWorkflows(string selection)
        {
            HandleExplorerWorkflows(null, selection);
        }

        public void HandleExplorerWorkflows(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            HandleExplorerWorkflows(connectionData, selection);
        }

        public void HandleExplorerWorkflows(ConnectionData connectionData, string selection)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    Controller.StartExplorerWorkflow(selection, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleAddingIntoPublishListFilesByTypeCommand(List<SelectedFile> selectedFiles, OpenFilesType openFilesType)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartAddingIntoPublishListFilesByType(selectedFiles, openFilesType, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCheckOpenFilesWithoutUTF8EncodingCommand(List<SelectedFile> selectedFiles)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(null);
                WriteToOutputEmptyLines(null, commonConfig);

                try
                {
                    Controller.StartOpenFilesWithouUTF8Encoding(selectedFiles);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandleCompareFilesWithoutUTF8EncodingCommand(List<SelectedFile> selectedFiles, bool withDetails)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null && selectedFiles.Count > 0)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartComparingFilesWithWrongEncoding(selectedFiles, connectionData, withDetails);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleCreateLaskLinkWebResourcesMultipleCommand(List<SelectedFile> selectedFiles)
        {
            if (!selectedFiles.Any())
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && commonConfig != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartCreatingLastLinkWebResourceMultiple(selectedFiles, connectionData);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportSolution()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            SelectedItem selectedItem = GetSelectedProjectItem();

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && selectedItem != null)
            {
                ActivateOutputWindow(connectionData);
                WriteToOutputEmptyLines(connectionData, commonConfig);

                try
                {
                    Controller.StartOpenSolutionExplorerWindow(selectedItem, connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleExportPluginConfigurationInfoFolder()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
            {
                return;
            }

            var selectedItem = GetSelectedProjectItem();

            var connectionData = crmConfig.CurrentConnectionData;

            if (connectionData != null && selectedItem != null)
            {
                var form = new WindowPluginConfiguration(commonConfig, connectionData, true);

                if (form.ShowDialog().GetValueOrDefault())
                {
                    connectionData = form.GetConnectionData();

                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartExportPluginConfigurationIntoFolder(selectedItem, connectionData, commonConfig);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleOpenCrmInWeb(ConnectionData connectionData, OpenCrmWebSiteType crmWebSiteType)
        {
            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                try
                {
                    connectionData.OpenCrmWebSite(crmWebSiteType);
                }
                catch (Exception ex)
                {
                    this.WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandlePublishAll(ConnectionData connectionData)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (connectionData == null)
            {
                if (!HasCurrentCrmConnection(out ConnectionConfiguration crmConfig))
                {
                    return;
                }

                connectionData = crmConfig.CurrentConnectionData;
            }

            if (connectionData != null)
            {
                CheckWishToChangeCurrentConnection(connectionData);

                string message = string.Format(Properties.MessageBoxStrings.PublishAllInConnectionFormat1, connectionData.Name);

                if (MessageBox.Show(message, Properties.MessageBoxStrings.QuestionTitle, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        this.Controller.StartPublishAll(connectionData);
                    }
                    catch (Exception ex)
                    {
                        this.WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleExportDefaultSitemap(string selectedSitemap)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (commonConfig != null)
            {
                string fileName = string.Format("SiteMap.{0}.xml", selectedSitemap);

                var dialog = new Microsoft.Win32.SaveFileDialog()
                {
                    DefaultExt = ".xml",

                    Filter = "SiteMap (.xml)|*.xml",
                    FilterIndex = 1,

                    RestoreDirectory = true,
                    FileName = fileName,

                    InitialDirectory = commonConfig.FolderForExport,
                };

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    commonConfig.Save();

                    ActivateOutputWindow(null);
                    WriteToOutputEmptyLines(null, commonConfig);

                    try
                    {
                        Uri uri = FileOperations.GetSiteMapResourceUri(selectedSitemap);
                        StreamResourceInfo info = Application.GetResourceStream(uri);

                        var doc = XDocument.Load(info.Stream);
                        info.Stream.Dispose();

                        var filePath = dialog.FileName;

                        doc.Save(filePath, SaveOptions.OmitDuplicateNamespaces);

                        this.WriteToOutput(null, string.Empty);
                        this.WriteToOutput(null, string.Empty);
                        this.WriteToOutput(null, string.Empty);

                        this.WriteToOutput(null, "{0} exported.", fileName);

                        this.WriteToOutput(null, string.Empty);

                        this.WriteToOutputFilePathUri(null, filePath);

                        PerformAction(null, filePath, true);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(null, ex);
                    }
                }
            }
        }

        public void HandleShowDifferenceWithDefaultSitemap(SelectedFile selectedFile, string selectedSitemap)
        {
            if (selectedFile == null || !File.Exists(selectedFile.FilePath))
            {
                return;
            }

            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (commonConfig == null)
            {
                return;
            }

            ActivateOutputWindow(null);
            WriteToOutputEmptyLines(null, commonConfig);

            try
            {
                Uri uri = FileOperations.GetSiteMapResourceUri(selectedSitemap);
                StreamResourceInfo info = Application.GetResourceStream(uri);

                var doc = XDocument.Load(info.Stream);
                info.Stream.Dispose();

                string fileName = string.Format("SiteMap.{0}.xml", selectedSitemap);

                var filePath = Path.Combine(FileOperations.GetTempFileFolder(), fileName);

                doc.Save(filePath, SaveOptions.OmitDuplicateNamespaces);

                this.WriteToOutput(null, string.Empty);
                this.WriteToOutput(null, string.Empty);
                this.WriteToOutput(null, string.Empty);

                this.WriteToOutput(null, "{0} exported.", fileName);

                this.WriteToOutput(null, string.Empty);

                this.WriteToOutputFilePathUri(null, filePath);

                this.ProcessStartProgramComparer(selectedFile.FilePath, filePath, selectedFile.FileName, fileName);
            }
            catch (Exception ex)
            {
                WriteErrorToOutput(null, ex);
            }
        }

        public void HandleExportXsdSchema(string[] fileNamesColl)
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (commonConfig != null)
            {
                var form = new WindowSelectFolderForExport(null, commonConfig.FolderForExport, commonConfig.DefaultFileAction);

                if (form.ShowDialog().GetValueOrDefault())
                {
                    commonConfig.FolderForExport = form.SelectedFolder;
                    commonConfig.DefaultFileAction = form.GetFileAction();

                    commonConfig.Save();

                    ActivateOutputWindow(null);
                    WriteToOutputEmptyLines(null, commonConfig);

                    try
                    {
                        foreach (var fileName in fileNamesColl)
                        {
                            Uri uri = FileOperations.GetSchemaResourceUri(fileName);
                            StreamResourceInfo info = Application.GetResourceStream(uri);

                            var doc = XDocument.Load(info.Stream);
                            info.Stream.Dispose();

                            var filePath = Path.Combine(commonConfig.FolderForExport, fileName);

                            doc.Save(filePath, SaveOptions.OmitDuplicateNamespaces);

                            this.WriteToOutput(null, string.Empty);
                            this.WriteToOutput(null, string.Empty);
                            this.WriteToOutput(null, string.Empty);

                            this.WriteToOutput(null, "{0} exported.", fileName);

                            this.WriteToOutput(null, string.Empty);

                            this.WriteToOutputFilePathUri(null, filePath);

                            PerformAction(null, filePath, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(null, ex);
                    }
                }
            }
        }

        public void HandleExportTraceEnableFile()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            if (commonConfig != null)
            {
                string fileName = "TraceEnable.reg";

                var dialog = new Microsoft.Win32.SaveFileDialog()
                {
                    DefaultExt = ".reg",

                    Filter = "Registry Edit (.reg)|*.reg",
                    FilterIndex = 1,

                    RestoreDirectory = true,
                    FileName = fileName,
                };

                if (!string.IsNullOrEmpty(commonConfig.FolderForExport))
                {
                    dialog.InitialDirectory = commonConfig.FolderForExport;
                }

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    ActivateOutputWindow(null);
                    WriteToOutputEmptyLines(null, commonConfig);

                    try
                    {
                        Uri uri = FileOperations.GetResourceUri(fileName);
                        StreamResourceInfo info = Application.GetResourceStream(uri);

                        var filePath = dialog.FileName;

                        byte[] buffer = new byte[16345];
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            int read;
                            while ((read = info.Stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, read);
                            }
                        }

                        this.WriteToOutput(null, string.Empty);
                        this.WriteToOutput(null, string.Empty);
                        this.WriteToOutput(null, string.Empty);

                        this.WriteToOutput(null, "{0} exported.", fileName);

                        this.WriteToOutput(null, string.Empty);

                        this.WriteToOutputFilePathUri(null, filePath);

                        SelectFileInFolder(null, filePath);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(null, ex);
                    }
                }
            }
        }

        public void HandleOpenXsdSchemaFolder()
        {
            var folder = FileOperations.GetSchemaXsdFolder();

            this.OpenFolder(folder);
        }

        public static string GetRelativePath(EnvDTE.Project project)
        {
            List<string> names = new List<string>();

            if (project != null)
            {
                AddNamesRecursive(names, project);
            }

            names.Reverse();

            return string.Join(@"\", names);
        }

        private static void AddNamesRecursive(List<string> names, EnvDTE.Project project)
        {
            if (project != null)
            {
                names.Add(project.Name);

                if (project.ParentProjectItem != null && project.ParentProjectItem.ContainingProject != null)
                {
                    AddNamesRecursive(names, project.ParentProjectItem.ContainingProject);
                }
            }
        }
    }
}