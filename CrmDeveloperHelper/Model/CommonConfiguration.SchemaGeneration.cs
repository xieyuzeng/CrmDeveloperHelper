﻿using System.Runtime.Serialization;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Model
{
    public partial class CommonConfiguration
    {
        private int _GenerateCommonSpaceCount = 4;
        /// <summary>
        /// Количество пробелов для отступа в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public int GenerateCommonSpaceCount
        {
            get => _GenerateCommonSpaceCount;
            set
            {
                if (_GenerateCommonSpaceCount == value)
                {
                    return;
                }

                this.OnPropertyChanging(nameof(GenerateCommonSpaceCount));
                this._GenerateCommonSpaceCount = value;
                this.OnPropertyChanged(nameof(GenerateCommonSpaceCount));
            }
        }

        private IndentType _GenerateCommonIndentType = IndentType.Spaces;
        /// <summary>
        /// Тип отступа в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public IndentType GenerateCommonIndentType
        {
            get => _GenerateCommonIndentType;
            set
            {
                if (_GenerateCommonIndentType == value)
                {
                    return;
                }

                this.OnPropertyChanging(nameof(GenerateCommonIndentType));
                this._GenerateCommonIndentType = value;
                this.OnPropertyChanged(nameof(GenerateCommonIndentType));
            }
        }

        private ConstantType _GenerateSchemaConstantType = ConstantType.Constant;
        /// <summary>
        /// Тип записей в файле с метаданными сущности. const или read only
        /// </summary>
        [DataMember]
        public ConstantType GenerateSchemaConstantType
        {
            get => _GenerateSchemaConstantType;
            set
            {
                if (_GenerateSchemaConstantType == value)
                {
                    return;
                }

                this.OnPropertyChanging(nameof(GenerateSchemaConstantType));
                this._GenerateSchemaConstantType = value;
                this.OnPropertyChanged(nameof(GenerateSchemaConstantType));
            }
        }

        private OptionSetExportType _GenerateSchemaOptionSetExportType = OptionSetExportType.Enums;
        [DataMember]
        public OptionSetExportType GenerateSchemaOptionSetExportType
        {
            get => _GenerateSchemaOptionSetExportType;
            set
            {
                if (_GenerateSchemaOptionSetExportType == value)
                {
                    return;
                }

                this.OnPropertyChanging(nameof(GenerateSchemaOptionSetExportType));
                this._GenerateSchemaOptionSetExportType = value;
                this.OnPropertyChanged(nameof(GenerateSchemaOptionSetExportType));
            }
        }

        private bool _GenerateCommonAllDescriptions;
        /// <summary>
        /// Генерировать все описания (description) или только первое по приоритету в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public bool GenerateCommonAllDescriptions
        {
            get => _GenerateCommonAllDescriptions;
            set
            {
                this.OnPropertyChanging(nameof(GenerateCommonAllDescriptions));
                this._GenerateCommonAllDescriptions = value;
                this.OnPropertyChanged(nameof(GenerateCommonAllDescriptions));
            }
        }

        private bool _GenerateSchemaEntityOptionSetsDependentComponents;
        [DataMember]
        public bool GenerateSchemaEntityOptionSetsDependentComponents
        {
            get => _GenerateSchemaEntityOptionSetsDependentComponents;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaEntityOptionSetsDependentComponents));
                this._GenerateSchemaEntityOptionSetsDependentComponents = value;
                this.OnPropertyChanged(nameof(GenerateSchemaEntityOptionSetsDependentComponents));
            }
        }

        private bool _GenerateSchemaGlobalOptionSetsWithDependentComponents;
        [DataMember]
        public bool GenerateSchemaGlobalOptionSetsWithDependentComponents
        {
            get => _GenerateSchemaGlobalOptionSetsWithDependentComponents;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaGlobalOptionSetsWithDependentComponents));
                this._GenerateSchemaGlobalOptionSetsWithDependentComponents = value;
                this.OnPropertyChanged(nameof(GenerateSchemaGlobalOptionSetsWithDependentComponents));
            }
        }

        private bool _GenerateSchemaAddDescriptionAttribute = true;
        [DataMember]
        public bool GenerateSchemaAddDescriptionAttribute
        {
            get => _GenerateSchemaAddDescriptionAttribute;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaAddDescriptionAttribute));
                this._GenerateSchemaAddDescriptionAttribute = value;
                this.OnPropertyChanged(nameof(GenerateSchemaAddDescriptionAttribute));
            }
        }

        private bool _GenerateSchemaAttributes = true;
        /// <summary>
        /// Генерировать атрибуты в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public bool GenerateSchemaAttributes
        {
            get => _GenerateSchemaAttributes;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaAttributes));
                this._GenerateSchemaAttributes = value;
                this.OnPropertyChanged(nameof(GenerateSchemaAttributes));
            }
        }

        private bool _GenerateSchemaManyToOne = true;
        /// <summary>
        /// Генерировать связи Many To One (N:1) в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public bool GenerateSchemaManyToOne
        {
            get => _GenerateSchemaManyToOne;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaManyToOne));
                this._GenerateSchemaManyToOne = value;
                this.OnPropertyChanged(nameof(GenerateSchemaManyToOne));
            }
        }

        private bool _GenerateSchemaOneToMany = true;
        /// <summary>
        /// Генерировать One To Many (1:N) в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public bool GenerateSchemaOneToMany
        {
            get => _GenerateSchemaOneToMany;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaOneToMany));
                this._GenerateSchemaOneToMany = value;
                this.OnPropertyChanged(nameof(GenerateSchemaOneToMany));
            }
        }

        private bool _GenerateSchemaManyToMany = true;
        /// <summary>
        /// Генерировать Many To Many (N:N) в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public bool GenerateSchemaManyToMany
        {
            get => _GenerateSchemaManyToMany;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaManyToMany));
                this._GenerateSchemaManyToMany = value;
                this.OnPropertyChanged(nameof(GenerateSchemaManyToMany));
            }
        }

        private bool _GenerateSchemaLocalOptionSet = true;
        /// <summary>
        /// Генерировать enum для значений Picklist-ов в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public bool GenerateSchemaLocalOptionSet
        {
            get => _GenerateSchemaLocalOptionSet;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaLocalOptionSet));
                this._GenerateSchemaLocalOptionSet = value;
                this.OnPropertyChanged(nameof(GenerateSchemaLocalOptionSet));
            }
        }

        private bool _GenerateSchemaGlobalOptionSet = false;
        [DataMember]
        public bool GenerateSchemaGlobalOptionSet
        {
            get => _GenerateSchemaGlobalOptionSet;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaGlobalOptionSet));
                this._GenerateSchemaGlobalOptionSet = value;
                this.OnPropertyChanged(nameof(GenerateSchemaGlobalOptionSet));
            }
        }

        private bool _GenerateSchemaStatusOptionSet = true;
        /// <summary>
        /// Генерировать enum для значения Statecode и Statuscode в файле с метаданными сущности
        /// </summary>
        [DataMember]
        public bool GenerateSchemaStatusOptionSet
        {
            get => _GenerateSchemaStatusOptionSet;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaStatusOptionSet));
                this._GenerateSchemaStatusOptionSet = value;
                this.OnPropertyChanged(nameof(GenerateSchemaStatusOptionSet));
            }
        }

        private bool _GenerateSchemaKeys = true;
        /// <summary>
        /// Генерировать уникальные ключи
        /// </summary>
        [DataMember]
        public bool GenerateSchemaKeys
        {
            get => _GenerateSchemaKeys;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaKeys));
                this._GenerateSchemaKeys = value;
                this.OnPropertyChanged(nameof(GenerateSchemaKeys));
            }
        }

        private bool _GenerateSchemaIntoSchemaClass;
        [DataMember]
        public bool GenerateSchemaIntoSchemaClass
        {
            get => _GenerateSchemaIntoSchemaClass;
            set
            {
                this.OnPropertyChanging(nameof(GenerateSchemaIntoSchemaClass));
                this._GenerateSchemaIntoSchemaClass = value;
                this.OnPropertyChanged(nameof(GenerateSchemaIntoSchemaClass));
            }
        }

        private void LoadFromDiskSchema(CommonConfiguration diskData)
        {
            this.GenerateCommonSpaceCount = diskData.GenerateCommonSpaceCount;
            this.GenerateCommonIndentType = diskData.GenerateCommonIndentType;

            this.GenerateCommonAllDescriptions = diskData.GenerateCommonAllDescriptions;

            this.GenerateSchemaIntoSchemaClass = diskData.GenerateSchemaIntoSchemaClass;

            this.GenerateSchemaAttributes = diskData.GenerateSchemaAttributes;
            this.GenerateSchemaManyToOne = diskData.GenerateSchemaManyToOne;
            this.GenerateSchemaOneToMany = diskData.GenerateSchemaOneToMany;
            this.GenerateSchemaManyToMany = diskData.GenerateSchemaManyToMany;
            this.GenerateSchemaLocalOptionSet = diskData.GenerateSchemaLocalOptionSet;
            this.GenerateSchemaGlobalOptionSet = diskData.GenerateSchemaGlobalOptionSet;
            this.GenerateSchemaStatusOptionSet = diskData.GenerateSchemaStatusOptionSet;
            this.GenerateSchemaKeys = diskData.GenerateSchemaKeys;

            this.GenerateSchemaConstantType = diskData.GenerateSchemaConstantType;
            this.GenerateSchemaOptionSetExportType = diskData.GenerateSchemaOptionSetExportType;

            this.GenerateSchemaEntityOptionSetsDependentComponents = diskData.GenerateSchemaEntityOptionSetsDependentComponents;
            this.GenerateSchemaGlobalOptionSetsWithDependentComponents = diskData.GenerateSchemaGlobalOptionSetsWithDependentComponents;

            this.GenerateSchemaAddDescriptionAttribute = diskData.GenerateSchemaAddDescriptionAttribute;
        }
    }
}
