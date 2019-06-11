﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.Checks
{
    internal sealed class CommonCheckPluginStepsRequiredComponentsCommand : AbstractCommandByConnectionAll
    {
        private CommonCheckPluginStepsRequiredComponentsCommand(OleMenuCommandService commandService)
            : base(
                commandService
                , PackageIds.CommonCheckPluginStepsRequiredComponentsCommandId
            )
        {

        }

        public static CommonCheckPluginStepsRequiredComponentsCommand Instance { get; private set; }

        public static void Initialize(OleMenuCommandService commandService)
        {
            Instance = new CommonCheckPluginStepsRequiredComponentsCommand(commandService);
        }

        private static void ActionExecute(DTEHelper helper, ConnectionData connectionData)
        {
        }

        protected override void CommandAction(DTEHelper helper, ConnectionData connectionData)
        {
            helper.HandleCheckPluginStepsRequiredComponents(connectionData);
        }
    }
}
