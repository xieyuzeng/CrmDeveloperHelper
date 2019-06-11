﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Explorers
{
    internal sealed class CommonSystemUsersExplorerCommand : AbstractCommand
    {
        private CommonSystemUsersExplorerCommand(OleMenuCommandService commandService)
           : base(commandService, PackageIds.CommonSystemUsersExplorerCommandId) { }

        public static CommonSystemUsersExplorerCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CommonSystemUsersExplorerCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            helper.HandleOpenSystemUsersExplorer();
        }
    }
}
