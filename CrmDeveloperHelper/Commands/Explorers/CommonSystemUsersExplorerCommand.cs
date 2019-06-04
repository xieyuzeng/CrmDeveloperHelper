﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Explorers
{
    internal sealed class CommonSystemUsersExplorerCommand : AbstractCommand
    {
        private CommonSystemUsersExplorerCommand(Package package)
           : base(package, PackageGuids.guidCommandSet, PackageIds.CommonSystemUsersExplorerCommandId, ActionExecute, null) { }

        public static CommonSystemUsersExplorerCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new CommonSystemUsersExplorerCommand(package);
        }

        private static void ActionExecute(DTEHelper helper)
        {
            helper.HandleOpenSystemUsersExplorer();
        }
    }
}
