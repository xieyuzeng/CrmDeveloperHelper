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
    ///     (English - United States - 1033): Lookup Mapping
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Lookup Mappings
    /// 
    /// Description:
    ///     (English - United States - 1033): In a data map, maps a lookup attribute in a source file to Microsoft Dynamics 365.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(LookUpMapping.EntityLogicalName)]
    public partial class LookUpMapping : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "lookupmapping";
        
        public const string EntitySchemaName = "LookUpMapping";
        
        public const int EntityTypeCode = 4419;
        
        public const string EntityPrimaryIdAttribute = "lookupmappingid";
        
        /// <summary>
        /// Default Constructor lookupmapping
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public LookUpMapping() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor lookupmapping for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public LookUpMapping(object anonymousObject) : 
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
        ///     (English - United States - 1033): Unique identifier of the lookup mapping.
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
                this.LookUpMappingId = value;
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> LookUpMappingId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(LookUpMappingId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(LookUpMappingId));
            }
        }
        #endregion
        
        #region Attributes
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Column Mapping Id
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the column mapping with which this lookup mapping is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.columnmappingid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ColumnMappingId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.columnmappingid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ColumnMappingId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.columnmappingid, value);
                this.OnPropertyChanged(nameof(ColumnMappingId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.componentstate);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.componentstate);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.GlobalOptionSets.componentstate), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.GlobalOptionSets.componentstate)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.GlobalOptionSets.componentstate), optionSet.Value)));
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
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.createdby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Date and time when the lookup mapping was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.createdon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the lookupmapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.createdonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.createdonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Introduced Version
        /// 
        /// Description:
        ///     (English - United States - 1033): Version in which the component is introduced.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.introducedversion)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string IntroducedVersion
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.introducedversion);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IntroducedVersion));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.introducedversion, value);
                this.OnPropertyChanged(nameof(IntroducedVersion));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): State
        /// 
        /// Description:
        ///     (English - United States - 1033): Information that specifies whether this component is managed.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Lookup Field Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the field with which the lookup is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupattributename)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string LookUpAttributeName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupattributename);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(LookUpAttributeName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupattributename, value);
                this.OnPropertyChanged(nameof(LookUpAttributeName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Lookup Entity Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the entity with which the lookup is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupentityname)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string LookUpEntityName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupentityname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(LookUpEntityName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupentityname, value);
                this.OnPropertyChanged(nameof(LookUpEntityName));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the LookUp Mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupmappingidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> LookUpMappingIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupmappingidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Lookup Source
        /// 
        /// Description:
        ///     (English - United States - 1033): Lookup source code for lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupsourcecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue LookUpSourceCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupsourcecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(LookUpSourceCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupsourcecode, value);
                this.OnPropertyChanged(nameof(LookUpSourceCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Lookup Source
        /// 
        /// Description:
        ///     (English - United States - 1033): Lookup source code for lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupsourcecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.lookupsourcecode> LookUpSourceCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupsourcecode);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.lookupsourcecode), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.lookupsourcecode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.lookupsourcecode), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(LookUpSourceCodeEnum));
                this.OnPropertyChanging(nameof(LookUpSourceCode));
                if ((value == null))
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupsourcecode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.lookupsourcecode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(LookUpSourceCode));
                this.OnPropertyChanged(nameof(LookUpSourceCodeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.modifiedby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the lookup mapping was last modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.modifiedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the lookupmapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.modifiedonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.modifiedonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Overwrite Time
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Process Code
        /// 
        /// Description:
        ///     (English - United States - 1033): Information about whether the lookup mapping has to be processed.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.processcode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ProcessCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.processcode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ProcessCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.processcode, value);
                this.OnPropertyChanged(nameof(ProcessCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Process Code
        /// 
        /// Description:
        ///     (English - United States - 1033): Information about whether the lookup mapping has to be processed.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.processcode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.processcode> ProcessCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.processcode);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.processcode), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.processcode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.processcode), optionSet.Value)));
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
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.processcode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.processcode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(ProcessCode));
                this.OnPropertyChanged(nameof(ProcessCodeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.solutionid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status
        /// 
        /// Description:
        ///     (English - United States - 1033): Status of the lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue StateCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statecode);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status
        /// 
        /// Description:
        ///     (English - United States - 1033): Status of the lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statecode> StateCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statecode);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statecode), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statecode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statecode), optionSet.Value)));
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
        /// 
        /// Description:
        ///     (English - United States - 1033): Reason for the status of the lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statuscode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue StatusCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statuscode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(StatusCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statuscode, value);
                this.OnPropertyChanged(nameof(StatusCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status Reason
        /// 
        /// Description:
        ///     (English - United States - 1033): Reason for the status of the lookup mapping.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statuscode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statuscode> StatusCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statuscode);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statuscode), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statuscode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.OptionSets.statuscode), optionSet.Value)));
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
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statuscode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.statuscode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(StatusCode));
                this.OnPropertyChanged(nameof(StatusCodeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Transformation Parameter Mapping Id
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the transformation parameter mapping with which this lookup mapping is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.transformationparametermappingid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference TransformationParameterMappingId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.transformationparametermappingid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TransformationParameterMappingId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.LookUpMapping.Schema.Attributes.transformationparametermappingid, value);
                this.OnPropertyChanged(nameof(TransformationParameterMappingId));
            }
        }
        #endregion
    }
}
