﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Repository
{
    public class WorkflowRepository
    {
        /// <summary>
        /// Сервис CRM
        /// </summary>
        private IOrganizationServiceExtented _service;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        /// <param name="service"></param>
        public WorkflowRepository(IOrganizationServiceExtented service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task<IEnumerable<Workflow>> GetListAsync(string filterEntity, int? category, int? mode, ColumnSet columnSet)
        {
            return Task.Run(() => GetList(filterEntity, category, mode, columnSet));
        }

        private IEnumerable<Workflow> GetList(string filterEntity, int? category, int? mode, ColumnSet columnSet)
        {
            QueryExpression query = new QueryExpression()
            {
                EntityName = Workflow.EntityLogicalName,

                NoLock = true,

                ColumnSet = columnSet ?? new ColumnSet(true),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Workflow.Schema.Attributes.componentstate, ConditionOperator.In
                            , (int)ComponentState.Published
                            , (int)ComponentState.Unpublished
                        ),
                        new ConditionExpression(Workflow.Schema.Attributes.parentworkflowid, ConditionOperator.Null),
                    },
                },

                Orders =
                {
                    new OrderExpression(Workflow.Schema.Attributes.primaryentity, OrderType.Ascending),
                    new OrderExpression(Workflow.Schema.Attributes.name, OrderType.Ascending),
                },

                PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = 5000,
                },
            };

            if (!string.IsNullOrEmpty(filterEntity))
            {
                query.Criteria.Conditions.Add(new ConditionExpression(Workflow.Schema.Attributes.primaryentity, ConditionOperator.Equal, filterEntity));
            }

            if (category.HasValue)
            {
                query.Criteria.Conditions.Add(new ConditionExpression(Workflow.Schema.Attributes.category, ConditionOperator.Equal, category.Value));
            }

            if (mode.HasValue)
            {
                query.Criteria.Conditions.Add(new ConditionExpression(Workflow.Schema.Attributes.mode, ConditionOperator.Equal, mode.Value));
            }

            var result = new List<Workflow>();

            try
            {
                while (true)
                {
                    var coll = _service.RetrieveMultiple(query);

                    result.AddRange(coll.Entities.Select(e => e.ToEntity<Workflow>()));

                    if (!coll.MoreRecords)
                    {
                        break;
                    }

                    query.PageInfo.PagingCookie = coll.PagingCookie;
                    query.PageInfo.PageNumber++;
                }
            }
            catch (Exception ex)
            {
                Helpers.DTEHelper.WriteExceptionToOutput(_service.ConnectionData, ex);
            }

            return result;
        }

        public Task<List<Workflow>> GetListByIdListAsync(IEnumerable<Guid> ids, ColumnSet columnSet)
        {
            return Task.Run(() => GetListByIdList(ids, columnSet));
        }

        private List<Workflow> GetListByIdList(IEnumerable<Guid> ids, ColumnSet columnSet)
        {
            QueryExpression query = new QueryExpression()
            {
                EntityName = Workflow.EntityLogicalName,

                NoLock = true,

                ColumnSet = columnSet ?? new ColumnSet(true),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Workflow.Schema.Attributes.componentstate, ConditionOperator.In
                            , (int)ComponentState.Published
                            , (int)ComponentState.Unpublished
                        ),
                        new ConditionExpression(Workflow.Schema.Attributes.parentworkflowid, ConditionOperator.Null),

                        new ConditionExpression(Workflow.Schema.EntityPrimaryIdAttribute, ConditionOperator.In, ids.ToArray()),
                    },
                },

                Orders =
                {
                    new OrderExpression(Workflow.Schema.Attributes.primaryentity, OrderType.Ascending),
                    new OrderExpression(Workflow.Schema.Attributes.name, OrderType.Ascending),
                },

                PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = 5000,
                },
            };

            var result = new List<Workflow>();

            try
            {
                while (true)
                {
                    var coll = _service.RetrieveMultiple(query);

                    result.AddRange(coll.Entities.Select(e => e.ToEntity<Workflow>()));

                    if (!coll.MoreRecords)
                    {
                        break;
                    }

                    query.PageInfo.PagingCookie = coll.PagingCookie;
                    query.PageInfo.PageNumber++;
                }
            }
            catch (Exception ex)
            {
                Helpers.DTEHelper.WriteExceptionToOutput(_service.ConnectionData, ex);
            }

            return result;
        }

        public Task<Workflow> GetByIdAsync(Guid idWorkflow, ColumnSet columnSet = null)
        {
            return Task.Run(() => GetById(idWorkflow, columnSet));
        }

        private Workflow GetById(Guid idWorkflow, ColumnSet columnSet)
        {
            QueryExpression query = new QueryExpression()
            {
                NoLock = true,

                TopCount = 2,

                EntityName = Workflow.EntityLogicalName,

                ColumnSet = columnSet ?? new ColumnSet(true),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Workflow.EntityPrimaryIdAttribute, ConditionOperator.Equal, idWorkflow),
                    },
                },
            };

            return _service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<Workflow>()).SingleOrDefault();
        }

        public Workflow FindLinkedWorkflow(Guid idSystemForm, ColumnSet columnSet)
        {
            QueryExpression query = new QueryExpression()
            {
                NoLock = true,

                TopCount = 2,

                EntityName = Workflow.EntityLogicalName,

                ColumnSet = columnSet ?? new ColumnSet(true),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Workflow.Schema.Attributes.parentworkflowid, ConditionOperator.Null),
                        new ConditionExpression(Workflow.Schema.Attributes.formid, ConditionOperator.Equal, idSystemForm),
                    },
                },
            };

            var coll = _service.RetrieveMultiple(query).Entities;

            return coll.Count == 1 ? coll.Select(e => e.ToEntity<Workflow>()).SingleOrDefault() : null;
        }

        public Task<List<Workflow>> GetListForEntitiesAsync(string[] entities, ColumnSet columnSet)
        {
            return Task.Run(() => GetListForEntities(entities, columnSet));
        }

        private List<Workflow> GetListForEntities(string[] entities, ColumnSet columnSet)
        {
            QueryExpression query = new QueryExpression()
            {
                EntityName = Workflow.EntityLogicalName,

                NoLock = true,

                ColumnSet = columnSet ?? new ColumnSet(true),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Workflow.Schema.Attributes.parentworkflowid, ConditionOperator.Null),
                        new ConditionExpression(Workflow.Schema.Attributes.componentstate, ConditionOperator.In
                            , (int)ComponentState.Published
                            , (int)ComponentState.Unpublished
                        ),
                        new ConditionExpression(Workflow.Schema.Attributes.primaryentity, ConditionOperator.In, entities),

                        new ConditionExpression(Workflow.Schema.Attributes.category, ConditionOperator.Equal, (int)Workflow.Schema.OptionSets.category.Business_Rule_2),
                    },
                },

                Orders =
                {
                    new OrderExpression(Workflow.Schema.Attributes.primaryentity, OrderType.Ascending),
                    new OrderExpression(Workflow.Schema.Attributes.name, OrderType.Ascending),
                },

                PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = 5000,
                },
            };

            var result = new List<Workflow>();

            try
            {
                while (true)
                {
                    var coll = _service.RetrieveMultiple(query);

                    result.AddRange(coll.Entities.Select(e => e.ToEntity<Workflow>()));

                    if (!coll.MoreRecords)
                    {
                        break;
                    }

                    query.PageInfo.PagingCookie = coll.PagingCookie;
                    query.PageInfo.PageNumber++;
                }
            }
            catch (Exception ex)
            {
                Helpers.DTEHelper.WriteExceptionToOutput(_service.ConnectionData, ex);
            }

            return result;
        }

        public static IEnumerable<DataGridColumn> GetDataGridColumn()
        {
            //<DataGridTextColumn Header="Entity Name" Width="260" Binding="{Binding EntityName}" />

            //<DataGridTextColumn Header="Category" Width="200" Binding="{Binding Category}" />

            //<DataGridTextColumn Header="Workflow Name" Width="200" Binding="{Binding WorkflowName}" />

            //<DataGridTextColumn Header="Mode" Width="150" Binding="{Binding ModeName}" />

            //<DataGridTextColumn Header="Status" Width="150" Binding="{Binding StatusName}" />

            return new List<DataGridColumn>()
            {
                new DataGridTextColumn()
                {
                    Header = "Entity Name",
                    Width = new DataGridLength(260),
                    Binding = new Binding
                    {
                        Path = new PropertyPath("PrimaryEntity"),
                        Mode = BindingMode.OneTime,
                    },
                },

                new DataGridTextColumn()
                {
                    Header = "Category",
                    Width = new DataGridLength(200),
                    Binding = new Binding
                    {
                        Path = new PropertyPath("FormattedValues[category]"),
                        Mode = BindingMode.OneTime,
                    },
                },

                new DataGridTextColumn()
                {
                    Header = "Name",
                    Width = new DataGridLength(200),
                    Binding = new Binding
                    {
                        Path = new PropertyPath("Name"),
                        Mode = BindingMode.OneTime,
                    },
                },

                new DataGridTextColumn()
                {
                    Header = "Mode",
                    Width = new DataGridLength(150),
                    Binding = new Binding
                    {
                        Path = new PropertyPath("FormattedValues[mode]"),
                        Mode = BindingMode.OneTime,
                    },
                },

                new DataGridTextColumn()
                {
                    Header = "Status",
                    Width = new DataGridLength(150),
                    Binding = new Binding
                    {
                        Path = new PropertyPath("FormattedValues[statuscode]"),
                        Mode = BindingMode.OneTime,
                    },
                },
            };
        }
    }
}
