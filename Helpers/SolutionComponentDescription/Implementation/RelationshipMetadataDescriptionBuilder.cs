using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.SolutionComponentDescription.Implementation
{
    public class RelationshipMetadataDescriptionBuilder : ISolutionComponentDescriptionBuilder
    {
        private const string formatSpacer = DefaultSolutionComponentDescriptionBuilder.formatSpacer;
        private const string unknowedMessage = DefaultSolutionComponentDescriptionBuilder.unknowedMessage;

        private readonly SolutionComponentMetadataSource _source;

        public int ComponentTypeValue => (int)ComponentType.EntityRelationship;

        public ComponentType? ComponentTypeEnum => ComponentType.EntityRelationship;

        public string EntityLogicalName => string.Empty;

        public string EntityPrimaryIdAttribute => string.Empty;

        public RelationshipMetadataDescriptionBuilder(SolutionComponentMetadataSource source)
        {
            this._source = source;
        }

        public List<T> GetEntities<T>(IEnumerable<Guid> components) where T : Entity
        {
            return new List<T>();
        }

        public List<T> GetEntities<T>(IEnumerable<Guid?> components) where T : Entity
        {
            return new List<T>();
        }

        public T GetEntity<T>(Guid idEntity) where T : Entity
        {
            return null;
        }

        public void FillSolutionImageComponent(ICollection<SolutionImageComponent> result, SolutionComponent solutionComponent)
        {
            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(solutionComponent.ObjectId.GetValueOrDefault());

            if (metaData != null)
            {
                if (metaData is OneToManyRelationshipMetadata)
                {
                    var relationship = metaData as OneToManyRelationshipMetadata;

                    result.Add(new SolutionImageComponent()
                    {
                        ComponentType = (int)ComponentType.EntityRelationship,
                        SchemaName = relationship.SchemaName,
                        ParentSchemaName = relationship.ReferencingEntity,
                        RootComponentBehavior = (solutionComponent.RootComponentBehavior?.Value).GetValueOrDefault((int)RootComponentBehavior.IncludeSubcomponents),

                        Description = GenerateDescriptionSingle(solutionComponent, false, true, true),
                    });

                    result.Add(new SolutionImageComponent()
                    {
                        ComponentType = (int)ComponentType.EntityRelationship,
                        SchemaName = relationship.SchemaName,
                        ParentSchemaName = relationship.ReferencedEntity,
                        RootComponentBehavior = (solutionComponent.RootComponentBehavior?.Value).GetValueOrDefault((int)RootComponentBehavior.IncludeSubcomponents),

                        Description = GenerateDescriptionSingle(solutionComponent, false, true, true),
                    });
                }
                else if (metaData is ManyToManyRelationshipMetadata)
                {
                    var relationship = metaData as ManyToManyRelationshipMetadata;

                    result.Add(new SolutionImageComponent()
                    {
                        ComponentType = (int)ComponentType.EntityRelationship,
                        SchemaName = relationship.SchemaName,
                        ParentSchemaName = relationship.Entity1LogicalName,
                        RootComponentBehavior = (solutionComponent.RootComponentBehavior?.Value).GetValueOrDefault((int)RootComponentBehavior.IncludeSubcomponents),

                        Description = GenerateDescriptionSingle(solutionComponent, false, true, true),
                    });

                    result.Add(new SolutionImageComponent()
                    {
                        ComponentType = (int)ComponentType.EntityRelationship,
                        SchemaName = relationship.SchemaName,
                        ParentSchemaName = relationship.Entity2LogicalName,
                        RootComponentBehavior = (solutionComponent.RootComponentBehavior?.Value).GetValueOrDefault((int)RootComponentBehavior.IncludeSubcomponents),

                        Description = GenerateDescriptionSingle(solutionComponent, false, true, true),
                    });
                }
            }
        }

        public void FillSolutionComponent(ICollection<SolutionComponent> result, SolutionImageComponent solutionImageComponent)
        {
            if (solutionImageComponent == null
                || string.IsNullOrEmpty(solutionImageComponent.SchemaName)
                )
            {
                return;
            }

            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(solutionImageComponent.SchemaName);

            if (metaData != null)
            {
                var component = new SolutionComponent()
                {
                    ComponentType = new OptionSetValue(this.ComponentTypeValue),
                    ObjectId = metaData.MetadataId.Value,
                    RootComponentBehavior = new OptionSetValue((int)RootComponentBehavior.IncludeSubcomponents),
                };

                if (solutionImageComponent.RootComponentBehavior.HasValue)
                {
                    component.RootComponentBehavior = new OptionSetValue(solutionImageComponent.RootComponentBehavior.Value);
                }

                result.Add(component);
            }
        }

        public void GenerateDescription(StringBuilder builder, IEnumerable<SolutionComponent> components, bool withUrls, bool withManaged, bool withSolutionInfo)
        {
            FormatTextTableHandler handler = new FormatTextTableHandler();

            FormatTextTableHandler handlerManyToOne = new FormatTextTableHandler();
            handlerManyToOne.SetHeader("ReferencingAttribute", "Type", "SchemaName", "Behaviour", "IsCustomizable");

            FormatTextTableHandler handlerManyToMany = new FormatTextTableHandler();
            handlerManyToMany.SetHeader("Entity - Entity", "Type", "SchemaName", "Behaviour", "IsCustomizable");

            if (withManaged)
            {
                handlerManyToOne.AppendHeader("IsManaged");
                handlerManyToMany.AppendHeader("IsManaged");
            }

            if (withUrls)
            {
                handlerManyToOne.AppendHeader("Url");
                handlerManyToMany.AppendHeader("Url");
            }

            foreach (var comp in components)
            {
                RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(comp.ObjectId.GetValueOrDefault());

                string behavior = string.Empty;

                if (comp.RootComponentBehavior != null)
                {
                    behavior = SolutionComponent.GetRootComponentBehaviorName(comp.RootComponentBehavior.Value);
                }

                if (metaData != null)
                {
                    if (metaData is OneToManyRelationshipMetadata)
                    {
                        var relationship = metaData as OneToManyRelationshipMetadata;

                        string relName = string.Format("{0}.{1}", relationship.ReferencingEntity, relationship.ReferencingAttribute);
                        string refEntity = relationship.ReferencedEntity;

                        List<string> values = new List<string>();

                        values.AddRange(new[]
                        {
                            relName
                            , "Many to One"
                            , refEntity
                            , relationship.SchemaName
                            , behavior
                            , relationship.IsCustomizable?.Value.ToString()
                        });

                        if (withManaged)
                        {
                            values.Add(metaData.IsManaged.ToString());
                        }

                        if (withUrls)
                        {
                            var entityMetadata = _source.GetEntityMetadata(refEntity);
                            if (entityMetadata != null)
                            {
                                values.Add(_source.Service.ConnectionData?.GetRelationshipMetadataRelativeUrl(entityMetadata.MetadataId.Value, relationship.MetadataId.Value));
                            }
                        }

                        handlerManyToOne.AddLine(values);
                    }
                    else if (metaData is ManyToManyRelationshipMetadata)
                    {
                        var relationship = metaData as ManyToManyRelationshipMetadata;

                        string relName = string.Format("{0} - {1}", relationship.Entity1LogicalName, relationship.Entity2LogicalName);

                        List<string> values = new List<string>();

                        values.AddRange(new[]
                        {
                            relName
                            , "Many to Many"
                            , relationship.SchemaName
                            , behavior
                            , relationship.IsCustomizable?.Value.ToString()
                        });

                        if (withManaged)
                        {
                            values.Add(metaData.IsManaged.ToString());
                        }

                        if (withUrls)
                        {
                            var entityMetadata = _source.GetEntityMetadata(relationship.Entity1LogicalName);
                            if (entityMetadata != null)
                            {
                                values.Add(_source.Service.ConnectionData?.GetRelationshipMetadataRelativeUrl(entityMetadata.MetadataId.Value, relationship.MetadataId.Value));
                            }
                        }

                        handlerManyToMany.AddLine(values);
                    }
                }
                else
                {
                    handler.AddLine(comp.ObjectId.ToString(), behavior);
                }
            }

            List<string> linesUnknowed = handler.GetFormatedLines(true);

            List<string> linesOne = handlerManyToOne.GetFormatedLines(true);

            List<string> linesMany = handlerManyToMany.GetFormatedLines(true);

            if (linesUnknowed.Any())
            {
                builder.AppendFormat(formatSpacer, unknowedMessage).AppendLine();
                linesUnknowed.ForEach(item => builder.AppendFormat(formatSpacer, item).AppendLine());
            }

            linesOne.ForEach(item => builder.AppendFormat(formatSpacer, item).AppendLine());
            linesMany.ForEach(item => builder.AppendFormat(formatSpacer, item).AppendLine());
        }

        public string GenerateDescriptionSingle(SolutionComponent solutionComponent, bool withUrls, bool withManaged, bool withSolutionInfo)
        {
            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(solutionComponent.ObjectId.Value);

            if (metaData != null)
            {
                string behavior = string.Empty;

                if (solutionComponent.RootComponentBehavior != null)
                {
                    behavior = SolutionComponent.GetRootComponentBehaviorName(solutionComponent.RootComponentBehavior.Value);
                }

                if (metaData is OneToManyRelationshipMetadata)
                {
                    FormatTextTableHandler handlerManyToOne = new FormatTextTableHandler();
                    handlerManyToOne.SetHeader("ReferencingAttribute", "Type", "SchemaName", "Behaviour", "IsCustomizable");

                    if (withManaged)
                    {
                        handlerManyToOne.AppendHeader("IsManaged");
                    }

                    if (withUrls)
                    {
                        handlerManyToOne.AppendHeader("Url");
                    }

                    var relationship = metaData as OneToManyRelationshipMetadata;

                    string relName = string.Format("{0}.{1}", relationship.ReferencingEntity, relationship.ReferencingAttribute);
                    string refEntity = relationship.ReferencedEntity;

                    List<string> values = new List<string>();

                    values.AddRange(new[]
                    {
                        relName
                        , "Many to One"
                        , refEntity
                        , relationship.SchemaName
                        , behavior
                        , relationship.IsCustomizable?.Value.ToString()
                    });

                    if (withManaged)
                    {
                        values.Add(metaData.IsManaged.ToString());
                    }

                    if (withUrls)
                    {
                        var entityMetadata = _source.GetEntityMetadata(refEntity);
                        if (entityMetadata != null)
                        {
                            values.Add(_source.Service.ConnectionData?.GetRelationshipMetadataRelativeUrl(entityMetadata.MetadataId.Value, relationship.MetadataId.Value));
                        }
                    }

                    handlerManyToOne.AddLine(values);

                    var str = handlerManyToOne.GetFormatedLinesWithHeadersInLine(false).FirstOrDefault();

                    return string.Format("{0} {1}", this.ComponentTypeEnum.ToString(), str);
                }
                else if (metaData is ManyToManyRelationshipMetadata)
                {
                    FormatTextTableHandler handlerManyToMany = new FormatTextTableHandler();
                    handlerManyToMany.SetHeader("Entity - Entity", "Type", "SchemaName", "Behaviour", "IsCustomizable");

                    if (withManaged)
                    {
                        handlerManyToMany.AppendHeader("IsManaged");
                    }

                    if (withUrls)
                    {
                        handlerManyToMany.AppendHeader("Url");
                    }

                    var relationship = metaData as ManyToManyRelationshipMetadata;

                    string relName = string.Format("{0} - {1}", relationship.Entity1LogicalName, relationship.Entity2LogicalName);

                    List<string> values = new List<string>();

                    values.AddRange(new[]
                    {
                            relName
                            , "Many to Many"
                            , relationship.SchemaName
                            , behavior
                            , relationship.IsCustomizable?.Value.ToString()
                        });

                    if (withManaged)
                    {
                        values.Add(metaData.IsManaged.ToString());
                    }

                    if (withUrls)
                    {
                        var entityMetadata = _source.GetEntityMetadata(relationship.Entity1LogicalName);
                        if (entityMetadata != null)
                        {
                            values.Add(_source.Service.ConnectionData?.GetRelationshipMetadataRelativeUrl(entityMetadata.MetadataId.Value, relationship.MetadataId.Value));
                        }
                    }

                    handlerManyToMany.AddLine(values);

                    var str = handlerManyToMany.GetFormatedLinesWithHeadersInLine(false).FirstOrDefault();

                    return string.Format("{0} {1}", this.ComponentTypeEnum.ToString(), str);
                }
            }

            return solutionComponent.ToString();
        }

        public string GetName(SolutionComponent solutionComponent)
        {
            if (solutionComponent == null || !solutionComponent.ObjectId.HasValue)
            {
                return "null";
            }

            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(solutionComponent.ObjectId.Value);

            if (metaData != null)
            {
                if (metaData is OneToManyRelationshipMetadata)
                {
                    var relationship = metaData as OneToManyRelationshipMetadata;

                    return string.Format("{0}.{1}.{2}"
                        , relationship.ReferencingEntity
                        , relationship.ReferencingAttribute
                        , relationship.SchemaName
                        );
                }
                else if (metaData is ManyToManyRelationshipMetadata)
                {
                    var relationship = metaData as ManyToManyRelationshipMetadata;

                    return string.Format("{0} - {1}.{2}", relationship.Entity1LogicalName, relationship.Entity2LogicalName, relationship.SchemaName);
                }
            }

            return solutionComponent.ObjectId.ToString();
        }

        public string GetDisplayName(SolutionComponent solutionComponent)
        {
            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(solutionComponent.ObjectId.Value);

            if (metaData != null)
            {
                if (metaData is OneToManyRelationshipMetadata)
                {
                    var relationship = metaData as OneToManyRelationshipMetadata;

                    return relationship.AssociatedMenuConfiguration?.Label?.UserLocalizedLabel?.Label;
                }
                else if (metaData is ManyToManyRelationshipMetadata)
                {
                    var relationship = metaData as ManyToManyRelationshipMetadata;

                    return (relationship.Entity1AssociatedMenuConfiguration ?? relationship.Entity1AssociatedMenuConfiguration)?.Label?.UserLocalizedLabel?.Label;
                }
            }

            return null;
        }

        public string GetCustomizableName(SolutionComponent solutionComponent)
        {
            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(solutionComponent.ObjectId.Value);

            if (metaData != null)
            {
                return metaData.IsCustomizable?.Value.ToString();
            }

            return null;
        }

        public string GetManagedName(SolutionComponent solutionComponent)
        {
            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(solutionComponent.ObjectId.Value);

            if (metaData != null)
            {
                return metaData.IsManaged.ToString();
            }

            return null;
        }

        public string GetFileName(string connectionName, Guid objectId, string fieldTitle, string extension)
        {
            RelationshipMetadataBase metaData = _source.GetRelationshipMetadata(objectId);

            if (metaData != null)
            {
                if (metaData is OneToManyRelationshipMetadata)
                {
                    var relationship = metaData as OneToManyRelationshipMetadata;

                    return string.Format("{0}.{1}.{2}"
                        , relationship.ReferencingEntity
                        , relationship.ReferencingAttribute
                        , relationship.SchemaName
                        );
                }
                else if (metaData is ManyToManyRelationshipMetadata)
                {
                    var relationship = metaData as ManyToManyRelationshipMetadata;

                    return string.Format("{0} - {1}.{2}", relationship.Entity1LogicalName, relationship.Entity2LogicalName, relationship.SchemaName);
                }
            }

            return string.Format("{0}.ComponentType {1} - {2} - {3}.{4}", connectionName, this.ComponentTypeValue, objectId, fieldTitle, extension);
        }

        public TupleList<string, string> GetComponentColumns()
        {
            return new TupleList<string, string>();
        }
    }
}