//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Entities
{
    
    
    /// <summary>
    /// DisplayName:
    ///     (English - United States - 1033): App Module Component
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): App Module Components
    /// 
    /// Description:
    ///     (English - United States - 1033): A component available in a business app such as entity, dashboard, form, view, chart, and business process.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(AppModuleComponent.EntityLogicalName)]
    public partial class AppModuleComponent : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "appmodulecomponent";
        
        public const string EntitySchemaName = "AppModuleComponent";
        
        public const int EntityTypeCode = 9007;
        
        public const string EntityPrimaryIdAttribute = "appmodulecomponentid";
        
        /// <summary>
        /// Default Constructor appmodulecomponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public AppModuleComponent() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor appmodulecomponent for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public AppModuleComponent(object anonymousObject) : 
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
        ///     (English - United States - 1033): App Module Component
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier for entity instances
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
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
                this.AppModuleComponentId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): App Module Component
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier for entity instances
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> AppModuleComponentId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AppModuleComponentId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(AppModuleComponentId));
            }
        }
        #endregion
        
        #region Attributes
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Application Component Unique Id
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the Application Component used when synchronizing customizations for the Microsoft Dynamics 365 client for Outlook
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.appmodulecomponentidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> AppModuleComponentIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.appmodulecomponentidunique);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AppModuleComponentIdUnique));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.appmodulecomponentidunique, value);
                this.OnPropertyChanged(nameof(AppModuleComponentIdUnique));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): AppModule
        /// 
        /// Description:
        ///     (English - United States - 1033): The App Module Id Unique
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.appmoduleidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference AppModuleIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.appmoduleidunique);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AppModuleIdUnique));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.appmoduleidunique, value);
                this.OnPropertyChanged(nameof(AppModuleIdUnique));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Object Type Code
        /// 
        /// Description:
        ///     (English - United States - 1033): The object type code of the component.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.componenttype)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentType
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.componenttype);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ComponentType));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.componenttype, value);
                this.OnPropertyChanged(nameof(ComponentType));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdby, value);
                this.OnPropertyChanged(nameof(CreatedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the record was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdon, value);
                this.OnPropertyChanged(nameof(CreatedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.createdonbehalfby, value);
                this.OnPropertyChanged(nameof(CreatedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): ExchangeRate
        /// 
        /// Description:
        ///     (English - United States - 1033): Exchange rate for the currency associated with the Application Component with respect to the base currency.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.exchangerate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<decimal> ExchangeRate
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<decimal>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.exchangerate);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ExchangeRate));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.exchangerate, value);
                this.OnPropertyChanged(nameof(ExchangeRate));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Introduced Version
        /// 
        /// Description:
        ///     (English - United States - 1033): Version in which the application component record is introduced.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.introducedversion)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string IntroducedVersion
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.introducedversion);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IntroducedVersion));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.introducedversion, value);
                this.OnPropertyChanged(nameof(IntroducedVersion));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Default
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.isdefault)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsDefault
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.isdefault);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsDefault));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.isdefault, value);
                this.OnPropertyChanged(nameof(IsDefault));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Metadata
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.ismetadata)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsMetadata
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.ismetadata);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsMetadata));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.ismetadata, value);
                this.OnPropertyChanged(nameof(IsMetadata));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who modified the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedby, value);
                this.OnPropertyChanged(nameof(ModifiedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the record was modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedon, value);
                this.OnPropertyChanged(nameof(ModifiedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who modified the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.modifiedonbehalfby, value);
                this.OnPropertyChanged(nameof(ModifiedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Object
        /// 
        /// Description:
        ///     (English - United States - 1033): Object Id
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.objectid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ObjectId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.objectid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ObjectId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.objectid, value);
                this.OnPropertyChanged(nameof(ObjectId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the record was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.overwritetime);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OverwriteTime));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.overwritetime, value);
                this.OnPropertyChanged(nameof(OverwriteTime));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Root App Module Component
        /// 
        /// Description:
        ///     (English - United States - 1033): The parent ID of the subcomponent, which will be a root
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.rootappmodulecomponentid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> RootAppModuleComponentId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.rootappmodulecomponentid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(RootAppModuleComponentId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.rootappmodulecomponentid, value);
                this.OnPropertyChanged(nameof(RootAppModuleComponentId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Root Component Behavior
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates the include behavior of the root component.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.rootcomponentbehavior)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue RootComponentBehavior
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.rootcomponentbehavior);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(RootComponentBehavior));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.rootcomponentbehavior, value);
                this.OnPropertyChanged(nameof(RootComponentBehavior));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Time Zone Rule Version Number
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.timezoneruleversionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> TimeZoneRuleVersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.timezoneruleversionnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TimeZoneRuleVersionNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.timezoneruleversionnumber, value);
                this.OnPropertyChanged(nameof(TimeZoneRuleVersionNumber));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): UTC Conversion Time Zone Code
        /// 
        /// Description:
        ///     (English - United States - 1033): Time zone code that was in use when the record was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.utcconversiontimezonecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> UTCConversionTimeZoneCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.utcconversiontimezonecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(UTCConversionTimeZoneCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.utcconversiontimezonecode, value);
                this.OnPropertyChanged(nameof(UTCConversionTimeZoneCode));
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.versionnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(VersionNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.AppModuleComponent.Schema.Attributes.versionnumber, value);
                this.OnPropertyChanged(nameof(VersionNumber));
            }
        }
        #endregion
    }
}
