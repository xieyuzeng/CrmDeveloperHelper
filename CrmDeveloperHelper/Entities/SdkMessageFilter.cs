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
    ///     (English - United States - 1033): Sdk Message Filter
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Sdk Message Filters
    /// 
    /// Description:
    ///     (English - United States - 1033): Filter that defines which SDK messages are valid for each type of entity.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(SdkMessageFilter.EntityLogicalName)]
    public partial class SdkMessageFilter : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "sdkmessagefilter";
        
        public const string EntitySchemaName = "SdkMessageFilter";
        
        public const int EntityTypeCode = 4607;
        
        public const string EntityPrimaryIdAttribute = "sdkmessagefilterid";
        
        /// <summary>
        /// Default Constructor sdkmessagefilter
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public SdkMessageFilter() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor sdkmessagefilter for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public SdkMessageFilter(object anonymousObject) : 
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
        ///     (English - United States - 1033): Unique identifier of the SDK message filter entity.
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
                this.SdkMessageFilterId = value;
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the SDK message filter entity.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SdkMessageFilterId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SdkMessageFilterId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(SdkMessageFilterId));
            }
        }
        #endregion
        
        #region Attributes
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Availability
        /// 
        /// Description:
        ///     (English - United States - 1033): Identifies where a method will be exposed. 0 - Server, 1 - Client, 2 - both.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.availability)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> Availability
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.availability);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Availability));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.availability, value);
                this.OnPropertyChanged(nameof(Availability));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.componentstate);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ComponentState));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.componentstate, value);
                this.OnPropertyChanged(nameof(ComponentState));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the SDK message filter.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdby, value);
                this.OnPropertyChanged(nameof(CreatedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the SDK message filter was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdon, value);
                this.OnPropertyChanged(nameof(CreatedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the sdkmessagefilter.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.createdonbehalfby, value);
                this.OnPropertyChanged(nameof(CreatedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Customization level of the SDK message filter.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.customizationlevel)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> CustomizationLevel
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.customizationlevel);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CustomizationLevel));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.customizationlevel, value);
                this.OnPropertyChanged(nameof(CustomizationLevel));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Introduced Version
        /// 
        /// Description:
        ///     (English - United States - 1033): Version in which the component is introduced.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.introducedversion)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string IntroducedVersion
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.introducedversion);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IntroducedVersion));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.introducedversion, value);
                this.OnPropertyChanged(nameof(IntroducedVersion));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Custom Processing Step Allowed
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates whether a custom SDK message processing step is allowed.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.iscustomprocessingstepallowed)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsCustomProcessingStepAllowed
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.iscustomprocessingstepallowed);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsCustomProcessingStepAllowed));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.iscustomprocessingstepallowed, value);
                this.OnPropertyChanged(nameof(IsCustomProcessingStepAllowed));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): State
        /// 
        /// Description:
        ///     (English - United States - 1033): Information that specifies whether this component is managed.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.ismanaged);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsManaged));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.ismanaged, value);
                this.OnPropertyChanged(nameof(IsManaged));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Indicates whether the filter should be visible.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.isvisible)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsVisible
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.isvisible);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsVisible));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.isvisible, value);
                this.OnPropertyChanged(nameof(IsVisible));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the SDK message filter.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedby, value);
                this.OnPropertyChanged(nameof(ModifiedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the SDK message filter was last modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedon, value);
                this.OnPropertyChanged(nameof(ModifiedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the sdkmessagefilter.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.modifiedonbehalfby, value);
                this.OnPropertyChanged(nameof(ModifiedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the organization with which the SDK message filter is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.organizationid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.organizationid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OrganizationId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.organizationid, value);
                this.OnPropertyChanged(nameof(OrganizationId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Overwrite Time
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.overwritetime);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OverwriteTime));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.overwritetime, value);
                this.OnPropertyChanged(nameof(OverwriteTime));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Primary Object Type Code
        /// 
        /// Description:
        ///     (English - United States - 1033): Type of entity with which the SDK message filter is primarily associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.primaryobjecttypecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string PrimaryObjectTypeCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.primaryobjecttypecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(PrimaryObjectTypeCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.primaryobjecttypecode, value);
                this.OnPropertyChanged(nameof(PrimaryObjectTypeCode));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.restrictionlevel)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> RestrictionLevel
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.restrictionlevel);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(RestrictionLevel));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.restrictionlevel, value);
                this.OnPropertyChanged(nameof(RestrictionLevel));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the SDK message filter.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.sdkmessagefilteridunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SdkMessageFilterIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.sdkmessagefilteridunique);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SdkMessageFilterIdUnique));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.sdkmessagefilteridunique, value);
                this.OnPropertyChanged(nameof(SdkMessageFilterIdUnique));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): SDK Message ID
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the related SDK message.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.sdkmessageid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference SdkMessageId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.sdkmessageid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SdkMessageId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.sdkmessageid, value);
                this.OnPropertyChanged(nameof(SdkMessageId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Secondary Object Type Code
        /// 
        /// Description:
        ///     (English - United States - 1033): Type of entity with which the SDK message filter is secondarily associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.secondaryobjecttypecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string SecondaryObjectTypeCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.secondaryobjecttypecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SecondaryObjectTypeCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.secondaryobjecttypecode, value);
                this.OnPropertyChanged(nameof(SecondaryObjectTypeCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.solutionid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.solutionid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SolutionId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.solutionid, value);
                this.OnPropertyChanged(nameof(SolutionId));
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.versionnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(VersionNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.versionnumber, value);
                this.OnPropertyChanged(nameof(VersionNumber));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): WorkflowSdkStepEnabled
        /// 
        /// Description:
        ///     (English - United States - 1033): Whether or not the SDK message can be called from a workflow.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.workflowsdkstepenabled)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> WorkflowSdkStepEnabled
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.workflowsdkstepenabled);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(WorkflowSdkStepEnabled));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageFilter.Schema.Attributes.workflowsdkstepenabled, value);
                this.OnPropertyChanged(nameof(WorkflowSdkStepEnabled));
            }
        }
        #endregion
    }
}
