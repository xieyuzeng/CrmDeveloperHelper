//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Entities
{
    
    
    /// <summary>
    /// DisplayName:
    ///     (English - United States - 1033): Plug-in Type
    ///     (Russian - 1049): Тип подключаемого модуля
    /// 
    /// DisplayCollectionName:
    ///     (English - United States - 1033): Plug-in Types
    ///     (Russian - 1049): Типы подключаемых модулей
    /// 
    /// Description:
    ///     (English - United States - 1033): Type that inherits from the IPlugin interface and is contained within a plug-in assembly.
    ///     (Russian - 1049): Тип, производный от интерфейса IPlugin, и содержащийся в сборке подключаемого модуля.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute(PluginType.EntityLogicalName)]
    [System.ComponentModel.DescriptionAttribute("Plug-in Type")]
    public partial class PluginType : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        
        public const string EntityLogicalName = "plugintype";
        
        public const string EntitySchemaName = "PluginType";
        
        public const int EntityTypeCode = 4602;
        
        public const string EntityPrimaryIdAttribute = "plugintypeid";
        
        public const string EntityPrimaryNameAttribute = "name";
        
        /// <summary>
        /// Default Constructor plugintype
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public PluginType() : 
                base(EntityLogicalName)
        {
        }
        
        /// <summary>
        /// Constructor plugintype for populating via LINQ queries given a LINQ anonymous type object
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public PluginType(object anonymousObject) : 
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
        ///     (English - United States - 1033): Plug-in Type
        ///     (Russian - 1049): Тип подключаемого модуля
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the plug-in type.
        ///     (Russian - 1049): Уникальный идентификатор подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Plug-in Type")]
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
                this.PluginTypeId = value;
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Plug-in Type
        ///     (Russian - 1049): Тип подключаемого модуля
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the plug-in type.
        ///     (Russian - 1049): Уникальный идентификатор подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryIdAttribute)]
        [System.ComponentModel.DescriptionAttribute("Plug-in Type")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> PluginTypeId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(EntityPrimaryIdAttribute);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(PluginTypeId));
                this.SetAttributeValue(EntityPrimaryIdAttribute, value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged(nameof(PluginTypeId));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Name
        ///     (Russian - 1049): Название
        /// 
        /// Description:
        ///     (English - United States - 1033): Name of the plug-in type.
        ///     (Russian - 1049): Название типа подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(EntityPrimaryNameAttribute)]
        [System.ComponentModel.DescriptionAttribute("Name")]
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
        ///     (English - United States - 1033): Assembly Name
        ///     (Russian - 1049): Название сборки
        /// 
        /// Description:
        ///     (English - United States - 1033): Full path name of the plug-in assembly.
        ///     (Russian - 1049): Полный путь к сборке подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.assemblyname)]
        [System.ComponentModel.DescriptionAttribute("Assembly Name")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string AssemblyName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.assemblyname);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        ///     (Russian - 1049): Состояние компонента
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.componentstate);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Component State
        ///     (Russian - 1049): Состояние компонента
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.componentstate)]
        [System.ComponentModel.DescriptionAttribute("Component State")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate> ComponentStateEnum
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSetValue = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.componentstate);
                if (((optionSetValue != null) 
                            && System.Enum.IsDefined(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate), optionSetValue.Value)))
                {
                    return ((Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate)(System.Enum.ToObject(typeof(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.GlobalOptionSets.componentstate), optionSetValue.Value)));
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
        ///     (Russian - 1049): Создано
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who created the plug-in type.
        ///     (Russian - 1049): Уникальный идентификатор пользователя, создавшего тип подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.createdby)]
        [System.ComponentModel.DescriptionAttribute("Created By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.createdby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created On
        ///     (Russian - 1049): Дата создания
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the plug-in type was created.
        ///     (Russian - 1049): Дата и время создания подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.createdon)]
        [System.ComponentModel.DescriptionAttribute("Created On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> CreatedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.createdon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Created By (Delegate)
        ///     (Russian - 1049): Кем создано (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who created the plugintype.
        ///     (Russian - 1049): Уникальный идентификатор пользователя-делегата, создавшего plugintype.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.createdonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Created By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.createdonbehalfby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Culture
        ///     (Russian - 1049): Культура
        /// 
        /// Description:
        ///     (English - United States - 1033): Culture code for the plug-in assembly.
        ///     (Russian - 1049): Код культуры для сборки подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.culture)]
        [System.ComponentModel.DescriptionAttribute("Culture")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Culture
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.culture);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Customization level of the plug-in type.
        ///     (Russian - 1049): Уровень настройки типа подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.customizationlevel)]
        [System.ComponentModel.DescriptionAttribute("Customization level of the plug-in type.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> CustomizationLevel
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.customizationlevel);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Custom Workflow Activity Info
        ///     (Russian - 1049): Сведения о настраиваемом действии бизнес-процесса
        /// 
        /// Description:
        ///     (English - United States - 1033): Serialized Custom Activity Type information, including required arguments. For more information, see SandboxCustomActivityInfo.
        ///     (Russian - 1049): Сведения о сериализованном настраиваемом типе действия. Дополнительные сведения см. в описании SandboxCustomActivityInfo.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.customworkflowactivityinfo)]
        [System.ComponentModel.DescriptionAttribute("Custom Workflow Activity Info")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string CustomWorkflowActivityInfo
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.customworkflowactivityinfo);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Description
        ///     (Russian - 1049): Описание
        /// 
        /// Description:
        ///     (English - United States - 1033): Description of the plug-in type.
        ///     (Russian - 1049): Описание типа подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.description)]
        [System.ComponentModel.DescriptionAttribute("Description")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Description
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.description);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(Description));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.description, value);
                this.OnPropertyChanged(nameof(Description));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Display Name
        ///     (Russian - 1049): Отображаемое имя
        /// 
        /// Description:
        ///     (English - United States - 1033): User friendly name for the plug-in.
        ///     (Russian - 1049): Понятное имя пользователя подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.friendlyname)]
        [System.ComponentModel.DescriptionAttribute("Display Name")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string FriendlyName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.friendlyname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(FriendlyName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.friendlyname, value);
                this.OnPropertyChanged(nameof(FriendlyName));
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.ismanaged)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsManaged
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.ismanaged);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Is Workflow Activity
        ///     (Russian - 1049): Является действием бизнес-процесса?
        /// 
        /// Description:
        ///     (English - United States - 1033): Indicates if the plug-in is a custom activity for workflows.
        ///     (Russian - 1049): Указывает, является ли подключаемый модуль настраиваемым действием для бизнес-процессов.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.isworkflowactivity)]
        [System.ComponentModel.DescriptionAttribute("Is Workflow Activity")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<bool> IsWorkflowActivity
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<bool>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.isworkflowactivity);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version major
        ///     (Russian - 1049): Основной номер версии
        /// 
        /// Description:
        ///     (English - United States - 1033): Major of the version number of the assembly for the plug-in type.
        ///     (Russian - 1049): Основной номер версии сборки типа подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.major)]
        [System.ComponentModel.DescriptionAttribute("Version major")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> Major
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.major);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version minor
        ///     (Russian - 1049): Дополнительный номер версии
        /// 
        /// Description:
        ///     (English - United States - 1033): Minor of the version number of the assembly for the plug-in type.
        ///     (Russian - 1049): Дополнительный номер версии сборки типа подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.minor)]
        [System.ComponentModel.DescriptionAttribute("Version minor")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<int> Minor
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.minor);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By
        ///     (Russian - 1049): Изменено
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the user who last modified the plug-in type.
        ///     (Russian - 1049): Уникальный идентификатор последнего пользователя, изменившего подключаемый модуль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.modifiedby)]
        [System.ComponentModel.DescriptionAttribute("Modified By")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.modifiedby);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified On
        ///     (Russian - 1049): Дата изменения
        /// 
        /// Description:
        ///     (English - United States - 1033): Date and time when the plug-in type was last modified.
        ///     (Russian - 1049): Дата и время последнего изменения подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.modifiedon)]
        [System.ComponentModel.DescriptionAttribute("Modified On")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.modifiedon);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Modified By (Delegate)
        ///     (Russian - 1049): Кем изменено (делегат)
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the delegate user who last modified the plugintype.
        ///     (Russian - 1049): Уникальный идентификатор пользователя-делегата, последним изменившего plugintype.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.modifiedonbehalfby)]
        [System.ComponentModel.DescriptionAttribute("Modified By (Delegate)")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.modifiedonbehalfby);
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the organization with which the plug-in type is associated.
        ///     (Russian - 1049): Уникальный идентификатор организации, с которой связан подключаемый модуль.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.organizationid)]
        [System.ComponentModel.DescriptionAttribute("Unique identifier of the organization with which the plug-in type is associated.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference OrganizationId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.organizationid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Record Overwrite Time
        ///     (Russian - 1049): Время замены записи
        /// 
        /// Description:
        ///     (English - United States - 1033): For internal use only.
        ///     (Russian - 1049): Только для внутреннего использования.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.overwritetime)]
        [System.ComponentModel.DescriptionAttribute("Record Overwrite Time")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.DateTime> OverwriteTime
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.overwritetime);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Plugin Assembly
        ///     (Russian - 1049): Сборка подключаемого модуля
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the plug-in assembly that contains this plug-in type.
        ///     (Russian - 1049): Уникальный идентификатор сборки подключаемого модуля, включающей этот тип подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.pluginassemblyid)]
        [System.ComponentModel.DescriptionAttribute("Plugin Assembly")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityReference PluginAssemblyId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.pluginassemblyid);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(PluginAssemblyId));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.pluginassemblyid, value);
                this.OnPropertyChanged(nameof(PluginAssemblyId));
            }
        }
        
        /// <summary>
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the plug-in type.
        ///     (Russian - 1049): Уникальный идентификатор подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.plugintypeidunique)]
        [System.ComponentModel.DescriptionAttribute("Unique identifier of the plug-in type.")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> PluginTypeIdUnique
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.plugintypeidunique);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Public Key Token
        ///     (Russian - 1049): Токен открытого ключа
        /// 
        /// Description:
        ///     (English - United States - 1033): Public key token of the assembly for the plug-in type.
        ///     (Russian - 1049): Токен общего ключа сборки для этого типа подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.publickeytoken)]
        [System.ComponentModel.DescriptionAttribute("Public Key Token")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string PublicKeyToken
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.publickeytoken);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Solution
        ///     (Russian - 1049): Решение
        /// 
        /// Description:
        ///     (English - United States - 1033): Unique identifier of the associated solution.
        ///     (Russian - 1049): Уникальный идентификатор связанного решения.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.solutionid)]
        [System.ComponentModel.DescriptionAttribute("Solution")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<System.Guid> SolutionId
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.solutionid);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Type Name
        ///     (Russian - 1049): Название типа
        /// 
        /// Description:
        ///     (English - United States - 1033): Fully qualified type name of the plug-in type.
        ///     (Russian - 1049): Полное имя типа подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.typename)]
        [System.ComponentModel.DescriptionAttribute("Type Name")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string TypeName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.typename);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(TypeName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.typename, value);
                this.OnPropertyChanged(nameof(TypeName));
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Version
        ///     (Russian - 1049): Версия
        /// 
        /// Description:
        ///     (English - United States - 1033): Version number of the assembly for the plug-in type.
        ///     (Russian - 1049): Номер версии сборки подключаемого модуля.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.version)]
        [System.ComponentModel.DescriptionAttribute("Version")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string Version
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.version);
            }
        }
        
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.versionnumber)]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Nullable<long> VersionNumber
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.versionnumber);
            }
        }
        
        /// <summary>
        /// DisplayName:
        ///     (English - United States - 1033): Workflow Activity Group Name
        ///     (Russian - 1049): Название группы действия бизнес-процесса
        /// 
        /// Description:
        ///     (English - United States - 1033): Group name of workflow custom activity.
        ///     (Russian - 1049): Название группы настраиваемого действия бизнес-процесса.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.workflowactivitygroupname)]
        [System.ComponentModel.DescriptionAttribute("Workflow Activity Group Name")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string WorkflowActivityGroupName
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                return this.GetAttributeValue<string>(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.workflowactivitygroupname);
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.OnPropertyChanging(nameof(WorkflowActivityGroupName));
                this.SetAttributeValue(Nav.Common.VSPackages.CrmDeveloperHelper.Entities.PluginType.Schema.Attributes.workflowactivitygroupname, value);
                this.OnPropertyChanged(nameof(WorkflowActivityGroupName));
            }
        }
        

        #endregion
    }
}
