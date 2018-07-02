﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Repository
{
    public class EntityMetadataRepository
    {
        /// <summary>
        /// Сервис CRM
        /// </summary>
        private IOrganizationServiceExtented _Service { get; set; }

        /// <summary>
        /// Конструктор репозитория функция по поиску издателей.
        /// </summary>
        /// <param name="service"></param>
        public EntityMetadataRepository(IOrganizationServiceExtented service)
        {
            _Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private List<EntityMetadata> GetEntitiesDisplayName()
        {
            //RetrieveAllEntitiesRequest raer = new RetrieveAllEntitiesRequest() { EntityFilters = flags };

            //RetrieveAllEntitiesResponse resp = (RetrieveAllEntitiesResponse)_Service.Execute(raer);

            //return resp.EntityMetadata.OrderBy(ent => ent.LogicalName).ToList();

            MetadataPropertiesExpression entityProperties = new MetadataPropertiesExpression("LogicalName", "DisplayName", "Description", "DisplayCollectionName", "OwnershipType", "IsIntersect", "ObjectTypeCode")
            {
                AllProperties = false
            };

            EntityQueryExpression entityQueryExpression = new EntityQueryExpression()
            {
                Properties = entityProperties,
            };

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest()
            {
                Query = entityQueryExpression,
            };

            RetrieveMetadataChangesResponse response = (RetrieveMetadataChangesResponse)_Service.Execute(request);

            return response.EntityMetadata.OrderBy(ent => ent.LogicalName).ToList();
        }

        public Task<List<EntityMetadata>> GetEntitiesDisplayNameAsync()
        {
            return Task<List<EntityMetadata>>.Run(() => GetEntitiesDisplayName());
        }

        public Task<List<EntityMetadata>> GetEntitiesWithAttributesAndRelationshipsAsync()
        {
            return Task.Run(() => GetEntitiesWithAttributesAndRelationships());
        }

        private List<EntityMetadata> GetEntitiesWithAttributesAndRelationships()
        {
            MetadataPropertiesExpression relationshipProperties = new MetadataPropertiesExpression(
                "SchemaName",
                "ReferencedEntity",
                "ReferencingEntity",
                "ReferencedAttribute",
                "ReferencingAttribute",
                "Entity1LogicalName",
                "Entity2LogicalName",
                "Entity1IntersectAttribute",
                "Entity2IntersectAttribute"
                )
            { AllProperties = false };

            EntityQueryExpression entityQueryExpression = new EntityQueryExpression()
            {
                Properties = new MetadataPropertiesExpression() { AllProperties = true },

                AttributeQuery = new AttributeQueryExpression()
                {
                    Properties = new MetadataPropertiesExpression("AttributeOf", "LogicalName", "EntityLogicalName")
                },

                RelationshipQuery = new RelationshipQueryExpression()
                {
                    Properties = relationshipProperties
                },
            };

            var isEntityKeyExists = _Service.IsRequestExists(SdkMessageRequest.Instances.RetrieveEntityKeyRequest);

            if (isEntityKeyExists)
            {
                entityQueryExpression.KeyQuery = new EntityKeyQueryExpression() { Properties = new MetadataPropertiesExpression("EntityLogicalName", "LogicalName", "KeyAttributes") };
            }

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest()
            {
                Query = entityQueryExpression,
            };

            RetrieveMetadataChangesResponse response = (RetrieveMetadataChangesResponse)_Service.Execute(request);

            var result = response.EntityMetadata.OrderBy(ent => ent.LogicalName).ToList();

            return result;
        }

        public Task<List<EntityMetadata>> GetEntitiesWithAttributesAsync()
        {
            return Task.Run(() => GetEntitiesWithAttributes());
        }

        private List<EntityMetadata> GetEntitiesWithAttributes()
        {
            MetadataPropertiesExpression entityProperties = new MetadataPropertiesExpression("LogicalName", "DisplayName", "Description", "DisplayCollectionName", "OwnershipType", "Attributes", "ObjectTypeCode")
            {
                AllProperties = false
            };

            MetadataPropertiesExpression attributeProperties = new MetadataPropertiesExpression("AttributeOf", "LogicalName", "EntityLogicalName", "OptionSet")
            {
                AllProperties = false
            };

            EntityQueryExpression entityQueryExpression = new EntityQueryExpression()
            {
                Properties = entityProperties,

                AttributeQuery = new AttributeQueryExpression()
                {
                    Properties = attributeProperties
                },
            };

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest()
            {
                Query = entityQueryExpression,
            };

            RetrieveMetadataChangesResponse response = (RetrieveMetadataChangesResponse)_Service.Execute(request);

            return response.EntityMetadata.OrderBy(ent => ent.LogicalName).ToList();
        }

        public EntityMetadata GetEntityMetadata(string entityName)
        {
            try
            {
                MetadataFilterExpression entityFilter = new MetadataFilterExpression(LogicalOperator.And);
                entityFilter.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityName));

                var entityQueryExpression = new EntityQueryExpression()
                {
                    Properties = new MetadataPropertiesExpression() { AllProperties = true },
                    AttributeQuery = new AttributeQueryExpression() { Properties = new MetadataPropertiesExpression() { AllProperties = true } },
                    RelationshipQuery = new RelationshipQueryExpression() { Properties = new MetadataPropertiesExpression() { AllProperties = true } },
                    LabelQuery = new LabelQueryExpression(),

                    Criteria = entityFilter,
                };

                var isEntityKeyExists = _Service.IsRequestExists(SdkMessageRequest.Instances.RetrieveEntityKeyRequest);

                if (isEntityKeyExists)
                {
                    entityQueryExpression.KeyQuery = new EntityKeyQueryExpression() { Properties = new MetadataPropertiesExpression() { AllProperties = true } };
                }

                var response = (RetrieveMetadataChangesResponse)_Service.Execute(
                    new RetrieveMetadataChangesRequest()
                    {
                        ClientVersionStamp = null,
                        Query = entityQueryExpression,
                    }
                );

                return response.EntityMetadata.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Helpers.DTEHelper.WriteExceptionToOutput(ex);

                return null;
            }
        }

        public bool IsEntityExists(string entityName)
        {
            try
            {
                MetadataFilterExpression entityFilter = new MetadataFilterExpression(LogicalOperator.And);
                entityFilter.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityName));

                var entityQueryExpression = new EntityQueryExpression()
                {
                    Criteria = entityFilter,
                };

                var response = (RetrieveMetadataChangesResponse)_Service.Execute(
                    new RetrieveMetadataChangesRequest()
                    {
                        ClientVersionStamp = null,
                        Query = entityQueryExpression,
                    }
                );

                return response.EntityMetadata.FirstOrDefault() != null;
            }
            catch (Exception ex)
            {
                Helpers.DTEHelper.WriteExceptionToOutput(ex);

                return false;
            }
        }

        private List<EntityMetadata> GetEntitiesProperties(string entityName, int? entityTypeCode, params string[] properties)
        {
            MetadataPropertiesExpression entityProperties = new MetadataPropertiesExpression(properties)
            {
                AllProperties = false
            };

            EntityQueryExpression entityQueryExpression = new EntityQueryExpression()
            {
                Properties = entityProperties,

                AttributeQuery = new AttributeQueryExpression()
                {
                    Properties = new MetadataPropertiesExpression("LogicalName", "AttributeType"),
                },
            };

            if (!string.IsNullOrEmpty(entityName) || entityTypeCode.HasValue)
            {
                var criteria = new MetadataFilterExpression(LogicalOperator.Or);

                if (!string.IsNullOrEmpty(entityName))
                {
                    criteria.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityName));
                }

                if (entityTypeCode.HasValue)
                {
                    criteria.Conditions.Add(new MetadataConditionExpression("ObjectTypeCode", MetadataConditionOperator.Equals, entityTypeCode.Value));
                }

                entityQueryExpression.Criteria = criteria;
            }

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest()
            {
                Query = entityQueryExpression,
            };

            RetrieveMetadataChangesResponse response = (RetrieveMetadataChangesResponse)_Service.Execute(request);

            if (response.EntityMetadata.Any())
            {
                return response.EntityMetadata.OrderBy(ent => ent.LogicalName).ToList();
            }
            else if (!string.IsNullOrEmpty(entityName) || entityTypeCode.HasValue)
            {
                return GetEntitiesProperties(null, null, properties);
            }

            return new List<EntityMetadata>();
        }

        public Task<List<EntityMetadata>> GetEntitiesPropertiesAsync(string entityName, int? entityTypeCode, params string[] properties)
        {
            return Task<List<EntityMetadata>>.Run(() => GetEntitiesProperties(entityName, entityTypeCode, properties));
        }

        public Task ExportEntityXmlAsync(string entityName, string filePath)
        {
            return Task.Run(() => ExportEntityXml(entityName, filePath));
        }

        private void ExportEntityXml(string entityName, string filePath)
        {
            var request = new OrganizationRequest("RetrieveEntityXml");
            request.Parameters["EntityName"] = entityName;

            var response = _Service.Execute(request);

            if (response.Results.ContainsKey("EntityXml"))
            {
                var text = response.Results["EntityXml"].ToString();

                if (ContentCoparerHelper.TryParseXml(text, out var doc))
                {
                    text = doc.ToString();
                }

                File.WriteAllText(filePath, text, Encoding.UTF8);
            }
        }
    }
}
