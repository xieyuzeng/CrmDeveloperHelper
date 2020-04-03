﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Nav.Common.VSPackages.CrmDeveloperHelper.Entities;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Views
{
    public static class WindowHelper
    {
        public static void OpenEntityMetadataExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenEntityMetadataExplorer(iWriteToOutput, service, commonConfig, null, null, null, false, null);
        }

        public static void OpenEntityMetadataExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
        )
        {
            OpenEntityMetadataExplorer(iWriteToOutput, service, commonConfig, null, filterEntityName, null, false, null);
        }

        public static void OpenEntityMetadataExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
            , EnvDTE.SelectedItem selectedItem
        )
        {
            OpenEntityMetadataExplorer(iWriteToOutput, service, commonConfig, null, filterEntityName, null, false, selectedItem);
        }

        public static void OpenEntityMetadataExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
            , string filePath
            , bool isJavaScript
        )
        {
            OpenEntityMetadataExplorer(iWriteToOutput, service, commonConfig, null, filterEntityName, filePath, isJavaScript, null);
        }

        public static void OpenEntityMetadataExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , IEnumerable<EntityMetadata> entityMetadataList
            , string filterEntityName
        )
        {
            OpenEntityMetadataExplorer(iWriteToOutput, service, commonConfig, entityMetadataList, filterEntityName, null, false, null);
        }

        public static void OpenEntityMetadataExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , IEnumerable<EntityMetadata> entityMetadataList
            , string filterEntityName
            , string filePath
            , bool isJavaScript
            , EnvDTE.SelectedItem selectedItem
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerEntityMetadata(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        , entityMetadataList
                        , filePath
                        , isJavaScript
                        , selectedItem
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityMetadataFileGenerationOptions(FileGenerationOptions fileGenerationOptions)
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerEntityMetadataOptions(fileGenerationOptions);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityAttributeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerEntityAttribute(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityKeyExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerEntityKey(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityRelationshipOneToManyExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerEntityRelationshipOneToMany(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityRelationshipManyToManyExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerEntityRelationshipManyToMany(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityPrivilegesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenEntityPrivilegesExplorer(iWriteToOutput, service, commonConfig, null, null);
        }

        public static void OpenEntityPrivilegesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
        )
        {
            OpenEntityPrivilegesExplorer(iWriteToOutput, service, commonConfig, filterEntityName, null);
        }

        public static void OpenEntityPrivilegesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
            , IEnumerable<EntityMetadata> entityMetadataList
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerEntityPrivileges(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityMetadataList
                        , filterEntityName
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOtherPrivilegesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenOtherPrivilegesExplorer(iWriteToOutput, service, commonConfig, null, null);
        }

        public static void OpenOtherPrivilegesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
        )
        {
            OpenOtherPrivilegesExplorer(iWriteToOutput, service, commonConfig, filter, null);
        }

        public static void OpenOtherPrivilegesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
            , IEnumerable<Privilege> privilegesList
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerOtherPrivileges(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , privilegesList
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityEditor(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string entityName
            , Guid entityId
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowEntityEditor(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityName
                        , entityId
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityBulkEditor(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string entityName
            , IEnumerable<Guid> entityIds
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowEntityBulkEditor(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityName
                        , entityIds
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenEntityBulkTransfer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , EntityMetadata entityMetadata
            , IEnumerable<Entity> entities
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowEntityBulkTransfer(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityMetadata
                        , entities
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenApplicationRibbonExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerApplicationRibbon(
                        iWriteToOutput
                        , service
                        , commonConfig
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSystemFormExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenSystemFormExplorer(iWriteToOutput, service, commonConfig, null, null, null);
        }

        public static void OpenSystemFormExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
        )
        {
            OpenSystemFormExplorer(iWriteToOutput, service, commonConfig, filterEntityName, null, null);
        }

        public static void OpenSystemFormExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
            , string selection
        )
        {
            OpenSystemFormExplorer(iWriteToOutput, service, commonConfig, filterEntityName, selection, null);
        }

        public static void OpenSystemFormExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName
            , string selection
            , EnvDTE.SelectedItem selectedItem
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSystemForm(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        , selectedItem
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSavedQueryExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName = null
            , string selection = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSavedQuery(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSavedQueryVisualizationExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName = null
            , string selection = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSavedQueryVisualization(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenWorkflowExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filterEntityName = null
            , string selection = null
        )
        {

            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerWorkflow(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filterEntityName
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenCustomControlExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerCustomControl(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenTraceReaderExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            )
        {

            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowTraceReader(
                        iWriteToOutput
                        , service
                        , commonConfig
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenPluginTreeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string entityFilter = null
            , string pluginTypeFilter = null
            , string messageFilter = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowPluginTree(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityFilter
                        , pluginTypeFilter
                        , messageFilter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSdkMessageTreeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string entityFilter = null
            , string messageFilter = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowSdkMessageTree(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityFilter
                        , messageFilter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSdkMessageRequestTreeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenSdkMessageRequestTreeExplorer(iWriteToOutput, service, commonConfig, null, false, null, null, null);
        }

        public static void OpenSdkMessageRequestTreeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig

            , string entityFilter
        )
        {
            OpenSdkMessageRequestTreeExplorer(iWriteToOutput, service, commonConfig, null, false, null, entityFilter, null);
        }

        public static void OpenSdkMessageRequestTreeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig

            , string entityFilter
            , string messageFilter
        )
        {
            OpenSdkMessageRequestTreeExplorer(iWriteToOutput, service, commonConfig, null, false, null, entityFilter, messageFilter);
        }

        public static void OpenSdkMessageRequestTreeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig

            , string filePath
            , bool isJavaScript
            , EnvDTE.SelectedItem selectedItem

            , string entityFilter
            , string messageFilter
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowSdkMessageRequestTree(
                        iWriteToOutput
                        , service
                        , commonConfig

                        , filePath
                        , isJavaScript
                        , selectedItem

                        , entityFilter
                        , messageFilter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSystemUsersExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
        )
        {
            OpenSystemUsersExplorer(iWriteToOutput, service, commonConfig, filter, null, null);
        }

        public static void OpenSystemUsersExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
            , IEnumerable<EntityMetadata> entityMetadataList
            , IEnumerable<Privilege> privileges
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSystemUser(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityMetadataList
                        , privileges
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenTeamsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
        )
        {
            OpenTeamsExplorer(iWriteToOutput, service, commonConfig, filter, null, null);
        }

        public static void OpenTeamsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
            , IEnumerable<EntityMetadata> entityMetadataList
            , IEnumerable<Privilege> privileges
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerTeam(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityMetadataList
                        , privileges
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenRolesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenRolesExplorer(iWriteToOutput, service, commonConfig, null, null, null);
        }

        public static void OpenRolesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
        )
        {
            OpenRolesExplorer(iWriteToOutput, service, commonConfig, filter, null, null);
        }

        public static void OpenRolesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
            , IEnumerable<EntityMetadata> entityMetadataList
            , IEnumerable<Privilege> privileges
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerRole(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , entityMetadataList
                        , privileges
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenExplorerSolutionExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , int? componentType
            , Guid? objectId
            , EnvDTE.SelectedItem selectedItem
            )
        {
            var worker = new Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSolution(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , componentType
                        , objectId
                        , selectedItem
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenImportJobExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string selection
        )
        {
            var worker = new Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerImportJob(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSolutionComponentDependenciesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , SolutionComponentDescriptor descriptor
            , CommonConfiguration commonConfig
            , string solutionUniqueName
            , string selection = null
            )
        {
            var worker = new Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSolutionComponents(iWriteToOutput, service, descriptor, commonConfig, solutionUniqueName, selection);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSolutionComponentDependenciesExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , SolutionComponentDescriptor descriptor
            , CommonConfiguration commonConfig
            , int componentType
            , Guid objectId
            , string selection = null
            )
        {
            var worker = new Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSolutionComponentDependencies(
                        iWriteToOutput
                        , service
                        , descriptor
                        , commonConfig
                        , componentType
                        , objectId
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenExplorerComponentsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , SolutionComponentDescriptor descriptor
            , CommonConfiguration commonConfig
            , IEnumerable<SolutionComponent> solutionComponents
            , string solutionUniqueName
            , string header
            , string selection
        )
        {
            var worker = new Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerComponents(iWriteToOutput, service, descriptor, commonConfig, solutionComponents, solutionUniqueName, header, selection);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenGlobalOptionSetsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenGlobalOptionSetsExplorer(iWriteToOutput, service, commonConfig, null, null, null, null, false, null);
        }

        public static void OpenGlobalOptionSetsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
        )
        {
            OpenGlobalOptionSetsExplorer(iWriteToOutput, service, commonConfig, null, null, filter, null, false, null);
        }

        public static void OpenGlobalOptionSetsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
            , string filterEntityName
        )
        {
            OpenGlobalOptionSetsExplorer(iWriteToOutput, service, commonConfig, null, filterEntityName, filter, null, false, null);
        }

        public static void OpenGlobalOptionSetsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
            , EnvDTE.SelectedItem selectedItem
        )
        {
            OpenGlobalOptionSetsExplorer(iWriteToOutput, service, commonConfig, null, null, filter, null, false, selectedItem);
        }

        public static void OpenGlobalOptionSetsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
            , string filePath
            , bool isJavaScript
        )
        {
            OpenGlobalOptionSetsExplorer(iWriteToOutput, service, commonConfig, null, null, filter, filePath, isJavaScript, null);
        }

        public static void OpenGlobalOptionSetsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , IEnumerable<OptionSetMetadata> optionSets
            , string filter
        )
        {
            OpenGlobalOptionSetsExplorer(iWriteToOutput, service, commonConfig, optionSets, null, filter, null, false, null);
        }

        public static void OpenGlobalOptionSetsExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , IEnumerable<OptionSetMetadata> optionSets
            , string filterEntityName
            , string filter
            , string filePath
            , bool isJavaScript
            , EnvDTE.SelectedItem selectedItem
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerGlobalOptionSets(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , optionSets
                        , filterEntityName
                        , filter
                        , filePath
                        , isJavaScript
                        , selectedItem
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenGlobalOptionSetsFileGenerationOptions(FileGenerationOptions fileGenerationOptions)
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerGlobalOptionSetsOptions(fileGenerationOptions);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenReportExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string selection = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerReport(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenExportSiteMapExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenExportSiteMapExplorer(iWriteToOutput, service, commonConfig, string.Empty);
        }

        public static void OpenExportSiteMapExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string filter
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerSiteMap(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenWebResourceExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
        )
        {
            OpenWebResourceExplorer(iWriteToOutput, service, commonConfig, null);
        }

        public static void OpenWebResourceExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string selection
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerWebResource(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenPluginTypeExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string selection = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerPluginType(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenPluginAssemblyExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string selection = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerPluginAssembly(
                        iWriteToOutput
                        , service
                        , commonConfig
                        , selection
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenPluginAssemblyUpdateWindow(IWriteToOutput iWriteToOutput, IOrganizationServiceExtented service, PluginAssembly assembly, EnvDTE.Project project)
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    string defaultOutputFilePath = null;

                    if (project != null)
                    {
                        defaultOutputFilePath = PropertiesHelper.GetOutputFilePath(project);
                    }

                    var form = new WindowPluginAssembly(iWriteToOutput, service, assembly, defaultOutputFilePath, project);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationExplorer(
            IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowExplorerOrganization(
                        iWriteToOutput
                        , service
                        , commonConfig
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSolutionImageWindow(
            IWriteToOutput iWriteToOutput
            , ConnectionData connectionData
            , CommonConfiguration commonConfig
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowSolutionImage(
                        iWriteToOutput
                        , commonConfig
                        , connectionData
                        , null
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenSolutionDifferenceImageWindow(
            IWriteToOutput iWriteToOutput
            , ConnectionData connectionData
            , CommonConfiguration commonConfig
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowSolutionDifferenceImage(
                        iWriteToOutput
                        , commonConfig
                        , connectionData
                        , null
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationDifferenceImageWindow(
            IWriteToOutput iWriteToOutput
            , ConnectionData connectionData
            , CommonConfiguration commonConfig
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationDifferenceImage(
                        iWriteToOutput
                        , commonConfig
                        , connectionData
                        , null
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerWindow(
            IWriteToOutput iWriteToOutput
            , ConnectionConfiguration crmConfig
            , CommonConfiguration commonConfig
        )
        {
            OpenOrganizationComparerWindow(iWriteToOutput, crmConfig, commonConfig, null);
        }

        public static void OpenOrganizationComparerWindow(
            IWriteToOutput iWriteToOutput
            , ConnectionConfiguration crmConfig
            , CommonConfiguration commonConfig
            , SolutionImage solutionImage
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparer(
                        iWriteToOutput
                        , crmConfig
                        , commonConfig
                        , solutionImage
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerEntityMetadataWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string entityFilter = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerEntityMetadata(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , entityFilter
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerApplicationRibbonWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerApplicationRibbon(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerGlobalOptionSetsWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filter = null
            )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerGlobalOptionSets(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filter
                        );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerSystemFormWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filterEntity = null
            , string filter = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerSystemForm(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filterEntity
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerSavedQueryWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filterEntity = null
            , string filter = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerSavedQuery(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filterEntity
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerSavedQueryVisualizationWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filterEntity = null
            , string filter = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerSavedQueryVisualization(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filterEntity
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerWorkflowWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filterEntity = null
            , string filter = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerWorkflow(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filterEntity
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerReportWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filter = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerReport(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerSiteMapWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerSiteMap(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerWebResourcesWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filter = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerWebResources(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenOrganizationComparerPluginAssemblyWindow(
            IWriteToOutput iWriteToOutput
            , CommonConfiguration commonConfig
            , ConnectionData connection1
            , ConnectionData connection2
            , string filter = null
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowOrganizationComparerPluginAssembly(
                        iWriteToOutput
                        , commonConfig
                        , connection1
                        , connection2
                        , filter
                    );

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenCrmConnectionCard(
            IWriteToOutput iWriteToOutput
            , ConnectionData connectionData
        )
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowCrmConnectionCard(iWriteToOutput, connectionData, connectionData.ConnectionConfiguration.Users);

                    form.ShowDialog();

                    connectionData.Save();
                    connectionData.ConnectionConfiguration.Save();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(connectionData, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenFileGenerationOptions(FileGenerationOptions fileGenerationOptions)
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowFileGenerationOptions(fileGenerationOptions);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static void OpenFileGenerationConfiguration(FileGenerationConfiguration fileGenerationConfiguration)
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                try
                {
                    var form = new WindowFileGenerationConfiguration(fileGenerationConfiguration);

                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            });

            worker.SetApartmentState(System.Threading.ApartmentState.STA);

            worker.Start();
        }

        public static bool IsDefinedExplorer(ComponentType componentType)
        {
            switch (componentType)
            {
                case ComponentType.Entity:
                case ComponentType.Attribute:
                case ComponentType.OptionSet:
                case ComponentType.EntityRelationship:
                case ComponentType.EntityKey:

                case ComponentType.Role:
                case ComponentType.Organization:

                case ComponentType.SavedQuery:
                case ComponentType.SavedQueryVisualization:
                case ComponentType.SystemForm:

                case ComponentType.Workflow:
                case ComponentType.Report:
                case ComponentType.WebResource:
                case ComponentType.SiteMap:

                case ComponentType.PluginType:
                case ComponentType.PluginAssembly:

                case ComponentType.SdkMessageProcessingStep:
                case ComponentType.SdkMessageProcessingStepImage:

                case ComponentType.SdkMessage:
                case ComponentType.SdkMessageFilter:

                case ComponentType.SdkMessagePair:
                case ComponentType.SdkMessageRequest:
                case ComponentType.SdkMessageRequestField:
                case ComponentType.SdkMessageResponse:
                case ComponentType.SdkMessageResponseField:

                case ComponentType.RibbonCustomization:

                case ComponentType.CustomControl:

                    return true;
            }

            return false;
        }

        public static void OpenComponentExplorer(
            ComponentType componentType
            , IWriteToOutput iWriteToOutput
            , IOrganizationServiceExtented service
            , CommonConfiguration commonConfig
            , string componentName
            , string parameter
        )
        {
            switch (componentType)
            {
                case ComponentType.Entity:
                    OpenEntityMetadataExplorer(iWriteToOutput, service, commonConfig, null, componentName);
                    break;

                case ComponentType.Attribute:
                    OpenEntityAttributeExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;

                case ComponentType.OptionSet:
                    OpenGlobalOptionSetsExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;

                case ComponentType.EntityRelationship:
                    if (string.Equals(parameter, typeof(OneToManyRelationshipMetadata).Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        OpenEntityRelationshipOneToManyExplorer(iWriteToOutput, service, commonConfig, componentName);
                    }
                    else if (string.Equals(parameter, typeof(ManyToManyRelationshipMetadata).Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        OpenEntityRelationshipManyToManyExplorer(iWriteToOutput, service, commonConfig, componentName);
                    }
                    break;

                case ComponentType.EntityKey:
                    OpenEntityKeyExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;

                case ComponentType.Role:
                    OpenRolesExplorer(iWriteToOutput, service, commonConfig, componentName, null, null);
                    break;

                case ComponentType.Organization:
                    OpenOrganizationExplorer(iWriteToOutput, service, commonConfig);
                    break;

                case ComponentType.SavedQuery:
                    OpenSavedQueryExplorer(iWriteToOutput, service, commonConfig, null, componentName);
                    break;

                case ComponentType.SavedQueryVisualization:
                    OpenSavedQueryVisualizationExplorer(iWriteToOutput, service, commonConfig, null, componentName);
                    break;

                case ComponentType.SystemForm:
                    OpenSystemFormExplorer(iWriteToOutput, service, commonConfig, null, componentName);
                    break;

                case ComponentType.Workflow:
                    OpenWorkflowExplorer(iWriteToOutput, service, commonConfig, null, componentName);
                    break;

                case ComponentType.Report:
                    OpenReportExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;

                case ComponentType.WebResource:
                    OpenWebResourceExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;

                case ComponentType.SiteMap:
                    OpenExportSiteMapExplorer(iWriteToOutput, service, commonConfig);
                    break;

                case ComponentType.PluginType:
                    OpenPluginTypeExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;

                case ComponentType.PluginAssembly:
                    OpenPluginAssemblyExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;

                case ComponentType.SdkMessageProcessingStep:
                case ComponentType.SdkMessageProcessingStepImage:
                    OpenPluginTreeExplorer(iWriteToOutput, service, commonConfig);
                    break;

                case ComponentType.SdkMessage:
                case ComponentType.SdkMessageFilter:
                    OpenSdkMessageTreeExplorer(iWriteToOutput, service, commonConfig, null, componentName);
                    break;

                case ComponentType.SdkMessagePair:
                    OpenSdkMessageRequestTreeExplorer(iWriteToOutput, service, commonConfig, null, componentName);
                    break;

                case ComponentType.SdkMessageRequest:
                case ComponentType.SdkMessageRequestField:
                case ComponentType.SdkMessageResponse:
                case ComponentType.SdkMessageResponseField:
                    OpenSdkMessageRequestTreeExplorer(iWriteToOutput, service, commonConfig);
                    break;

                case ComponentType.RibbonCustomization:
                    OpenApplicationRibbonExplorer(iWriteToOutput, service, commonConfig);
                    break;

                case ComponentType.CustomControl:
                    OpenCustomControlExplorer(iWriteToOutput, service, commonConfig, componentName);
                    break;
            }
        }
    }
}