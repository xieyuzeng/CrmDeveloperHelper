﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Explorers
{
    internal sealed class CommonGlobalOptionSetsExplorerCommand : AbstractCommand
    {
        private CommonGlobalOptionSetsExplorerCommand(OleMenuCommandService commandService)
           : base(commandService, PackageIds.CommonGlobalOptionSetsExplorerCommandId) { }

        public static CommonGlobalOptionSetsExplorerCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CommonGlobalOptionSetsExplorerCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            helper.HandleExportGlobalOptionSets();
        }
    }
}
