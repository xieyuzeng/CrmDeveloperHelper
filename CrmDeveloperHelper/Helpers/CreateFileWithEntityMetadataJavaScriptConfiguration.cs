﻿using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System.Collections.Generic;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers
{
    public class CreateFileWithEntityMetadataJavaScriptConfiguration
    {
        public string TabSpacer { get; private set; }

        public string EntityName { get; private set; }

        public bool WithDependentComponents { get; private set; }

        public CreateFileWithEntityMetadataJavaScriptConfiguration(string entityName
            , string tabSpacer
            , bool withDependentComponents
        )
        {
            this.EntityName = entityName;
            this.TabSpacer = tabSpacer;
            this.WithDependentComponents = withDependentComponents;
        }
    }
}
