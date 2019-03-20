﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands
{
    internal class DocumentsCSharpProjectPluginTypeStepsAddIntoSolutionInConnectionCommand : IServiceProviderOwner
    {
        private readonly Package _package;

        public IServiceProvider ServiceProvider => this._package;

        private const int _baseIdStart = PackageIds.DocumentsCSharpProjectPluginTypeStepsAddIntoSolutionInConnectionCommandId;

        private DocumentsCSharpProjectPluginTypeStepsAddIntoSolutionInConnectionCommand(Package package)
        {
            this._package = package ?? throw new ArgumentNullException(nameof(package));

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                for (int i = 0; i < ConnectionData.CountConnectionToQuickList; i++)
                {
                    var menuCommandID = new CommandID(PackageGuids.guidDynamicCommandSet, _baseIdStart + i);

                    var menuCommand = new OleMenuCommand(this.menuItemCallback, menuCommandID);

                    menuCommand.Enabled = menuCommand.Visible = false;

                    menuCommand.BeforeQueryStatus += menuItem_BeforeQueryStatus;

                    commandService.AddCommand(menuCommand);
                }
            }
        }

        public static DocumentsCSharpProjectPluginTypeStepsAddIntoSolutionInConnectionCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new DocumentsCSharpProjectPluginTypeStepsAddIntoSolutionInConnectionCommand(package);
        }

        private void menuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            try
            {
                if (sender is OleMenuCommand menuCommand)
                {
                    menuCommand.Enabled = menuCommand.Visible = false;

                    var index = menuCommand.CommandID.ID - _baseIdStart;

                    var connectionConfig = ConnectionConfiguration.Get();

                    var list = connectionConfig.GetConnectionsWithoutCurrent();

                    if (0 <= index && index < list.Count)
                    {
                        var connectionData = list[index];

                        menuCommand.Text = connectionData.Name;

                        menuCommand.Enabled = menuCommand.Visible = true;

                        CommonHandlers.ActionBeforeQueryStatusOpenedDocumentsCSharp(this, menuCommand);
                    }
                }
            }
            catch (Exception ex)
            {
                DTEHelper.WriteExceptionToOutput(null, ex);
            }
        }

        private async void menuItemCallback(object sender, EventArgs e)
        {
            try
            {
                OleMenuCommand menuCommand = sender as OleMenuCommand;
                if (menuCommand == null)
                {
                    return;
                }

                var applicationObject = this.ServiceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;
                if (applicationObject == null)
                {
                    return;
                }

                var index = menuCommand.CommandID.ID - _baseIdStart;

                var connectionConfig = Model.ConnectionConfiguration.Get();

                var list = connectionConfig.GetConnectionsWithoutCurrent();

                if (0 <= index && index < list.Count)
                {
                    var connectionData = list[index];

                    var helper = DTEHelper.Create(applicationObject);

                    var listFiles = helper.GetOpenedDocumentsAsDocument(FileOperations.SupportsCSharpType);

                    var pluginTypeNames = new List<string>();

                    helper.ActivateOutputWindow(null);

                    foreach (var item in listFiles)
                    {
                        helper.WriteToOutput(null, Properties.OutputStrings.GettingClassFullNameFromFileFormat1, item?.FullName);
                        var typeName = await PropertiesHelper.GetTypeFullNameAsync(item);

                        if (!string.IsNullOrEmpty(typeName))
                        {
                            pluginTypeNames.Add(typeName);
                        }
                    }

                    if (pluginTypeNames.Any())
                    {
                        helper.HandleAddingPluginAssemblyIntoSolutionByProjectCommand(connectionData, null, true, pluginTypeNames.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                DTEHelper.WriteExceptionToOutput(null, ex);
            }
        }
    }
}
