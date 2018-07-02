﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Repository
{
    public class SystemFormRepository
    {
        /// <summary>
        /// Сервис CRM
        /// </summary>
        private IOrganizationServiceExtented _Service { get; set; }

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        /// <param name="service"></param>
        public SystemFormRepository(IOrganizationServiceExtented service)
        {
            _Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task<List<SystemForm>> GetListAsync(string filterEntity = null, ColumnSet columnSet = null)
        {
            return Task.Run(() => GetList(filterEntity, columnSet));
        }

        /// <summary>
        /// Получить все представления
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private List<SystemForm> GetList(string filterEntity, ColumnSet columnSet)
        {
            QueryExpression query = new QueryExpression()
            {
                EntityName = SystemForm.EntityLogicalName,

                NoLock = true,

                ColumnSet = columnSet ?? new ColumnSet(true),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(SystemForm.Schema.Attributes.componentstate, ConditionOperator.In, 0, 1),
                    },
                },

                Orders =
                {
                    new OrderExpression(SystemForm.Schema.Attributes.objecttypecode, OrderType.Ascending),
                    new OrderExpression(SystemForm.Schema.Attributes.type, OrderType.Ascending),
                    new OrderExpression(SystemForm.Schema.Attributes.name, OrderType.Ascending),
                },

                PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = 5000,
                },
            };

            if (!string.IsNullOrEmpty(filterEntity))
            {
                query.Criteria.Conditions.Add(new ConditionExpression(SystemForm.Schema.Attributes.objecttypecode, ConditionOperator.Equal, filterEntity));
            }

            var result = new List<SystemForm>();

            try
            {
                while (true)
                {
                    var coll = _Service.RetrieveMultiple(query);

                    result.AddRange(coll.Entities.Select(e => e.ToEntity<SystemForm>()));

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
                Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.DTEHelper.WriteExceptionToOutput(ex);
            }

            result = result.Where(ent =>
                !ent.Contains(SystemForm.Schema.Attributes.formactivationstate) || (ent.Contains(SystemForm.Schema.Attributes.formactivationstate) && ent.GetAttributeValue<OptionSetValue>(SystemForm.Schema.Attributes.formactivationstate).Value == 1)
                ).ToList();

            return result;
        }

        public Task<SystemForm> GetByIdAsync(Guid idSystemForm, ColumnSet columnSet)
        {
            return Task.Run(() => GetById(idSystemForm, columnSet));
        }

        private SystemForm GetById(Guid idSystemForm, ColumnSet columnSet)
        {
            QueryExpression query = new QueryExpression()
            {
                EntityName = SystemForm.EntityLogicalName,

                NoLock = true,

                ColumnSet = columnSet ?? new ColumnSet(true),

                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(SystemForm.PrimaryIdAttribute, ConditionOperator.Equal, idSystemForm),
                    },
                },
            };

            return _Service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<SystemForm>()).FirstOrDefault();
        }
    }
}
