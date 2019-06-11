﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Explorers
{
    internal sealed class CommonPluginTypeExplorerCommand : AbstractCommand
    {
        private CommonPluginTypeExplorerCommand(OleMenuCommandService commandService)
           : base(commandService, PackageIds.CommonPluginTypeExplorerCommandId) { }

        public static CommonPluginTypeExplorerCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CommonPluginTypeExplorerCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            string selection = helper.GetSelectedText();

            helper.HandleOpenPluginTypeExplorer(selection);
        }
    }
}
