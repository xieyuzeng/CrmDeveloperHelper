﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.SolutionComponentDescription.Implementation
{
    public class ImportEntityMappingDescriptionBuilder : DefaultSolutionComponentDescriptionBuilder
    {
        public ImportEntityMappingDescriptionBuilder(IOrganizationServiceExtented service)
            : base(service, (int)ComponentType.ImportEntityMapping)
        {

        }

        public override ComponentType? ComponentTypeEnum => ComponentType.ImportEntityMapping;

        public override int ComponentTypeValue => (int)ComponentType.ImportEntityMapping;

        public override string EntityLogicalName => ImportEntityMapping.EntityLogicalName;

        public override string EntityPrimaryIdAttribute => ImportEntityMapping.EntityPrimaryIdAttribute;

        protected override ColumnSet GetColumnSet()
        {
            return new ColumnSet
                (
                    ImportEntityMapping.Schema.Attributes.sourceentityname
                    , ImportEntityMapping.Schema.Attributes.targetentityname
                    , ImportEntityMapping.Schema.Attributes.ismanaged
                );
        }

        protected override FormatTextTableHandler GetDescriptionHeader(bool withManaged, bool withSolutionInfo, bool withUrls, Action<FormatTextTableHandler, bool, bool, bool> action)
        {
            FormatTextTableHandler handler = new FormatTextTableHandler();
            handler.SetHeader("Source", "", "Target", "Behavior");

            action(handler, withUrls, withManaged, withSolutionInfo);

            return handler;
        }

        protected override List<string> GetDescriptionValues(Entity entityInput, string behavior, bool withManaged, bool withSolutionInfo, bool withUrls, Action<List<string>, Entity, bool, bool, bool> action)
        {
            var entity = entityInput.ToEntity<ImportEntityMapping>();

            List<string> values = new List<string>();

            values.AddRange(new[]
            {
                entity.SourceEntityName
                , "->"
                , entity.TargetEntityName
                , behavior
            });

            action(values, entity, withUrls, withManaged, withSolutionInfo);

            return values;
        }

        public override string GetName(SolutionComponent solutionComponent)
        {
            var entity = GetEntity<ImportEntityMapping>(solutionComponent.ObjectId.Value);

            if (entity != null)
            {
                return string.Format("{0} -> {1}"
                    , entity.SourceEntityName
                    , entity.TargetEntityName
                );
            }

            return base.GetName(solutionComponent);
        }

        public override TupleList<string, string> GetComponentColumns()
        {
            return new TupleList<string, string>
                {
                    { ImportEntityMapping.Schema.Attributes.sourceentityname, "SourceEntityName" }
                    , { ImportEntityMapping.Schema.Attributes.targetentityname, "TargetEntityName" }
                    , { ImportEntityMapping.Schema.Attributes.ismanaged, "IsManaged" }
                    , { "solution.uniquename", "SolutionName" }
                    , { "solution.ismanaged", "SolutionIsManaged" }
                    , { "suppsolution.uniquename", "SupportingName" }
                    , { "suppsolution.ismanaged", "SupportingIsManaged" }
                };
        }
    }
}
