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
    ///     (English - United States - 1033): Mobile Offline Profile
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Mobile Offline Profiles
    /// 
    /// Description:
    ///     (English - United States - 1033): Information to administer and manage the data available to mobile devices in offline mode.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(MobileOfflineProfile.EntityLogicalName)]
    public partial class MobileOfflineProfile : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "mobileofflineprofile";
        
        public const string EntitySchemaName = "MobileOfflineProfile";
        
        public const int EntityTypeCode = 9866;
        
        public const string EntityPrimaryIdAttribute = "mobileofflineprofileid";
        
        public const string EntityPrimaryNameAttribute = "name";
        
        /// <summary>
        /// Default Constructor mobileofflineprofile
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public MobileOfflineProfile() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor mobileofflineprofile for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public MobileOfflineProfile(object anonymousObject) : 
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
        ///     (English - United States - 1033): Mobile Offline Profile
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the mobile offline profile.
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
                this.MobileOfflineProfileId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Mobile Offline Profile
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the mobile offline profile.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> MobileOfflineProfileId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(MobileOfflineProfileId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(MobileOfflineProfileId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Enter the name of the mobile offline profile.
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
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.componentstate);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ComponentState));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.componentstate, value);
                this.OnPropertyChanged(nameof(ComponentState));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows who created the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdby, value);
                this.OnPropertyChanged(nameof(CreatedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows the date and time when the record was created. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdon, value);
                this.OnPropertyChanged(nameof(CreatedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows who created the record on behalf of another user.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(CreatedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.createdonbehalfby, value);
                this.OnPropertyChanged(nameof(CreatedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Description
        /// 
        /// Description:
        ///     (English - United States - 1033): Enter a description of the mobile offline profile.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.description)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Description
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.description);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Description));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.description, value);
                this.OnPropertyChanged(nameof(Description));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Introduced Version
        /// 
        /// Description:
        ///     (English - United States - 1033): Version in which the Mobile offline Profile is introduced.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.introducedversion)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string IntroducedVersion
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.introducedversion);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IntroducedVersion));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.introducedversion, value);
                this.OnPropertyChanged(nameof(IntroducedVersion));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Managed
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.ismanaged);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsManaged));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.ismanaged, value);
                this.OnPropertyChanged(nameof(IsManaged));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Validated
        /// 
        /// Description:
        ///     (English - United States - 1033): Information about whether profile is validated or not
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.isvalidated)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsValidated
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.isvalidated);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsValidated));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.isvalidated, value);
                this.OnPropertyChanged(nameof(IsValidated));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Unique Id
        /// 
        /// Description:
        ///     (English - United States - 1033): For Internal Use Only
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.mobileofflineprofileidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> MobileOfflineProfileIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.mobileofflineprofileidunique);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(MobileOfflineProfileIdUnique));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.mobileofflineprofileidunique, value);
                this.OnPropertyChanged(nameof(MobileOfflineProfileIdUnique));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows who last updated the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedby, value);
                this.OnPropertyChanged(nameof(ModifiedBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows the date and time when the record was last updated. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedon, value);
                this.OnPropertyChanged(nameof(ModifiedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows who updated the record on behalf of another user.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedonbehalfby);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ModifiedOnBehalfBy));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.modifiedonbehalfby, value);
                this.OnPropertyChanged(nameof(ModifiedOnBehalfBy));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Organization
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the organization associated with the Mobile Offline Profile.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.organizationid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.organizationid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OrganizationId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.organizationid, value);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.overwritetime);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OverwriteTime));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.overwritetime, value);
                this.OnPropertyChanged(nameof(OverwriteTime));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Process
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows the ID of the process.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.processid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ProcessId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.processid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ProcessId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.processid, value);
                this.OnPropertyChanged(nameof(ProcessId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Published On
        /// 
        /// Description:
        ///     (English - United States - 1033): Displays the last published date time.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.publishedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> PublishedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.publishedon);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(PublishedOn));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.publishedon, value);
                this.OnPropertyChanged(nameof(PublishedOn));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Internal Use Only
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.selectedentitymetadata)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string SelectedEntityMetadata
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.selectedentitymetadata);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SelectedEntityMetadata));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.selectedentitymetadata, value);
                this.OnPropertyChanged(nameof(SelectedEntityMetadata));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.solutionid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.solutionid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SolutionId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.solutionid, value);
                this.OnPropertyChanged(nameof(SolutionId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Process Stage
        /// 
        /// Description:
        ///     (English - United States - 1033): Shows the ID of the stage.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.stageid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> StageId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.stageid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(StageId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.stageid, value);
                this.OnPropertyChanged(nameof(StageId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Traversed Path
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.traversedpath)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string TraversedPath
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.traversedpath);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TraversedPath));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.traversedpath, value);
                this.OnPropertyChanged(nameof(TraversedPath));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version Number
        /// 
        /// Description:
        ///     (English - United States - 1033): Version number of the Mobile Offline Profile.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.versionnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(VersionNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.MobileOfflineProfile.Schema.Attributes.versionnumber, value);
                this.OnPropertyChanged(nameof(VersionNumber));
            }
        }
        #endregion
    }
}
