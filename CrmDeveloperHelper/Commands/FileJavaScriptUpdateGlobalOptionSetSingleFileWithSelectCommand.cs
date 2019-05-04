﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands
{
    internal sealed class FileJavaScriptUpdateGlobalOptionSetSingleFileWithSelectCommand : AbstractCommand
    {
        private FileJavaScriptUpdateGlobalOptionSetSingleFileWithSelectCommand(Package package)
            : base(package, PackageGuids.guidCommandSet, PackageIds.FileJavaScriptUpdateGlobalOptionSetSingleFileWithSelectCommandId, ActionExecute, ActionBeforeQueryStatus) { }

        public static FileJavaScriptUpdateGlobalOptionSetSingleFileWithSelectCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new FileJavaScriptUpdateGlobalOptionSetSingleFileWithSelectCommand(package);
        }

        private static void ActionExecute(DTEHelper helper)
        {
            List<SelectedFile> selectedFiles = helper.GetSelectedFilesInSolutionExplorer(FileOperations.SupportsJavaScriptType, false).ToList();

            helper.HandleUpdateGlobalOptionSetSingleFileJavaScript(null, selectedFiles, true);
        }

        private static void ActionBeforeQueryStatus(IServiceProviderOwner command, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusSolutionExplorerJavaScriptSingle(command, menuCommand);

            CommonHandlers.CorrectCommandNameForConnectionName(command, menuCommand, Properties.CommandNames.FileJavaScriptUpdateGlobalOptionSetSingleFileWithSelectCommand);
        }
    }
}
