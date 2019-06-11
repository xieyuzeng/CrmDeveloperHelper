using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Connections
{
    internal sealed class CommonPublishAllInCrmConnectionCommand : AbstractCommandByConnectionAll
    {
        private CommonPublishAllInCrmConnectionCommand(OleMenuCommandService commandService)
            : base(
                commandService
                , PackageIds.CommonPublishAllInCrmConnectionCommandId
            )
        {

        }

        public static CommonPublishAllInCrmConnectionCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CommonPublishAllInCrmConnectionCommand(commandService);
        }

        private static void ActionExecute(DTEHelper helper, ConnectionData connectionData)
        {
        }

        protected override void CommandAction(DTEHelper helper, ConnectionData connectionData)
        {
            helper.HandlePublishAll(connectionData);
        }
    }
}