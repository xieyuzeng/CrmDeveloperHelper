using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.CSharp
{
    internal sealed class DocumentsCSharpProjectPluginAssemblyStepsAddToSolutionInConnectionCommand : AbstractDynamicCommandByConnectionAll
    {
        private DocumentsCSharpProjectPluginAssemblyStepsAddToSolutionInConnectionCommand(OleMenuCommandService commandService)
            : base(
                commandService
                , PackageIds.guidDynamicCommandSet.DocumentsCSharpProjectPluginAssemblyStepsAddToSolutionInConnectionCommandId
            )
        {

        }

        public static DocumentsCSharpProjectPluginAssemblyStepsAddToSolutionInConnectionCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new DocumentsCSharpProjectPluginAssemblyStepsAddToSolutionInConnectionCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper, ConnectionData connectionData)
        {
            var listProjects = helper
                   .GetOpenedDocumentsAsDocument(FileOperations.SupportsCSharpType)
                   .Where(i => i.ProjectItem?.ContainingProject != null && !string.IsNullOrEmpty(i.ProjectItem?.ContainingProject?.Name))
                   .Select(i => i.ProjectItem.ContainingProject.Name)
                   .ToArray()
                   ;

            if (listProjects.Any())
            {
                helper.HandlePluginAssemblyAddingProcessingStepsByProjectCommand(connectionData, null, true, listProjects);
            }
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, ConnectionData connectionData, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusOpenedDocumentsCSharp(applicationObject, menuCommand);
        }
    }
}