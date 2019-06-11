﻿using Microsoft.VisualStudio.Shell;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands
{
    internal abstract class AbstractCommandByConnectionByGroupWithCurrent : AbstractCommandByConnection
    {
        public AbstractCommandByConnectionByGroupWithCurrent(
            OleMenuCommandService commandService
            , int baseIdStart
        ) : base(
            commandService
            , baseIdStart
            , config => config.GetConnectionsByGroupWithCurrent()
            , connectionData => connectionData.NameWithCurrentMark
        )
        {

        }
    }
}
