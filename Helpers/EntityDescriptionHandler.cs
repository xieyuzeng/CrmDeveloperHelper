﻿using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers
{
    public static class EntityDescriptionHandler
    {
        public static Task<string> GetEntityDescriptionAsync(Entity entity, ICollection<string> attributeToIgnore)
        {
            return Task.Run(() => GetEntityDescription(entity, attributeToIgnore));
        }

        private static string GetEntityDescription(Entity entity, ICollection<string> attributeToIgnore)
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Entity: {0}", entity.LogicalName).AppendLine();
            result.AppendFormat("Id: {0}", entity.Id).AppendLine();

            result.AppendLine();

            FormatTextTableHandler table = new FormatTextTableHandler();
            table.SetHeader("Field Name", "Type", "Value");

            foreach (var attr in entity.Attributes.OrderBy(s => s.Key))
            {
                if (attributeToIgnore != null)
                {
                    if (attributeToIgnore.Contains(attr.Key))
                    {
                        continue;
                    }
                }

                table.AddLine(attr.Key, GetAttributeType(attr.Value), GetAttributeString(entity, attr.Key));
            }

            table.GetFormatedLines(false).ForEach(s => result.AppendLine(s));

            return result.ToString();
        }

        private static string GetAttributeType(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is AliasedValue)
            {
                var aliased = value as AliasedValue;

                if (aliased.Value != null)
                {
                    return value.GetType().Name + "." + GetAttributeType(aliased.Value);
                }
            }

            return value.GetType().Name;
        }

        public static object GetUnderlyingValue(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is AliasedValue aliased)
            {
                if (aliased.Value == null)
                {
                    return null;
                }

                return GetUnderlyingValue(aliased.Value);
            }

            return value;
        }

        private static string GetAttributeStringInternal(Entity entity, string key, Func<FormattedValueCollection, string, object, string> getterString)
        {
            if (!entity.Attributes.Contains(key))
            {
                return string.Empty;
            }

            var value = entity.Attributes[key];

            if (value is EntityReference)
            {
                var reference = (EntityReference)value;

                if (reference.Id == Guid.Empty)
                {
                    entity.Attributes.Remove(key);
                    return string.Empty;
                }
            }

            if (value == null)
            {
                return string.Empty;
            }

            return getterString(entity.FormattedValues, key, value);
        }

        public static string GetAttributeString(Entity entity, string key)
        {
            return GetAttributeStringInternal(entity, key, GetValueStringFull);
        }

        public static string GetAttributeStringShortEntityReferenceAndPicklist(Entity entity, string key)
        {
            return GetAttributeStringInternal(entity, key, GetValueStringShortEntityReferenceAndPicklist);
        }

        private static string GetValueStringFull(FormattedValueCollection formattedValues, string key, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is EntityReference entityReference)
            {
                return string.Format("{0} - {1} - {2}", entityReference.LogicalName, entityReference.Name, entityReference.Id.ToString());
            }

            if (value is BooleanManagedProperty booleanManaged)
            {
                return string.Format("{0}    CanBeChanged = {1}", booleanManaged.Value, booleanManaged.CanBeChanged);
            }

            if (value is OptionSetValue optionSetValue)
            {
                return optionSetValue.Value.ToString() + (formattedValues.ContainsKey(key) ? string.Format(" - {0}", formattedValues[key]) : string.Empty);
            }

            if (value is Money money)
            {
                return money.Value.ToString();
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
            }

            if (value is AliasedValue aliasedValue)
            {
                return GetValueStringFull(formattedValues, key, aliasedValue.Value);
            }

            return value.ToString();
        }

        private static string GetValueStringShortEntityReferenceAndPicklist(FormattedValueCollection formattedValues, string key, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is EntityReference entityReference)
            {
                if (!string.IsNullOrEmpty(entityReference.Name))
                {
                    return entityReference.Name;
                }
                else
                {
                    return string.Format("{0} - {1}", entityReference.LogicalName, entityReference.Id.ToString());
                }
            }

            if (value is BooleanManagedProperty booleanManaged)
            {
                return string.Format("{0}    CanBeChanged = {1}", booleanManaged.Value, booleanManaged.CanBeChanged);
            }

            if (value is OptionSetValue optionSetValue)
            {
                return formattedValues.ContainsKey(key) && !string.IsNullOrEmpty(formattedValues[key]) ? formattedValues[key] : optionSetValue.Value.ToString();
            }

            if (value is Money money)
            {
                return money.Value.ToString();
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
            }

            if (value is AliasedValue aliasedValue)
            {
                return GetValueStringShortEntityReferenceAndPicklist(formattedValues, key, aliasedValue.Value);
            }

            return value.ToString();
        }

        public static string GetAttributeStringShortEntityReference(Entity entity, string key)
        {
            return GetAttributeStringInternal(entity, key, GetValueStringShortEntityReference);
        }

        private static string GetValueStringShortEntityReference(FormattedValueCollection formattedValues, string key, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is EntityReference entityReference)
            {
                if (!string.IsNullOrEmpty(entityReference.Name))
                {
                    return entityReference.Name;
                }
                else
                {
                    return string.Format("{0} - {1}", entityReference.LogicalName, entityReference.Id.ToString());
                }
            }

            if (value is BooleanManagedProperty booleanManagedboolean)
            {
                return string.Format("{0}    CanBeChanged = {1}", booleanManagedboolean.Value, booleanManagedboolean.CanBeChanged);
            }

            if (value is OptionSetValue optionSetValue)
            {
                return optionSetValue.Value.ToString() + (formattedValues.ContainsKey(key) ? string.Format(" - {0}", formattedValues[key]) : string.Empty);
            }

            if (value is Money money)
            {
                return money.Value.ToString();
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
            }

            if (value is AliasedValue aliasedValue)
            {
                return GetValueStringShortEntityReference(formattedValues, key, aliasedValue.Value);
            }

            return value.ToString();
        }

        public static string GetValueStringShortEntityReferenceAndInversePicklist(FormattedValueCollection formattedValues, string key, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is EntityReference entityReference)
            {
                if (!string.IsNullOrEmpty(entityReference.Name))
                {
                    return entityReference.Name;
                }
                else
                {
                    return string.Format("{0} - {1}", entityReference.LogicalName, entityReference.Id.ToString());
                }
            }

            if (value is BooleanManagedProperty booleanManagedboolean)
            {
                return string.Format("{0}    CanBeChanged = {1}", booleanManagedboolean.Value, booleanManagedboolean.CanBeChanged);
            }

            if (value is OptionSetValue optionSetValue)
            {
                return (formattedValues != null && formattedValues.ContainsKey(key) ? string.Format("{0} - ", formattedValues[key]) : string.Empty) + optionSetValue.Value.ToString();
            }

            if (value is Money money)
            {
                return money.Value.ToString();
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
            }

            if (value is AliasedValue aliasedValue)
            {
                return GetValueStringShortEntityReferenceAndInversePicklist(formattedValues, key, aliasedValue.Value);
            }

            return value.ToString();
        }

        public static Task ExportEntityDescriptionAsync(string filePath, Entity entity, ICollection<string> list)
        {
            return Task.Run(() => ExportEntityDescription(filePath, entity, list));
        }

        private static void ExportEntityDescription(string filePath, Entity entity, ICollection<string> list)
        {
            string content = EntityDescriptionHandler.GetEntityDescription(entity, list);

            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
    }
}
