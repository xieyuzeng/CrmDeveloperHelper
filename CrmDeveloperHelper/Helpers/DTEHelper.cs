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

        private void GetConnectionConfigAndExecute(ConnectionData connectionData, Action<ConnectionData, CommonConfiguration> action)
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
                    action(connectionData, commonConfig);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleFileCompareCommand(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool withDetails)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartComparing(conn, selectedFiles, withDetails));
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
                        Controller.StartUpdateContentAndPublish(connectionData, selectedFiles);
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
                        Controller.StartUpdateContentAndPublishEqualByText(connectionData, selectedFiles);
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
            if (selectedFiles.Count == 0)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartAddingWebResourcesToSolution(conn, commonConfig, solutionUniqueName, selectedFiles, withSelect));
        }

        public void HandleAddingPluginAssemblyToSolutionByProjectCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, params string[] projectNames)
        {
            if (projectNames == null || !projectNames.Any())
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartAddingPluginAssemblyToSolution(conn, commonConfig, solutionUniqueName, projectNames, withSelect));
        }

        public void HandleAddingPluginAssemblyProcessingStepsByProjectCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, params string[] projectNames)
        {
            if (projectNames == null || !projectNames.Any())
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartAddingPluginAssemblyProcessingStepsToSolution(conn, commonConfig, solutionUniqueName, projectNames, withSelect));
        }

        public void HandleAddingPluginTypeProcessingStepsByProjectCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, params string[] pluginTypeNames)
        {
            if (pluginTypeNames == null || !pluginTypeNames.Any())
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartAddingPluginTypeProcessingStepsToSolution(conn, commonConfig, solutionUniqueName, pluginTypeNames, withSelect));
        }

        public void HandleAddingReportsToSolutionCommand(ConnectionData connectionData, string solutionUniqueName, bool withSelect, List<SelectedFile> selectedFiles)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartAddingReportsToSolution(conn, commonConfig, solutionUniqueName, selectedFiles, withSelect));
        }

        public void HandleComparingPluginAssemblyAndLocalAssemblyCommand(ConnectionData connectionData, EnvDTE.Project project)
        {
            if (project == null || string.IsNullOrEmpty(project.Name))
            {
                return;
            }

            var defaultOutputFilePath = PropertiesHelper.GetOutputFilePath(project);

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartComparingPluginAssemblyAndLocalAssembly(conn, commonConfig, project.Name, defaultOutputFilePath));
        }

        public void HandleUpdatingPluginAssembliesInWindowCommand(ConnectionData connectionData, params EnvDTE.Project[] projectList)
        {
            HandleUpdatingPluginAssembliesInWindowCommand(connectionData, projectList.ToList());
        }

        public void HandleUpdatingPluginAssembliesInWindowCommand(ConnectionData connectionData, List<EnvDTE.Project> projectList)
        {
            if (projectList == null || !projectList.Any(p => !string.IsNullOrEmpty(p.Name)))
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingPluginAssembliesInWindow(conn, commonConfig, projectList));
        }

        public void HandleBuildProjectUpdatePluginAssemblyCommand(ConnectionData connectionData, bool registerPlugins, params EnvDTE.Project[] projectList)
        {
            HandleBuildProjectUpdatePluginAssemblyCommand(connectionData, registerPlugins, projectList.ToList());
        }

        public void HandleBuildProjectUpdatePluginAssemblyCommand(ConnectionData connectionData, bool registerPlugins, List<Project> projectList)
        {
            if (projectList == null || !projectList.Any(p => !string.IsNullOrEmpty(p.Name)))
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartBuildProjectUpdatePluginAssembly(conn, commonConfig, projectList, registerPlugins));
        }

        public void HandleRegisterPluginAssemblyCommand(ConnectionData connectionData, List<EnvDTE.Project> projectList)
        {
            if (projectList == null || !projectList.Any(p => !string.IsNullOrEmpty(p.Name)))
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartRegisterPluginAssembly(conn, commonConfig, projectList));
        }

        public void HandleExecutingFetchXml(ConnectionData connectionData, SelectedFile selectedFile, bool strictConnection)
        {
            if (selectedFile == null || !File.Exists(selectedFile.FilePath))
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => CrmDeveloperHelperPackage.Singleton?.ExecuteFetchXmlQueryAsync(selectedFile.FilePath, conn, this, strictConnection));
        }

        public void HandleUpdateEntityMetadataFileCSharpSchema(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingFileWithEntityMetadataCSharpSchema(conn, commonConfig, selectedFiles, selectEntity, openOptions));
        }

        public void HandleUpdateEntityMetadataFileCSharpProxyClassOrSchema(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingFileWithEntityMetadataCSharpProxyClassOrSchema(conn, commonConfig, selectedFiles, selectEntity, openOptions));
        }

        public void HandleUpdateEntityMetadataFileCSharpProxyClass(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingFileWithEntityMetadataCSharpProxyClass(conn, commonConfig, selectedFiles, selectEntity, openOptions));
        }

        public void HandleUpdateEntityMetadataFileJavaScript(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingFileWithEntityMetadataJavaScript(conn, commonConfig, selectedFiles, selectEntity, openOptions));
        }

        public void HandleUpdateGlobalOptionSetsFile(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool withSelect)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingFileWithGlobalOptionSets(conn, commonConfig, selectedFiles, withSelect, openOptions));
        }

        public void HandleUpdateGlobalOptionSetSingleFileJavaScript(ConnectionData connectionData, List<SelectedFile> selectedFiles, bool selectEntity)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingFileWithGlobalOptionSetSingleJavaScript(conn, commonConfig, selectedFiles, selectEntity, openOptions));
        }

        public void HandleUpdateGlobalOptionSetAllFileJavaScript(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            bool openOptions = (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != 0;

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartUpdatingFileWithGlobalOptionSetAllJavaScript(conn, commonConfig, selectedFile, openOptions));
        }

        public void HandleReportDifferenceCommand(ConnectionData connectionData, string fieldName, string fieldTitle, bool isCustom)
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartReportDifference(conn, commonConfig, selectedFiles[0], fieldName, fieldTitle, isCustom));
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
                    Controller.StartReportThreeFileDifference(connectionData1, connectionData2, commonConfig, selectedFiles[0], fieldName, fieldTitle, differenceType);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(null, ex);
                }
            }
        }

        public void HandleReportUpdateCommand(ConnectionData connectionData)
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartReportUpdate(conn, commonConfig, selectedFiles[0]));
        }

        public void HandleOpenReportExplorerCommand()
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsReportType, false).Take(2).ToList();

            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartOpenReportExplorer(conn, commonConfig, selectedFiles[0].FileName));
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
                    Controller.StartOpeningReport(connectionData, commonConfig, selectedFile, action);
                }
                catch (Exception ex)
                {
                    WriteErrorToOutput(connectionData, ex);
                }
            }
        }

        public void HandleOpenLastSelectedSolution(ConnectionData connectionData, string solutionUniqueName, ActionOpenComponent action)
        {
            if (string.IsNullOrEmpty(solutionUniqueName))
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpeningSolution(conn, commonConfig, solutionUniqueName, action));
        }

        public void HandleCreateLaskLinkReportCommand(List<SelectedFile> selectedFiles)
        {
            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartCreatingLastLinkReport(conn, selectedFiles[0]));
        }

        public void HandleFileClearLink(List<SelectedFile> selectedFiles)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartClearingLastLink(conn, selectedFiles));
        }

        public void HandleWebResourceDifferenceCommand(ConnectionData connectionData, bool isCustom)
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceTextType, false).Take(2).ToList();

            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceDifference(conn, commonConfig, selectedFiles[0], isCustom));
        }

        public void HandleWebResourceCreateEntityDescriptionCommand(ConnectionData connectionData)
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceTextType, false).Take(2).ToList();

            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceCreateEntityDescription(conn, commonConfig, selectedFiles[0]));
        }

        public void HandleWebResourceChangeInEntityEditorCommand(ConnectionData connectionData)
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceTextType, false).Take(2).ToList();

            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceChangeInEntityEditor(conn, commonConfig, selectedFiles[0]));
        }

        public void HandleWebResourceGetAttributeCommand(ConnectionData connectionData, string fieldName, string fieldTitle)
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceTextType, false).Take(2).ToList();

            if (selectedFiles.Count != 1)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceGetAttribute(conn, commonConfig, selectedFiles[0], fieldName, fieldTitle));
        }

        public void HandleRibbonDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartRibbonDifference(conn, commonConfig, selectedFile));
        }

        public void HandleRibbonDifferenceCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartRibbonDifference(conn, commonConfig, doc, filePath));
        }

        public void HandleSiteMapDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSiteMapDifference(conn, commonConfig, selectedFile));
        }

        public void HandleSiteMapDifferenceCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSiteMapDifference(conn, commonConfig, doc, filePath));
        }

        public void HandleSiteMapUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSiteMapUpdate(conn, commonConfig, selectedFile));
        }

        public void HandleSiteMapUpdateCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSiteMapUpdate(conn, commonConfig, doc, filePath));
        }

        public void HandleSiteMapOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSiteMapOpenInWeb(conn, commonConfig, selectedFile));
        }

        #region SystemForm

        public void HandleSystemFormDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSystemFormDifference(conn, commonConfig, selectedFile));
        }

        public void HandleSystemFormDifferenceCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSystemFormDifference(conn, commonConfig, doc, filePath));
        }

        public void HandleSystemFormUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSystemFormUpdate(conn, commonConfig, selectedFile));
        }

        public void HandleSystemFormUpdateCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSystemFormUpdate(conn, commonConfig, doc, filePath));
        }

        public void HandleSystemFormOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSystemFormOpenInWeb(conn, commonConfig, selectedFile));
        }

        #endregion SystemForm

        //StartConvertingFetchXmlToQueryExpression

        #region SavedQuery

        public void HandleSavedQueryDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSavedQueryDifference(conn, commonConfig, selectedFile));
        }

        public void HandleSavedQueryDifferenceCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSavedQueryDifference(conn, commonConfig, doc, filePath));
        }

        public void HandleSavedQueryUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSavedQueryUpdate(conn, commonConfig, selectedFile));
        }

        public void HandleSavedQueryUpdateCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSavedQueryUpdate(conn, commonConfig, doc, filePath));
        }

        public void HandleSavedQueryOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartSavedQueryOpenInWeb(conn, commonConfig, selectedFile));
        }

        #endregion SavedQuery

        #region Workflow

        public void HandleWorkflowDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWorkflowDifference(conn, commonConfig, selectedFile));
        }

        public void HandleWorkflowDifferenceCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWorkflowDifference(conn, commonConfig, doc, filePath));
        }

        public void HandleWorkflowUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWorkflowUpdate(conn, commonConfig, selectedFile));
        }

        public void HandleWorkflowUpdateCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWorkflowUpdate(conn, commonConfig, doc, filePath));
        }

        public void HandleWorkflowOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWorkflowOpenInWeb(conn, commonConfig, selectedFile));
        }

        #endregion Workflow

        #region RibbonDiff

        public void HandleRibbonDiffXmlDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartRibbonDiffXmlDifference(conn, commonConfig, selectedFile));
        }

        public void HandleRibbonDiffXmlDifferenceCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartRibbonDiffXmlDifference(conn, commonConfig, doc, filePath));
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
                        Controller.StartRibbonDiffXmlUpdate(connectionData, commonConfig, selectedFile);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        public void HandleRibbonDiffXmlUpdateCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
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

                string message = string.Format(Properties.MessageBoxStrings.PublishRibbonDiffXmlFormat2, Path.GetFileName(filePath), connectionData.GetDescription());

                var dialog = new WindowConfirmPublish(message, false);

                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    ActivateOutputWindow(connectionData);
                    WriteToOutputEmptyLines(connectionData, commonConfig);

                    CheckWishToChangeCurrentConnection(connectionData);

                    try
                    {
                        Controller.StartRibbonDiffXmlUpdate(connectionData, commonConfig, doc, filePath);
                    }
                    catch (Exception ex)
                    {
                        WriteErrorToOutput(connectionData, ex);
                    }
                }
            }
        }

        #endregion RibbonDiff

        #region Ribbon

        public void HandleEntityRibbonOpenInWeb(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartEntityRibbonOpenInWeb(conn, commonConfig, selectedFile));
        }

        public void HandleRibbonExplorerCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartRibbonExplorer(conn, commonConfig, selectedFile));
        }

        #endregion Ribbon

        #region WebResource

        public void HandleWebResourceDependencyXmlDifferenceCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceDependencyXmlDifference(conn, commonConfig, selectedFile));
        }

        public void HandleWebResourceDependencyXmlDifferenceCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceDependencyXmlDifference(conn, commonConfig, doc, filePath));
        }

        public void HandleWebResourceDependencyXmlUpdateCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceDependencyXmlUpdate(conn, commonConfig, selectedFile));
        }

        public void HandleWebResourceDependencyXmlUpdateCommand(ConnectionData connectionData, XDocument doc, string filePath)
        {
            if (doc == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceDependencyXmlUpdate(conn, commonConfig, doc, filePath));
        }

        public void HandleWebResourceDependencyXmlOpenInWebCommand(ConnectionData connectionData, SelectedFile selectedFile)
        {
            if (selectedFile == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartWebResourceDependencyXmlOpenInWeb(conn, commonConfig, selectedFile));
        }

        #endregion WebResource

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
                    Controller.StartWebResourceThreeFileDifference(connectionData1, connectionData2, commonConfig, selectedFiles[0], differenceType);
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
                    Controller.StartOpeningWebResource(connectionData, commonConfig, selectedFile, action);
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
                            Controller.ShowingWebResourcesDependentComponents(connectionData, commonConfig, selectedFiles);
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

                Controller.StartOpeningFiles(connectionData, commonConfig, selectedFiles, openFilesType, inTextEditor);
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
            if (selectedFiles.Count == 0)
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartWebResourceMultiDifferenceFiles(conn, commonConfig, selectedFiles, openFilesType));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => QuickConnection.TestConnectAsync(conn, this, null));
        }

        public void EditConnection(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => WindowHelper.OpenCrmConnectionCard(this, connectionData));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckEntitiesOwnership(conn, commonConfig));
        }

        public void HandleCheckGlobalOptionSetDuplicates(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckGlobalOptionSetDuplicates(conn, commonConfig));
        }

        public void HandleCheckComponentTypeEnum(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckComponentTypeEnum(conn, commonConfig));
        }

        public void HandleCheckUnknownFormControlTypes(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckUnknownFormControlTypes(conn, commonConfig));
        }

        public void HandleCreateAllDependencyNodesDescription(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCreateAllDependencyNodesDescription(conn, commonConfig));
        }

        public void HandleCheckManagedEntities(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckManagedEntities(conn, commonConfig));
        }

        public void HandleCheckPluginSteps(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckPluginSteps(conn, commonConfig));
        }

        public void HandleCheckPluginImages(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckPluginImages(conn, commonConfig));
        }

        public void HandleCheckPluginStepsRequiredComponents(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckPluginStepsRequiredComponents(conn, commonConfig));
        }

        public void HandleCheckPluginImagesRequiredComponents(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCheckPluginImagesRequiredComponents(conn, commonConfig));
        }

        public void HandleOpenPluginTree(string entityFilter, string pluginTypeFilter, string messageFilter)
        {
            HandleOpenPluginTree(null, entityFilter, pluginTypeFilter, messageFilter);
        }

        public void HandleOpenPluginTree(ConnectionData connectionData, string entityFilter, string pluginTypeFilter, string messageFilter)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartShowingPluginTree(conn, commonConfig, entityFilter, pluginTypeFilter, messageFilter));
        }

        public void HandleSdkMessageTree()
        {
            HandleSdkMessageTree(null);
        }

        public void HandleSdkMessageTree(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartShowingSdkMessageTree(conn, commonConfig, selection, null));
        }

        public void HandleOpenSystemUsersExplorer()
        {
            HandleOpenSystemUsersExplorer(null);
        }

        public void HandleOpenSystemUsersExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartShowingSystemUserExplorer(conn, commonConfig, selection));
        }

        public void HandleOpenTeamsExplorer()
        {
            HandleOpenTeamsExplorer(null);
        }

        public void HandleOpenTeamsExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartShowingTeamsExplorer(conn, commonConfig, selection));
        }

        public void HandleOpenSecurityRolesExplorer()
        {
            HandleOpenSecurityRolesExplorer(null);
        }

        public void HandleOpenSecurityRolesExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartShowingSecurityRolesExplorer(conn, commonConfig, selection));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartTraceReaderWindow(conn, commonConfig));
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
            string selection = string.Empty;

            if (selectedItem == null)
            {
                selection = GetSelectedText();
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpeningEntityMetadataExplorer(conn, commonConfig, selection, selectedItem));
        }

        public void HandleEntityMetadataFileGenerationOptions()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            ActivateOutputWindow(null);
            WriteToOutputEmptyLines(null, commonConfig);

            try
            {
                Controller.StartOpeningEntityMetadataFileGenerationOptions();
            }
            catch (Exception ex)
            {
                WriteErrorToOutput(null, ex);
            }
        }

        public void HandleGlobalOptionSetsMetadataFileGenerationOptions()
        {
            CommonConfiguration commonConfig = CommonConfiguration.Get();

            ActivateOutputWindow(null);
            WriteToOutputEmptyLines(null, commonConfig);

            try
            {
                Controller.StartOpeningGlobalOptionSetsMetadataFileGenerationOptions();
            }
            catch (Exception ex)
            {
                WriteErrorToOutput(null, ex);
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
            string selection = string.Empty;

            if (selectedItem == null)
            {
                selection = GetSelectedText();
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartShowingSdkMessageRequestTree(conn, commonConfig, selection, null, selectedItem));
        }

        public void HandleOpenEntityAttributeExplorer()
        {
            HandleOpenEntityAttributeExplorer(null);
        }

        public void HandleOpenEntityAttributeExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerEntityAttribute(conn, commonConfig, selection));
        }

        public void HandleOpenEntityKeyExplorer()
        {
            HandleOpenEntityKeyExplorer(null);
        }

        public void HandleOpenEntityKeyExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerEntityKey(conn, commonConfig, selection));
        }

        public void HandleOpenEntityRelationshipOneToManyExplorer()
        {
            HandleOpenEntityRelationshipOneToManyExplorer(null);
        }

        public void HandleOpenEntityRelationshipOneToManyExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerEntityRelationshipOneToMany(conn, commonConfig, selection));
        }

        public void HandleOpenEntityRelationshipManyToManyExplorer()
        {
            HandleOpenEntityRelationshipManyToManyExplorer(null);
        }

        public void HandleOpenEntityRelationshipManyToManyExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerEntityRelationshipManyToMany(conn, commonConfig, selection));
        }

        public void HandleOpenEntityPrivilegesExplorer()
        {
            HandleOpenEntityPrivilegesExplorer(null);
        }

        public void HandleOpenEntityPrivilegesExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerEntityPrivileges(conn, commonConfig, selection));
        }

        public void HandleOpenOtherPrivilegesExplorer()
        {
            HandleOpenOtherPrivilegesExplorer(null);
        }

        public void HandleOpenOtherPrivilegesExplorer(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerOtherPrivileges(conn, commonConfig, selection));
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
            string selection = string.Empty;

            if (selectedItem == null)
            {
                selection = GetSelectedText();
            }

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartCreatingFileWithGlobalOptionSets(conn, commonConfig, selection, selectedItem));
        }

        public void HandleExportOrganizationInformation()
        {
            HandleExportOrganizationInformation(null);
        }

        public void HandleExportOrganizationInformation(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerOrganizationInformation(conn, commonConfig));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenPluginAssemblyExplorer(conn, commonConfig, selection));
        }

        public void HandleOpenPluginTypeExplorer(string selection)
        {
            HandleOpenPluginTypeExplorer(null, selection);
        }

        public void HandleOpenPluginTypeExplorer(ConnectionData connectionData, string selection)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenPluginTypeExplorer(conn, commonConfig, selection));
        }

        public void HandleAddPluginStep(string pluginTypeName, ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartAddPluginStep(conn, commonConfig, pluginTypeName));
        }

        public void HandleExportReport()
        {
            HandleExportReport(null);
        }

        public void HandleExportReport(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenReportExplorer(conn, commonConfig, selection));
        }

        public void HandleExportRibbon()
        {
            HandleExportRibbon(null);
        }

        public void HandleExportRibbon(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExportRibbonXml(conn, commonConfig, selection));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerSitemapXml(conn, commonConfig, filter));
        }

        public void HandleOpenSolutionExplorerWindow(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenSolutionExplorerWindow(conn, commonConfig, null));
        }

        public void HandleOpenImportJobExplorerWindow(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenImportJobExplorerWindow(conn, commonConfig));
        }

        public void HandleOpenSolutionImageWindow()
        {
            HandleOpenSolutionImageWindow(null);
        }

        public void HandleOpenSolutionImageWindow(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenSolutionImageWindow(conn, commonConfig));
        }

        public void HandleOpenSolutionDifferenceImageWindow()
        {
            HandleOpenSolutionDifferenceImageWindow(null);
        }

        public void HandleOpenSolutionDifferenceImageWindow(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenSolutionDifferenceImageWindow(conn, commonConfig));
        }

        public void HandleOpenOrganizationDifferenceImageWindow()
        {
            HandleOpenOrganizationDifferenceImageWindow(null);
        }

        public void HandleOpenOrganizationDifferenceImageWindow(ConnectionData connectionData)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenOrganizationDifferenceImageWindow(conn, commonConfig));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerSystemForm(conn, commonConfig, selection, selectedItem));
        }

        public void HandleExportCustomControl()
        {
            HandleExportCustomControl(null);
        }

        public void HandleExportCustomControl(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerCustomControl(conn, commonConfig, selection));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerSystemSavedQueryXml(conn, commonConfig, selection));
        }

        public void HandleExportSystemSavedQueryVisualization()
        {
            HandleExportSystemSavedQueryVisualization(null);
        }

        public void HandleExportSystemSavedQueryVisualization(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerSystemSavedQueryVisualization(conn, commonConfig, selection));
        }

        public void HandleExportWebResource()
        {
            HandleExportWebResource(null);
        }

        public void HandleExportWebResource(ConnectionData connectionData)
        {
            string selection = GetSelectedText();

            HandleOpenWebResourceExplorerCommand(connectionData, selection);
        }

        public void HandleOpenWebResourceExplorerCommand()
        {
            List<SelectedFile> selectedFiles = GetSelectedFilesAll(FileOperations.SupportsWebResourceType, false).Take(2).ToList();

            if (selectedFiles.Count == 1)
            {
                SelectedFile selectedFile = selectedFiles[0];

                HandleOpenWebResourceExplorerCommand(null, selectedFile.FileName);
            }
        }

        public void HandleOpenWebResourceExplorerCommand(string selection)
        {
            HandleOpenWebResourceExplorerCommand(null, selection);
        }

        private void HandleOpenWebResourceExplorerCommand(ConnectionData connectionData, string selection)
        {
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartOpenWebResourceExplorer(conn, commonConfig, selection));
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
            GetConnectionConfigAndExecute(connectionData, (conn, commonConfig) => Controller.StartExplorerWorkflow(conn, commonConfig, selection));
        }

        public void HandleAddingIntoPublishListFilesByTypeCommand(List<SelectedFile> selectedFiles, OpenFilesType openFilesType)
        {
            if (selectedFiles.Count == 0)
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartAddingIntoPublishListFilesByType(conn, commonConfig, selectedFiles, openFilesType));
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
            if (selectedFiles.Count == 0)
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartComparingFilesWithWrongEncoding(conn, selectedFiles, withDetails));
        }

        public void HandleCreateLaskLinkWebResourcesMultipleCommand(List<SelectedFile> selectedFiles)
        {
            if (!selectedFiles.Any())
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartCreatingLastLinkWebResourceMultiple(conn, selectedFiles));
        }

        public void HandleExportSolution()
        {
            SelectedItem selectedItem = GetSelectedProjectItem();

            if (selectedItem == null)
            {
                return;
            }

            GetConnectionConfigAndExecute(null, (conn, commonConfig) => Controller.StartOpenSolutionExplorerWindow(conn, commonConfig, selectedItem));
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
                        Controller.StartExportPluginConfigurationIntoFolder(connectionData, commonConfig, selectedItem);
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

                this.ProcessStartProgramComparerAsync(selectedFile.FilePath, filePath, selectedFile.FileName, fileName);
            }
            catch (Exception ex)
            {
                WriteErrorToOutput(null, ex);
            }
        }

        public void HandleXsdSchemaExport(string[] fileNamesColl)
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

        public void HandleXsdSchemaOpenFolder()
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