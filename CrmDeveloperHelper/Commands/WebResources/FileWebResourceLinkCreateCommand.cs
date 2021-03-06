﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.WebResources
{
    internal sealed class FileWebResourceLinkCreateCommand : AbstractCommand
    {
        private FileWebResourceLinkCreateCommand(OleMenuCommandService commandService)
            : base(commandService, PackageIds.guidCommandSet.FileWebResourceLinkCreateCommandId) { }

        public static FileWebResourceLinkCreateCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new FileWebResourceLinkCreateCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            List<SelectedFile> selectedFiles = helper.GetSelectedFilesInSolutionExplorer(FileOperations.SupportsWebResourceType, false).ToList();

            helper.HandleWebResourceCreateLaskLinkMultipleCommand(selectedFiles);
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusSolutionExplorerWebResourceAny(applicationObject, menuCommand);
        }
    }
}
