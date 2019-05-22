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
    ///     (English - United States - 1033): Report Related Entity
    ///     (Russian - 1049): Сущность, связанная с отчетом
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Report Related Entities
    ///     (Russian - 1049): Сущности, связанные с отчетом
    /// 
    /// Description:
    ///     (English - United States - 1033): Entities related to a report. A report can be related to multiple entities.
    ///     (Russian - 1049): Сущности, связанные с отчетом. Отчет может быть связан с несколькими сущностями.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(ReportEntity.EntityLogicalName)]
    [System.ComponentModel.DescriptionAttribute("Report Related Entity")]
    public partial class ReportEntity : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "reportentity";
        
        public const string EntitySchemaName = "ReportEntity";
        
        public const int EntityTypeCode = 9101;
        
        public const string EntityPrimaryIdAttribute = "reportentityid";
        
        /// <summary>
        /// Default Constructor reportentity
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ReportEntity() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor reportentity for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ReportEntity(object anonymousObject) : 
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
        /// DisplayName:
        ///     (English - United States - 1033): Report Related Entity
        ///     (Russian - 1049): Сущность, связанная с отчетом
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the report record.
        ///     (Russian - 1049): Уникальный идентификатор записи отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Report Related Entity")]
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
                this.ReportEntityId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Report Related Entity
        ///     (Russian - 1049): Сущность, связанная с отчетом
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the report record.
        ///     (Russian - 1049): Уникальный идентификатор записи отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Report Related Entity")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ReportEntityId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ReportEntityId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(ReportEntityId));
            }
        }
        

        #endregion
        
        #region Attributes

        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        ///     (Russian - 1049): Состояние компонента
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.componentstate);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.componentstate);
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
        ///     (English - United States - 1033): Unique identifier of the user who created the report record.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, создавшего запись отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.createdby)]
        [System.ComponentModel.DescriptionAttribute("Created By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        ///     (Russian - 1049): Дата создания
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the report record was created.
        ///     (Russian - 1049): Дата и время создания записи отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.createdon)]
        [System.ComponentModel.DescriptionAttribute("Created On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        ///     (Russian - 1049): Кем создано (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the reportentity.
        ///     (Russian - 1049): Уникальный идентификатор делегата, создавшего сущность отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.createdonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Created By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.createdonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Import Sequence Number
        ///     (Russian - 1049): Порядковый номер импорта
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the data import or data migration that created this record.
        ///     (Russian - 1049): Уникальный идентификатор импорта или переноса данных, создавшего эту запись.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.importsequencenumber)]
        [System.ComponentModel.DescriptionAttribute("Import Sequence Number")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> ImportSequenceNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.importsequencenumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ImportSequenceNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.importsequencenumber, value);
                this.OnPropertyChanged(nameof(ImportSequenceNumber));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Customizable
        ///     (Russian - 1049): Настраиваемый
        /// 
        /// Description:
        ///     (English - United States - 1033): Information that specifies whether this component can be customized.
        ///     (Russian - 1049): Сведения, указывающие на возможность настройки этого компонента.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.iscustomizable)]
        [System.ComponentModel.DescriptionAttribute("Customizable")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.BooleanManagedProperty IsCustomizable
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.iscustomizable);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsCustomizable));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.iscustomizable, value);
                this.OnPropertyChanged(nameof(IsCustomizable));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Filterable
        ///     (Russian - 1049): С фильтрацией
        /// 
        /// Description:
        ///     (English - United States - 1033): Information about whether the report is filterable.
        ///     (Russian - 1049): Информация о возможности фильтрации отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.isfilterable)]
        [System.ComponentModel.DescriptionAttribute("Filterable")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsFilterable
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.isfilterable);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsFilterable));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.isfilterable, value);
                this.OnPropertyChanged(nameof(IsFilterable));
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        ///     (Russian - 1049): Изменено
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the report record.
        ///     (Russian - 1049): Уникальный идентификатор последнего пользователя, изменившего запись отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.modifiedby)]
        [System.ComponentModel.DescriptionAttribute("Modified By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        ///     (Russian - 1049): Дата изменения
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the report record was last modified.
        ///     (Russian - 1049): Дата и время последнего изменения записи отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.modifiedon)]
        [System.ComponentModel.DescriptionAttribute("Modified On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        ///     (Russian - 1049): Кем изменено (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the reportentity.
        ///     (Russian - 1049): Уникальный идентификатор делегата, внесшего последнее изменение в сущность отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.modifiedonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Modified By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.modifiedonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Report Related Entity
        ///     (Russian - 1049): Сущность, связанная с отчетом
        /// 
        /// Description:
        ///     (English - United States - 1033): Type of record with which the report is associated.
        ///     (Russian - 1049): Тип записи, с которой связан отчет.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.objecttypecode)]
        [System.ComponentModel.DescriptionAttribute("Report Related Entity")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string ObjectTypeCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.objecttypecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ObjectTypeCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.objecttypecode, value);
                this.OnPropertyChanged(nameof(ObjectTypeCode));
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.overwritetime)]
        [System.ComponentModel.DescriptionAttribute("Record Overwrite Time")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owner
        ///     (Russian - 1049): Ответственный
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user or team who owns the report entity.
        ///     (Russian - 1049): Уникальный идентификатор пользователя или рабочей группы, которым принадлежит сущность отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.ownerid)]
        [System.ComponentModel.DescriptionAttribute("Owner")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OwnerId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.ownerid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owning Business Unit
        ///     (Russian - 1049): Ответственная бизнес-единица
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the business unit that owns the report record.
        ///     (Russian - 1049): Уникальный идентификатор бизнес-единицы, ответственной за запись отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.owningbusinessunit)]
        [System.ComponentModel.DescriptionAttribute("Owning Business Unit")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> OwningBusinessUnit
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.owningbusinessunit);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owning User
        ///     (Russian - 1049): Ответственный пользователь
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who owns the report record.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, ответственного за запись отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.owninguser)]
        [System.ComponentModel.DescriptionAttribute("Owning User")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> OwningUser
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.owninguser);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.reportentityidunique)]
        [System.ComponentModel.DescriptionAttribute("For internal use only.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ReportEntityIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.reportentityidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Report
        ///     (Russian - 1049): Отчет
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the report.
        ///     (Russian - 1049): Уникальный идентификатор отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.reportid)]
        [System.ComponentModel.DescriptionAttribute("Report")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ReportId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.reportid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ReportId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.reportid, value);
                this.OnPropertyChanged(nameof(ReportId));
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.solutionid)]
        [System.ComponentModel.DescriptionAttribute("Solution")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.solutionid);
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportEntity.Schema.Attributes.versionnumber);
            }
        }
        

        #endregion
    }
}
