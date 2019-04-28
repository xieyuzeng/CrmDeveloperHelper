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
    ///     (English - United States - 1033): Connection Role Object Type Code
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Connection Role Object Type Codes
    /// 
    /// Description:
    ///     (English - United States - 1033): Specifies the entity type that can play specific role in a connection.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(ConnectionRoleObjectTypeCode.EntityLogicalName)]
    public partial class ConnectionRoleObjectTypeCode : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "connectionroleobjecttypecode";
        
        public const string EntitySchemaName = "ConnectionRoleObjectTypeCode";
        
        public const int EntityTypeCode = 3233;
        
        public const string EntityPrimaryIdAttribute = "connectionroleobjecttypecodeid";
        
        /// <summary>
        /// Default Constructor connectionroleobjecttypecode
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ConnectionRoleObjectTypeCode() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor connectionroleobjecttypecode for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ConnectionRoleObjectTypeCode(object anonymousObject) : 
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
        ///     (English - United States - 1033): Unique identifier of the connection role object type association.
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
                this.ConnectionRoleObjectTypeCodeId = value;
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the connection role object type association.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ConnectionRoleObjectTypeCodeId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ConnectionRoleObjectTypeCodeId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(ConnectionRoleObjectTypeCodeId));
            }
        }
        #endregion
        
        #region Attributes
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.associatedobjecttypecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string AssociatedObjectTypeCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.associatedobjecttypecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AssociatedObjectTypeCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.associatedobjecttypecode, value);
                this.OnPropertyChanged(nameof(AssociatedObjectTypeCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Connection Role
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the connection role associated with the Connection Role Object Type Code.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.connectionroleid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ConnectionRoleId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.connectionroleid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ConnectionRoleId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.connectionroleid, value);
                this.OnPropertyChanged(nameof(ConnectionRoleId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Organization 
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the organization associated with the connectionroleobjecttypecode.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.organizationid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.organizationid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OrganizationId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.organizationid, value);
                this.OnPropertyChanged(nameof(OrganizationId));
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.versionnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(VersionNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ConnectionRoleObjectTypeCode.Schema.Attributes.versionnumber, value);
                this.OnPropertyChanged(nameof(VersionNumber));
            }
        }
        #endregion
    }
}
