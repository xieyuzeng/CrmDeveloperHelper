﻿using Microsoft.Xrm.Sdk.Metadata;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.ProxyClassGeneration
{
    public interface ICodeWriterFilterService
    {
        bool GenerateOptionSet(OptionSetMetadataBase optionSetMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        bool GenerateOption(OptionMetadata optionMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        bool GenerateEntity(EntityMetadata entityMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        bool GenerateAttribute(AttributeMetadata attributeMetadata, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        bool GenerateRelationship(
            RelationshipMetadataBase relationshipMetadata
            , EntityMetadata otherEntityMetadata
            , ICodeGenerationServiceProvider iCodeGenerationServiceProvider
        );

        bool GenerateSdkMessage(CodeGenerationSdkMessage sdkMessage, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        bool GenerateSdkMessagePair(CodeGenerationSdkMessagePair sdkMessagePair, ICodeGenerationServiceProvider iCodeGenerationServiceProvider);

        bool GenerateServiceContext(ICodeGenerationServiceProvider iCodeGenerationServiceProvider);
    }
}
