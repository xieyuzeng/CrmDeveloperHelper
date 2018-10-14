﻿using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.SolutionComponentDescription.Implementation
{
    public class SdkMessageResponseFieldDescriptionBuilder : DefaultSolutionComponentDescriptionBuilder
    {
        public SdkMessageResponseFieldDescriptionBuilder(IOrganizationServiceExtented service)
            : base(service, (int)ComponentType.SdkMessageResponseField)
        {

        }

        public override ComponentType? ComponentTypeEnum => ComponentType.SdkMessageResponseField;

        public override int ComponentTypeValue => (int)ComponentType.SdkMessageResponseField;

        public override string EntityLogicalName => SdkMessageResponseField.EntityLogicalName;

        public override string EntityPrimaryIdAttribute => SdkMessageResponseField.Schema.EntityPrimaryIdAttribute;

        protected override ColumnSet GetColumnSet()
        {
            return new ColumnSet
                (
                    SdkMessageResponseField.Schema.Attributes.position
                    , SdkMessageResponseField.Schema.Attributes.name
                    , SdkMessageResponseField.Schema.Attributes.publicname
                    , SdkMessageResponseField.Schema.Attributes.value
                    , SdkMessageResponseField.Schema.Attributes.clrformatter
                    , SdkMessageResponseField.Schema.Attributes.formatter
                    , SdkMessageResponseField.Schema.Attributes.parameterbindinginformation
                    , SdkMessageResponseField.Schema.Attributes.customizationlevel
                );
        }

        protected override QueryExpression GetQuery(List<Guid> idsNotCached)
        {
            var query = new QueryExpression()
            {
                NoLock = true,

                EntityName = this.EntityLogicalName,

                ColumnSet = GetColumnSet(),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(this.EntityPrimaryIdAttribute, ConditionOperator.In, idsNotCached.ToArray()),
                    },
                },

                LinkEntities =
                {
                    new LinkEntity()
                    {
                        LinkFromEntityName = SdkMessageResponseField.EntityLogicalName,
                        LinkFromAttributeName = SdkMessageResponseField.Schema.Attributes.sdkmessageresponseid,

                        LinkToEntityName = SdkMessageResponse.EntityLogicalName,
                        LinkToAttributeName = SdkMessageResponse.PrimaryIdAttribute,

                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                LinkFromEntityName = SdkMessageResponse.EntityLogicalName,
                                LinkFromAttributeName = SdkMessageResponse.Schema.Attributes.sdkmessagerequestid,

                                LinkToEntityName = SdkMessageRequest.EntityLogicalName,
                                LinkToAttributeName = SdkMessageRequest.PrimaryIdAttribute,

                                EntityAlias = SdkMessageResponse.Schema.Attributes.sdkmessagerequestid,

                                Columns = new ColumnSet(SdkMessageRequest.Schema.Attributes.name),

                                LinkEntities =
                                {
                                    new LinkEntity()
                                    {
                                        LinkFromEntityName = SdkMessageRequest.EntityLogicalName,
                                        LinkFromAttributeName = SdkMessageRequest.Schema.Attributes.sdkmessagepairid,

                                        LinkToEntityName = SdkMessagePair.EntityLogicalName,
                                        LinkToAttributeName = SdkMessagePair.PrimaryIdAttribute,

                                        LinkEntities =
                                        {
                                            new LinkEntity()
                                            {
                                                LinkFromEntityName = SdkMessagePair.EntityLogicalName,
                                                LinkFromAttributeName = SdkMessagePair.Schema.Attributes.sdkmessageid,

                                                LinkToEntityName = SdkMessage.EntityLogicalName,
                                                LinkToAttributeName = SdkMessage.PrimaryIdAttribute,

                                                EntityAlias = SdkMessagePair.Schema.Attributes.sdkmessageid,

                                                Columns = new ColumnSet(SdkMessage.Schema.Attributes.name),
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                },

                PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = 5000,
                }
            };

            return query;
        }

        public override void GenerateDescription(StringBuilder builder, IEnumerable<SolutionComponent> components, bool withUrls)
        {
            var list = GetEntities<SdkMessageResponseField>(components.Select(c => c.ObjectId));

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

            handler.SetHeader("Message", "RequestName", "Position", "Name", "PublicName", "ClrParser", "Parser", "Optional", "CustomizationLevel");

            foreach (var entity in list.Select(ent => ent.ToEntity<SdkMessageResponseField>()))
            {
                handler.AddLine(
                    EntityDescriptionHandler.GetAttributeString(entity, SdkMessageResponse.Schema.Attributes.sdkmessagerequestid + "." + SdkMessageRequest.Schema.Attributes.name)
                    , EntityDescriptionHandler.GetAttributeString(entity, SdkMessagePair.Schema.Attributes.sdkmessageid + "." + SdkMessage.Schema.Attributes.name)
                    , entity.Position.ToString()
                    , entity.Name
                    , entity.PublicName
                    , entity.Value
                    , entity.ClrFormatter
                    , entity.Formatter
                    , entity.ParameterBindingInformation
                    , entity.CustomizationLevel.ToString()
                );
            }

            List<string> lines = handler.GetFormatedLines(true);

            lines.ForEach(item => builder.AppendFormat(formatSpacer, item).AppendLine());
        }

        public override string GenerateDescriptionSingle(SolutionComponent component, bool withUrls)
        {
            var sdkMessageResponseField = GetEntity<SdkMessageResponseField>(component.ObjectId.Value);

            if (sdkMessageResponseField != null)
            {
                FormatTextTableHandler handler = new FormatTextTableHandler();

                handler.AddLine(
                    EntityDescriptionHandler.GetAttributeString(sdkMessageResponseField, SdkMessageResponse.Schema.Attributes.sdkmessagerequestid + "." + SdkMessageRequest.Schema.Attributes.name)
                    , EntityDescriptionHandler.GetAttributeString(sdkMessageResponseField, SdkMessagePair.Schema.Attributes.sdkmessageid + "." + SdkMessage.Schema.Attributes.name)
                    , sdkMessageResponseField.Position.ToString()
                    , sdkMessageResponseField.Name
                    , sdkMessageResponseField.PublicName
                    , sdkMessageResponseField.Value
                    , sdkMessageResponseField.ClrFormatter
                    , sdkMessageResponseField.Formatter
                    , sdkMessageResponseField.ParameterBindingInformation
                    , sdkMessageResponseField.CustomizationLevel.ToString()
                );

                var str = handler.GetFormatedLinesWithHeadersInLine(false).FirstOrDefault();

                return string.Format("SdkMessageResponseField {0}", str);
            }

            return base.GenerateDescriptionSingle(component, withUrls);
        }

        public override TupleList<string, string> GetComponentColumns()
        {
            return new TupleList<string, string>
                {
                    { SdkMessageResponse.Schema.Attributes.sdkmessagerequestid + "." + SdkMessageRequest.Schema.Attributes.name, "Message" }
                    , { SdkMessagePair.Schema.Attributes.sdkmessageid + "." + SdkMessage.Schema.Attributes.name, "RequestName" }
                    , { SdkMessageResponseField.Schema.Attributes.position, "Position" }
                    , { SdkMessageResponseField.Schema.Attributes.name, "Name" }
                    , { SdkMessageResponseField.Schema.Attributes.publicname, "PublicName" }
                    , { SdkMessageResponseField.Schema.Attributes.value, "Value" }
                    , { SdkMessageResponseField.Schema.Attributes.clrformatter, "ClrFormatter" }
                    , { SdkMessageResponseField.Schema.Attributes.formatter, "Formatter" }
                    , { SdkMessageResponseField.Schema.Attributes.parameterbindinginformation, "ParameterBindingInformation" }
                    , { SdkMessageResponseField.Schema.Attributes.customizationlevel, "CustomizationLevel" }
                };
        }
    }
}
