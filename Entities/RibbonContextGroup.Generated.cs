﻿
namespace Nav.Common.VSPackages.CrmDeveloperHelper.Entities
{
    ///<summary>
    /// DisplayName:
    /// (English - United States - 1033): Ribbon Context Group
    /// (Russian - 1049): Контекстная группа ленты
    /// 
    /// DisplayCollectionName:
    /// (English - United States - 1033): Ribbon Context Groups
    /// (Russian - 1049): Контекстные группы ленты
    /// 
    /// Description:
    /// (English - United States - 1033): Groupings of contextual tabs.
    /// (Russian - 1049): Группы контекстных вкладок.
    /// 
    /// PropertyName                          Value                         CanBeChanged
    /// ActivityTypeMask                      0
    /// AutoCreateAccessTeams                 False
    /// AutoRouteToOwnerQueue                 False
    /// CanBeInManyToMany                     False                         False
    /// CanBePrimaryEntityInRelationship      False                         False
    /// CanBeRelatedEntityInRelationship      False                         False
    /// CanChangeHierarchicalRelationship     False                         False
    /// CanChangeTrackingBeEnabled            False                         False
    /// CanCreateAttributes                   False                         False
    /// CanCreateCharts                       False                         False
    /// CanCreateForms                        False                         False
    /// CanCreateViews                        False                         False
    /// CanEnableSyncToExternalSearchIndex    False                         False
    /// CanModifyAdditionalSettings           False                         True
    /// CanTriggerWorkflow                    False
    /// ChangeTrackingEnabled                 False
    /// CollectionSchemaName                  RibbonContextGroups
    /// DaysSinceRecordLastModified           0
    /// EnforceStateTransitions               False
    /// EntityColor
    /// EntityHelpUrl
    /// EntityHelpUrlEnabled                  False
    /// EntitySetName                         ribboncontextgroups
    /// IconLargeName
    /// IconMediumName
    /// IconSmallName
    /// IsActivity                            False
    /// IsActivityParty                       False
    /// IsAIRUpdated                          False
    /// IsAuditEnabled                        False                         False
    /// IsAvailableOffline                    True
    /// IsBusinessProcessEnabled              False
    /// IsChildEntity                         False
    /// IsConnectionsEnabled                  False                         False
    /// IsCustomEntity                        False
    /// IsCustomizable                        False                         False
    /// IsDocumentManagementEnabled           False
    /// IsDuplicateDetectionEnabled           False                         False
    /// IsEnabledForCharts                    False
    /// IsEnabledForExternalChannels          False
    /// IsEnabledForTrace                     False
    /// IsImportable                          False
    /// IsInteractionCentricEnabled           False
    /// IsIntersect                           False
    /// IsKnowledgeManagementEnabled          False
    /// IsMailMergeEnabled                    False                         False
    /// IsMappable                            False                         False
    /// IsOfflineInMobileClient               False                         True
    /// IsOneNoteIntegrationEnabled           False
    /// IsOptimisticConcurrencyEnabled        True
    /// IsPrivate                             True
    /// IsQuickCreateEnabled                  False
    /// IsReadingPaneEnabled                  True
    /// IsReadOnlyInMobileClient              False                         False
    /// IsRenameable                          False                         False
    /// IsStateModelAware                     False
    /// IsValidForAdvancedFind                False
    /// IsValidForQueue                       False                         False
    /// IsVisibleInMobile                     False                         False
    /// IsVisibleInMobileClient               False                         False
    /// LogicalCollectionName                 ribboncontextgroups
    /// LogicalName                           ribboncontextgroup
    /// ObjectTypeCode                        1115
    /// OwnershipType                         OrganizationOwned
    /// RecurrenceBaseEntityLogicalName
    /// ReportViewName                        FilteredRibbonContextGroup
    /// SchemaName                            RibbonContextGroup
    /// SyncToExternalSearchIndex             False
    ///</summary>
    public partial class RibbonContextGroup
    {
        public static partial class Schema
        {
            public const string EntityLogicalName = "ribboncontextgroup";

            public const string EntitySchemaName = "RibbonContextGroup";

            public const string EntityPrimaryIdAttribute = "ribboncontextgroupid";

            #region Attributes.

            public static partial class Attributes
            {
                ///<summary>
                /// DisplayName:
                ///     (English - United States - 1033): Component State
                ///     (Russian - 1049): Состояние компонента
                /// 
                /// Description:
                ///     (English - United States - 1033): For internal use only.
                ///     (Russian - 1049): Только для внутреннего использования.
                /// 
                /// SchemaName: ComponentState
                /// PicklistAttributeMetadata    AttributeType: Picklist    AttributeTypeName: PicklistType    RequiredLevel: SystemRequired
                /// IsValidForCreate: False    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: 0
                /// Global System  OptionSet componentstate
                /// DefaultFormValue = -1
                ///</summary>
                public const string componentstate = "componentstate";

                ///<summary>
                /// Description:
                ///     (English - United States - 1033): The id of a group of contextual tabs.
                ///     (Russian - 1049): Идентификатор группы контекстных вкладок.
                /// 
                /// SchemaName: ContextGroupId
                /// MemoAttributeMetadata    AttributeType: Memo    AttributeTypeName: MemoType    RequiredLevel: SystemRequired
                /// IsValidForCreate: True    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                /// MaxLength = 200
                /// Format = Text    ImeMode = Auto    IsLocalizable = False
                ///</summary>
                public const string contextgroupid = "contextgroupid";

                ///<summary>
                /// Description:
                ///     (English - United States - 1033): Layout XML for a contextual group header
                ///     (Russian - 1049): XML-файл макета для заголовка контекстной группы
                /// 
                /// SchemaName: ContextGroupXml
                /// MemoAttributeMetadata    AttributeType: Memo    AttributeTypeName: MemoType    RequiredLevel: ApplicationRequired
                /// IsValidForCreate: True    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                /// MaxLength = 1073741823
                /// Format = TextArea    ImeMode = Auto    IsLocalizable = False
                ///</summary>
                public const string contextgroupxml = "contextgroupxml";

                ///<summary>
                /// Description:
                ///     (English - United States - 1033): The entity this rule applies to, also the entity this rule was imported from, will be exported to.
                ///     (Russian - 1049): Сущность, к которой применяется это правило, также сущность, из которой это правило было импортировано или в которую оно будет экспортировано.
                /// 
                /// SchemaName: Entity
                /// StringAttributeMetadata    AttributeType: String    AttributeTypeName: StringType    RequiredLevel: None
                /// IsValidForCreate: True    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: 0
                /// MaxLength = 50
                /// Format = Text    ImeMode = Auto    IsLocalizable = False
                ///</summary>
                public const string entity = "entity";

                ///<summary>
                /// SchemaName: IsManaged
                /// BooleanAttributeMetadata    AttributeType: Boolean    AttributeTypeName: BooleanType    RequiredLevel: SystemRequired
                /// IsValidForCreate: False    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: 0
                /// DefaultValue = False
                /// 
                /// DisplayName:
                ///     (English - United States - 1033): Unmanaged
                ///     (Russian - 1049): Неуправляемый
                /// FalseOption = 0
                /// 
                /// DisplayName:
                ///     (English - United States - 1033): Managed
                ///     (Russian - 1049): Управляемый
                /// TrueOption = 1
                ///</summary>
                public const string ismanaged = "ismanaged";

                ///<summary>
                /// Description:
                ///     (English - United States - 1033): Unique identifier of the organization.
                ///     (Russian - 1049): Уникальный идентификатор организации.
                /// 
                /// SchemaName: OrganizationId
                /// LookupAttributeMetadata    AttributeType: Lookup    AttributeTypeName: LookupType    RequiredLevel: SystemRequired
                /// IsValidForCreate: False    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                /// Targets: organization
                /// 
                ///     Target organization    PrimaryIdAttribute organizationid    PrimaryNameAttribute name
                ///         DisplayName:
                ///         (English - United States - 1033): Organization
                ///         (Russian - 1049): Предприятие
                ///         
                ///         DisplayCollectionName:
                ///         (English - United States - 1033): Organizations
                ///         (Russian - 1049): Предприятия
                ///         
                ///         Description:
                ///         (English - United States - 1033): Top level of the Microsoft Dynamics 365 business hierarchy. The organization can be a specific business, holding company, or corporation.
                ///         (Russian - 1049): Верхний уровень бизнес-иерархии Microsoft Dynamics 365. Организация может являться конкретной компанией, холдингом или корпорацией.
                ///</summary>
                public const string organizationid = "organizationid";

                ///<summary>
                /// DisplayName:
                ///     (English - United States - 1033): Record Overwrite Time
                ///     (Russian - 1049): Время замены записи
                /// 
                /// Description:
                ///     (English - United States - 1033): For internal use only.
                ///     (Russian - 1049): Только для внутреннего использования.
                /// 
                /// SchemaName: OverwriteTime
                /// DateTimeAttributeMetadata    AttributeType: DateTime    AttributeTypeName: DateTimeType    RequiredLevel: SystemRequired
                /// IsValidForCreate: False    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: 0
                /// DateTimeBehavior = UserLocal    CanChangeDateTimeBehavior = False
                /// ImeMode = Inactive    Format = DateOnly
                ///</summary>
                public const string overwritetime = "overwritetime";

                ///<summary>
                /// DisplayName:
                ///     (English - United States - 1033): Primary Key
                ///     (Russian - 1049): Первичный ключ
                /// 
                /// Description:
                ///     (English - United States - 1033): Unique identifier.
                ///     (Russian - 1049): Уникальный идентификатор.
                /// 
                /// SchemaName: RibbonContextGroupId
                /// AttributeMetadata    AttributeType: Uniqueidentifier    AttributeTypeName: UniqueidentifierType    RequiredLevel: SystemRequired
                /// IsValidForCreate: True    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                ///</summary>
                public const string ribboncontextgroupid = "ribboncontextgroupid";

                ///<summary>
                /// Description:
                ///     (English - United States - 1033): Unique identifier of the form used when synchronizing customizations for the Microsoft Dynamics 365 client for Outlook.
                ///     (Russian - 1049): Уникальный идентификатор формы, используемой при синхронизации настроек для клиента Microsoft Dynamics 365 для Outlook.
                /// 
                /// SchemaName: RibbonContextGroupUniqueId
                /// AttributeMetadata    AttributeType: Uniqueidentifier    AttributeTypeName: UniqueidentifierType    RequiredLevel: SystemRequired
                /// IsValidForCreate: False    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                ///</summary>
                public const string ribboncontextgroupuniqueid = "ribboncontextgroupuniqueid";

                ///<summary>
                /// Description:
                ///     (English - United States - 1033): Unique identifier of the ribbon customization with which the ribbon command is associated.
                ///     (Russian - 1049): Уникальный идентификатор настройки ленты, с которой связана команда ленты.
                /// 
                /// SchemaName: RibbonCustomizationId
                /// LookupAttributeMetadata    AttributeType: Lookup    AttributeTypeName: LookupType    RequiredLevel: ApplicationRequired
                /// IsValidForCreate: True    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                /// Targets: ribboncustomization
                /// 
                ///     Target ribboncustomization    PrimaryIdAttribute ribboncustomizationid
                ///         DisplayName:
                ///         (English - United States - 1033): Application Ribbons
                ///         (Russian - 1049): Ленты приложения
                ///         
                ///         DisplayCollectionName:
                ///         
                ///         Description:
                ///         (English - United States - 1033): Ribbon customizations for the application ribbon and entity ribbon templates.
                ///         (Russian - 1049): Настройки ленты для ленты приложения и шаблоны ленты сущности.
                ///</summary>
                public const string ribboncustomizationid = "ribboncustomizationid";

                ///<summary>
                /// DisplayName:
                ///     (English - United States - 1033): Solution
                ///     (Russian - 1049): Решение
                /// 
                /// Description:
                ///     (English - United States - 1033): Unique identifier of the associated solution.
                ///     (Russian - 1049): Уникальный идентификатор связанного решения.
                /// 
                /// SchemaName: SolutionId
                /// AttributeMetadata    AttributeType: Uniqueidentifier    AttributeTypeName: UniqueidentifierType    RequiredLevel: SystemRequired
                /// IsValidForCreate: False    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                ///</summary>
                public const string solutionid = "solutionid";

                ///<summary>
                /// DisplayName:
                ///     (English - United States - 1033): Solution
                ///     (Russian - 1049): Решение
                /// 
                /// Description:
                ///     (English - United States - 1033): For internal use only.
                ///     (Russian - 1049): Только для внутреннего использования.
                /// 
                /// SchemaName: SupportingSolutionId
                /// AttributeMetadata    AttributeType: Uniqueidentifier    AttributeTypeName: UniqueidentifierType    RequiredLevel: None
                /// IsValidForCreate: False    IsValidForRead: False    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                ///</summary>
                public const string supportingsolutionid = "supportingsolutionid";

                ///<summary>
                /// Description:
                ///     (English - United States - 1033): Represents a version of customizations to be synchronized with the Microsoft Dynamics 365 client for Outlook.
                ///     (Russian - 1049): Указывает версию из настроек для синхронизации с клиентом Microsoft Dynamics 365 для Outlook.
                /// 
                /// SchemaName: VersionNumber
                /// BigIntAttributeMetadata    AttributeType: BigInt    AttributeTypeName: BigIntType    RequiredLevel: None
                /// IsValidForCreate: False    IsValidForRead: True    IsValidForUpdate: False    IsValidForAdvancedFind: False    CanBeChanged = False
                /// IsLogical: False    IsSecured: False    IsCustomAttribute: False    SourceType: Simple
                /// MinValue = -9223372036854775808    MaxValue = 9223372036854775807
                ///</summary>
                public const string versionnumber = "versionnumber";
            }

            #endregion Attributes.

            #region Relationship ManyToOne - N:1.

            public static partial class ManyToOne
            {
                ///<summary>
                /// N:1 - Relationship organization_ribbon_context_group
                /// 
                /// PropertyName                               Value                                CanBeChanged
                /// IsHierarchical                             False
                /// ReferencedEntityNavigationPropertyName     organization_ribbon_context_group
                /// ReferencingEntityNavigationPropertyName    organizationid
                /// IsCustomizable                             False                                False
                /// IsCustomRelationship                       False
                /// IsValidForAdvancedFind                     False
                /// RelationshipType                           OneToManyRelationship
                /// SecurityTypes                              None
                /// CascadeConfiguration.Assign                NoCascade
                /// CascadeConfiguration.Delete                NoCascade
                /// CascadeConfiguration.Merge                 NoCascade
                /// CascadeConfiguration.Reparent              NoCascade
                /// CascadeConfiguration.Share                 NoCascade
                /// CascadeConfiguration.Unshare               NoCascade
                /// AssociatedMenuConfiguration.Behavior       DoNotDisplay
                /// AssociatedMenuConfiguration.Group          Details
                /// AssociatedMenuConfiguration.Order          null
                /// 
                /// ReferencedEntity organization:
                ///     DisplayName:
                ///     (English - United States - 1033): Organization
                ///     (Russian - 1049): Предприятие
                ///     
                ///     DisplayCollectionName:
                ///     (English - United States - 1033): Organizations
                ///     (Russian - 1049): Предприятия
                ///     
                ///     Description:
                ///     (English - United States - 1033): Top level of the Microsoft Dynamics 365 business hierarchy. The organization can be a specific business, holding company, or corporation.
                ///     (Russian - 1049): Верхний уровень бизнес-иерархии Microsoft Dynamics 365. Организация может являться конкретной компанией, холдингом или корпорацией.
                ///</summary>
                public static partial class organization_ribbon_context_group
                {
                    public const string Name = "organization_ribbon_context_group";

                    public const string ReferencedEntity_organization = "organization";

                    public const string ReferencedAttribute_organizationid = "organizationid";

                    public const string ReferencedEntity_PrimaryNameAttribute_name = "name";

                    public const string ReferencingEntity_ribboncontextgroup = "ribboncontextgroup";

                    public const string ReferencingAttribute_organizationid = "organizationid";
                }

                ///<summary>
                /// N:1 - Relationship RibbonCustomization_RibbonContextGroup
                /// 
                /// PropertyName                               Value                                     CanBeChanged
                /// IsHierarchical                             False
                /// ReferencedEntityNavigationPropertyName     RibbonCustomization_RibbonContextGroup
                /// ReferencingEntityNavigationPropertyName    ribboncustomizationid
                /// IsCustomizable                             False                                     False
                /// IsCustomRelationship                       False
                /// IsValidForAdvancedFind                     False
                /// RelationshipType                           OneToManyRelationship
                /// SecurityTypes                              ParentChild
                /// CascadeConfiguration.Assign                NoCascade
                /// CascadeConfiguration.Delete                Cascade
                /// CascadeConfiguration.Merge                 NoCascade
                /// CascadeConfiguration.Reparent              NoCascade
                /// CascadeConfiguration.Share                 NoCascade
                /// CascadeConfiguration.Unshare               NoCascade
                /// AssociatedMenuConfiguration.Behavior       DoNotDisplay
                /// AssociatedMenuConfiguration.Group          Details
                /// AssociatedMenuConfiguration.Order          null
                /// 
                /// ReferencedEntity ribboncustomization:
                ///     DisplayName:
                ///     (English - United States - 1033): Application Ribbons
                ///     (Russian - 1049): Ленты приложения
                ///     
                ///     DisplayCollectionName:
                ///     
                ///     Description:
                ///     (English - United States - 1033): Ribbon customizations for the application ribbon and entity ribbon templates.
                ///     (Russian - 1049): Настройки ленты для ленты приложения и шаблоны ленты сущности.
                ///</summary>
                public static partial class ribboncustomization_ribboncontextgroup
                {
                    public const string Name = "RibbonCustomization_RibbonContextGroup";

                    public const string ReferencedEntity_ribboncustomization = "ribboncustomization";

                    public const string ReferencedAttribute_ribboncustomizationid = "ribboncustomizationid";

                    public const string ReferencingEntity_ribboncontextgroup = "ribboncontextgroup";

                    public const string ReferencingAttribute_ribboncustomizationid = "ribboncustomizationid";
                }
            }

            #endregion Relationship ManyToOne - N:1.

            #region Relationship OneToMany - 1:N.

            public static partial class OneToMany
            {
                ///<summary>
                /// 1:N - Relationship userentityinstancedata_ribboncontextgroup
                /// 
                /// PropertyName                               Value                                        CanBeChanged
                /// IsHierarchical                             False
                /// ReferencedEntityNavigationPropertyName     userentityinstancedata_ribboncontextgroup
                /// ReferencingEntityNavigationPropertyName    objectid_ribboncontextgroup
                /// IsCustomizable                             False                                        False
                /// IsCustomRelationship                       False
                /// IsValidForAdvancedFind                     False
                /// RelationshipType                           OneToManyRelationship
                /// SecurityTypes                              None
                /// CascadeConfiguration.Assign                NoCascade
                /// CascadeConfiguration.Delete                Cascade
                /// CascadeConfiguration.Merge                 NoCascade
                /// CascadeConfiguration.Reparent              NoCascade
                /// CascadeConfiguration.Share                 NoCascade
                /// CascadeConfiguration.Unshare               NoCascade
                /// AssociatedMenuConfiguration.Behavior       DoNotDisplay
                /// AssociatedMenuConfiguration.Group          Details
                /// AssociatedMenuConfiguration.Order          null
                /// 
                /// ReferencingEntity userentityinstancedata:
                ///     DisplayName:
                ///     (English - United States - 1033): User Entity Instance Data
                ///     (Russian - 1049): Данные экземпляра сущности пользователя
                ///     
                ///     DisplayCollectionName:
                ///     
                ///     Description:
                ///     (English - United States - 1033): Per User item instance data
                ///     (Russian - 1049): Данные экземпляра позиции "на пользователя"
                ///</summary>
                public static partial class userentityinstancedata_ribboncontextgroup
                {
                    public const string Name = "userentityinstancedata_ribboncontextgroup";

                    public const string ReferencedEntity_ribboncontextgroup = "ribboncontextgroup";

                    public const string ReferencedAttribute_ribboncontextgroupid = "ribboncontextgroupid";

                    public const string ReferencingEntity_userentityinstancedata = "userentityinstancedata";

                    public const string ReferencingAttribute_objectid = "objectid";
                }
            }

            #endregion Relationship OneToMany - 1:N.
        }
    }
}
