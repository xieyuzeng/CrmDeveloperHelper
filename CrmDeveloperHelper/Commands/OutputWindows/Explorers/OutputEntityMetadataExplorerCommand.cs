using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.OutputWindows.Explorers
{
    internal sealed class OutputEntityMetadataExplorerCommand : AbstractCommand
    {
        private OutputEntityMetadataExplorerCommand(OleMenuCommandService commandService)
           : base(commandService, PackageIds.OutputEntityMetadataExplorerCommandId) { }

        public static OutputEntityMetadataExplorerCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new OutputEntityMetadataExplorerCommand(commandService);
        }

        protected override void CommandAction(DTEHelper helper)
        {
            var connectionData = helper.GetOutputWindowConnection();

            if (connectionData == null)
            {
                return;
            }

            helper.HandleExportFileWithEntityMetadata(connectionData);
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusIsConnectionOutput(applicationObject, menuCommand);
        }
    }
}