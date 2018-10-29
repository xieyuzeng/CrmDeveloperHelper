using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.SolutionComponentDescription.Implementation
{
    public class RibbonCustomizationDescriptionBuilder : DefaultSolutionComponentDescriptionBuilder
    {
        public RibbonCustomizationDescriptionBuilder(IOrganizationServiceExtented service)
            : base(service, (int)ComponentType.RibbonCustomization)
        {

        }

        public override ComponentType? ComponentTypeEnum => ComponentType.RibbonCustomization;

        public override int ComponentTypeValue => (int)ComponentType.RibbonCustomization;

        public override string EntityLogicalName => RibbonCustomization.EntityLogicalName;

        public override string EntityPrimaryIdAttribute => RibbonCustomization.Schema.EntityPrimaryIdAttribute;

        protected override ColumnSet GetColumnSet()
        {
            return new ColumnSet
                (
                    RibbonCustomization.Schema.Attributes.entity
                    , RibbonCustomization.Schema.Attributes.ismanaged
                );
        }

        protected override FormatTextTableHandler GetDescriptionHeader(bool withUrls, bool withManaged, bool withSolutionInfo, Action<FormatTextTableHandler, bool, bool, bool> action)
        {
            FormatTextTableHandler handler = new FormatTextTableHandler();
            handler.SetHeader("Entity", "Id");

            action(handler, withUrls, withManaged, withSolutionInfo);

            return handler;
        }

        protected override List<string> GetDescriptionValues(Entity entityInput, bool withUrls, bool withManaged, bool withSolutionInfo, Action<List<string>, Entity, bool, bool, bool> action)
        {
            var entity = entityInput.ToEntity<RibbonCustomization>();

            List<string> values = new List<string>();

            values.AddRange(new[]
            {
                entity.Entity ?? "ApplicationRibbon"
                , entity.Id.ToString()
            });

            action(values, entity, withUrls, withManaged, withSolutionInfo);

            return values;
        }

        public override string GetName(SolutionComponent component)
        {
            var entity = GetEntity<RibbonCustomization>(component.ObjectId.Value);

            if (entity != null)
            {
                return entity.ToEntity<RibbonCustomization>().Entity ?? "ApplicationRibbon";
            }

            return base.GetName(component);
        }

        public override void FillSolutionImageComponent(ICollection<SolutionImageComponent> result, SolutionComponent solutionComponent)
        {
            if (solutionComponent == null
                || !solutionComponent.ObjectId.HasValue
                )
            {
                return;
            }

            var entity = GetEntity<RibbonCustomization>(solutionComponent.ObjectId.Value);

            if (entity != null)
            {
                var repository = new RibbonCustomizationRepository(_service);

                result.Add(new SolutionImageComponent()
                {
                    SchemaName = string.Format("{0}:RibbonDiffXml", entity.Entity),
                    ComponentType = (solutionComponent.ComponentType?.Value).GetValueOrDefault(),
                    RootComponentBehavior = (solutionComponent.RootComponentBehavior?.Value).GetValueOrDefault((int)RootComponentBehavior.IncludeSubcomponents),

                    Description = GenerateDescriptionSingle(solutionComponent, false, true, true),
                });
            }
        }

        public override void FillSolutionComponent(ICollection<SolutionComponent> result, SolutionImageComponent solutionImageComponent)
        {
            if (solutionImageComponent == null)
            {
                return;
            }

            if (string.Equals(solutionImageComponent.SchemaName, ":RibbonDiffXml", StringComparison.InvariantCultureIgnoreCase))
            {
                var repository = new RibbonCustomizationRepository(_service);

                var entity = repository.FindApplicationRibbonCustomization();

                if (entity != null)
                {
                    var component = new SolutionComponent()
                    {
                        ComponentType = new OptionSetValue(this.ComponentTypeValue),
                        ObjectId = entity.Id,
                        RootComponentBehavior = new OptionSetValue((int)RootComponentBehavior.IncludeSubcomponents),
                    };

                    if (solutionImageComponent.RootComponentBehavior.HasValue)
                    {
                        component.RootComponentBehavior = new OptionSetValue(solutionImageComponent.RootComponentBehavior.Value);
                    }

                    result.Add(component);
                }
            }
        }

        public override TupleList<string, string> GetComponentColumns()
        {
            return new TupleList<string, string>
                {
                    { RibbonCustomization.Schema.Attributes.entity, "Entity" }
                    , { RibbonCustomization.Schema.Attributes.ismanaged, "IsManaged" }
                    , { "solution.uniquename", "SolutionName" }
                    , { "solution.ismanaged", "SolutionIsManaged" }
                    , { "suppsolution.uniquename", "SupportingName" }
                    , { "suppsolution.ismanaged", "SupportingIsManaged" }
                };
        }
    }
}