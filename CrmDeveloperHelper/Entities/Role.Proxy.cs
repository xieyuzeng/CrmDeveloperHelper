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
    ///     (English - United States - 1033): Security Role
    ///     (Russian - 1049): Роль безопасности
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Security Roles
    ///     (Russian - 1049): Роли безопасности
    /// 
    /// Description:
    ///     (English - United States - 1033): Grouping of security privileges. Users are assigned roles that authorize their access to the Microsoft CRM system.
    ///     (Russian - 1049): Группа привилегий безопасности. Пользователям назначаются роли, которые контролируют их доступ к CRM-системе Microsoft.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(Role.EntityLogicalName)]
    [System.ComponentModel.DescriptionAttribute("Security Role")]
    public partial class Role : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "role";
        
        public const string EntitySchemaName = "Role";
        
        public const int EntityTypeCode = 1036;
        
        public const string EntityPrimaryIdAttribute = "roleid";
        
        public const string EntityPrimaryNameAttribute = "name";
        
        /// <summary>
        /// Default Constructor role
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Role() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor role for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Role(object anonymousObject) : 
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
        ///     (English - United States - 1033): Role
        ///     (Russian - 1049): Роль
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the role.
        ///     (Russian - 1049): Уникальный идентификатор роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Role")]
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
                this.RoleId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Role
        ///     (Russian - 1049): Роль
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the role.
        ///     (Russian - 1049): Уникальный идентификатор роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Role")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> RoleId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(RoleId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(RoleId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name
        ///     (Russian - 1049): Имя
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the role.
        ///     (Russian - 1049): Имя роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryNameAttribute)]
        [System.ComponentModel.DescriptionAttribute("Name")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Name
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(EntityPrimaryNameAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Name));
                this.SetAttributeValue(EntityPrimaryNameAttribute, value);
                this.OnPropertyChanged(nameof(Name));
            }
        }
        

        #endregion
        
        #region Attributes

        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Business Unit
        ///     (Russian - 1049): Бизнес-единица
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the business unit with which the role is associated.
        ///     (Russian - 1049): Уникальный идентификатор бизнес-единицы, с которой связана эта роль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.businessunitid)]
        [System.ComponentModel.DescriptionAttribute("Business Unit")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference BusinessUnitId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.businessunitid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(BusinessUnitId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.businessunitid, value);
                this.OnPropertyChanged(nameof(BusinessUnitId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Can Be Deleted
        ///     (Russian - 1049): Можно удалить
        /// 
        /// Description:
        ///     (English - United States - 1033): Tells whether the role can be deleted.
        ///     (Russian - 1049): Сообщает, можно ли удалить роль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.canbedeleted)]
        [System.ComponentModel.DescriptionAttribute("Can Be Deleted")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.BooleanManagedProperty CanBeDeleted
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.canbedeleted);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CanBeDeleted));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.canbedeleted, value);
                this.OnPropertyChanged(nameof(CanBeDeleted));
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.componentstate);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.componentstate);
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
        ///     (English - United States - 1033): Unique identifier of the user who created the role.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, создавшего роль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.createdby)]
        [System.ComponentModel.DescriptionAttribute("Created By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        ///     (Russian - 1049): Дата создания
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the role was created.
        ///     (Russian - 1049): Дата и время создания роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.createdon)]
        [System.ComponentModel.DescriptionAttribute("Created On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By Impersonator
        ///     (Russian - 1049): Создано персонатором
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the role.
        ///     (Russian - 1049): Уникальный идентификатор пользователя-делегата, создавшего роль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.createdonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Created By Impersonator")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.createdonbehalfby);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.importsequencenumber)]
        [System.ComponentModel.DescriptionAttribute("Import Sequence Number")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> ImportSequenceNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.importsequencenumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ImportSequenceNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.importsequencenumber, value);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.iscustomizable)]
        [System.ComponentModel.DescriptionAttribute("Customizable")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.BooleanManagedProperty IsCustomizable
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.iscustomizable);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsCustomizable));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.iscustomizable, value);
                this.OnPropertyChanged(nameof(IsCustomizable));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): State
        ///     (Russian - 1049): Область
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates whether the solution component is part of a managed solution.
        ///     (Russian - 1049): Указывает, является ли компонент решения частью управляемого решения.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.ismanaged)]
        [System.ComponentModel.DescriptionAttribute("State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        ///     (Russian - 1049): Изменено
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the role.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, внесшего последнее изменение в роль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.modifiedby)]
        [System.ComponentModel.DescriptionAttribute("Modified By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        ///     (Russian - 1049): Дата изменения
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the role was last modified.
        ///     (Russian - 1049): Дата и время последнего изменения роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.modifiedon)]
        [System.ComponentModel.DescriptionAttribute("Modified On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        ///     (Russian - 1049): Кем изменено (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the role.
        ///     (Russian - 1049): Уникальный идентификатор делегата, внесшего последнее изменение в роль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.modifiedonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Modified By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.modifiedonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Organization
        ///     (Russian - 1049): Предприятие
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the organization associated with the role.
        ///     (Russian - 1049): Уникальный идентификатор организации, связанной с ролью.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.organizationid)]
        [System.ComponentModel.DescriptionAttribute("Organization")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.organizationid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Created On
        ///     (Russian - 1049): Дата создания записи
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time that the record was migrated.
        ///     (Russian - 1049): Дата и время переноса записи.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.overriddencreatedon)]
        [System.ComponentModel.DescriptionAttribute("Record Created On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverriddenCreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.overriddencreatedon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OverriddenCreatedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.overriddencreatedon, value);
                this.OnPropertyChanged(nameof(OverriddenCreatedOn));
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.overwritetime)]
        [System.ComponentModel.DescriptionAttribute("Record Overwrite Time")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Parent Role
        ///     (Russian - 1049): Родительская роль
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the parent role.
        ///     (Russian - 1049): Уникальный идентификатор родительской роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.parentroleid)]
        [System.ComponentModel.DescriptionAttribute("Parent Role")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ParentRoleId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.parentroleid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Parent Root Role
        ///     (Russian - 1049): Корневая родительская роль
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the parent root role.
        ///     (Russian - 1049): Уникальный идентификатор корневой родительской роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.parentrootroleid)]
        [System.ComponentModel.DescriptionAttribute("Parent Root Role")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ParentRootRoleId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.parentrootroleid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Unique Id
        ///     (Russian - 1049): Уникальный идентификатор
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.roleidunique)]
        [System.ComponentModel.DescriptionAttribute("Unique Id")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> RoleIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.roleidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Role Template
        ///     (Russian - 1049): Шаблон роли
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the role template that is associated with the role.
        ///     (Russian - 1049): Уникальный идентификатор шаблона роли, связанного с ролью.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.roletemplateid)]
        [System.ComponentModel.DescriptionAttribute("Role Template")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference RoleTemplateId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.roletemplateid);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.solutionid)]
        [System.ComponentModel.DescriptionAttribute("Solution")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version number
        ///     (Russian - 1049): Номер версии
        /// 
        /// Description:
        ///     (English - United States - 1033): Version number of the role.
        ///     (Russian - 1049): Номер версии роли.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.versionnumber)]
        [System.ComponentModel.DescriptionAttribute("Version number")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.Role.Schema.Attributes.versionnumber);
            }
        }
        

        #endregion
    }
}
