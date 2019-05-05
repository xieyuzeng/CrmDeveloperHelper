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
    ///     (English - United States - 1033): Sdk Message Processing Step Image
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Sdk Message Processing Step Images
    /// 
    /// Description:
    ///     (English - United States - 1033): Copy of an entity's attributes before or after the core system operation.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(SdkMessageProcessingStepImage.EntityLogicalName)]
    public partial class SdkMessageProcessingStepImage : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "sdkmessageprocessingstepimage";
        
        public const string EntitySchemaName = "SdkMessageProcessingStepImage";
        
        public const int EntityTypeCode = 4615;
        
        public const string EntityPrimaryIdAttribute = "sdkmessageprocessingstepimageid";
        
        public const string EntityPrimaryNameAttribute = "name";
        
        /// <summary>
        /// Default Constructor sdkmessageprocessingstepimage
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public SdkMessageProcessingStepImage() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor sdkmessageprocessingstepimage for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public SdkMessageProcessingStepImage(object anonymousObject) : 
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
        ///     (English - United States - 1033): Unique identifier of the SDK message processing step image entity.
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
                this.SdkMessageProcessingStepImageId = value;
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the SDK message processing step image entity.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SdkMessageProcessingStepImageId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SdkMessageProcessingStepImageId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(SdkMessageProcessingStepImageId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of SdkMessage processing step image.
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
        ///     (English - United States - 1033): Attributes
        /// 
        /// Description:
        ///     (English - United States - 1033): Comma-separated list of attributes that are to be passed into the SDK message processing step image.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.attributes)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Attributes1
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.attributes);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Attributes1));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.attributes, value);
                this.OnPropertyChanged(nameof(Attributes1));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.componentstate);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.componentstate);
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
        ///     (English - United States - 1033): Created By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the SDK message processing step image.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.createdby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the SDK message processing step image was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.createdon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the sdkmessageprocessingstepimage.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.createdonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.createdonbehalfby);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Customization level of the SDK message processing step image.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.customizationlevel)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> CustomizationLevel
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.customizationlevel);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Description
        /// 
        /// Description:
        ///     (English - United States - 1033): Description of the SDK message processing step image.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.description)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Description
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.description);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Description));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.description, value);
                this.OnPropertyChanged(nameof(Description));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Entity Alias
        /// 
        /// Description:
        ///     (English - United States - 1033): Key name used to access the pre-image or post-image property bags in a step.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.entityalias)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string EntityAlias
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.entityalias);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(EntityAlias));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.entityalias, value);
                this.OnPropertyChanged(nameof(EntityAlias));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Image Type
        /// 
        /// Description:
        ///     (English - United States - 1033): Type of image requested.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.imagetype)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ImageType
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.imagetype);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ImageType));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.imagetype, value);
                this.OnPropertyChanged(nameof(ImageType));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Image Type
        /// 
        /// Description:
        ///     (English - United States - 1033): Type of image requested.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.imagetype)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.OptionSets.imagetype> ImageTypeEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.imagetype);
                if (((optionSet != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.OptionSets.imagetype), optionSet.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.OptionSets.imagetype)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.OptionSets.imagetype), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ImageTypeEnum));
                this.OnPropertyChanging(nameof(ImageType));
                if ((value == null))
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.imagetype, null);
                }
                else
                {
                    this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.imagetype, new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged(nameof(ImageType));
                this.OnPropertyChanged(nameof(ImageTypeEnum));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Introduced Version
        /// 
        /// Description:
        ///     (English - United States - 1033): Version in which the form is introduced.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.introducedversion)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string IntroducedVersion
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.introducedversion);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IntroducedVersion));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.introducedversion, value);
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
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.iscustomizable)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.BooleanManagedProperty IsCustomizable
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.iscustomizable);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsCustomizable));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.iscustomizable, value);
                this.OnPropertyChanged(nameof(IsCustomizable));
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Message Property Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the property on the Request message.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.messagepropertyname)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string MessagePropertyName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.messagepropertyname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(MessagePropertyName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.messagepropertyname, value);
                this.OnPropertyChanged(nameof(MessagePropertyName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the SDK message processing step.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.modifiedby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the SDK message processing step was last modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.modifiedon)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the sdkmessageprocessingstepimage.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.modifiedonbehalfby)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.modifiedonbehalfby);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the organization with which the SDK message processing step is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.organizationid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.organizationid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Overwrite Time
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Related Attribute Name
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the related entity.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.relatedattributename)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RelatedAttributeName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.relatedattributename);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(RelatedAttributeName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.relatedattributename, value);
                this.OnPropertyChanged(nameof(RelatedAttributeName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): SDK Message Processing Step
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the SDK message processing step.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.sdkmessageprocessingstepid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference SdkMessageProcessingStepId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.sdkmessageprocessingstepid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SdkMessageProcessingStepId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.sdkmessageprocessingstepid, value);
                this.OnPropertyChanged(nameof(SdkMessageProcessingStepId));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the SDK message processing step image.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.sdkmessageprocessingstepimageidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SdkMessageProcessingStepImageIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.sdkmessageprocessingstepimageidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.solutionid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Number that identifies a specific revision of the step image. 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageProcessingStepImage.Schema.Attributes.versionnumber);
            }
        }
        #endregion
    }
}
