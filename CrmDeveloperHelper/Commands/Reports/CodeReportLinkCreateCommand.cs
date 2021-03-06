﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Reports
{
    internal sealed class CodeReportLinkCreateCommand : AbstractCommand
    {
        private CodeReportLinkCreateCommand(OleMenuCommandService commandService)
            : base(commandService, PackageIds.guidCommandSet.CodeReportLinkCreateCommandId) { }

        public static CodeReportLinkCreateCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CodeReportLinkCreateCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            List<SelectedFile> selectedFiles = helper.GetOpenedFileInCodeWindow(FileOperations.SupportsReportType).ToList();

            helper.HandleReportCreateLaskLinkCommand(selectedFiles);
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusActiveDocumentReport(applicationObject, menuCommand);
        }
    }
}
