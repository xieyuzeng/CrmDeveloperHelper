﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Xmls
{
    internal sealed class CodeXmlRibbonDiffXmlUpdateInConnectionGroupCommand : AbstractDynamicCommandByConnectionByGroupWithoutCurrent
    {
        private CodeXmlRibbonDiffXmlUpdateInConnectionGroupCommand(OleMenuCommandService commandService)
            : base(
                commandService
                , PackageIds.guidDynamicCommandSet.CodeXmlRibbonDiffXmlUpdateInConnectionGroupCommandId
            )
        {

        }

        public static CodeXmlRibbonDiffXmlUpdateInConnectionGroupCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CodeXmlRibbonDiffXmlUpdateInConnectionGroupCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper, ConnectionData connectionData)
        {
            List<SelectedFile> selectedFiles = helper.GetOpenedFileInCodeWindow(FileOperations.SupportsXmlType).Take(2).ToList();

            if (selectedFiles.Count == 1)
            {
                helper.HandleRibbonDiffXmlUpdateCommand(connectionData, selectedFiles.FirstOrDefault());
            }
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, ConnectionData connectionData, OleMenuCommand menuCommand)
        {
            if (connectionData.IsReadOnly)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
            else
            {
                menuCommand.Enabled = menuCommand.Visible = true;

                CommonHandlers.ActionBeforeQueryStatusActiveDocumentIsXmlWithRootWithAttribute(applicationObject
                    , menuCommand
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeEntityName
                    , out var attribute
                    , AbstractDynamicCommandXsdSchemas.RootRibbonDiffXml
                );

                if (attribute != null)
                {
                    string entityName = attribute.Value;

                    if (string.IsNullOrEmpty(entityName))
                    {
                        entityName = "ApplicationRibbon";
                    }

                    string nameCommand = string.Format(Properties.CommandNames.CommandNameWithConnectionFormat2, entityName, connectionData.Name);

                    menuCommand.Text = nameCommand;
                }
            }
        }
    }
}
