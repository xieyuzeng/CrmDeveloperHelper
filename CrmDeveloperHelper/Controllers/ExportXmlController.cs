using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Nav.Common.VSPackages.CrmDeveloperHelper.Commands;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using Nav.Common.VSPackages.CrmDeveloperHelper.Repository;
using Nav.Common.VSPackages.CrmDeveloperHelper.Views;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Controllers
{
    /// <summary>
    /// Контроллер для экспорта риббона
    /// </summary>
    public class ExportXmlController
    {
        private readonly IWriteToOutput _iWriteToOutput = null;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="iWriteToOutput"></param>
        public ExportXmlController(IWriteToOutput iWriteToOutput)
        {
            this._iWriteToOutput = iWriteToOutput;
        }

        #region Экспортирование списка событий форм.

        public async Task ExecuteExportingFormsEvents(ConnectionData connectionData, CommonConfiguration commonConfig)
        {
            string operation = string.Format(Properties.OperationNames.ExportingSystemFormsEventsFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await ExportingFormsEvents(connectionData, commonConfig);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task ExportingFormsEvents(ConnectionData connectionData, CommonConfiguration commonConfig)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            string filePath = await CreateFormsEventsFile(service, commonConfig.FolderForExport, commonConfig.FormsEventsFileName, connectionData.Name, commonConfig.FormsEventsOnlyWithFormLibraries);

            this._iWriteToOutput.PerformAction(service.ConnectionData, filePath);
        }

        private async Task<string> CreateFormsEventsFile(IOrganizationServiceExtented service, string fileFolder, string fileNameFormat, string connectionDataName, bool onlyWithFormLibraries)
        {
            this._iWriteToOutput.WriteToOutput(service.ConnectionData, "Start analyzing System Forms.");

            var repository = new SystemFormRepository(service);

            var allForms = (await repository.GetListAsync())
                .OrderBy(ent => ent.ObjectTypeCode)
                .ThenBy(ent => ent.Type.Value)
                .ThenBy(ent => ent.Name)
                ;

            SolutionComponentDescriptor descriptor = new SolutionComponentDescriptor(service);

            var handler = new FormDescriptionHandler(descriptor, new DependencyRepository(service));

            StringBuilder content = new StringBuilder();

            foreach (var savedQuery in allForms)
            {
                string entityName = savedQuery.ObjectTypeCode;
                string name = savedQuery.Name;

                string typeName = savedQuery.FormattedValues[SystemForm.Schema.Attributes.type];

                string formXml = savedQuery.FormXml;

                if (!string.IsNullOrEmpty(formXml))
                {
                    XElement doc = XElement.Parse(formXml);

                    string events = handler.GetEventsDescription(doc);

                    if (!string.IsNullOrEmpty(events))
                    {
                        string desc = handler.GetFormLibrariesDescription(doc);

                        if (onlyWithFormLibraries && string.IsNullOrEmpty(desc))
                        {
                            continue;
                        }

                        if (content.Length > 0)
                        {
                            content
                                .AppendLine()
                                .AppendLine(new string('-', 100))
                                .AppendLine()
                                ;
                        }

                        content.AppendFormat("Entity: {0}", entityName).AppendLine();
                        content.AppendFormat("Form Type: {0}", typeName).AppendLine();
                        content.AppendFormat("Form Name: {0}", name).AppendLine();

                        if (!string.IsNullOrEmpty(desc))
                        {
                            content.AppendLine("FormLibraries:");
                            content.Append(desc);
                        }

                        content.AppendLine("Events:");
                        content.AppendLine(events);
                    }
                }
            }

            string filePath = string.Empty;

            if (content.Length > 0)
            {
                string fileName = string.Format("{0}{1} {2}.txt"
                    , (!string.IsNullOrEmpty(connectionDataName) ? connectionDataName + "." : string.Empty)
                    , fileNameFormat.Trim()
                    , DateTime.Now.ToString("yyyy.MM.dd HH-mm-ss")
                );

                filePath = Path.Combine(fileFolder, FileOperations.RemoveWrongSymbols(fileName));

                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }

                File.WriteAllText(filePath, content.ToString(), new UTF8Encoding(false));

                this._iWriteToOutput.WriteToOutput(service.ConnectionData, "System Forms Events were exported to {0}", filePath);
            }
            else
            {
                this._iWriteToOutput.WriteToOutput(service.ConnectionData, "No Forms Events were founded.");
            }

            return filePath;
        }

        #endregion Экспортирование списка событий форм.

        private bool ParseXmlDocument(ConnectionData connectionData, SelectedFile selectedFile, out XDocument doc)
        {
            doc = null;

            if (!File.Exists(selectedFile.FilePath))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotExistsFormat1, selectedFile.FilePath);
                return false;
            }

            string fileText = File.ReadAllText(selectedFile.FilePath);

            if (!ContentComparerHelper.TryParseXmlDocument(fileText, out doc))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileTextIsNotXmlFormat1, selectedFile.FilePath);
                return false;
            }

            return true;
        }

        private async Task CheckAttributeValidateGetEntityExecuteAction<T>(
            ConnectionData connectionData
            , CommonConfiguration commonConfig
            , XDocument doc
            , string filePath
            , XName attributeName
            , Func<ConnectionData, string, XAttribute, bool> validatorAttribute
            , Func<ConnectionData, XDocument, Task<bool>> validatorDocument
            , Func<IOrganizationServiceExtented, CommonConfiguration, string, Task<Tuple<bool, T>>> entityGetter
            , Func<IOrganizationServiceExtented, CommonConfiguration, XDocument, string, T, Task> continueAction
        ) where T : Entity
        {
            var attribute = doc.Root.Attribute(attributeName);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(
                    connectionData
                    , Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , attributeName.ToString()
                    , filePath
                );

                return;
            }

            if (validatorAttribute != null && !validatorAttribute(connectionData, filePath, attribute))
            {
                return;
            }

            if (validatorDocument != null && !await validatorDocument(connectionData, doc))
            {
                return;
            }

            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var getResult = await entityGetter(service, commonConfig, attribute.Value);

            if (!getResult.Item1)
            {
                return;
            }

            await continueAction(service, commonConfig, doc, filePath, getResult.Item2);
        }

        #region SiteMap

        public async Task ExecuteDifferenceSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceSiteMapFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await DifferenceSiteMap(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteDifferenceSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceSiteMapFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await DifferenceSiteMap(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task DifferenceSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string siteMapNameUnique = string.Empty;

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSiteMapNameUnique);

            if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
            {
                siteMapNameUnique = attribute.Value;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repository = new SitemapRepository(service);

            var siteMap = repository.FindByExactName(siteMapNameUnique, new ColumnSet(true));

            if (siteMap == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SiteMapNotFoundedFormat2, connectionData.Name, siteMapNameUnique);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            string siteMapXml = siteMap.GetAttributeValue<string>(SiteMap.Schema.Attributes.sitemapxml);

            string fieldTitle = "SiteMapXml";

            string fileTitle2 = EntityFileNameFormatter.GetSiteMapFileName(connectionData.Name, siteMap.SiteMapNameUnique, siteMap.Id, fieldTitle, "xml");
            string filePath2 = FileOperations.GetNewTempFilePath(Path.GetFileNameWithoutExtension(fileTitle2), Path.GetExtension(fileTitle2));

            if (string.IsNullOrEmpty(siteMapXml))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, SiteMap.Schema.EntityLogicalName, siteMap.SiteMapNameUnique, fieldTitle);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            try
            {
                siteMapXml = ContentComparerHelper.FormatXmlByConfiguration(
                    siteMapXml
                    , commonConfig
                    , XmlOptionsControls.SiteMapXmlOptions
                    , schemaName: AbstractDynamicCommandXsdSchemas.SchemaSiteMapXml
                    , siteMapUniqueName: siteMap.SiteMapNameUnique ?? string.Empty
                );

                File.WriteAllText(filePath2, siteMapXml, new UTF8Encoding(false));

                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, SiteMap.Schema.EntityLogicalName, siteMap.SiteMapNameUnique, fieldTitle, filePath2);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }

            string filePath1 = filePath;
            string fileTitle1 = Path.GetFileName(filePath);

            this._iWriteToOutput.ProcessStartProgramComparerAsync(filePath1, filePath2, fileTitle1, fileTitle2);
        }

        public async Task ExecuteUpdateSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingSiteMapFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await UpdateSiteMap(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteUpdateSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingSiteMapFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await UpdateSiteMap(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task UpdateSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            string siteMapNameUnique = string.Empty;

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSiteMapNameUnique);

            if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
            {
                siteMapNameUnique = attribute.Value;
            }

            ContentComparerHelper.ClearRoot(doc);

            bool validateResult = await SitemapRepository.ValidateXmlDocumentAsync(connectionData, _iWriteToOutput, doc);

            if (!validateResult)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ValidatingXmlForFieldFailedFormat1, SiteMap.Schema.Attributes.sitemapxml);
                _iWriteToOutput.ActivateOutputWindow(connectionData);

                var dialogResult = MessageBoxResult.Cancel;

                var t = new Thread(() =>
                {
                    dialogResult = MessageBox.Show(Properties.MessageBoxStrings.ContinueOperation, Properties.MessageBoxStrings.QuestionTitle, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

                t.Join();

                if (dialogResult != MessageBoxResult.OK)
                {
                    return;
                }
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repositorySiteMap = new SitemapRepository(service);

            var siteMap = repositorySiteMap.FindByExactName(siteMapNameUnique, new ColumnSet(true));

            if (siteMap == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SiteMapNotFoundedFormat2, connectionData.Name, SiteMap.Schema.EntityLogicalName, siteMapNameUnique);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            {
                string siteMapXml = siteMap.GetAttributeValue<string>(SiteMap.Schema.Attributes.sitemapxml);

                string fieldTitle = SiteMap.Schema.Headers.sitemapxml + " BackUp";

                string fileNameBackUp = EntityFileNameFormatter.GetSiteMapFileName(connectionData.Name, siteMap.SiteMapName, siteMap.Id, fieldTitle, "xml");
                string filePathBackUp = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileNameBackUp));

                if (!string.IsNullOrEmpty(siteMapXml))
                {
                    try
                    {
                        siteMapXml = ContentComparerHelper.FormatXmlByConfiguration(
                            siteMapXml
                            , commonConfig
                            , XmlOptionsControls.SiteMapXmlOptions
                            , schemaName: AbstractDynamicCommandXsdSchemas.SchemaSiteMapXml
                            , siteMapUniqueName: siteMap.SiteMapNameUnique ?? string.Empty
                        );

                        File.WriteAllText(filePathBackUp, siteMapXml, new UTF8Encoding(false));

                        this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, SiteMap.Schema.EntityLogicalName, siteMap.SiteMapNameUnique, fieldTitle, filePathBackUp);
                    }
                    catch (Exception ex)
                    {
                        this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                    }
                }
                else
                {
                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, SiteMap.Schema.EntityLogicalName, siteMap.SiteMapNameUnique, fieldTitle);
                    this._iWriteToOutput.ActivateOutputWindow(connectionData);
                }
            }

            var newText = doc.ToString(SaveOptions.DisableFormatting);

            var updateEntity = new SiteMap
            {
                Id = siteMap.Id
            };
            updateEntity.Attributes[SiteMap.Schema.Attributes.sitemapxml] = newText;

            await service.UpdateAsync(updateEntity);

            _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.PublishingSiteMapFormat3, service.ConnectionData.Name, siteMap.SiteMapName, siteMap.Id.ToString());

            var repositoryPublish = new PublishActionsRepository(service);

            await repositoryPublish.PublishSiteMapsAsync(new[] { siteMap.Id });
        }

        public async Task ExecuteOpenInWebSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.OpeningSiteMapInWebFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await OpenInWebSiteMap(connectionData, commonConfig, selectedFile);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task OpenInWebSiteMap(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            if (!File.Exists(selectedFile.FilePath))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotExistsFormat1, selectedFile.FilePath);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            string fileText = File.ReadAllText(selectedFile.FilePath);

            if (!ContentComparerHelper.TryParseXmlDocument(fileText, out var doc))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileTextIsNotXmlFormat1, selectedFile.FilePath);
                _iWriteToOutput.ActivateOutputWindow(connectionData);

                WindowHelper.OpenExportSiteMapExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            string siteMapNameUnique = string.Empty;

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSiteMapNameUnique);

            if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
            {
                siteMapNameUnique = attribute.Value;
            }

            var repositorySiteMap = new SitemapRepository(service);

            var siteMap = repositorySiteMap.FindByExactName(siteMapNameUnique, new ColumnSet(false));

            if (siteMap == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SiteMapNotFoundedFormat2, connectionData.Name, SiteMap.Schema.EntityLogicalName, siteMapNameUnique);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);

                WindowHelper.OpenExportSiteMapExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            service.UrlGenerator.OpenSolutionComponentInWeb(ComponentType.SiteMap, siteMap.Id);
        }

        #endregion SiteMap

        #region SystemForm

        public async Task ExecuteDifferenceSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceSystemFormFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await DifferenceSystemForm(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteDifferenceSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceSystemFormFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await DifferenceSystemForm(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task DifferenceSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId.ToString()
                    , filePath
                );

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var systemFormId)
            )
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId.ToString()
                    , attribute.Value
                    , filePath
                );

                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repository = new SystemFormRepository(service);

            var systemForm = await repository.GetByIdAsync(systemFormId, new ColumnSet(true));

            if (systemForm == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SystemFormNotFoundedFormat2, connectionData.Name, systemFormId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            string formXml = systemForm.GetAttributeValue<string>(SystemForm.Schema.Attributes.formxml);

            string fieldTitle = SystemForm.Schema.Headers.formxml;

            string fileTitle2 = EntityFileNameFormatter.GetSystemFormFileName(connectionData.Name, systemForm.ObjectTypeCode, systemForm.Name, fieldTitle, "xml");
            string filePath2 = FileOperations.GetNewTempFilePath(Path.GetFileNameWithoutExtension(fileTitle2), Path.GetExtension(fileTitle2));

            if (!string.IsNullOrEmpty(formXml))
            {
                try
                {
                    formXml = ContentComparerHelper.FormatXmlByConfiguration(
                        formXml
                        , commonConfig
                        , XmlOptionsControls.FormXmlOptions
                        , schemaName: AbstractDynamicCommandXsdSchemas.SchemaFormXml
                        , formId: systemForm.Id
                        , entityName: systemForm.ObjectTypeCode
                    );

                    File.WriteAllText(filePath2, formXml, new UTF8Encoding(false));

                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, SystemForm.Schema.EntityLogicalName, systemForm.Name, fieldTitle, filePath2);
                }
                catch (Exception ex)
                {
                    this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                }
            }
            else
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, SystemForm.Schema.EntityLogicalName, systemForm.Name, fieldTitle);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            string filePath1 = filePath;
            string fileTitle1 = Path.GetFileName(filePath);

            this._iWriteToOutput.ProcessStartProgramComparerAsync(filePath1, filePath2, fileTitle1, fileTitle2);
        }

        public async Task ExecuteUpdateSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingSystemFormFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await UpdateSystemForm(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteUpdateSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingSystemFormFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await UpdateSystemForm(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task UpdateSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId.ToString()
                    , filePath
                );

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var formId)
                )
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId.ToString()
                    , attribute.Value
                    , filePath
                );

                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ValidatingXmlForFieldFormat1, SystemForm.Schema.Attributes.formxml);

            ContentComparerHelper.ClearRoot(doc);

            bool validateResult = await SystemFormRepository.ValidateXmlDocumentAsync(connectionData, _iWriteToOutput, doc);

            if (!validateResult)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ValidatingXmlForFieldFailedFormat1, SystemForm.Schema.Attributes.formxml);
                _iWriteToOutput.ActivateOutputWindow(connectionData);

                var dialogResult = MessageBoxResult.Cancel;

                var t = new Thread(() =>
                {
                    dialogResult = MessageBox.Show(Properties.MessageBoxStrings.ContinueOperation, Properties.MessageBoxStrings.QuestionTitle, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

                t.Join();

                if (dialogResult != MessageBoxResult.OK)
                {
                    return;
                }
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repositorySystemForm = new SystemFormRepository(service);

            var systemForm = await repositorySystemForm.GetByIdAsync(formId, new ColumnSet(true));

            if (systemForm == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SystemFormNotFoundedFormat2, connectionData.Name, formId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            {
                string formXml = systemForm.GetAttributeValue<string>(SystemForm.Schema.Attributes.formxml);

                string fieldTitle = SystemForm.Schema.Headers.formxml + " BackUp";

                string fileNameBackUp = EntityFileNameFormatter.GetSystemFormFileName(connectionData.Name, systemForm.ObjectTypeCode, systemForm.Name, fieldTitle, "xml");
                string filePathBackUp = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileNameBackUp));

                if (!string.IsNullOrEmpty(formXml))
                {
                    try
                    {
                        formXml = ContentComparerHelper.FormatXmlByConfiguration(
                            formXml
                            , commonConfig
                            , XmlOptionsControls.FormXmlOptions
                            , schemaName: AbstractDynamicCommandXsdSchemas.SchemaFormXml
                            , formId: systemForm.Id
                            , entityName: systemForm.ObjectTypeCode
                        );

                        File.WriteAllText(filePathBackUp, formXml, new UTF8Encoding(false));

                        this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, SystemForm.Schema.EntityLogicalName, systemForm.Name, fieldTitle, filePathBackUp);
                    }
                    catch (Exception ex)
                    {
                        this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                    }
                }
                else
                {
                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, SystemForm.Schema.EntityLogicalName, systemForm.Name, fieldTitle);
                    this._iWriteToOutput.ActivateOutputWindow(connectionData);
                }
            }

            var newText = doc.ToString(SaveOptions.DisableFormatting);

            var updateEntity = new SystemForm
            {
                Id = formId
            };
            updateEntity.Attributes[SystemForm.Schema.Attributes.formxml] = newText;

            await service.UpdateAsync(updateEntity);

            var repositoryPublish = new PublishActionsRepository(service);

            _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.PublishingSystemFormFormat3, service.ConnectionData.Name, systemForm.ObjectTypeCode, systemForm.Name);
            await repositoryPublish.PublishDashboardsAsync(new[] { formId });

            if (systemForm.ObjectTypeCode.IsValidEntityName())
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.PublishingEntitiesFormat2, service.ConnectionData.Name, systemForm.ObjectTypeCode);
                await repositoryPublish.PublishEntitiesAsync(new[] { systemForm.ObjectTypeCode });
            }
        }

        public async Task ExecuteOpenInWebSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.OpeningSystemFormInWebFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await OpenInWebSystemForm(connectionData, commonConfig, selectedFile);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task OpenInWebSystemForm(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            if (!File.Exists(selectedFile.FilePath))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotExistsFormat1, selectedFile.FilePath);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            string fileText = File.ReadAllText(selectedFile.FilePath);

            if (!ContentComparerHelper.TryParseXmlDocument(fileText, out var doc))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileTextIsNotXmlFormat1, selectedFile.FilePath);

                WindowHelper.OpenSystemFormExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData
                    , Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId.ToString()
                    , selectedFile.FilePath
                );

                WindowHelper.OpenSystemFormExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var formId)
            )
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeFormId.ToString()
                    , attribute.Value
                    , selectedFile.FilePath
                );

                WindowHelper.OpenSystemFormExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            var repositorySystemForm = new SystemFormRepository(service);

            var systemForm = await repositorySystemForm.GetByIdAsync(formId, new ColumnSet(false));

            if (systemForm == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SystemFormNotFoundedFormat2, connectionData.Name, formId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);

                WindowHelper.OpenSystemFormExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            service.UrlGenerator.OpenSolutionComponentInWeb(ComponentType.SystemForm, systemForm.Id);
        }

        #endregion SystemForm

        #region SavedQuery

        public async Task ExecuteDifferenceSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceSavedQueryFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await DifferenceSavedQuery(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteDifferenceSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceSavedQueryFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await DifferenceSavedQuery(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task DifferenceSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId.ToString()
                    , filePath
                );

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var savedQueryId)
                )
            {
                this._iWriteToOutput.WriteToOutput(connectionData
                    , Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId.ToString()
                    , attribute.Value
                    , filePath
                );

                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repository = new SavedQueryRepository(service);

            var savedQuery = await repository.GetByIdAsync(savedQueryId, new ColumnSet(true));

            if (savedQuery == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SavedQueryNotFoundedFormat2, connectionData.Name, savedQueryId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            string fieldName = SavedQueryRepository.GetFieldNameByXmlRoot(doc.Root.Name.ToString());
            string fieldTitle = SavedQueryRepository.GetFieldTitleByXmlRoot(doc.Root.Name.ToString());

            string xmlContent = savedQuery.GetAttributeValue<string>(fieldName);

            string fileTitle2 = EntityFileNameFormatter.GetSavedQueryFileName(connectionData.Name, savedQuery.ReturnedTypeCode, savedQuery.Name, fieldTitle, "xml");
            string filePath2 = FileOperations.GetNewTempFilePath(Path.GetFileNameWithoutExtension(fileTitle2), Path.GetExtension(fileTitle2));

            if (!string.IsNullOrEmpty(xmlContent))
            {
                try
                {
                    xmlContent = ContentComparerHelper.FormatXmlByConfiguration(
                        xmlContent
                        , commonConfig
                        , XmlOptionsControls.SavedQueryXmlOptions
                        , schemaName: AbstractDynamicCommandXsdSchemas.SchemaFetch
                        , savedQueryId: savedQueryId
                        , entityName: savedQuery.ReturnedTypeCode
                    );

                    File.WriteAllText(filePath2, xmlContent, new UTF8Encoding(false));

                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, SavedQuery.Schema.EntityLogicalName, savedQuery.Name, fieldTitle, filePath2);
                }
                catch (Exception ex)
                {
                    this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                }
            }
            else
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, SavedQuery.Schema.EntityLogicalName, savedQuery.Name, fieldTitle);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            string filePath1 = filePath;
            string fileTitle1 = Path.GetFileName(filePath);

            this._iWriteToOutput.ProcessStartProgramComparerAsync(filePath1, filePath2, fileTitle1, fileTitle2);
        }

        public async Task ExecuteUpdateSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingSavedQueryFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await UpdateSavedQuery(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteUpdateSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingSavedQueryFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await UpdateSavedQuery(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task UpdateSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId.ToString()
                    , filePath
                );

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var savedQueryId)
                )
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId.ToString()
                    , attribute.Value
                    , filePath
                );

                return;
            }

            string fieldName = SavedQueryRepository.GetFieldNameByXmlRoot(doc.Root.Name.ToString());
            string fieldTitle = SavedQueryRepository.GetFieldTitleByXmlRoot(doc.Root.Name.ToString());

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ValidatingXmlForFieldFormat1, fieldTitle);

            ContentComparerHelper.ClearRoot(doc);

            bool validateResult = await SavedQueryRepository.ValidateXmlDocumentAsync(connectionData, _iWriteToOutput, doc, fieldTitle);

            if (!validateResult)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ValidatingXmlForFieldFailedFormat1, fieldTitle);
                _iWriteToOutput.ActivateOutputWindow(connectionData);

                var dialogResult = MessageBoxResult.Cancel;

                var t = new Thread(() =>
                {
                    dialogResult = MessageBox.Show(Properties.MessageBoxStrings.ContinueOperation, Properties.MessageBoxStrings.QuestionTitle, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

                t.Join();

                if (dialogResult != MessageBoxResult.OK)
                {
                    return;
                }
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repositorySavedQuery = new SavedQueryRepository(service);

            var savedQuery = await repositorySavedQuery.GetByIdAsync(savedQueryId, new ColumnSet(true));

            if (savedQuery == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SavedQueryNotFoundedFormat2, connectionData.Name, savedQueryId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            if (string.Equals(fieldName, SavedQuery.Schema.Attributes.layoutxml, StringComparison.InvariantCulture)
                && !string.IsNullOrEmpty(savedQuery.ReturnedTypeCode)
            )
            {
                var entityData = connectionData.GetEntityIntellisenseData(savedQuery.ReturnedTypeCode);

                if (entityData != null && entityData.ObjectTypeCode.HasValue)
                {
                    XAttribute attr = doc.Root.Attribute("object");

                    if (attr != null)
                    {
                        attr.Value = entityData.ObjectTypeCode.ToString();
                    }
                }
            }

            {
                string xmlContent = savedQuery.GetAttributeValue<string>(fieldName);

                fieldTitle += " BackUp";

                string fileNameBackUp = EntityFileNameFormatter.GetSavedQueryFileName(connectionData.Name, savedQuery.ReturnedTypeCode, savedQuery.Name, fieldTitle, "xml");
                string filePathBackUp = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileNameBackUp));

                if (!string.IsNullOrEmpty(xmlContent))
                {
                    try
                    {
                        xmlContent = ContentComparerHelper.FormatXmlByConfiguration(
                            xmlContent
                            , commonConfig
                            , XmlOptionsControls.SavedQueryXmlOptions
                            , schemaName: AbstractDynamicCommandXsdSchemas.SchemaFetch
                            , savedQueryId: savedQueryId
                            , entityName: savedQuery.ReturnedTypeCode
                        );

                        File.WriteAllText(filePathBackUp, xmlContent, new UTF8Encoding(false));

                        this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, SavedQuery.Schema.EntityLogicalName, savedQuery.Name, fieldTitle, filePathBackUp);
                    }
                    catch (Exception ex)
                    {
                        this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                    }
                }
                else
                {
                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, SavedQuery.Schema.EntityLogicalName, savedQuery.Name, fieldTitle);
                    this._iWriteToOutput.ActivateOutputWindow(connectionData);
                }
            }

            var newText = doc.ToString(SaveOptions.DisableFormatting);

            if (string.Equals(fieldName, SavedQuery.Schema.Attributes.fetchxml, StringComparison.InvariantCulture))
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ExecutingValidateSavedQueryRequest);

                var request = new ValidateSavedQueryRequest()
                {
                    FetchXml = newText,
                    QueryType = savedQuery.QueryType.GetValueOrDefault()
                };

                service.Execute(request);
            }

            var updateEntity = new SavedQuery
            {
                Id = savedQueryId
            };
            updateEntity.Attributes[fieldName] = newText;

            await service.UpdateAsync(updateEntity);

            _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.PublishingEntitiesFormat2, service.ConnectionData.Name, savedQuery.ReturnedTypeCode);

            {
                var repositoryPublish = new PublishActionsRepository(service);

                await repositoryPublish.PublishEntitiesAsync(new[] { savedQuery.ReturnedTypeCode });
            }
        }

        public async Task ExecuteOpenInWebSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.OpeningSavedQueryInWebFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await OpenInWebSavedQuery(connectionData, commonConfig, selectedFile);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task OpenInWebSavedQuery(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            if (!File.Exists(selectedFile.FilePath))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotExistsFormat1, selectedFile.FilePath);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            string fileText = File.ReadAllText(selectedFile.FilePath);

            if (!ContentComparerHelper.TryParseXmlDocument(fileText, out var doc))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileTextIsNotXmlFormat1, selectedFile.FilePath);

                WindowHelper.OpenSavedQueryExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData
                    , Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId.ToString()
                    , selectedFile.FilePath
                );

                WindowHelper.OpenSavedQueryExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var savedQueryId)
                )
            {
                this._iWriteToOutput.WriteToOutput(connectionData
                    , Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeSavedQueryId.ToString()
                    , attribute.Value
                    , selectedFile.FilePath
                );

                WindowHelper.OpenSavedQueryExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            var repositorySavedQuery = new SavedQueryRepository(service);

            var savedQuery = await repositorySavedQuery.GetByIdAsync(savedQueryId, new ColumnSet(false));

            if (savedQuery == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.SavedQueryNotFoundedFormat2, connectionData.Name, savedQueryId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);

                WindowHelper.OpenSavedQueryExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            service.UrlGenerator.OpenSolutionComponentInWeb(ComponentType.SavedQuery, savedQuery.Id);
        }

        #endregion SavedQuery

        #region Workflow

        public async Task ExecuteDifferenceWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceWorkflowFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await DifferenceWorkflow(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteDifferenceWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceWorkflowFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await DifferenceWorkflow(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task DifferenceWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId.ToString()
                    , filePath
                );

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var workflowId)
            )
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId.ToString()
                    , attribute.Value
                    , filePath
                );

                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repository = new WorkflowRepository(service);

            var workflow = await repository.GetByIdAsync(workflowId, new ColumnSet(true));

            if (workflow == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.WorkflowNotFoundedFormat2, connectionData.Name, workflowId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            string formXml = workflow.GetAttributeValue<string>(Workflow.Schema.Attributes.xaml);

            string fieldTitle = Workflow.Schema.Headers.xaml;

            string fileTitle2 = EntityFileNameFormatter.GetWorkflowFileName(connectionData.Name, workflow.PrimaryEntity, workflow.FormattedValues[Workflow.Schema.Attributes.category], workflow.Name, fieldTitle, "xml");
            string filePath2 = FileOperations.GetNewTempFilePath(Path.GetFileNameWithoutExtension(fileTitle2), Path.GetExtension(fileTitle2));

            if (!string.IsNullOrEmpty(formXml))
            {
                try
                {
                    formXml = ContentComparerHelper.FormatXmlByConfiguration(
                        formXml
                        , commonConfig
                        , XmlOptionsControls.WorkflowXmlOptions
                        , workflowId: workflow.Id
                    );

                    File.WriteAllText(filePath2, formXml, new UTF8Encoding(false));

                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, Workflow.Schema.EntityLogicalName, workflow.Name, fieldTitle, filePath2);
                }
                catch (Exception ex)
                {
                    this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                }
            }
            else
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, Workflow.Schema.EntityLogicalName, workflow.Name, fieldTitle);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            string filePath1 = filePath;
            string fileTitle1 = Path.GetFileName(filePath);

            this._iWriteToOutput.ProcessStartProgramComparerAsync(filePath1, filePath2, fileTitle1, fileTitle2);
        }

        public async Task ExecuteUpdateWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingWorkflowFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await UpdateWorkflow(connectionData, commonConfig, doc, selectedFile.FilePath);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteUpdateWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingWorkflowFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await UpdateWorkflow(connectionData, commonConfig, doc, filePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task UpdateWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId.ToString()
                    , filePath
                );

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value)
                || !Guid.TryParse(attribute.Value, out var idWorkflow)
                )
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId.ToString()
                    , attribute.Value
                    , filePath
                );

                return;
            }

            ContentComparerHelper.ClearRootWorkflow(doc);

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            var repositoryWorkflow = new WorkflowRepository(service);

            var workflow = await repositoryWorkflow.GetByIdAsync(idWorkflow, new ColumnSet(true));

            if (workflow == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.WorkflowNotFoundedFormat2, connectionData.Name, idWorkflow);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);
                return;
            }

            {
                string xaml = workflow.GetAttributeValue<string>(Workflow.Schema.Attributes.xaml);

                string fieldTitle = Workflow.Schema.Headers.xaml + " BackUp";

                string fileNameBackUp = EntityFileNameFormatter.GetWorkflowFileName(connectionData.Name, workflow.PrimaryEntity, workflow.FormattedValues[Workflow.Schema.Attributes.category], workflow.Name, fieldTitle, "xml");
                string filePathBackUp = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileNameBackUp));

                if (!string.IsNullOrEmpty(xaml))
                {
                    try
                    {
                        xaml = ContentComparerHelper.FormatXmlByConfiguration(
                            xaml
                            , commonConfig
                            , XmlOptionsControls.WorkflowXmlOptions
                            , workflowId: workflow.Id
                        );

                        File.WriteAllText(filePathBackUp, xaml, new UTF8Encoding(false));

                        this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, connectionData.Name, Workflow.Schema.EntityLogicalName, workflow.Name, fieldTitle, filePathBackUp);
                    }
                    catch (Exception ex)
                    {
                        this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
                    }
                }
                else
                {
                    this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, connectionData.Name, Workflow.Schema.EntityLogicalName, workflow.Name, fieldTitle);
                    this._iWriteToOutput.ActivateOutputWindow(connectionData);
                }
            }

            if (workflow.StateCodeEnum == Workflow.Schema.OptionSets.statecode.Activated_1)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.DeactivatingWorkflowFormat2, connectionData.Name, workflow.Name);

                await service.ExecuteAsync<SetStateResponse>(new SetStateRequest()
                {
                    EntityMoniker = workflow.ToEntityReference(),
                    State = new OptionSetValue((int)Workflow.Schema.OptionSets.statecode.Draft_0),
                    Status = new OptionSetValue((int)Workflow.Schema.OptionSets.statuscode.Draft_0_Draft_1),
                });
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.UpdatingFieldFormat2, connectionData.Name, Workflow.Schema.Headers.xaml);

            var newText = doc.ToString(SaveOptions.DisableFormatting);

            var updateEntity = new Workflow
            {
                Id = idWorkflow
            };
            updateEntity.Attributes[Workflow.Schema.Attributes.xaml] = newText;

            await service.UpdateAsync(updateEntity);

            if (workflow.StateCodeEnum == Workflow.Schema.OptionSets.statecode.Activated_1)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ActivatingWorkflowFormat2, connectionData.Name, workflow.Name);

                await service.ExecuteAsync<SetStateResponse>(new SetStateRequest()
                {
                    EntityMoniker = workflow.ToEntityReference(),
                    State = new OptionSetValue((int)Workflow.Schema.OptionSets.statecode.Activated_1),
                    Status = new OptionSetValue((int)Workflow.Schema.OptionSets.statuscode.Activated_1_Activated_2),
                });
            }
        }

        public async Task ExecuteOpenInWebWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.OpeningWorkflowInWebFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await OpenInWebWorkflow(connectionData, commonConfig, selectedFile);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private async Task OpenInWebWorkflow(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            if (connectionData == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.NoCurrentCRMConnection);
                return;
            }

            if (!File.Exists(selectedFile.FilePath))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileNotExistsFormat1, selectedFile.FilePath);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectingToCRM);

            this._iWriteToOutput.WriteToOutput(connectionData, connectionData.GetConnectionDescription());

            // Подключаемся к CRM.
            var service = await QuickConnection.ConnectAsync(connectionData);

            if (service == null)
            {
                _iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ConnectionFailedFormat1, connectionData.Name);
                return;
            }

            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.CurrentServiceEndpointFormat1, service.CurrentServiceEndpoint);

            string fileText = File.ReadAllText(selectedFile.FilePath);

            if (!ContentComparerHelper.TryParseXmlDocument(fileText, out var doc))
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.FileTextIsNotXmlFormat1, selectedFile.FilePath);

                WindowHelper.OpenWorkflowExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            var attribute = doc.Root.Attribute(Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId);

            if (attribute == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData
                    , Properties.OutputStrings.FileNotContainsXmlAttributeFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId.ToString()
                    , selectedFile.FilePath
                );

                WindowHelper.OpenWorkflowExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            if (string.IsNullOrEmpty(attribute.Value) || !Guid.TryParse(attribute.Value, out var workflowId))
            {
                this._iWriteToOutput.WriteToOutput(
                    connectionData
                    , Properties.OutputStrings.XmlAttributeNotValidGuidFormat3
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWorkflowId.ToString()
                    , attribute.Value
                    , selectedFile.FilePath
                );

                WindowHelper.OpenWorkflowExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            var repositoryWorkflow = new WorkflowRepository(service);

            var workflow = await repositoryWorkflow.GetByIdAsync(workflowId, new ColumnSet(false));

            if (workflow == null)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.WorkflowNotFoundedFormat2, connectionData.Name, workflowId);
                this._iWriteToOutput.ActivateOutputWindow(connectionData);

                WindowHelper.OpenWorkflowExplorer(_iWriteToOutput, service, commonConfig);

                return;
            }

            service.UrlGenerator.OpenSolutionComponentInWeb(ComponentType.Workflow, workflow.Id);
        }

        #endregion Workflow

        #region WebResource DependencyXml

        public async Task ExecuteDifferenceWebResourceDependencyXml(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceWebResourceDependencyXmlFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await CheckAttributeValidateGetWebResourceExecuteAction(connectionData, commonConfig, doc, selectedFile.FilePath, null, DifferenceWebResourceDependencyXml);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteDifferenceWebResourceDependencyXml(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.DifferenceWebResourceDependencyXmlFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await CheckAttributeValidateGetWebResourceExecuteAction(connectionData, commonConfig, doc, filePath, null, DifferenceWebResourceDependencyXml);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteUpdateWebResourceDependencyXml(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingWebResourceDependencyXmlFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await CheckAttributeValidateGetWebResourceExecuteAction(connectionData, commonConfig, doc, selectedFile.FilePath, ValidateDocumentDependencyXml, UpdateWebResourceDependencyXml);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteUpdateWebResourceDependencyXml(ConnectionData connectionData, CommonConfiguration commonConfig, XDocument doc, string filePath)
        {
            string operation = string.Format(Properties.OperationNames.UpdatingWebResourceDependencyXmlFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                await CheckAttributeValidateGetWebResourceExecuteAction(connectionData, commonConfig, doc, filePath, ValidateDocumentDependencyXml, UpdateWebResourceDependencyXml);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteOpenInWebWebResourceDependencyXml(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.OpeningWebResourceFormat1, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await CheckAttributeValidateGetWebResourceExecuteAction(connectionData, commonConfig, doc, selectedFile.FilePath, null, OpenInWebWebResourceDependencyXml);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        public async Task ExecuteGetWebResourceCurrentDependencyXml(ConnectionData connectionData, CommonConfiguration commonConfig, SelectedFile selectedFile)
        {
            string operation = string.Format(Properties.OperationNames.GettingWebResourceCurrentDependencyXml, connectionData?.Name);

            this._iWriteToOutput.WriteToOutputStartOperation(connectionData, operation);

            try
            {
                if (ParseXmlDocument(connectionData, selectedFile, out var doc))
                {
                    await CheckAttributeValidateGetWebResourceExecuteAction(connectionData, commonConfig, doc, selectedFile.FilePath, null, GetCurrentWebResourceDependencyXml);
                }
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(connectionData, ex);
            }
            finally
            {
                this._iWriteToOutput.WriteToOutputEndOperation(connectionData, operation);
            }
        }

        private Task CheckAttributeValidateGetWebResourceExecuteAction(
            ConnectionData connectionData
            , CommonConfiguration commonConfig
            , XDocument doc
            , string filePath
            , Func<ConnectionData, XDocument, Task<bool>> validatorDocument
            , Func<IOrganizationServiceExtented, CommonConfiguration, XDocument, string, WebResource, Task> continueAction
        )
        {
            return CheckAttributeValidateGetEntityExecuteAction(
                connectionData
                , commonConfig
                , doc
                , filePath
                , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWebResourceName
                , ValidateAttributeWebResourceDependencyXml
                , validatorDocument
                , GetWebResourceByAttribute
                , continueAction
            );
        }

        private async Task<Tuple<bool, WebResource>> GetWebResourceByAttribute(IOrganizationServiceExtented service, CommonConfiguration commonConfig, string webResourceName)
        {
            var repositoryWebResource = new WebResourceRepository(service);

            var webResource = await repositoryWebResource.FindByExactNameAsync(webResourceName, new ColumnSet(true));

            if (webResource == null)
            {
                this._iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.WebResourceNotFoundedInConnectionFormat2, service.ConnectionData.Name, webResourceName);
                this._iWriteToOutput.ActivateOutputWindow(service.ConnectionData);

                WindowHelper.OpenWebResourceExplorer(_iWriteToOutput, service, commonConfig);

                return Tuple.Create(true, webResource);
            }

            return Tuple.Create(true, webResource);
        }

        private bool ValidateAttributeWebResourceDependencyXml(ConnectionData connectionData, string filePath, XAttribute attribute)
        {
            if (string.IsNullOrEmpty(attribute.Value))
            {
                this._iWriteToOutput.WriteToOutput(
                    connectionData
                    , Properties.OutputStrings.XmlAttributeIsEmptyFormat2
                    , Intellisense.Model.IntellisenseContext.IntellisenseContextAttributeWebResourceName.ToString()
                    , filePath
                );

                return false;
            }

            return true;
        }

        private async Task DifferenceWebResourceDependencyXml(IOrganizationServiceExtented service, CommonConfiguration commonConfig, XDocument doc, string filePath, WebResource webResource)
        {
            string dependencyXml = webResource.GetAttributeValue<string>(WebResource.Schema.Attributes.dependencyxml);

            string fieldTitle = WebResource.Schema.Headers.dependencyxml;

            string fileTitle2 = EntityFileNameFormatter.GetWebResourceFileName(service.ConnectionData.Name, webResource.Name, fieldTitle, "xml");
            string filePath2 = FileOperations.GetNewTempFilePath(Path.GetFileNameWithoutExtension(fileTitle2), Path.GetExtension(fileTitle2));

            if (string.IsNullOrEmpty(dependencyXml))
            {
                this._iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, service.ConnectionData.Name, WebResource.Schema.EntityLogicalName, webResource.Name, fieldTitle);
                this._iWriteToOutput.ActivateOutputWindow(service.ConnectionData);
                return;
            }

            try
            {
                dependencyXml = ContentComparerHelper.FormatXmlByConfiguration(
                    dependencyXml
                    , commonConfig
                    , XmlOptionsControls.WebResourceDependencyXmlOptions
                    , schemaName: AbstractDynamicCommandXsdSchemas.SchemaDependencyXml
                    , webResourceName: webResource.Name
                );

                File.WriteAllText(filePath2, dependencyXml, new UTF8Encoding(false));

                this._iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, service.ConnectionData.Name, WebResource.Schema.EntityLogicalName, webResource.Name, fieldTitle, filePath2);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(service.ConnectionData, ex);
            }

            string filePath1 = filePath;
            string fileTitle1 = Path.GetFileName(filePath);

            await this._iWriteToOutput.ProcessStartProgramComparerAsync(filePath1, filePath2, fileTitle1, fileTitle2);
        }

        private async Task UpdateWebResourceDependencyXml(IOrganizationServiceExtented service, CommonConfiguration commonConfig, XDocument doc, string filePath, WebResource webResource)
        {
            {
                string dependencyXml = webResource.GetAttributeValue<string>(WebResource.Schema.Attributes.dependencyxml);

                string fieldTitle = WebResource.Schema.Headers.dependencyxml + " BackUp";

                string fileNameBackUp = EntityFileNameFormatter.GetWebResourceFileName(service.ConnectionData.Name, webResource.Name, fieldTitle, "xml");
                string filePathBackUp = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(fileNameBackUp));

                if (!string.IsNullOrEmpty(dependencyXml))
                {
                    try
                    {
                        dependencyXml = ContentComparerHelper.FormatXmlByConfiguration(
                            dependencyXml
                            , commonConfig
                            , XmlOptionsControls.WebResourceDependencyXmlOptions
                            , schemaName: AbstractDynamicCommandXsdSchemas.SchemaDependencyXml
                            , webResourceName: webResource.Name
                        );

                        File.WriteAllText(filePathBackUp, dependencyXml, new UTF8Encoding(false));

                        this._iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, service.ConnectionData.Name, WebResource.Schema.EntityLogicalName, webResource.Name, fieldTitle, filePathBackUp);
                    }
                    catch (Exception ex)
                    {
                        this._iWriteToOutput.WriteErrorToOutput(service.ConnectionData, ex);
                    }
                }
                else
                {
                    this._iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, service.ConnectionData.Name, WebResource.Schema.EntityLogicalName, webResource.Name, fieldTitle);
                    this._iWriteToOutput.ActivateOutputWindow(service.ConnectionData);
                }
            }

            var newText = doc.ToString(SaveOptions.DisableFormatting);

            var updateEntity = new WebResource
            {
                Id = webResource.Id
            };
            updateEntity.Attributes[WebResource.Schema.Attributes.dependencyxml] = newText;

            await service.UpdateAsync(updateEntity);

            var repositoryPublish = new PublishActionsRepository(service);

            _iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.PublishingWebResourceFormat2, service.ConnectionData.Name, webResource.Name);

            await repositoryPublish.PublishWebResourcesAsync(new[] { webResource.Id });
        }

        private async Task<bool> ValidateDocumentDependencyXml(ConnectionData connectionData, XDocument doc)
        {
            this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ValidatingXmlForFieldFormat1, WebResource.Schema.Attributes.dependencyxml);

            ContentComparerHelper.ClearRoot(doc);

            bool validateResult = await WebResourceRepository.ValidateXmlDocumentAsync(connectionData, _iWriteToOutput, doc);

            if (!validateResult)
            {
                this._iWriteToOutput.WriteToOutput(connectionData, Properties.OutputStrings.ValidatingXmlForFieldFailedFormat1, WebResource.Schema.Attributes.dependencyxml);
                _iWriteToOutput.ActivateOutputWindow(connectionData);

                var dialogResult = MessageBoxResult.Cancel;

                var t = new Thread(() =>
                {
                    dialogResult = MessageBox.Show(Properties.MessageBoxStrings.ContinueOperation, Properties.MessageBoxStrings.QuestionTitle, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

                t.Join();

                if (dialogResult != MessageBoxResult.OK)
                {
                    return false;
                }
            }

            return true;
        }

        private Task OpenInWebWebResourceDependencyXml(IOrganizationServiceExtented service, CommonConfiguration commonConfig, XDocument doc, string filePath, WebResource webResource)
        {
            return Task.Run(() => service.UrlGenerator.OpenSolutionComponentInWeb(ComponentType.WebResource, webResource.Id));
        }

        private async Task GetCurrentWebResourceDependencyXml(IOrganizationServiceExtented service, CommonConfiguration commonConfig, XDocument doc, string filePath, WebResource webResource)
        {
            string dependencyXml = webResource.GetAttributeValue<string>(WebResource.Schema.Attributes.dependencyxml);

            if (string.IsNullOrEmpty(dependencyXml))
            {
                this._iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.EntityFieldIsEmptyFormat4, service.ConnectionData.Name, WebResource.Schema.EntityLogicalName, webResource.Name, WebResource.Schema.Headers.dependencyxml);
                this._iWriteToOutput.ActivateOutputWindow(service.ConnectionData);
                return;
            }

            dependencyXml = ContentComparerHelper.FormatXmlByConfiguration(
                dependencyXml
                , commonConfig
                , XmlOptionsControls.WebResourceDependencyXmlOptions
                , schemaName: AbstractDynamicCommandXsdSchemas.SchemaDependencyXml
                , webResourceName: webResource.Name
            );

            if (string.IsNullOrEmpty(commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportIsEmpty);
                commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }
            else if (!Directory.Exists(commonConfig.FolderForExport))
            {
                _iWriteToOutput.WriteToOutput(null, Properties.OutputStrings.FolderForExportDoesNotExistsFormat1, commonConfig.FolderForExport);
                commonConfig.FolderForExport = FileOperations.GetDefaultFolderForExportFilePath();
            }

            string currentFileName = EntityFileNameFormatter.GetWebResourceFileName(service.ConnectionData.Name, webResource.Name, WebResource.Schema.Headers.dependencyxml, "xml");
            string currentFilePath = Path.Combine(commonConfig.FolderForExport, FileOperations.RemoveWrongSymbols(currentFileName));

            try
            {
                File.WriteAllText(currentFilePath, dependencyXml, new UTF8Encoding(false));

                this._iWriteToOutput.WriteToOutput(service.ConnectionData, Properties.OutputStrings.EntityFieldExportedToFormat5, service.ConnectionData.Name, WebResource.Schema.EntityLogicalName, webResource.Name, WebResource.Schema.Headers.dependencyxml, currentFilePath);
            }
            catch (Exception ex)
            {
                this._iWriteToOutput.WriteErrorToOutput(service.ConnectionData, ex);
            }

            this._iWriteToOutput.PerformAction(service.ConnectionData, currentFilePath);
        }

        #endregion WebResource DependencyXml
    }
}