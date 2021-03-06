﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using System;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Xmls
{
    internal sealed class CodeJavaScriptLinkedEntityExplorerCommand : AbstractCommand
    {
        private CodeJavaScriptLinkedEntityExplorerCommand(OleMenuCommandService commandService)
            : base(commandService, PackageIds.guidCommandSet.CodeJavaScriptLinkedEntityExplorerCommandId)
        {
        }

        public static CodeJavaScriptLinkedEntityExplorerCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CodeJavaScriptLinkedEntityExplorerCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            if (helper.TryGetLinkedEntityName(out string entityName))
            {
                helper.HandleExportFileWithEntityMetadata(entityName);
            }
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusActiveDocumentJavaScriptHasLinkedEntityName(applicationObject, menuCommand);

            CommonHandlers.CorrectCommandNameForConnectionName(applicationObject, menuCommand, Properties.CommandNames.CodeJavaScriptLinkedEntityExplorerCommand);
        }
    }
}