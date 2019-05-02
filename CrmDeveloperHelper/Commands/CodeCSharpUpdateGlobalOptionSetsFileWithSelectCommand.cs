﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands
{
    internal sealed class CodeCSharpUpdateGlobalOptionSetsFileWithSelectCommand : AbstractCommand
    {
        private CodeCSharpUpdateGlobalOptionSetsFileWithSelectCommand(Package package)
           : base(package, PackageGuids.guidCommandSet, PackageIds.CodeCSharpUpdateGlobalOptionSetsFileWithSelectCommandId, ActionExecute, ActionBeforeQueryStatus) { }

        public static CodeCSharpUpdateGlobalOptionSetsFileWithSelectCommand Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new CodeCSharpUpdateGlobalOptionSetsFileWithSelectCommand(package);
        }

        private static void ActionExecute(DTEHelper helper)
        {
            List<SelectedFile> selectedFiles = helper.GetOpenedFileInCodeWindow(FileOperations.SupportsCSharpType).ToList();

            helper.HandleUpdateGlobalOptionSetsFile(null, selectedFiles, true);
        }

        private static void ActionBeforeQueryStatus(IServiceProviderOwner command, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusActiveDocumentCSharp(command, menuCommand);

            CommonHandlers.CorrectCommandNameForConnectionName(command, menuCommand, Properties.CommandNames.CodeCSharpUpdateGlobalOptionSetsFileWithSelectCommand);
        }
    }
}
