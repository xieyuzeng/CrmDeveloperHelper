﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using System;
using System.ComponentModel.Design;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands
{
    internal sealed class FileWebResourceShowDifferenceInConnectionGroupCommand : IServiceProviderOwner
    {
        private readonly Package _package;

        public IServiceProvider ServiceProvider => this._package;

        private const int _baseIdStart = PackageIds.FileWebResourceShowDifferenceInConnectionGroupCommandId;

        private FileWebResourceShowDifferenceInConnectionGroupCommand(Package package)
        {
            this._package = package ?? throw new ArgumentNullException(nameof(package));

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                for (int i = 0; i < Model.ConnectionData.CountConnectionToQuickList; i++)
                {
                    var menuCommandID = new CommandID(PackageGuids.guidCommandSet, _baseIdStart + i);

                    var menuCommand = new OleMenuCommand(this.menuItemCallback, menuCommandID);

                    menuCommand.Enabled = menuCommand.Visible = false;

                    menuCommand.BeforeQueryStatus += menuItem_BeforeQueryStatus;

                    commandService.AddCommand(menuCommand);
                }
            }
        }

        public static FileWebResourceShowDifferenceInConnectionGroupCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new FileWebResourceShowDifferenceInConnectionGroupCommand(package);
        }

        private void menuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            if (sender is OleMenuCommand menuCommand)
            {
                menuCommand.Enabled = menuCommand.Visible = false;

                var index = menuCommand.CommandID.ID - _baseIdStart;

                var connectionConfig = Model.ConnectionConfiguration.Get();

                var list = connectionConfig.GetConnectionsByGroupWithoutCurrent();

                if (0 <= index && index < list.Count)
                {
                    var connectionData = list[index];

                    menuCommand.Text = connectionData.Name;

                    menuCommand.Enabled = menuCommand.Visible = true;

                    CommonHandlers.ActionBeforeQueryStatusSolutionExplorerWebResourceTextSingle(this, menuCommand);
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

            var index = menuCommand.CommandID.ID - _baseIdStart;

            var connectionConfig = Model.ConnectionConfiguration.Get();

            var list = connectionConfig.GetConnectionsByGroupWithoutCurrent();

            if (0 <= index && index < list.Count)
            {
                var connectionData = list[index];

                var helper = DTEHelper.Create(applicationObject);

                helper.HandleWebResourceDifferenceCommand(connectionData, false);
            }
        }
    }
}