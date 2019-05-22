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
    ///     (English - United States - 1033): Report Related Category
    ///     (Russian - 1049): Категория, связанная с отчетом
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Report Related Categories
    ///     (Russian - 1049): Категории, связанные с отчетом
    /// 
    /// Description:
    ///     (English - United States - 1033): Categories related to a report. A report can be related to multiple categories.
    ///     (Russian - 1049): Категории, связанные с отчетом. Отчет может быть связан с несколькими категориями.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(ReportCategory.EntityLogicalName)]
    [System.ComponentModel.DescriptionAttribute("Report Related Category")]
    public partial class ReportCategory : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "reportcategory";
        
        public const string EntitySchemaName = "ReportCategory";
        
        public const int EntityTypeCode = 9102;
        
        public const string EntityPrimaryIdAttribute = "reportcategoryid";
        
        /// <summary>
        /// Default Constructor reportcategory
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ReportCategory() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor reportcategory for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ReportCategory(object anonymousObject) : 
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
        ///     (English - United States - 1033): Report Category
        ///     (Russian - 1049): Категория отчета
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the report category.
        ///     (Russian - 1049): Уникальный идентификатор категории отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Report Category")]
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
                this.ReportCategoryId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Report Category
        ///     (Russian - 1049): Категория отчета
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the report category.
        ///     (Russian - 1049): Уникальный идентификатор категории отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Report Category")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ReportCategoryId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ReportCategoryId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(ReportCategoryId));
            }
        }
        

        #endregion
        
        #region Attributes

        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Category
        ///     (Russian - 1049): Категория
        /// 
        /// Description:
        ///     (English - United States - 1033): Category of the report.
        ///     (Russian - 1049): Категория отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.categorycode)]
        [System.ComponentModel.DescriptionAttribute("Category")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue CategoryCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.categorycode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CategoryCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.categorycode, value);
                this.OnPropertyChanged(nameof(CategoryCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Category
        ///     (Russian - 1049): Категория
        /// 
        /// Description:
        ///     (English - United States - 1033): Category of the report.
        ///     (Russian - 1049): Категория отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.categorycode)]
        [System.ComponentModel.DescriptionAttribute("Category")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.OptionSets.categorycode> CategoryCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.categorycode);
                if (((optionSetValue != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.OptionSets.categorycode), optionSetValue.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.OptionSets.categorycode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.OptionSets.categorycode), optionSetValue.Value)));
                }
                else
                {
                    return null;
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CategoryCodeEnum));
                this.OnPropertyChanging(nameof(CategoryCode));
                if ((value == null))
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.categorycode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.categorycode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(CategoryCode));
                this.OnPropertyChanged(nameof(CategoryCodeEnum));
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.componentstate);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.componentstate);
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
        ///     (English - United States - 1033): Unique identifier of the user who created the report category.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, создавшего категорию отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.createdby)]
        [System.ComponentModel.DescriptionAttribute("Created By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        ///     (Russian - 1049): Дата создания
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the report category record was created.
        ///     (Russian - 1049): Дата и время создания записи категории отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.createdon)]
        [System.ComponentModel.DescriptionAttribute("Created On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        ///     (Russian - 1049): Кем создано (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the report category.
        ///     (Russian - 1049): Уникальный идентификатор делегата, создавшего категорию отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.createdonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Created By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.createdonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Exchange Rate
        ///     (Russian - 1049): Валютный курс
        /// 
        /// Description:
        ///     (English - United States - 1033): Exchange rate for the currency associated with the report category with respect to the base currency.
        ///     (Russian - 1049): Курс обмена валюты, связанной с категорией отчета, по отношению к базовой валюте.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.exchangerate)]
        [System.ComponentModel.DescriptionAttribute("Exchange Rate")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<decimal> ExchangeRate
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<decimal>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.exchangerate);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.importsequencenumber)]
        [System.ComponentModel.DescriptionAttribute("Import Sequence Number")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> ImportSequenceNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.importsequencenumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ImportSequenceNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.importsequencenumber, value);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.iscustomizable)]
        [System.ComponentModel.DescriptionAttribute("Customizable")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.BooleanManagedProperty IsCustomizable
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.iscustomizable);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsCustomizable));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.iscustomizable, value);
                this.OnPropertyChanged(nameof(IsCustomizable));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Managed
        ///     (Russian - 1049): Управляемый
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates whether the solution component is part of a managed solution.
        ///     (Russian - 1049): Указывает, является ли компонент решения частью управляемого решения.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.ismanaged)]
        [System.ComponentModel.DescriptionAttribute("Is Managed")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        ///     (Russian - 1049): Изменено
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the report category.
        ///     (Russian - 1049): Уникальный идентификатор последнего пользователя, изменившего категорию отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.modifiedby)]
        [System.ComponentModel.DescriptionAttribute("Modified By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        ///     (Russian - 1049): Дата изменения
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the report category was last modified.
        ///     (Russian - 1049): Дата и время последнего изменения категории отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.modifiedon)]
        [System.ComponentModel.DescriptionAttribute("Modified On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        ///     (Russian - 1049): Кем изменено (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the report category.
        ///     (Russian - 1049): Уникальный идентификатор делегата, внесшего последнее изменение в отчет.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.modifiedonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Modified By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.modifiedonbehalfby);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.overwritetime)]
        [System.ComponentModel.DescriptionAttribute("Record Overwrite Time")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owner
        ///     (Russian - 1049): Ответственный
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user or team who owns the report category.
        ///     (Russian - 1049): Уникальный идентификатор пользователя или рабочей группы, которым принадлежит категория отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.ownerid)]
        [System.ComponentModel.DescriptionAttribute("Owner")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OwnerId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.ownerid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owning Business Unit
        ///     (Russian - 1049): Ответственная бизнес-единица
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the business unit that owns the report category.
        ///     (Russian - 1049): Уникальный идентификатор бизнес-единицы, ответственной за категорию отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.owningbusinessunit)]
        [System.ComponentModel.DescriptionAttribute("Owning Business Unit")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> OwningBusinessUnit
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.owningbusinessunit);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owning User
        ///     (Russian - 1049): Ответственный пользователь
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who owns the report category.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, ответственного за категорию отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.owninguser)]
        [System.ComponentModel.DescriptionAttribute("Owning User")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> OwningUser
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.owninguser);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Report Category
        ///     (Russian - 1049): Категория отчета
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.reportcategoryidunique)]
        [System.ComponentModel.DescriptionAttribute("Report Category")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ReportCategoryIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.reportcategoryidunique);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.reportid)]
        [System.ComponentModel.DescriptionAttribute("Report")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ReportId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.reportid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ReportId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.reportid, value);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.solutionid)]
        [System.ComponentModel.DescriptionAttribute("Solution")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Time Zone Rule Version Number
        ///     (Russian - 1049): Номер версии правила часового пояса
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.timezoneruleversionnumber)]
        [System.ComponentModel.DescriptionAttribute("Time Zone Rule Version Number")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> TimeZoneRuleVersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.timezoneruleversionnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TimeZoneRuleVersionNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.timezoneruleversionnumber, value);
                this.OnPropertyChanged(nameof(TimeZoneRuleVersionNumber));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Currency
        ///     (Russian - 1049): Валюта
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the currency associated with the Report category.
        ///     (Russian - 1049): Уникальный идентификатор валюты, связанной с категорией отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.transactioncurrencyid)]
        [System.ComponentModel.DescriptionAttribute("Currency")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference TransactionCurrencyId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.transactioncurrencyid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TransactionCurrencyId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.transactioncurrencyid, value);
                this.OnPropertyChanged(nameof(TransactionCurrencyId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): UTC Conversion Time Zone Code
        ///     (Russian - 1049): Код часового пояса (преобразование в UTC)
        /// 
        /// Description:
        ///     (English - United States - 1033): Time zone code that was in use when the record was created.
        ///     (Russian - 1049): Код часового пояса, использовавшийся при создании записи.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.utcconversiontimezonecode)]
        [System.ComponentModel.DescriptionAttribute("UTC Conversion Time Zone Code")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> UTCConversionTimeZoneCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.utcconversiontimezonecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(UTCConversionTimeZoneCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.utcconversiontimezonecode, value);
                this.OnPropertyChanged(nameof(UTCConversionTimeZoneCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version Number
        ///     (Russian - 1049): Номер версии
        /// 
        /// Description:
        ///     (English - United States - 1033): Version number of the report category.
        ///     (Russian - 1049): Номер версии категории отчета.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.versionnumber)]
        [System.ComponentModel.DescriptionAttribute("Version Number")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ReportCategory.Schema.Attributes.versionnumber);
            }
        }
        

        #endregion
    }
}
