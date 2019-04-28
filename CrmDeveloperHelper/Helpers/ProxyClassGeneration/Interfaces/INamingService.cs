﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.ProxyClassGeneration
{
    public interface INamingService
    {
        string GetNameForOptionSet(
            EntityMetadata entityMetadata
            , OptionSetMetadataBase optionSetMetadata
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        string GetNameForOption(
            OptionSetMetadataBase optionSetMetadata
            , OptionMetadata optionMetadata
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        string GetNameForEntity(EntityMetadata entityMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        string GetNameForAttribute(
            EntityMetadata entityMetadata
            , AttributeMetadata attributeMetadata
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        string GetNameForRelationship(
            EntityMetadata entityMetadata
            , RelationshipMetadataBase relationshipMetadata
            , EntityRole? reflexiveRole
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        string GetNameForServiceContext(ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        string GetNameForEntitySet(EntityMetadata entityMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        string GetNameForMessagePair(CodeGenerationSdkMessagePair messagePair, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        string GetNameForRequestField(
            CodeGenerationSdkMessageRequest request
            , Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageRequestField requestField
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        string GetNameForResponseField(
            CodeGenerationSdkMessageResponse response
            , Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageResponseField responseField
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForEntity(EntityMetadata entityMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        IEnumerable<string> GetCommentsForEntityDefaultConstructor(EntityMetadata entityMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        IEnumerable<string> GetCommentsForEntityAnonymousConstructor(EntityMetadata entityMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        IEnumerable<string> GetCommentsForAttribute(
            EntityMetadata entityMetadata
            , AttributeMetadata attributeMetadata
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForRelationshipOneToMany(
            EntityMetadata entityMetadata
            , OneToManyRelationshipMetadata relationshipMetadata
            , EntityRole? reflexiveRole
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForRelationshipManyToOne(
            EntityMetadata entityMetadata
            , OneToManyRelationshipMetadata relationshipMetadata
            , EntityRole? reflexiveRole
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForRelationshipManyToMany(
            EntityMetadata entityMetadata
            , ManyToManyRelationshipMetadata relationshipMetadata
            , EntityRole? reflexiveRole
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForOptionSet(
            EntityMetadata entityMetadata
            , OptionSetMetadataBase optionSetMetadata
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForOption(
            OptionSetMetadataBase optionSetMetadata
            , OptionMetadata optionMetadata
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForServiceContext(ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        IEnumerable<string> GetCommentsForServiceContextConstructor(ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        IEnumerable<string> GetCommentsForEntitySet(EntityMetadata entityMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        IEnumerable<string> GetCommentsForMessagePair(CodeGenerationSdkMessagePair messagePair, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        IEnumerable<string> GetCommentsForRequestField(
            CodeGenerationSdkMessageRequest request
            , Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageRequestField requestField
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        IEnumerable<string> GetCommentsForResponseField(
            CodeGenerationSdkMessageResponse response
            , Nav.Common.VSPackages.CrmDeveloperHelper.Entities.SdkMessageResponseField responseField
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );
    }
}
