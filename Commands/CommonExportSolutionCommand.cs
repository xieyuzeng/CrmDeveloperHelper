﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands
{
    internal sealed class CommonExportSolutionCommand : AbstractCommand
    {
        private CommonExportSolutionCommand(Package package)
           : base(package, PackageGuids.guidCommandSet, PackageIds.CommonExportSolutionCommandId, ActionExecute, null) { }

        public static CommonExportSolutionCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new CommonExportSolutionCommand(package);
        }

        private static void ActionExecute(DTEHelper helper)
        {
            helper.HandleOpenExportSolutionWindow();
        }
    }
}
