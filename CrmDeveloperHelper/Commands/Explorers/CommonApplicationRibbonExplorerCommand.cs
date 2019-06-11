﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Explorers
{
    internal sealed class CommonApplicationRibbonExplorerCommand : AbstractCommand
    {
        private CommonApplicationRibbonExplorerCommand(OleMenuCommandService commandService)
           : base(commandService, PackageIds.CommonApplicationRibbonExplorerCommandId) { }

        public static CommonApplicationRibbonExplorerCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CommonApplicationRibbonExplorerCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            helper.HandleExportRibbon();
        }
    }
}
