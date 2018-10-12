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
    public class ChannelAccessProfileDescriptionBuilder : DefaultSolutionComponentDescriptionBuilder
    {
        public ChannelAccessProfileDescriptionBuilder(IOrganizationServiceExtented service)
            : base(service, (int)ComponentType.ChannelAccessProfile)
        {

        }

        public override ComponentType? ComponentTypeEnum => ComponentType.ChannelAccessProfile;

        public override int ComponentTypeValue => (int)ComponentType.ChannelAccessProfile;

        public override string EntityLogicalName => ChannelAccessProfile.EntityLogicalName;

        public override string EntityPrimaryIdAttribute => ChannelAccessProfile.Schema.EntityPrimaryIdAttribute;

        protected override ColumnSet GetColumnSet()
        {
            return new ColumnSet
                (
                    ChannelAccessProfile.Schema.Attributes.name
                    , ChannelAccessProfile.Schema.Attributes.statuscode
                    , ChannelAccessProfile.Schema.Attributes.ismanaged
                );
        }

        public override void GenerateDescription(StringBuilder builder, IEnumerable<SolutionComponent> components, bool withUrls)
        {
            var list = GetEntities<ChannelAccessProfile>(components.Select(c => c.ObjectId));

            {
                var hash = new HashSet<Guid>(list.Select(en => en.Id));
                var notFinded = components.Where(en => !hash.Contains(en.ObjectId.Value)).ToList();
                if (notFinded.Any())
                {
                    builder.AppendFormat(formatSpacer, unknowedMessage).AppendLine();
                    notFinded.ForEach(item => builder.AppendFormat(formatSpacer, item.ToString()).AppendLine());
                }
            }

            FormatTextTableHandler handler = new FormatTextTableHandler();
            handler.SetHeader("Name", "StatusCode", "Id", "IsManaged", "SolutionName", "SolutionIsManaged", "SupportingName", "SupportinIsManaged");

            foreach (var entity in list)
            {
                handler.AddLine(
                    entity.Name
                    , entity.FormattedValues[ChannelAccessProfile.Schema.Attributes.statuscode]
                    , entity.Id.ToString()
                    , entity.IsManaged.ToString()
                    , EntityDescriptionHandler.GetAttributeString(entity, "solution.uniquename")
                    , EntityDescriptionHandler.GetAttributeString(entity, "solution.ismanaged")
                    , EntityDescriptionHandler.GetAttributeString(entity, "suppsolution.uniquename")
                    , EntityDescriptionHandler.GetAttributeString(entity, "suppsolution.ismanaged")
                    );
            }

            List<string> lines = handler.GetFormatedLines(true);

            lines.ForEach(item => builder.AppendFormat(formatSpacer, item).AppendLine());
        }

        public override string GenerateDescriptionSingle(SolutionComponent component, bool withUrls)
        {
            var channelAccessProfile = GetEntity<ChannelAccessProfile>(component.ObjectId.Value);

            if (channelAccessProfile != null)
            {
                return string.Format("Name {0}    StatusCode {1}    Id {2}    IsManaged {3}    SolutionName {4}"
                   , channelAccessProfile.Name
                   , channelAccessProfile.FormattedValues[ChannelAccessProfile.Schema.Attributes.statuscode]
                   , channelAccessProfile.Id.ToString()
                   , channelAccessProfile.IsManaged.ToString()
                   , EntityDescriptionHandler.GetAttributeString(channelAccessProfile, "solution.uniquename")
                   );
            }

            return base.GenerateDescriptionSingle(component, withUrls);
        }

        public override TupleList<string, string> GetComponentColumns()
        {
            return new TupleList<string, string>
                {
                    { ChannelAccessProfile.Schema.Attributes.name, "Name" }
                    , { ChannelAccessProfile.Schema.Attributes.statuscode, "StatusCode" }
                    , { ChannelAccessProfile.Schema.Attributes.ismanaged, "IsManaged" }
                    , { "solution.uniquename", "SolutionName" }
                    , { "solution.ismanaged", "SolutionIsManaged" }
                    , { "suppsolution.uniquename", "SupportingName" }
                    , { "suppsolution.ismanaged", "SupportingIsManaged" }
                };
        }
    }
}