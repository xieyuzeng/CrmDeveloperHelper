﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using Nav.Common.VSPackages.CrmDeveloperHelper.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Controllers
{
    public class PluginTypeDescriptionController
    {
        private readonly IWriteToOutput _iWriteToOutput = null;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="iWriteToOutput"></param>
        public PluginTypeDescriptionController(IWriteToOutput iWriteToOutput)
        {
            this._iWriteToOutput = iWriteToOutput;
        }

        #region Описание шагов плагинов для типа.

        public async Task ExecuteCreatingPluginTypeDescription(ConnectionData connectionData, CommonConfiguration commonConfig, string selection)
        {
            string operation = string.Format(Properties.OperationNames.CreatingPluginTypeDescriptionFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await CreatingPluginTypeDescription(connectionData, commonConfig, selection);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task CreatingPluginTypeDescription(ConnectionData connectionData, CommonConfiguration commonConfig, string selection)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            WindowHelper.OpenPluginTypeWindow(
                this._iWriteToOutput
                , service
                , commonConfig
                , selection
                );
        }

        #endregion Описание шагов плагинов для типа.

        #region Описание шагов плагинов сборки.

        public async Task ExecuteExportingPluginAssembly(ConnectionData connectionData, CommonConfiguration commonConfig, string selection)
        {
            string operation = string.Format(Properties.OperationNames.ExportingPluginAssemblyFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await ExportingPluginAssembly(connectionData, commonConfig, selection);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task ExportingPluginAssembly(ConnectionData connectionData, CommonConfiguration commonConfig, string selection)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            WindowHelper.OpenPluginAssemblyWindow(
                this._iWriteToOutput
                , service
                , commonConfig
                , selection
                );
        }

        #endregion Описание шагов плагинов сборки.

        #region Сравнение сборки плагинов и локальной сборки.

        public async Task ExecuteComparingAssemblyAndCrmSolution(ConnectionData connectionData, CommonConfiguration commonConfig, string projectName, string defaultFolder)
        {
            string operation = string.Format(Properties.OperationNames.ComparingCrmPluginAssemblyAndLocalAssemblyFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await ComparingAssemblyAndCrmSolution(connectionData, commonConfig, projectName, defaultFolder);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task ComparingAssemblyAndCrmSolution(ConnectionData connectionData, CommonConfiguration commonConfig, string projectName, string defaultFolder)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            if (string.IsNullOrEmpty(projectName))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, "Project Name is empty.");
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repositoryAssembly = new PluginAssemblyRepository(service);

            var assembly = await repositoryAssembly.FindAssemblyAsync(projectName);

            if (assembly == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, "PluginAssembly not founded by name {0}.", projectName);

                WindowHelper.OpenPluginAssemblyWindow(
                    this._iWriteToOutput
                    , service
                    , commonConfig
                    , projectName
                    );

                return;
            }

            string filePath = await CreateFileWithAssemblyComparing(commonConfig.FolderForExport, connectionData, service, assembly.Id, assembly.Name, defaultFolder);

            this._iWriteToOutput.PerformAction(service.ConnectionData, filePath);
        }

        public async Task<string> CreateFileWithAssemblyComparing(string folder, ConnectionData connectionData, IOrganizationServiceExtented service, Guid idPluginAssembly, string assemblyName, string defaultFolder)
        {
            var repositoryType = new PluginTypeRepository(service);

            var task = repositoryType.GetPluginTypesAsync(idPluginAssembly);

            string assemblyPath = connectionData.GetLastAssemblyPath(assemblyName);
            bool showDialog = false;

            var t = new Thread(() =>
            {
                try
                {
                    var openFileDialog1 = new Microsoft.Win32.OpenFileDialog
                    {
                        Filter = "Plugin Assebmly (.dll)|*.dll",
                        FilterIndex = 1,
                        RestoreDirectory = true
                    };

                    if (!string.IsNullOrEmpty(assemblyPath))
                    {
                        openFileDialog1.InitialDirectory = Path.GetDirectoryName(assemblyPath);
                        openFileDialog1.FileName = Path.GetFileName(assemblyPath);
                    }
                    else if (!string.IsNullOrEmpty(defaultFolder))
                    {
                        openFileDialog1.InitialDirectory = defaultFolder;
                    }

                    if (openFileDialog1.ShowDialog() == true)
                    {
                        showDialog = true;
                        assemblyPath = openFileDialog1.FileName;
                    }
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(connectionData, ex);
                }
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            t.Join();

            if (!showDialog)
            {
                return string.Empty;
            }

            string filePath = string.Empty;

            connectionData.AddAssemblyMapping(assemblyName, assemblyPath);
            connectionData.Save();

            HashSet<string> assemblyPlugins = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            HashSet<string> assemblyWorkflow = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            AppDomain childDomain = CreateChildDomain();

            try
            {
                childDomain.SetData(_assemblyKey, assemblyPath);
                childDomain.DoCallBack(new CrossAppDomainDelegate(CheckAssembly));

                var check = childDomain.GetData(_resultKey);

                if (check is Tuple<HashSet<string>, HashSet<string>> paths)
                {
                    assemblyPlugins = paths.Item1;
                    assemblyWorkflow = paths.Item2;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                AppDomain.Unload(childDomain);
            }

            var pluginTypes = await task;

            var crmPlugins = new HashSet<string>(pluginTypes.Where(e => !e.IsWorkflowActivity.GetValueOrDefault()).Select(e => e.TypeName), StringComparer.InvariantCultureIgnoreCase);
            var crmWorkflows = new HashSet<string>(pluginTypes.Where(e => e.IsWorkflowActivity.GetValueOrDefault()).Select(e => e.TypeName), StringComparer.InvariantCultureIgnoreCase);

            var content = new StringBuilder();

            content.AppendLine(connectionData.GetConnectionInfo()).AppendLine();
            content.AppendFormat("Description for PluginAssembly '{0}' at {1}", assemblyName, DateTime.Now.ToString("G", System.Globalization.CultureInfo.CurrentCulture)).AppendLine();

            content.AppendFormat("Local Assembly '{0}'", assemblyPath).AppendLine();

            var pluginsInCrm = crmPlugins.Except(assemblyPlugins, StringComparer.InvariantCultureIgnoreCase);
            var workflowInCrm = crmWorkflows.Except(assemblyWorkflow, StringComparer.InvariantCultureIgnoreCase);

            var pluginsInLocalAssembly = assemblyPlugins.Except(crmPlugins, StringComparer.InvariantCultureIgnoreCase);
            var workflowInLocalAssembly = assemblyWorkflow.Except(crmWorkflows, StringComparer.InvariantCultureIgnoreCase);

            const string tabspacer = "      ";

            FillInformation(content, "Plugins ONLY in Crm Assembly {0}", pluginsInCrm, tabspacer);
            FillInformation(content, "Workflows ONLY in Crm Assembly {0}", workflowInCrm, tabspacer);
            FillInformation(content, "Plugins ONLY in Local Assembly {0}", pluginsInLocalAssembly, tabspacer);
            FillInformation(content, "Workflows ONLY in Local Assembly {0}", workflowInLocalAssembly, tabspacer);

            if (!pluginsInCrm.Any()
                && !workflowInCrm.Any()
                && !pluginsInLocalAssembly.Any()
                && !workflowInLocalAssembly.Any()
                )
            {
                content.AppendLine().AppendLine().AppendLine();

                content.AppendLine("No difference in Assemblies.");
            }

            FillInformation(content, "Plugins in Crm Assembly {0}", crmPlugins, tabspacer);
            FillInformation(content, "Workflows in Crm Assembly {0}", crmWorkflows, tabspacer);
            FillInformation(content, "Plugins in Local Assembly {0}", assemblyPlugins, tabspacer);
            FillInformation(content, "Workflows in Local Assembly {0}", assemblyWorkflow, tabspacer);

            string fileName = EntityFileNameFormatter.GetPluginAssemblyFileName(connectionData.Name, assemblyName, "Comparing", "txt");
            filePath = Path.Combine(folder, FileOperations.RemoveWrongSymbols(fileName));

            File.WriteAllText(filePath, content.ToString(), new UTF8Encoding(false));

            this._iWriteToOutput.WriteToOutput(connectionData, "Assembly {0} Comparing exported to {1}", assemblyName, filePath);
            this._iWriteToOutput.WriteToOutputFilePathUri(connectionData, filePath);

            return filePath;
        }

        private static AppDomain CreateChildDomain()
        {
            string path = typeof(PluginsAndWorkflowLoader).Assembly.Location;

            string directory = Path.GetDirectoryName(path);

            var setup = new AppDomainSetup
            {
                ApplicationBase = directory,
                CachePath = directory,
                LoaderOptimization = LoaderOptimization.MultiDomain,
            };

            AppDomain childDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), AppDomain.CurrentDomain.Evidence.Clone(), setup);

            return childDomain;
        }

        private const string _assemblyKey = "Assembly";
        private const string _resultKey = "Result";

        public static void CheckAssembly()
        {
            try
            {
                var path = (string)AppDomain.CurrentDomain.GetData(_assemblyKey);

                var temp = new PluginsAndWorkflowLoader();
                var result = temp.LoadAssembly(path);

                AppDomain.CurrentDomain.SetData(_resultKey, result);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private static void FillInformation(StringBuilder content, string format, IEnumerable<string> collection, string tabspacer)
        {
            if (collection.Any())
            {
                content.AppendLine().AppendLine().AppendLine();
                content.AppendLine(new string('-', 150));
                content.AppendLine().AppendLine().AppendLine();

                content.AppendFormat(format, collection.Count()).AppendLine();
                foreach (var item in collection.OrderBy(s => s))
                {
                    content.AppendLine(tabspacer + item);
                }
            }
        }

        #endregion Сравнение сборки плагинов и локальной сборки.

        #region Добавление шага плагина.

        public async Task ExecuteAddingPluginStepForType(ConnectionData connectionData, CommonConfiguration commonConfig, string pluginTypeName)
        {
            string operation = string.Format(Properties.OperationNames.AddingPluginStepFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await AddingPluginStepForType(connectionData, commonConfig, pluginTypeName);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task AddingPluginStepForType(ConnectionData connectionData, CommonConfiguration commonConfig, string pluginTypeName)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            if (string.IsNullOrEmpty(pluginTypeName))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, "PluginType Name is empty.");
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repository = new PluginTypeRepository(service);

            var pluginType = await repository.FindPluginTypeAsync(pluginTypeName);

            if (pluginType == null)
            {
                pluginType = await repository.FindPluginTypeByLikeNameAsync(pluginTypeName);
            }

            if (pluginType == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, "PluginType not founded by name {0}.", pluginTypeName);

                WindowHelper.OpenPluginTypeWindow(
                    this._iWriteToOutput
                    , service
                    , commonConfig
                    , pluginTypeName
                );

                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.WindowStatusStrings.GettingMessages);

            var repositoryFilters = new SdkMessageFilterRepository(service);

            List<SdkMessageFilter> filters = await repositoryFilters.GetAllAsync(new ColumnSet(SdkMessageFilter.Schema.Attributes.sdkmessageid, SdkMessageFilter.Schema.Attributes.primaryobjecttypecode, SdkMessageFilter.Schema.Attributes.secondaryobjecttypecode, SdkMessageFilter.Schema.Attributes.availability));

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.WindowStatusStrings.GettingMessagesCompleted);

            var step = new SdkMessageProcessingStep()
            {
                EventHandler = pluginType.ToEntityReference(),
            };

            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowSdkMessageProcessingStep(_iWriteToOutput, service, filters, step);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        #endregion Добавление шага плагина.
    }
}