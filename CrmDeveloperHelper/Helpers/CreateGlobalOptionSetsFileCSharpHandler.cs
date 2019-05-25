﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers
{
    public class CreateGlobalOptionSetsFileCSharpHandler : CreateFileHandler
    {
        private readonly IOrganizationServiceExtented _service;
        private readonly CreateFileCSharpConfiguration _config;

        private readonly string _fieldHeader;

        private readonly SolutionComponentDescriptor _descriptor;
        private readonly DependencyDescriptionHandler _descriptorHandler;
        private readonly DependencyRepository _dependencyRepository;

        private readonly StringMapRepository _repositoryStringMap;

        private readonly IWriteToOutput _iWriteToOutput;

        public CreateGlobalOptionSetsFileCSharpHandler(
            IOrganizationServiceExtented service
            , IWriteToOutput iWriteToOutput
            , CreateFileCSharpConfiguration config
        ) : base(config.TabSpacer, config.AllDescriptions)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
            this._iWriteToOutput = iWriteToOutput ?? throw new ArgumentNullException(nameof(iWriteToOutput));

            this._config = config;

            this._descriptor = new SolutionComponentDescriptor(_service)
            {
                WithManagedInfo = config.WithManagedInfo,
            };

            this._dependencyRepository = new DependencyRepository(this._service);
            this._descriptorHandler = new DependencyDescriptionHandler(this._descriptor);
            this._repositoryStringMap = new StringMapRepository(_service);

            if (config.ConstantType == ConstantType.ReadOnlyField)
            {
                _fieldHeader = "static readonly";
            }
            else
            {
                _fieldHeader = "const";
            }
        }

        public Task CreateFileAsync(string filePath, IEnumerable<OptionSetMetadata> optionSets)
        {
            return Task.Run(async () => await this.CreateFile(filePath, optionSets));
        }

        private async Task CreateFile(string filePath, IEnumerable<OptionSetMetadata> optionSets)
        {
            optionSets = optionSets
                .Where(e => e.Options.Any(o => o.Value.HasValue))
                .OrderBy(e => e.Name);

            StartWriting(filePath);

            WriteLine();

            WriteLine("namespace {0}", this._service.ConnectionData.NamespaceOptionSetsCSharp);
            WriteLine("{");

            await WriteRegularOptionSets(optionSets);

            Write("}");

            EndWriting();
        }

        private async Task WriteRegularOptionSets(IEnumerable<OptionSetMetadata> optionSets)
        {
            foreach (var optionSetMetadata in optionSets)
            {
                await GenerateOptionSetEnums(optionSetMetadata);
            }
        }

        private async Task GenerateOptionSetEnums(OptionSetMetadata optionSet)
        {
            var dependent = await _dependencyRepository.GetDependentComponentsAsync((int)ComponentType.OptionSet, optionSet.MetadataId.Value);

            string entityname = null;
            string attributename = null;
            List<StringMap> listStringmap = null;

            if (dependent.Any(e => e.DependentComponentType.Value == (int)ComponentType.Attribute))
            {
                var attr = dependent.FirstOrDefault(e => e.DependentComponentType.Value == (int)ComponentType.Attribute);

                var attributeMetadata = _descriptor.MetadataSource.GetAttributeMetadata(attr.DependentComponentObjectId.Value);

                if (attributeMetadata != null)
                {
                    entityname = attributeMetadata.EntityLogicalName;
                    attributename = attributeMetadata.LogicalName;

                    listStringmap = await _repositoryStringMap.GetListAsync(entityname);
                }
            }

            WriteLine();

            List<string> headers = new List<string>();

            string temp = string.Format("OptionSet Name: {0}      IsCustomOptionSet: {1}", optionSet.Name, optionSet.IsCustomOptionSet);
            if (this._config.WithManagedInfo)
            {
                temp += string.Format("      IsManaged: {0}", optionSet.IsManaged);
            }
            headers.Add(temp);

            if (this._config.WithDependentComponents)
            {
                var desc = await _descriptorHandler.GetDescriptionDependentAsync(dependent);

                var split = desc.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Any())
                {
                    headers.Add(string.Empty);

                    foreach (var item in split)
                    {
                        headers.Add(item);
                    }
                }
            }

            WriteSummary(optionSet.DisplayName, optionSet.Description, null, headers);

            var options = CreateFileHandler.GetOptionItems(entityname, attributename, optionSet, listStringmap);

            {
                bool ignore = CreateFileHandler.IgnoreGlobalOptionSet(optionSet.Name) || !options.Any();

                if (!ignore)
                {
                    if (this._config.AddDescriptionAttribute)
                    {
                        string description = CreateFileHandler.GetLocalizedLabel(optionSet.DisplayName);

                        if (string.IsNullOrEmpty(description))
                        {
                            description = CreateFileHandler.GetLocalizedLabel(optionSet.Description);
                        }

                        if (!string.IsNullOrEmpty(description))
                        {
                            WriteLine("[System.ComponentModel.DescriptionAttribute(\"{0}\")]", description);
                        }
                    }

                    if (this._config.OptionSetExportType == OptionSetExportType.Enums && _config.AddTypeConverterAttributeForEnums && !string.IsNullOrEmpty(_config.TypeConverterName))
                    {
                        WriteLine("[System.ComponentModel.TypeConverterAttribute(\"{0}\")]", _config.TypeConverterName);
                    }
                }

                StringBuilder str = new StringBuilder();

                if (ignore)
                {
                    str.Append("// ");
                }

                if (this._config.OptionSetExportType == OptionSetExportType.Enums)
                {
                    str.AppendFormat("public enum {0}", optionSet.Name);
                }
                else
                {
                    str.AppendFormat("public static partial class {0}", optionSet.Name);
                }

                WriteLine(str.ToString());

                if (ignore)
                {
                    return;
                }
            }

            WriteLine("{");

            bool first = true;

            // Формируем значения
            foreach (var item in options)
            {
                if (first) { first = false; } else { WriteLine(); }

                List<string> header = new List<string>() { item.Value.ToString() };

                if (item.DisplayOrder.HasValue)
                {
                    header.Add(string.Format("DisplayOrder: {0}", item.DisplayOrder.Value));
                }

                WriteSummary(item.Label, item.Description, header, null);

                if (this._config.AddDescriptionAttribute)
                {
                    string description = CreateFileHandler.GetLocalizedLabel(item.Label);

                    if (string.IsNullOrEmpty(description))
                    {
                        description = CreateFileHandler.GetLocalizedLabel(item.Description);
                    }

                    if (!string.IsNullOrEmpty(description))
                    {
                        WriteLine("[System.ComponentModel.DescriptionAttribute(\"{0}\")]", description);
                    }
                }

                var str = item.MakeStrings();

                if (this._config.OptionSetExportType == OptionSetExportType.Enums)
                {
                    WriteLine("[System.Runtime.Serialization.EnumMemberAttribute()]");

                    WriteLine(str + ",");
                }
                else
                {
                    WriteLine("public {0} int {1};", _fieldHeader, str);
                }
            }

            WriteLine("}");
        }
    }
}
