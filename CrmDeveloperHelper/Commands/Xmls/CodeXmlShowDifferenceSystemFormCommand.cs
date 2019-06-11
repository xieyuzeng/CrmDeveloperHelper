﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Xmls
{
    internal sealed class CodeXmlShowDifferenceSystemFormCommand : AbstractCommand
    {
        private CodeXmlShowDifferenceSystemFormCommand(OleMenuCommandService commandService)
            : base(commandService, PackageIds.CodeXmlShowDifferenceSystemFormCommandId) { }

        public static CodeXmlShowDifferenceSystemFormCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CodeXmlShowDifferenceSystemFormCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            List<SelectedFile> selectedFiles = helper.GetOpenedFileInCodeWindow(FileOperations.SupportsXmlType).Take(2).ToList();

            if (selectedFiles.Count == 1)
            {
                helper.HandleSystemFormDifferenceCommand(null, selectedFiles.FirstOrDefault());
            }
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusActiveDocumentIsXmlWithRootWithAttribute(applicationObject, menuCommand, Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId, out var attribute, AbstractDynamicCommandXsdSchemas.RootForm);

            CommonHandlers.CorrectCommandNameForConnectionName(applicationObject, menuCommand, Properties.CommandNames.CodeXmlShowDifferenceSystemFormCommand);

            if (attribute == null
                || !Guid.TryParse(attribute.Value, out _)
                )
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }
    }
}
