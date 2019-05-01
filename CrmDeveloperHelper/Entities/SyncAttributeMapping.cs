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
    ///     (English - United States - 1033): Sync Attribute Mapping
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Sync Attribute Mappings
    /// 
    /// Description:
    ///     (English - United States - 1033): Group of Sync-Attribute Mappings used to provide Attribute mappings during sync for a particular user
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(SyncAttributeMapping.EntityLogicalName)]
    public partial class SyncAttributeMapping : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "syncattributemapping";
        
        public const string EntitySchemaName = "SyncAttributeMapping";
        
        public const int EntityObjectTypeCode = 1401;
        
        public const string EntityPrimaryIdAttribute = "syncattributemappingid";
        
        /// <summary>
        /// Default Constructor syncattributemapping
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public SyncAttributeMapping() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor syncattributemapping for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public SyncAttributeMapping(object anonymousObject) : 
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
        ///     (English - United States - 1033): Sync-Attribute Mapping Id
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the Sync-Attribute Mapping.
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
                this.SyncAttributeMappingId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Sync-Attribute Mapping Id
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the Sync-Attribute Mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SyncAttributeMappingId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SyncAttributeMappingId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(SyncAttributeMappingId));
            }
        }
        #endregion
        
        #region Attributes
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Allowed Sync Directions
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.allowedsyncdirection)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> AllowedSyncDirection
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.allowedsyncdirection);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AllowedSyncDirection));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.allowedsyncdirection, value);
                this.OnPropertyChanged(nameof(AllowedSyncDirection));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): CRM Name of the attribute for which this mapping is defined
        /// 
        /// Description:
        ///     (English - United States - 1033): CRM Attribute Name.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.attributecrmname)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string AttributeCRMName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.attributecrmname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AttributeCRMName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.attributecrmname, value);
                this.OnPropertyChanged(nameof(AttributeCRMName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Exchange Name of the attribute for which this mapping is defined
        /// 
        /// Description:
        ///     (English - United States - 1033): Exchange Attribute Name.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.attributeexchangename)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string AttributeExchangeName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.attributeexchangename);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AttributeExchangeName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.attributeexchangename, value);
                this.OnPropertyChanged(nameof(AttributeExchangeName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.componentstate);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Computed Properties for one attribute
        /// 
        /// Description:
        ///     (English - United States - 1033): Computed Properties.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.computedproperties)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string ComputedProperties
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.computedproperties);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ComputedProperties));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.computedproperties, value);
                this.OnPropertyChanged(nameof(ComputedProperties));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Sync Direction
        /// 
        /// Description:
        ///     (English - United States - 1033): Default Sync Direction
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.defaultsyncdirection)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue DefaultSyncDirection
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.defaultsyncdirection);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(DefaultSyncDirection));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.defaultsyncdirection, value);
                this.OnPropertyChanged(nameof(DefaultSyncDirection));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name of the Entity for which this attribute mapping is defined
        /// 
        /// Description:
        ///     (English - United States - 1033): Entity name.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.entitytypecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string EntityTypeCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.entitytypecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(EntityTypeCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.entitytypecode, value);
                this.OnPropertyChanged(nameof(EntityTypeCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Computed
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates whether the mapping is a computed property
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.iscomputed)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsComputed
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.iscomputed);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Managed
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates whether the solution component is part of a managed solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name of the attribute for which this mapping is defined
        /// 
        /// Description:
        ///     (English - United States - 1033): Attribute Name.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.mappingname)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string MappingName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.mappingname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(MappingName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.mappingname, value);
                this.OnPropertyChanged(nameof(MappingName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Organization Id
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier for the organization
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.organizationid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.organizationid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Overwrite Time
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Parent Sync-Attribute Mapping
        /// 
        /// Description:
        ///     (English - United States - 1033): Parent Sync-Attribute Mapping to which this mapping belongs
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.parentsyncattributemappingid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ParentSyncAttributeMappingId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.parentsyncattributemappingid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ParentSyncAttributeMappingId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.parentsyncattributemappingid, value);
                this.OnPropertyChanged(nameof(ParentSyncAttributeMappingId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.solutionid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Sync-Attribute Mapping
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncattributemappingidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SyncAttributeMappingIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncattributemappingidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Profile
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of profile to which this mapping belongs.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncattributemappingprofileid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference SyncAttributeMappingProfileId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncattributemappingprofileid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SyncAttributeMappingProfileId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncattributemappingprofileid, value);
                this.OnPropertyChanged(nameof(SyncAttributeMappingProfileId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Sync Direction
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncdirection)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue SyncDirection
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncdirection);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SyncDirection));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SyncAttributeMapping.Schema.Attributes.syncdirection, value);
                this.OnPropertyChanged(nameof(SyncDirection));
            }
        }
        #endregion
    }
}
