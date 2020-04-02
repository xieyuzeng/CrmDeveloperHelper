﻿namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers.ProxyClassGeneration
{
    /// <summary>
    /// <see cref="CodeGenerationServiceProvider"/>
    /// </summary>
    public interface ICodeGenerationServiceProvider
    {
        ICodeGenerationService CodeGenerationService { get; }
        
        ICodeWriterFilterService CodeWriterFilterService { get; }

        IMetadataProviderService MetadataProviderService { get; }

        INamingService NamingService { get; }

        ITypeMappingService TypeMappingService { get; }
    }
}
