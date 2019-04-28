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
    ///     (English - United States - 1033): Attachment
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Attachments
    /// 
    /// Description:
    ///     (English - United States - 1033): MIME attachment for an activity.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(ActivityMimeAttachment.EntityLogicalName)]
    public partial class ActivityMimeAttachment : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "activitymimeattachment";
        
        public const string EntitySchemaName = "ActivityMimeAttachment";
        
        public const int EntityTypeCode = 1001;
        
        public const string EntityPrimaryIdAttribute = "activitymimeattachmentid";
        
        public const string EntityPrimaryNameAttribute = "filename";
        
        /// <summary>
        /// Default Constructor activitymimeattachment
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ActivityMimeAttachment() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor activitymimeattachment for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ActivityMimeAttachment(object anonymousObject) : 
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
        ///     (English - United States - 1033): Activity Mime Attachment
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the attachment.
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
                this.ActivityMimeAttachmentId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Activity Mime Attachment
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ActivityMimeAttachmentId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ActivityMimeAttachmentId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(ActivityMimeAttachmentId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): File Name
        /// 
        /// Description:
        ///     (English - United States - 1033): File name of the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryNameAttribute)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string FileName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(EntityPrimaryNameAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(FileName));
                this.SetAttributeValue(EntityPrimaryNameAttribute, value);
                this.OnPropertyChanged(nameof(FileName));
            }
        }
        #endregion
        
        #region Attributes
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Regarding
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the activity with which the attachment is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activityid)]
        [System.ObsoleteAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ActivityId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activityid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ActivityId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activityid, value);
                this.OnPropertyChanged(nameof(ActivityId));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activitymimeattachmentidunique)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> ActivityMimeAttachmentIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activitymimeattachmentidunique);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ActivityMimeAttachmentIdUnique));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activitymimeattachmentidunique, value);
                this.OnPropertyChanged(nameof(ActivityMimeAttachmentIdUnique));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): ActivitySubject
        /// 
        /// Description:
        ///     (English - United States - 1033): Descriptive subject for the activity.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activitysubject)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string ActivitySubject
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activitysubject);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ActivitySubject));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.activitysubject, value);
                this.OnPropertyChanged(nameof(ActivitySubject));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): For internal use only.
        /// 
        /// Description:
        ///     (English - United States - 1033): anonymous link
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.anonymouslink)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string AnonymousLink
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.anonymouslink);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AnonymousLink));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.anonymouslink, value);
                this.OnPropertyChanged(nameof(AnonymousLink));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Content Id
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentcontentid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string AttachmentContentId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentcontentid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AttachmentContentId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentcontentid, value);
                this.OnPropertyChanged(nameof(AttachmentContentId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Attachment
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the attachment with which this activitymimeattachment is associated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference AttachmentId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AttachmentId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentid, value);
                this.OnPropertyChanged(nameof(AttachmentId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Attachment Number
        /// 
        /// Description:
        ///     (English - United States - 1033): Number of the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> AttachmentNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(AttachmentNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.attachmentnumber, value);
                this.OnPropertyChanged(nameof(AttachmentNumber));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Body
        /// 
        /// Description:
        ///     (English - United States - 1033): Contents of the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.body)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Body
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.body);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Body));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.body, value);
                this.OnPropertyChanged(nameof(Body));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.componentstate)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.componentstate);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ComponentState));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.componentstate, value);
                this.OnPropertyChanged(nameof(ComponentState));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): File Size (Bytes)
        /// 
        /// Description:
        ///     (English - United States - 1033): File size of the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.filesize)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> FileSize
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.filesize);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(FileSize));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.filesize, value);
                this.OnPropertyChanged(nameof(FileSize));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Followed
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates if this attachment is followed.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.isfollowed)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsFollowed
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.isfollowed);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsFollowed));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.isfollowed, value);
                this.OnPropertyChanged(nameof(IsFollowed));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Managed
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates whether the solution component is part of a managed solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.ismanaged);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(IsManaged));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.ismanaged, value);
                this.OnPropertyChanged(nameof(IsManaged));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Mime Type
        /// 
        /// Description:
        ///     (English - United States - 1033): MIME type of the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.mimetype)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string MimeType
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.mimetype);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(MimeType));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.mimetype, value);
                this.OnPropertyChanged(nameof(MimeType));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Item
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the record with which the attachment is associated
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.objectid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ObjectId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.objectid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ObjectId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.objectid, value);
                this.OnPropertyChanged(nameof(ObjectId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Entity
        /// 
        /// Description:
        ///     (English - United States - 1033): Object Type Code of the entity that is associated with the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.objecttypecode)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string ObjectTypeCode
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.objecttypecode);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(ObjectTypeCode));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.objecttypecode, value);
                this.OnPropertyChanged(nameof(ObjectTypeCode));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Overwrite Time
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.overwritetime)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.overwritetime);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OverwriteTime));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.overwritetime, value);
                this.OnPropertyChanged(nameof(OverwriteTime));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owner
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user or team who owns the activity_mime_attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.ownerid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OwnerId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.ownerid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OwnerId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.ownerid, value);
                this.OnPropertyChanged(nameof(OwnerId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owning Business Unit
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the business unit that owns the activity mime attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.owningbusinessunit)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OwningBusinessUnit
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.owningbusinessunit);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OwningBusinessUnit));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.owningbusinessunit, value);
                this.OnPropertyChanged(nameof(OwningBusinessUnit));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Owner
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who owns the activity mime attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.owninguser)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OwningUser
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.owninguser);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(OwningUser));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.owninguser, value);
                this.OnPropertyChanged(nameof(OwningUser));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.solutionid)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.solutionid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(SolutionId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.solutionid, value);
                this.OnPropertyChanged(nameof(SolutionId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Subject
        /// 
        /// Description:
        ///     (English - United States - 1033): Descriptive subject for the attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.subject)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Subject
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.subject);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Subject));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.subject, value);
                this.OnPropertyChanged(nameof(Subject));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version Number
        /// 
        /// Description:
        ///     (English - United States - 1033): Version number of the activity mime attachment.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.versionnumber);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(VersionNumber));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.ActivityMimeAttachment.Schema.Attributes.versionnumber, value);
                this.OnPropertyChanged(nameof(VersionNumber));
            }
        }
        #endregion
    }
}
