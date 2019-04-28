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
    ///     (English - United States - 1033): Team template
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Team templates
    /// 
    /// Description:
    ///     (English - United States - 1033): Team template for an entity enabled for automatically created access teams.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(TeamTemplate.EntityLogicalName)]
    public partial class TeamTemplate : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "teamtemplate";
        
        public const string EntitySchemaName = "TeamTemplate";
        
        public const int EntityTypeCode = 92;
        
        public const string EntityPrimaryIdAttribute = "teamtemplateid";
        
        public const string EntityPrimaryNameAttribute = "teamtemplatename";
        
        /// <summary>
        /// Default Constructor teamtemplate
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public TeamTemplate() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor teamtemplate for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public TeamTemplate(object anonymousObject) : 
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
        ///     (English - United States - 1033): Primary key for team template
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the team template.
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
                this.TeamTemplateId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Primary key for team template
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the team template.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> TeamTemplateId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TeamTemplateId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(TeamTemplateId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Type the name of the team template.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryNameAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string TeamTemplateName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(EntityPrimaryNameAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TeamTemplateName));
                this.SetAttributeValue(EntityPrimaryNameAttribute, value);
                this.OnPropertyChanged(nameof(TeamTemplateName));
            }
        }
        #endregion
        
        #region Attributes
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the team template.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdby, value);
                this.OnPropertyChanged(nameof(CreatedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the team template was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdon, value);
                this.OnPropertyChanged(nameof(CreatedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the team template.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.createdonbehalfby, value);
                this.OnPropertyChanged(nameof(CreatedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Access Rights
        /// 
        /// Description:
        ///     (English - United States - 1033): Default access rights mask for the access teams associated with entity instances.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.defaultaccessrightsmask)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> DefaultAccessRightsMask
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.defaultaccessrightsmask);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(DefaultAccessRightsMask));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.defaultaccessrightsmask, value);
                this.OnPropertyChanged(nameof(DefaultAccessRightsMask));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Description
        /// 
        /// Description:
        ///     (English - United States - 1033): Type additional information that describes the team.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.description)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Description
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.description);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Description));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.description, value);
                this.OnPropertyChanged(nameof(Description));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is System
        /// 
        /// Description:
        ///     (English - United States - 1033): Information about whether this team template is user-defined or system-defined.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.issystem)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsSystem
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.issystem);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsSystem));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.issystem, value);
                this.OnPropertyChanged(nameof(IsSystem));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the team template.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedby, value);
                this.OnPropertyChanged(nameof(ModifiedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the team template was last modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedon, value);
                this.OnPropertyChanged(nameof(ModifiedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who modified the team template.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.modifiedonbehalfby, value);
                this.OnPropertyChanged(nameof(ModifiedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Entity
        /// 
        /// Description:
        ///     (English - United States - 1033): Object type code of entity which is enabled for access teams
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.objecttypecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> ObjectTypeCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.objecttypecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ObjectTypeCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.TeamTemplate.Schema.Attributes.objecttypecode, value);
                this.OnPropertyChanged(nameof(ObjectTypeCode));
            }
        }
        #endregion
    }
}
