//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RFB.Core.Requests
{
    
    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.Serialization.DataContractAttribute(Namespace=CompoundCreateRequest.SdkMessageRequestName)]
    [Microsoft.Xrm.Sdk.Client.RequestProxyAttribute(CompoundCreateRequest.SdkMessageNamespace)]
    public partial class CompoundCreateRequest : Microsoft.Xrm.Sdk.OrganizationRequest
    {
        
        public const string SdkMessageNamespace = "http://schemas.microsoft.com/crm/2011/Contracts";
        
        public const string SdkMessageRequestName = "CompoundCreate";
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.Entity Entity
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                if (this.Parameters.Contains(nameof(Entity)))
                {
                    return ((Microsoft.Xrm.Sdk.Entity)(this.Parameters[nameof(Entity)]));
                }
                else
                {
                    return default(Microsoft.Xrm.Sdk.Entity);
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.Parameters[nameof(Entity)] = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Microsoft.Xrm.Sdk.EntityCollection ChildEntities
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                if (this.Parameters.Contains(nameof(ChildEntities)))
                {
                    return ((Microsoft.Xrm.Sdk.EntityCollection)(this.Parameters[nameof(ChildEntities)]));
                }
                else
                {
                    return default(Microsoft.Xrm.Sdk.EntityCollection);
                }
            }
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            set
            {
                this.Parameters[nameof(ChildEntities)] = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public CompoundCreateRequest()
        {
            this.RequestName = CompoundCreateRequest.SdkMessageRequestName;

            this.Entity = default(Microsoft.Xrm.Sdk.Entity);
            this.ChildEntities = default(Microsoft.Xrm.Sdk.EntityCollection);
        }
    }
    
    [System.Runtime.Serialization.DataContractAttribute(Namespace=CompoundCreateRequest.SdkMessageNamespace)]
    [Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute(CompoundCreateRequest.SdkMessageRequestName)]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class CompoundCreateResponse : Microsoft.Xrm.Sdk.OrganizationResponse
    {
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public CompoundCreateResponse()
        {
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Guid Id
        {
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            get
            {
                if (this.Results.Contains(nameof(Id)))
                {
                    return ((System.Guid)(this.Results[nameof(Id)]));
                }
                else
                {
                    return default(System.Guid);
                }
            }
        }
    }
}
