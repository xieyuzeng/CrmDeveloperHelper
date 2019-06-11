﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.CSharp
{
    internal sealed class FolderCSharpProjectPluginTypeStepsAddToSolutionLastCommand : AbstractDynamicCommandAddObjectToSolutionLast
    {
        private FolderCSharpProjectPluginTypeStepsAddToSolutionLastCommand(OleMenuCommandService commandService)
            : base(
                commandService
                , PackageIds.FolderCSharpProjectPluginTypeStepsAddToSolutionLastCommandId
                , ActionExecuteAsync
                , ActionBeforeQueryStatus
            )
        {

        }

        public static FolderCSharpProjectPluginTypeStepsAddToSolutionLastCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new FolderCSharpProjectPluginTypeStepsAddToSolutionLastCommand(commandService);
        }

        private static async void ActionExecuteAsync(DTEHelper helper, ConnectionData connectionData, string solutionUniqueName)
        {
            var listFiles = helper.GetSelectedProjectItemsInSolutionExplorer(FileOperations.SupportsCSharpType, true).ToList();

            var pluginTypeNames = new List<string>();
            var handledFilePaths = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            helper.ActivateOutputWindow(null);

            foreach (var item in listFiles)
            {
                string filePath = item.FileNames[1];

                if (handledFilePaths.Add(filePath))
                {
                    helper.WriteToOutput(null, Properties.OutputStrings.GettingClassFullNameFromFileFormat1, filePath);
                    var typeName = await PropertiesHelper.GetTypeFullNameAsync(item);

                    if (!string.IsNullOrEmpty(typeName))
                    {
                        pluginTypeNames.Add(typeName);
                    }
                }
            }

            if (pluginTypeNames.Any())
            {
                helper.HandleAddingPluginTypeProcessingStepsByProjectCommand(null, solutionUniqueName, false, pluginTypeNames.ToArray());
            }
        }

        private static void ActionBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusSolutionExplorerAnyItemContainsProject(applicationObject, menuCommand, true);
        }
    }
}
