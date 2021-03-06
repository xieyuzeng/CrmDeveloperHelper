﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.ProxyClassGeneration.Interfaces;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.ProxyClassGeneration.Implementations
{
    internal sealed class NamingService : INamingService
    {
        private static Regex nameRegex = new Regex("[a-z0-9_]*", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private string _typeName;
        private readonly string _serviceContextName;

        private readonly Dictionary<string, int> _nameMap;
        private readonly Dictionary<string, string> _knowNames;

        private readonly List<string> _reservedAttributeNames;

        private readonly CreateFileCSharpConfiguration _config;

        public ICodeGenerationServiceProvider iCodeGenerationServiceProvider { get; set; }

        internal NamingService(string serviceContextName, CreateFileCSharpConfiguration config)
        {
            this._serviceContextName = string.IsNullOrWhiteSpace(serviceContextName) ? typeof(OrganizationServiceContext).Name + "1" : serviceContextName;
            this._config = config;

            this._nameMap = new Dictionary<string, int>();
            this._knowNames = new Dictionary<string, string>();
            this._reservedAttributeNames = new List<string>();

            foreach (MemberInfo property in typeof(Entity).GetProperties())
            {
                this._reservedAttributeNames.Add(property.Name);
            }
        }

        public string GetNameForOptionSet(EntityMetadata entityMetadata, OptionSetMetadata optionSetMetadata)
        {
            if (this._knowNames.ContainsKey(optionSetMetadata.MetadataId.Value.ToString()))
            {
                return this._knowNames[optionSetMetadata.MetadataId.Value.ToString()];
            }

            string name = GetNameForOptionSetInternal(entityMetadata, optionSetMetadata);

            name = this.CreateValidTypeName(name);

            this._knowNames.Add(optionSetMetadata.MetadataId.Value.ToString(), name);

            return name;
        }

        private string GetNameForOptionSetInternal(EntityMetadata entityMetadata, OptionSetMetadata optionSetMetadata)
        {
            if (optionSetMetadata.OptionSetType == OptionSetType.State)
            {
                return entityMetadata.SchemaName + "_StateCode";
            }

            if (optionSetMetadata.OptionSetType == OptionSetType.Status)
            {
                return entityMetadata.SchemaName + "_StatusCode";
            }

            return optionSetMetadata.Name;
        }

        public string GetNameForOption(OptionSetMetadata optionSetMetadata, OptionMetadata optionMetadata)
        {
            if (this._knowNames.ContainsKey(optionSetMetadata.MetadataId.Value.ToString() + optionMetadata.Value.Value.ToString(CultureInfo.InvariantCulture)))
            {
                return this._knowNames[optionSetMetadata.MetadataId.Value.ToString() + optionMetadata.Value.Value.ToString(CultureInfo.InvariantCulture)];
            }

            string name = string.Empty;

            StateOptionMetadata stateOptionMetadata = optionMetadata as StateOptionMetadata;

            if (stateOptionMetadata != null)
            {
                name = stateOptionMetadata.InvariantName;
            }
            else
            {
                foreach (LocalizedLabel localizedLabel in optionMetadata.Label.LocalizedLabels)
                {
                    if (localizedLabel.LanguageCode == 1033)
                    {
                        name = localizedLabel.Label;
                    }
                }
            }

            if (string.IsNullOrEmpty(name))
            {
                name = string.Format(CultureInfo.InvariantCulture, "UnknownLabel{0}", (object)optionMetadata.Value.Value);
            }

            string validName = NamingService.CreateValidName(name);

            this._knowNames.Add(optionSetMetadata.MetadataId.Value.ToString() + optionMetadata.Value.Value.ToString(CultureInfo.InvariantCulture), validName);

            return validName;
        }

        public string GetNameForEntity(EntityMetadata entityMetadata)
        {
            if (this._knowNames.ContainsKey(entityMetadata.MetadataId.Value.ToString()))
            {
                return this._knowNames[entityMetadata.MetadataId.Value.ToString()];
            }

            string validTypeName = this.CreateValidTypeName(string.IsNullOrEmpty(StaticNamingService.GetNameForEntity(entityMetadata)) ? entityMetadata.SchemaName : StaticNamingService.GetNameForEntity(entityMetadata));

            this._knowNames.Add(entityMetadata.MetadataId.Value.ToString(), validTypeName);

            return validTypeName;
        }

        public string GetNameForAttributeAsEntityProperty(EntityMetadata entityMetadata, AttributeMetadata attributeMetadata)
        {
            if (this._knowNames.ContainsKey(entityMetadata.MetadataId.Value.ToString() + attributeMetadata.MetadataId.Value))
            {
                return this._knowNames[entityMetadata.MetadataId.Value.ToString() + attributeMetadata.MetadataId.Value];
            }

            var validName = GetNameForAttribute(entityMetadata, attributeMetadata);

            validName = ResolveConflictName(validName, s => this._reservedAttributeNames.Contains(s) || string.Equals(s, _typeName, StringComparison.InvariantCultureIgnoreCase));

            this._knowNames.Add(entityMetadata.MetadataId.Value.ToString() + attributeMetadata.MetadataId.Value, validName);

            return validName;
        }

        private string ResolveConflictName(string validName, Func<string, bool> conflictChecker)
        {
            int index = 0;
            string result = validName;

            while (conflictChecker(result))
            {
                index++;

                result = string.Format("{0}{1}", validName, index);
            }

            return result;
        }

        public string GetNameForAttribute(EntityMetadata entityMetadata, AttributeMetadata attributeMetadata)
        {
            string validName = NamingService.CreateValidName(StaticNamingService.GetNameForAttribute(attributeMetadata.LogicalName) ?? attributeMetadata.SchemaName);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            if (!provider.IsValidIdentifier(validName))
            {
                validName = "@" + validName;
            }

            return validName;
        }

        public string GetNameForRelationship(EntityMetadata entityMetadata, RelationshipMetadataBase relationshipMetadata, EntityRole? reflexiveRole)
        {
            string str = reflexiveRole.HasValue ? reflexiveRole.Value.ToString() : string.Empty;

            string keyRelationship = entityMetadata.MetadataId.Value.ToString() + relationshipMetadata.MetadataId.Value + str;

            if (this._knowNames.ContainsKey(keyRelationship))
            {
                return this._knowNames[keyRelationship];
            }

            string validName = NamingService.CreateValidName(relationshipMetadata.SchemaName + (!reflexiveRole.HasValue ? string.Empty : reflexiveRole.Value == EntityRole.Referenced ? "_Referenced" : "_Referencing"));

            Dictionary<string, string> knownNamesForEntityMetadata = this._knowNames.Where(d => d.Key.StartsWith(entityMetadata.MetadataId.Value.ToString())).ToDictionary(d => d.Key, d => d.Value);

            validName = ResolveConflictName(validName, s => this._reservedAttributeNames.Contains(s) || knownNamesForEntityMetadata.ContainsValue(s) || string.Equals(s, _typeName, StringComparison.InvariantCultureIgnoreCase));

            this._knowNames.Add(keyRelationship, validName);

            return validName;
        }

        public string GetNameForServiceContext()
        {
            return this._serviceContextName;
        }

        public string GetNameForEntitySet(EntityMetadata entityMetadata)
        {
            return iCodeGenerationServiceProvider.NamingService.GetNameForEntity(entityMetadata) + "Set";
        }

        public string GetNameForMessagePair(CodeGenerationSdkMessagePair messagePair)
        {
            if (this._knowNames.ContainsKey(messagePair.Id.ToString()))
            {
                return this._knowNames[messagePair.Id.ToString()];
            }

            string validTypeName = this.CreateValidTypeName(messagePair.Request.Name);

            this._knowNames.Add(messagePair.Id.ToString(), validTypeName);

            return validTypeName;
        }

        public string GetNameForRequestField(CodeGenerationSdkMessageRequest request, Entities.SdkMessageRequestField requestField)
        {
            if (this._knowNames.ContainsKey(request.Id.ToString() + requestField.Position.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)))
            {
                return this._knowNames[request.Id.ToString() + requestField.Position.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)];
            }

            string validName = NamingService.CreateValidName(requestField.Name);

            this._knowNames.Add(request.Id.ToString() + requestField.Position.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), validName);

            return validName;
        }

        public string GetNameForResponseField(CodeGenerationSdkMessageResponse response, Entities.SdkMessageResponseField responseField)
        {
            if (this._knowNames.ContainsKey(response.Id.ToString() + responseField.Position.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)))
            {
                return this._knowNames[response.Id.ToString() + responseField.Position.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)];
            }

            string validName = NamingService.CreateValidName(responseField.Name);
            this._knowNames.Add(response.Id.ToString() + responseField.Position.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), validName);
            return validName;
        }

        private string CreateValidTypeName(string name)
        {
            string validName = NamingService.CreateValidName(name);

            if (this._nameMap.ContainsKey(validName))
            {
                int num = ++this._nameMap[validName];
                return string.Format(CultureInfo.InvariantCulture, "{0}{1}", validName, num);
            }

            this._nameMap.Add(name, 0);

            return validName;
        }

        private static string CreateValidName(string name)
        {
            string input = name.Replace("$", "CurrencySymbol_").Replace("(", "_");

            StringBuilder stringBuilder = new StringBuilder();
            for (Match match = NamingService.nameRegex.Match(input); match.Success; match = match.NextMatch())
            {
                stringBuilder.Append(match.Value);
            }

            return stringBuilder.ToString();
        }

        public IEnumerable<string> GetCommentsForEntity(EntityMetadata entityMetadata)
        {
            List<string> comments = new List<string>();

            CreateFileHandler.FillLabelEntity(comments, _config.AllDescriptions, entityMetadata.DisplayName, entityMetadata.DisplayCollectionName, entityMetadata.Description, _config.TabSpacer);

            return comments;
        }

        public IEnumerable<string> GetCommentsForEntityDefaultConstructor(EntityMetadata entityMetadata)
        {
            return new[] { $"Default Constructor {entityMetadata.LogicalName}" };
        }

        public IEnumerable<string> GetCommentsForEntityAnonymousConstructor(EntityMetadata entityMetadata)
        {
            return new[] { $"Constructor {entityMetadata.LogicalName} for populating via LINQ queries given a LINQ anonymous type object" };
        }

        public IEnumerable<string> GetCommentsForAttribute(EntityMetadata entityMetadata, AttributeMetadata attributeMetadata)
        {
            var listStrings = CreateFileHandler.UnionStrings(attributeMetadata.DisplayName, attributeMetadata.Description, null, null, _config.AllDescriptions, _config.TabSpacer);

            return listStrings;
        }

        public IEnumerable<string> GetCommentsForRelationshipOneToMany(EntityMetadata entityMetadata, OneToManyRelationshipMetadata relationshipMetadata, EntityRole? reflexiveRole)
        {
            return new[] { "1:N " + relationshipMetadata.SchemaName };
        }

        public IEnumerable<string> GetCommentsForRelationshipManyToOne(EntityMetadata entityMetadata, OneToManyRelationshipMetadata relationshipMetadata, EntityRole? reflexiveRole)
        {
            return new[] { "N:1 " + relationshipMetadata.SchemaName };
        }

        public IEnumerable<string> GetCommentsForRelationshipManyToMany(EntityMetadata entityMetadata, ManyToManyRelationshipMetadata relationshipMetadata, EntityRole? reflexiveRole)
        {
            return new[] { "N:N " + relationshipMetadata.SchemaName };
        }

        public IEnumerable<string> GetCommentsForServiceContext()
        {
            return new[] { "Represents a source of entities bound to a CRM service. It tracks and manages changes made to the retrieved entities." };
        }

        public IEnumerable<string> GetCommentsForEntitySet(EntityMetadata entityMetadata)
        {
            CodeTypeReference typeForEntity = iCodeGenerationServiceProvider.TypeMappingService.GetTypeForEntity(entityMetadata);

            return new[] { string.Format(CultureInfo.InvariantCulture, "Gets a binding to the set of all <see cref=\"{0}\"/> entities.", typeForEntity.BaseType) };
        }

        public IEnumerable<string> GetCommentsForServiceContextConstructor()
        {
            return new[] { "Constructor" };
        }

        public IEnumerable<string> GetCommentsForOptionSet(EntityMetadata entityMetadata, OptionSetMetadata optionSetMetadata)
        {
            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetCommentsForOption(OptionSetMetadata optionSetMetadata, OptionMetadata optionMetadata)
        {
            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetCommentsForMessagePair(CodeGenerationSdkMessagePair messagePair)
        {
            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetCommentsForRequestField(CodeGenerationSdkMessageRequest request, Entities.SdkMessageRequestField requestField)
        {
            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetCommentsForResponseField(CodeGenerationSdkMessageResponse response, Entities.SdkMessageResponseField responseField)
        {
            return Enumerable.Empty<string>();
        }

        public void SetCurrentTypeName(string typeName)
        {
            this._typeName = typeName;
        }
    }
}
