﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.OutputWindows.FindEdit
{
    internal sealed class OutputFindEntityObjectsByPrefixCommand : AbstractOutputWindowCommand
    {
        private OutputFindEntityObjectsByPrefixCommand(OleMenuCommandService commandService)
           : base(commandService, PackageIds.guidCommandSet.OutputFindEntityObjectsByPrefixCommandId) { }

        public static OutputFindEntityObjectsByPrefixCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new OutputFindEntityObjectsByPrefixCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper, ConnectionData connectionData)
        {
            helper.HandleFindEntityObjectsByPrefix(connectionData);
        }
    }
}
