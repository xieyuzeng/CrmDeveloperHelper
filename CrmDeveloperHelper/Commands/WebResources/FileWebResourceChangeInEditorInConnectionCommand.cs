using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.WebResources
{
    internal sealed class FileWebResourceChangeInEditorInConnectionCommand : AbstractDynamicCommandByConnectionAll
    {
        private FileWebResourceChangeInEditorInConnectionCommand(OleMenuCommandService commandService)
            : base(commandService, PackageIds.guidDynamicCommandSet.FileWebResourceChangeInEditorInConnectionCommandId)
        {

        }

        public static FileWebResourceChangeInEditorInConnectionCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new FileWebResourceChangeInEditorInConnectionCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper, ConnectionData connectionData)
        {
            helper.HandleWebResourceChangeInEntityEditorCommand(connectionData);
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, ConnectionData connectionData, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusSolutionExplorerWebResourceSingle(applicationObject, menuCommand);
        }
    }
}