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
    internal sealed class CodeCSharpProjectPluginTypeStepsAddIntoSolutionLastCommand : IServiceProviderOwner
    {
        private readonly Package _package;

        public IServiceProvider ServiceProvider => this._package;

        private const int _baseIdStart = PackageIds.CodeCSharpProjectPluginTypeStepsAddIntoSolutionLastCommandId;

        private CodeCSharpProjectPluginTypeStepsAddIntoSolutionLastCommand(Package package)
        {
            this._package = package ?? throw new ArgumentNullException(nameof(package));

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                for (int i = 0; i < ConnectionData.CountLastSolutions; i++)
                {
                    var menuCommandID = new CommandID(PackageGuids.guidCommandSet, _baseIdStart + i);

                    var menuCommand = new OleMenuCommand(this.menuItemCallback, menuCommandID);

                    menuCommand.Enabled = menuCommand.Visible = false;

                    menuCommand.BeforeQueryStatus += menuItem_BeforeQueryStatus;

                    commandService.AddCommand(menuCommand);
                }
            }
        }

        public static CodeCSharpProjectPluginTypeStepsAddIntoSolutionLastCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new CodeCSharpProjectPluginTypeStepsAddIntoSolutionLastCommand(package);
        }

        private void menuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            if (sender is OleMenuCommand menuCommand)
            {
                menuCommand.Enabled = menuCommand.Visible = false;

                var connectionConfig = ConnectionConfiguration.Get();

                if (connectionConfig.CurrentConnectionData != null)
                {
                    var index = menuCommand.CommandID.ID - _baseIdStart;

                    if (0 <= index && index < connectionConfig.CurrentConnectionData.LastSelectedSolutionsUniqueName.Count)
                    {
                        menuCommand.Text = connectionConfig.CurrentConnectionData.LastSelectedSolutionsUniqueName.ElementAt(index);

                        menuCommand.Enabled = menuCommand.Visible = true;

                        CommonHandlers.ActionBeforeQueryStatusActiveDocumentCSharp(this, menuCommand);

                        CommonHandlers.ActionBeforeQueryStatusActiveDocumentContainingProject(this, menuCommand, FileOperations.SupportsCSharpType);
                    }
                }
            }
        }

        private void menuItemCallback(object sender, EventArgs e)
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

            var connectionConfig = ConnectionConfiguration.Get();

            if (connectionConfig.CurrentConnectionData != null)
            {
                var index = menuCommand.CommandID.ID - _baseIdStart;

                if (0 <= index && index < connectionConfig.CurrentConnectionData.LastSelectedSolutionsUniqueName.Count)
                {
                    string solutionUniqueName = connectionConfig.CurrentConnectionData.LastSelectedSolutionsUniqueName.ElementAt(index);

                    var helper = DTEHelper.Create(applicationObject);

                    List<SelectedFile> selectedFiles = helper.GetOpenedFileInCodeWindow(FileOperations.SupportsCSharpType);

                    var file = selectedFiles.FirstOrDefault();

                    if (file != null)
                    {
                        string selection = file.Name.Split('.').FirstOrDefault();

                        helper.HandleAddingPluginTypeProcessingStepsByProjectCommand(selection, false, solutionUniqueName);
                    }
                }
            }
        }
    }
}