//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Entities
{
    
    
    /// <summary>
    /// DisplayName:
    ///     (English - United States - 1033): Column Mapping
    ///     (Russian - 1049): Сопоставление столбцов
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Column Mappings
    ///     (Russian - 1049): Сопоставления столбцов
    /// 
    /// Description:
    ///     (English - United States - 1033): Mapping for columns in a data map.
    ///     (Russian - 1049): Сопоставление столбцов в сопоставлении данных.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(ColumnMapping.EntityLogicalName)]
    [System.ComponentModel.DescriptionAttribute("Column Mapping")]
    public partial class ColumnMapping : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "columnmapping";
        
        public const string EntitySchemaName = "ColumnMapping";
        
        public const int EntityTypeCode = 4417;
        
        public const string EntityPrimaryIdAttribute = "columnmappingid";
        
        public const string EntityPrimaryNameAttribute = "sourceattributename";
        
        /// <summary>
        /// Default Constructor columnmapping
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ColumnMapping() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor columnmapping for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ColumnMapping(object anonymousObject) : 
                this()
        {
            if (anonymousObject == null)
            {
                return;
            }

            System.Type anonymousObjectType = anonymousObject.GetType();

            if (!anonymousObjectType.Name.StartsWith("<>")
                || anonymousObjectType.Name.IndexOf("AnonymousType", System.StringComparison.InvariantCultureIgnoreCase) == -1
            )
            {
                return;
            }

            foreach (var prop in anonymousObjectType.GetProperties())
            {
                var value = prop.GetValue(anonymousObject, null);
                var name = prop.Name.ToLower();

                switch (name)
                {
                    case "id":
                    case EntityPrimaryIdAttribute:
                        if (value is System.Guid idValue)
                        {
                            Attributes[EntityPrimaryIdAttribute] = base.Id = idValue;
                        }
                        break;

                    default:
                        if (value is Microsoft.Xrm.Sdk.FormattedValueCollection formattedValueCollection)
                        {
                            FormattedValues.AddRange(formattedValueCollection);
                        }
                        else
                        {
                            Attributes[name] = value;
                        }
                        break;
                }
            }
        }
        
        #region NotifyProperty Events

        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void OnPropertyChanged(string propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void OnPropertyChanging(string propertyName)
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
            }
        }
        

        #endregion
        
        #region Primary Attributes

        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the column mapping.
        ///     (Russian - 1049): Уникальный идентификатор сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Unique identifier of the column mapping.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override System.Guid Id
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return base.Id;
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.ColumnMappingId = value;
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the column mapping.
        ///     (Russian - 1049): Уникальный идентификатор сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Unique identifier of the column mapping.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ColumnMappingId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ColumnMappingId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(ColumnMappingId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Source Attribute
        ///     (Russian - 1049): Исходный атрибут
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the source attribute.
        ///     (Russian - 1049): Имя исходного атрибута.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryNameAttribute)]
        [System.ComponentModel.DescriptionAttribute("Source Attribute")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string SourceAttributeName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(EntityPrimaryNameAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SourceAttributeName));
                this.SetAttributeValue(EntityPrimaryNameAttribute, value);
                this.OnPropertyChanged(nameof(SourceAttributeName));
            }
        }
        

        #endregion
        
        #region Attributes

        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the Column Mapping.
        ///     (Russian - 1049): Уникальный идентификатор сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.columnmappingidunique)]
        [System.ComponentModel.DescriptionAttribute("Unique identifier of the Column Mapping.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ColumnMappingIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.columnmappingidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        ///     (Russian - 1049): Состояние компонента
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.componentstate);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        ///     (Russian - 1049): Состояние компонента
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.componentstate);
                if (((optionSetValue != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate), optionSetValue.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate), optionSetValue.Value)));
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By
        ///     (Russian - 1049): Создано
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the column mapping.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, создавшего сопоставление столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.createdby)]
        [System.ComponentModel.DescriptionAttribute("Created By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Date and time when the column mapping was created.
        ///     (Russian - 1049): Дата и время создания сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.createdon)]
        [System.ComponentModel.DescriptionAttribute("Date and time when the column mapping was created.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        ///     (Russian - 1049): Кем создано (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the columnmapping.
        ///     (Russian - 1049): Уникальный идентификатор делегата, создавшего columnmapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.createdonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Created By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.createdonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Data Map ID
        ///     (Russian - 1049): Идентификатор сопоставления данных
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated data map.
        ///     (Russian - 1049): Уникальный идентификатор связанного сопоставления данных.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.importmapid)]
        [System.ComponentModel.DescriptionAttribute("Data Map ID")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ImportMapId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.importmapid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ImportMapId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.importmapid, value);
                this.OnPropertyChanged(nameof(ImportMapId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Introduced Version
        ///     (Russian - 1049): Версия добавления
        /// 
        /// Description:
        ///     (English - United States - 1033): Version in which the component is introduced.
        ///     (Russian - 1049): Версия, в которой был введен компонент.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.introducedversion)]
        [System.ComponentModel.DescriptionAttribute("Introduced Version")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string IntroducedVersion
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.introducedversion);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IntroducedVersion));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.introducedversion, value);
                this.OnPropertyChanged(nameof(IntroducedVersion));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): State
        ///     (Russian - 1049): Состояние
        /// 
        /// Description:
        ///     (English - United States - 1033): Information that specifies whether this component is managed.
        ///     (Russian - 1049): Сведения о том, является ли компонент управляемым.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.ismanaged)]
        [System.ComponentModel.DescriptionAttribute("State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        ///     (Russian - 1049): Изменено
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the column mapping.
        ///     (Russian - 1049): Уникальный идентификатор последнего пользователя, изменившего сопоставление столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.modifiedby)]
        [System.ComponentModel.DescriptionAttribute("Modified By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        ///     (Russian - 1049): Дата изменения
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the column mapping was last modified.
        ///     (Russian - 1049): Дата и время изменения сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.modifiedon)]
        [System.ComponentModel.DescriptionAttribute("Modified On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        ///     (Russian - 1049): Кем изменено (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the columnmapping.
        ///     (Russian - 1049): Уникальный идентификатор последнего делегата, изменившего columnmapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.modifiedonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Modified By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.modifiedonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Overwrite Time
        ///     (Russian - 1049): Время замены записи
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.overwritetime)]
        [System.ComponentModel.DescriptionAttribute("Record Overwrite Time")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Process Code
        ///     (Russian - 1049): Код процесса
        /// 
        /// Description:
        ///     (English - United States - 1033): Information about whether the column mapping needs to be processed.
        ///     (Russian - 1049): Информация о необходимости обработки сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.processcode)]
        [System.ComponentModel.DescriptionAttribute("Process Code")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ProcessCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.processcode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ProcessCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.processcode, value);
                this.OnPropertyChanged(nameof(ProcessCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Process Code
        ///     (Russian - 1049): Код процесса
        /// 
        /// Description:
        ///     (English - United States - 1033): Information about whether the column mapping needs to be processed.
        ///     (Russian - 1049): Информация о необходимости обработки сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.processcode)]
        [System.ComponentModel.DescriptionAttribute("Process Code")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.processcode> ProcessCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.processcode);
                if (((optionSetValue != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.processcode), optionSetValue.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.processcode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.processcode), optionSetValue.Value)));
                }
                else
                {
                    return null;
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ProcessCodeEnum));
                this.OnPropertyChanging(nameof(ProcessCode));
                if ((value == null))
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.processcode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.processcode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(ProcessCode));
                this.OnPropertyChanged(nameof(ProcessCodeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        ///     (Russian - 1049): Решение
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        ///     (Russian - 1049): Уникальный идентификатор связанного решения.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.solutionid)]
        [System.ComponentModel.DescriptionAttribute("Solution")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Source Entity Name
        ///     (Russian - 1049): Имя исходной сущности
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the source entity.
        ///     (Russian - 1049): Имя исходной сущности.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.sourceentityname)]
        [System.ComponentModel.DescriptionAttribute("Source Entity Name")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string SourceEntityName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.sourceentityname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SourceEntityName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.sourceentityname, value);
                this.OnPropertyChanged(nameof(SourceEntityName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status
        ///     (Russian - 1049): Состояние
        /// 
        /// Description:
        ///     (English - United States - 1033): Status of the column mapping.
        ///     (Russian - 1049): Состояние сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statecode)]
        [System.ComponentModel.DescriptionAttribute("Status")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue StateCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statecode);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status
        ///     (Russian - 1049): Состояние
        /// 
        /// Description:
        ///     (English - United States - 1033): Status of the column mapping.
        ///     (Russian - 1049): Состояние сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statecode)]
        [System.ComponentModel.DescriptionAttribute("Status")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statecode> StateCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statecode);
                if (((optionSetValue != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statecode), optionSetValue.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statecode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statecode), optionSetValue.Value)));
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status Reason
        ///     (Russian - 1049): Причина состояния
        /// 
        /// Description:
        ///     (English - United States - 1033): Reason for the status of the column mapping.
        ///     (Russian - 1049): Причина состояния сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statuscode)]
        [System.ComponentModel.DescriptionAttribute("Status Reason")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue StatusCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statuscode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(StatusCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statuscode, value);
                this.OnPropertyChanged(nameof(StatusCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status Reason
        ///     (Russian - 1049): Причина состояния
        /// 
        /// Description:
        ///     (English - United States - 1033): Reason for the status of the column mapping.
        ///     (Russian - 1049): Причина состояния сопоставления столбцов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statuscode)]
        [System.ComponentModel.DescriptionAttribute("Status Reason")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statuscode> StatusCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statuscode);
                if (((optionSetValue != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statuscode), optionSetValue.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statuscode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.OptionSets.statuscode), optionSetValue.Value)));
                }
                else
                {
                    return null;
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(StatusCodeEnum));
                this.OnPropertyChanging(nameof(StatusCode));
                if ((value == null))
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statuscode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.statuscode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(StatusCode));
                this.OnPropertyChanged(nameof(StatusCodeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Target Attribute
        ///     (Russian - 1049): Конечный атрибут
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the Microsoft Dynamics 365 attribute.
        ///     (Russian - 1049): Имя атрибута Microsoft Dynamics 365.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.targetattributename)]
        [System.ComponentModel.DescriptionAttribute("Target Attribute")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string TargetAttributeName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.targetattributename);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TargetAttributeName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.targetattributename, value);
                this.OnPropertyChanged(nameof(TargetAttributeName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Target Entity
        ///     (Russian - 1049): Целевая сущность
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the Microsoft Dynamics 365 entity.
        ///     (Russian - 1049): Имя сущности Microsoft Dynamics 365.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.targetentityname)]
        [System.ComponentModel.DescriptionAttribute("Target Entity")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string TargetEntityName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.targetentityname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TargetEntityName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ColumnMapping.Schema.Attributes.targetentityname, value);
                this.OnPropertyChanged(nameof(TargetEntityName));
            }
        }
        

        #endregion
    }
}
