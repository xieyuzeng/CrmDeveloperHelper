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
    ///     (English - United States - 1033): Connection Role
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Connection Roles
    /// 
    /// Description:
    ///     (English - United States - 1033): Role describing a relationship between a two records.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(ConnectionRole.EntityLogicalName)]
    public partial class ConnectionRole : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "connectionrole";
        
        public const string EntitySchemaName = "ConnectionRole";
        
        public const int EntityTypeCode = 3231;
        
        public const string EntityPrimaryIdAttribute = "connectionroleid";
        
        public const string EntityPrimaryNameAttribute = "name";
        
        /// <summary>
        /// Default Constructor connectionrole
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ConnectionRole() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor connectionrole for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ConnectionRole(object anonymousObject) : 
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
        ///     (English - United States - 1033): Connection Role
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the connection role.
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
                this.ConnectionRoleId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Connection Role
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ConnectionRoleId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ConnectionRoleId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(ConnectionRoleId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryNameAttribute)]
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
        ///     (English - United States - 1033): Connection Role Category
        /// 
        /// Description:
        ///     (English - United States - 1033): Categories for connection roles.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.category)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue Category
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.category);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Category));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.category, value);
                this.OnPropertyChanged(nameof(Category));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Connection Role Category
        /// 
        /// Description:
        ///     (English - United States - 1033): Categories for connection roles.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.category)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.connectionrole_category> CategoryEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.category);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.connectionrole_category), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.connectionrole_category)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.connectionrole_category), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CategoryEnum));
                this.OnPropertyChanging(nameof(Category));
                if ((value == null))
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.category, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.category, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(Category));
                this.OnPropertyChanged(nameof(CategoryEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): State of the component.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.componentstate);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): State of the component.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.componentstate);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Unique ID
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the published or unpublished connection role record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.connectionroleidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ConnectionRoleIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.connectionroleidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the relationship role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.createdby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the connection role was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.createdon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the relationship role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.createdonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.createdonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Description
        /// 
        /// Description:
        ///     (English - United States - 1033): Description of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.description)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Description
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.description);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Description));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.description, value);
                this.OnPropertyChanged(nameof(Description));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Import Sequence Number
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the data import or data migration that created this record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.importsequencenumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> ImportSequenceNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.importsequencenumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ImportSequenceNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.importsequencenumber, value);
                this.OnPropertyChanged(nameof(ImportSequenceNumber));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Introduced Version
        /// 
        /// Description:
        ///     (English - United States - 1033): Version in which the form is introduced.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.introducedversion)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string IntroducedVersion
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.introducedversion);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IntroducedVersion));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.introducedversion, value);
                this.OnPropertyChanged(nameof(IntroducedVersion));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Customizable
        /// 
        /// Description:
        ///     (English - United States - 1033): Information that specifies whether this component can be customized.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.iscustomizable)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.BooleanManagedProperty IsCustomizable
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.iscustomizable);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsCustomizable));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.iscustomizable, value);
                this.OnPropertyChanged(nameof(IsCustomizable));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): State
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates whether the solution component is part of a managed solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.modifiedby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the connection role was last modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.modifiedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who modified the relationship role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.modifiedonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.modifiedonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Organization
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the organization that this connection role belongs to.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.organizationid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.organizationid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Overwritten On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the record was last overwritten.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.solutionid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status
        /// 
        /// Description:
        ///     (English - United States - 1033): Status of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue StateCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(StateCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statecode, value);
                this.OnPropertyChanged(nameof(StateCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status
        /// 
        /// Description:
        ///     (English - United States - 1033): Status of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statecode> StateCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statecode);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statecode), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statecode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statecode), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(StateCodeEnum));
                this.OnPropertyChanging(nameof(StateCode));
                if ((value == null))
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statecode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statecode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(StateCode));
                this.OnPropertyChanged(nameof(StateCodeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status Reason
        /// 
        /// Description:
        ///     (English - United States - 1033): Reason for the status of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statuscode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue StatusCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statuscode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(StatusCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statuscode, value);
                this.OnPropertyChanged(nameof(StatusCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Status Reason
        /// 
        /// Description:
        ///     (English - United States - 1033): Reason for the status of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statuscode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statuscode> StatusCodeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statuscode);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statuscode), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statuscode)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.OptionSets.statuscode), optionSet.Value)));
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
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statuscode, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.statuscode, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(StatusCode));
                this.OnPropertyChanged(nameof(StatusCodeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version Number
        /// 
        /// Description:
        ///     (English - United States - 1033): Version number of the connection role.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRole.Schema.Attributes.versionnumber);
            }
        }
        #endregion
    }
}
