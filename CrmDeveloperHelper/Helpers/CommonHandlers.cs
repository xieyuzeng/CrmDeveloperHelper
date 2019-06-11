﻿using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Interfaces;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Xml.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Helpers
{
    public static class CommonHandlers
    {
        private static readonly TimeSpan _cacheItemSpan = TimeSpan.FromSeconds(1);

        private static bool CheckActiveDocumentExtension(EnvDTE80.DTE2 applicationObject, Func<string, bool> checker)
        {
            bool result = false;

            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeDocument
                && applicationObject.ActiveWindow.Document != null
            )
            {
                try
                {
                    string file = applicationObject.ActiveWindow.Document.FullName.ToString().ToLower();

                    result = checker(file);
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);

                    result = false;
                }
            }

            return result;
        }

        private static bool CheckActiveDocumentIsXmlWithRoot(EnvDTE80.DTE2 applicationObject, out XElement doc, params string[] rootNames)
        {
            doc = null;

            if (rootNames == null || !rootNames.Any())
            {
                return false;
            }

            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeDocument
                && applicationObject.ActiveWindow.Document != null
            )
            {
                try
                {
                    string file = applicationObject.ActiveWindow.Document.FullName.ToString().ToLower();

                    if (FileOperations.SupportsXmlType(file))
                    {
                        var objTextDoc = applicationObject.ActiveWindow.Document.Object("TextDocument");
                        if (objTextDoc != null
                            && objTextDoc is TextDocument textDocument
                            )
                        {
                            string text = textDocument.StartPoint.CreateEditPoint().GetText(textDocument.EndPoint);

                            if (!string.IsNullOrEmpty(text))
                            {
                                if (ContentCoparerHelper.TryParseXml(text, out doc))
                                {
                                    string docRootName = doc.Name.ToString();

                                    foreach (var item in rootNames)
                                    {
                                        if (string.Equals(docRootName, item, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            }

            return false;
        }

        private static bool CheckActiveDocumentIsXmlWithRootWithAttribute(
            EnvDTE80.DTE2 applicationObject
            , XName attributeName
            , out XAttribute attribute
            , params string[] rootNames
        )
        {
            attribute = null;

            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeDocument
                && applicationObject.ActiveWindow.Document != null
            )
            {
                try
                {
                    string file = applicationObject.ActiveWindow.Document.FullName.ToString().ToLower();

                    if (FileOperations.SupportsXmlType(file))
                    {
                        var objTextDoc = applicationObject.ActiveWindow.Document.Object("TextDocument");
                        if (objTextDoc != null
                            && objTextDoc is TextDocument textDocument
                        )
                        {
                            string text = textDocument.StartPoint.CreateEditPoint().GetText(textDocument.EndPoint);

                            if (!string.IsNullOrEmpty(text))
                            {
                                if (ContentCoparerHelper.TryParseXml(text, out var doc))
                                {
                                    string docRootName = doc.Name.ToString();

                                    foreach (var rootName in rootNames)
                                    {
                                        if (string.Equals(docRootName, rootName, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            attribute = doc.Attribute(attributeName);

                                            if (attribute != null)
                                            {
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            }

            return false;
        }

        internal static bool CheckActiveDocumentIsFetchOrGrid(EnvDTE80.DTE2 applicationObject)
        {
            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeDocument
                && applicationObject.ActiveWindow.Document != null
            )
            {
                try
                {
                    string file = applicationObject.ActiveWindow.Document.FullName.ToString().ToLower();

                    if (FileOperations.SupportsXmlType(file))
                    {
                        var objTextDoc = applicationObject.ActiveWindow.Document.Object("TextDocument");
                        if (objTextDoc != null
                            && objTextDoc is TextDocument textDocument
                            )
                        {
                            string text = textDocument.StartPoint.CreateEditPoint().GetText(textDocument.EndPoint);

                            if (!string.IsNullOrEmpty(text))
                            {
                                if (ContentCoparerHelper.TryParseXml(text, out var doc))
                                {
                                    if (string.Equals(doc.Name.ToString(), Commands.AbstractDynamicCommandXsdSchemas.RootFetch, StringComparison.InvariantCultureIgnoreCase)
                                        || string.Equals(doc.Name.ToString(), Commands.AbstractDynamicCommandXsdSchemas.RootGrid, StringComparison.InvariantCultureIgnoreCase)
                                        || string.Equals(doc.Name.ToString(), Commands.AbstractDynamicCommandXsdSchemas.RootSavedQuery, StringComparison.InvariantCultureIgnoreCase)
                                    )
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            }

            return false;
        }

        private static bool CheckOpenedDocumentsExtension(EnvDTE80.DTE2 applicationObject, Func<string, bool> checker)
        {
            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeDocument
                && applicationObject.ActiveWindow.Document != null
            )
            {
                foreach (var document in applicationObject.Documents.OfType<EnvDTE.Document>())
                {
                    if (document == applicationObject.ActiveWindow.Document
                        || document.ActiveWindow == null
                        || document.ActiveWindow.Type != EnvDTE.vsWindowType.vsWindowTypeDocument
                        || document.ActiveWindow.Visible == false
                        )
                    {
                        continue;
                    }

                    if (applicationObject.ItemOperations.IsFileOpen(document.FullName, EnvDTE.Constants.vsViewKindTextView)
                        || applicationObject.ItemOperations.IsFileOpen(document.FullName, EnvDTE.Constants.vsViewKindCode)
                    )
                    {
                        try
                        {
                            string file = document.FullName;

                            if (checker(file))
                            {
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            DTEHelper.WriteExceptionToOutput(null, ex);
                        }
                    }
                }
            }

            return false;
        }

        private static bool CheckInSolutionExplorerSingle(EnvDTE80.DTE2 applicationObject, Func<string, bool> checker)
        {
            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeSolutionExplorer
                && applicationObject.SelectedItems != null
            )
            {
                try
                {
                    var items = applicationObject.SelectedItems.Cast<EnvDTE.SelectedItem>();

                    var count = items.Where(a =>
                    {
                        try
                        {
                            string file = a.Name.ToLower();

                            return checker(file);
                        }
                        catch (Exception ex)
                        {
                            DTEHelper.WriteExceptionToOutput(null, ex);
                        }

                        return false;
                    }).Take(2).Count();

                    if (count == 1)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            }

            return false;
        }

        private static bool CheckInSolutionExplorerAny(EnvDTE80.DTE2 applicationObject, Func<string, bool> checker)
        {
            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeSolutionExplorer
                && applicationObject.SelectedItems != null
            )
            {
                try
                {
                    var items = applicationObject.SelectedItems.Cast<EnvDTE.SelectedItem>();

                    return items.Any(a =>
                    {
                        try
                        {
                            string file = a.Name.ToLower();

                            return checker(file);
                        }
                        catch (Exception ex)
                        {
                            DTEHelper.WriteExceptionToOutput(null, ex);

                            return false;
                        }
                    });
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);
                }
            }

            return false;
        }

        private static bool CheckInSolutionExplorerRecursive(EnvDTE80.DTE2 applicationObject, Func<string, bool> checker)
        {
            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeSolutionExplorer
                && applicationObject.SelectedItems != null
                )
            {
                var items = applicationObject.SelectedItems.Cast<EnvDTE.SelectedItem>().ToList();

                foreach (var item in items)
                {
                    if (item.ProjectItem != null)
                    {
                        if (FillListProjectItems(item.ProjectItem.ProjectItems, checker))
                        {
                            return true;
                        }
                    }

                    if (item.Project != null)
                    {
                        if (FillListProjectItems(item.Project.ProjectItems, checker))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool FillListProjectItems(EnvDTE.ProjectItems projectItems, Func<string, bool> checker)
        {
            if (projectItems != null)
            {
                foreach (EnvDTE.ProjectItem projItem in projectItems)
                {
                    string path = projItem.FileNames[1];

                    if (checker(path))
                    {
                        return true;
                    }

                    if (FillListProjectItems(projItem.ProjectItems, checker))
                    {
                        return true;
                    }

                    if (projItem.SubProject != null)
                    {
                        if (FillListProjectItems(projItem.SubProject.ProjectItems, checker))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        internal static void ActionBeforeQueryStatusActiveDocumentWebResourceText(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusActiveDocumentWebResourceText);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckActiveDocumentExtension(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentWebResource(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusActiveDocumentWebResource);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckActiveDocumentExtension(applicationObject, FileOperations.SupportsWebResourceType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentXml(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusActiveDocumentXml);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckActiveDocumentExtension(applicationObject, FileOperations.SupportsXmlType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentIsXmlWithRoot(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand, out XElement doc, params string[] rootNames)
        {
            doc = null;

            bool visible = false;

            visible = CheckActiveDocumentIsXmlWithRoot(applicationObject, out doc, rootNames);

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentIsXmlWithRootWithAttribute(
            EnvDTE80.DTE2 applicationObject
            , OleMenuCommand menuCommand
            , XName attributeName
            , out XAttribute attribute
            , params string[] rootNames
        )
        {
            attribute = null;

            bool visible = false;

            visible = CheckActiveDocumentIsXmlWithRootWithAttribute(applicationObject, attributeName, out attribute, rootNames);

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentIsFetchOrGrid(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusActiveDocumentIsFetchOrGrid);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckActiveDocumentIsFetchOrGrid(applicationObject);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentReport(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusActiveDocumentReport);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckActiveDocumentExtension(applicationObject, FileOperations.SupportsReportType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentCSharp(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusActiveDocumentCSharp);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckActiveDocumentExtension(applicationObject, FileOperations.SupportsCSharpType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentJavaScript(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusActiveDocumentJavaScript);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckActiveDocumentExtension(applicationObject, FileOperations.SupportsJavaScriptType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusActiveDocumentContainingProject(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerSingleItemContainsProject);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                var helper = DTEHelper.Create(applicationObject);

                var document = helper.GetOpenedDocumentInCodeWindow(FileOperations.SupportsCSharpType);

                if (document != null
                    && document.ProjectItem != null
                    && document.ProjectItem.ContainingProject != null
                )
                {
                    visible = true;
                }

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerSingleItemContainsProject(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerSingleItemContainsProject);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                var helper = DTEHelper.Create(applicationObject);

                var projectItem = helper.GetSingleSelectedProjectItemInSolutionExplorer(FileOperations.SupportsCSharpType);

                if (projectItem != null && projectItem.ContainingProject != null)
                {
                    visible = true;
                }

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerAnyItemContainsProject(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand, bool recursive)
        {
            ObjectCache cache = MemoryCache.Default;
            string cacheName = nameof(ActionBeforeQueryStatusOpenedDocumentsWebResourceText) + recursive.ToString();

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                var helper = DTEHelper.Create(applicationObject);

                var list = helper.GetSelectedProjectItemsInSolutionExplorer(FileOperations.SupportsCSharpType, recursive);

                visible = list.Any(item => item != null && item.ContainingProject != null);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusOpenedDocumentsWebResourceText(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusOpenedDocumentsWebResourceText);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckOpenedDocumentsExtension(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusOpenedDocumentsWebResource(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusOpenedDocumentsWebResource);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckOpenedDocumentsExtension(applicationObject, FileOperations.SupportsWebResourceType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusOpenedDocumentsReport(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusOpenedDocumentsReport);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckOpenedDocumentsExtension(applicationObject, FileOperations.SupportsReportType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusOpenedDocumentsCSharp(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusOpenedDocumentsCSharp);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckOpenedDocumentsExtension(applicationObject, FileOperations.SupportsCSharpType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusOpenedDocumentsJavaScript(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusOpenedDocumentsJavaScript);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckOpenedDocumentsExtension(applicationObject, FileOperations.SupportsJavaScriptType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusOpenedDocumentsXml(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusOpenedDocumentsXml);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckOpenedDocumentsExtension(applicationObject, FileOperations.SupportsXmlType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerCSharpSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerCSharpSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerSingle(applicationObject, FileOperations.SupportsCSharpType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerCSharpAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerCSharpAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerAny(applicationObject, FileOperations.SupportsCSharpType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerCSharpRecursive(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerCSharpRecursive);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerRecursive(applicationObject, FileOperations.SupportsCSharpType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerJavaScriptSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerJavaScriptSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerSingle(applicationObject, FileOperations.SupportsJavaScriptType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerJavaScriptAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerJavaScriptAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerAny(applicationObject, FileOperations.SupportsJavaScriptType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerJavaScriptRecursive(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerJavaScriptRecursive);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerRecursive(applicationObject, FileOperations.SupportsJavaScriptType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerWebResourceSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerWebResourceSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerSingle(applicationObject, FileOperations.SupportsWebResourceType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerWebResourceAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerWebResourceAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerAny(applicationObject, FileOperations.SupportsWebResourceType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerWebResourceRecursive(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerWebResourceRecursive);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerRecursive(applicationObject, FileOperations.SupportsWebResourceType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerWebResourceTextSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerWebResourceTextSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerSingle(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerWebResourceTextAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerWebResourceTextAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerAny(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerXmlAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerXmlAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerAny(applicationObject, FileOperations.SupportsXmlType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerWebResourceTextRecursive(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerWebResourceTextRecursive);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerRecursive(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerXmlRecursive(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerXmlRecursive);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerRecursive(applicationObject, FileOperations.SupportsXmlType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerReportSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerReportSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerSingle(applicationObject, FileOperations.SupportsReportType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerReportAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerReportAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerAny(applicationObject, FileOperations.SupportsReportType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusSolutionExplorerReportRecursive(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusSolutionExplorerReportRecursive);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerRecursive(applicationObject, FileOperations.SupportsReportType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActiveSolutionExplorerFolderSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            bool visible = false;

            if (applicationObject.ActiveWindow != null
               && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeSolutionExplorer
               && applicationObject.SelectedItems != null
            )
            {
                var items = applicationObject.SelectedItems.Cast<EnvDTE.SelectedItem>().Take(2);

                if (items.Count() == 1)
                {
                    var item = items.First();

                    visible = true;
                }
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActiveSolutionExplorerProjectSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActiveSolutionExplorerProjectSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerProjectSingle(applicationObject);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActiveSolutionExplorerProjectAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActiveSolutionExplorerProjectAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInSolutionExplorerProjectAny(applicationObject);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static bool CheckInSolutionExplorerProjectSingle(EnvDTE80.DTE2 applicationObject)
        {
            bool visible = false;

            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeSolutionExplorer
                && applicationObject.SelectedItems != null)
            {
                var items = applicationObject.SelectedItems.Cast<EnvDTE.SelectedItem>().Take(2);

                if (items.Count() == 1)
                {
                    var item = items.First();

                    if (item.Project != null)
                    {
                        visible = true;
                    }
                }
            }

            return visible;
        }

        internal static bool CheckInSolutionExplorerProjectAny(EnvDTE80.DTE2 applicationObject)
        {
            if (applicationObject.ActiveWindow != null
                && applicationObject.ActiveWindow.Type == EnvDTE.vsWindowType.vsWindowTypeSolutionExplorer
                && applicationObject.SelectedItems != null
            )
            {
                var items = applicationObject.SelectedItems.Cast<EnvDTE.SelectedItem>();

                return items.All(i => i.Project != null) && items.Any(i => i.Project != null);
            }

            return false;
        }

        private static bool CheckInPublishListSingle(EnvDTE80.DTE2 applicationObject, Func<string, bool> checker)
        {
            bool result = false;

            if (applicationObject != null)
            {
                try
                {
                    var helper = DTEHelper.Create(applicationObject);

                    var selectedFiles = helper.GetSelectedFilesFromListForPublish();

                    var count = selectedFiles.Where(f => checker(f.FilePath)).Take(2).Count();

                    result = count == 1;
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);

                    result = false;
                }
            }

            return result;
        }

        private static bool CheckInPublishListAny(EnvDTE80.DTE2 applicationObject, Func<string, bool> checker)
        {
            bool result = false;

            if (applicationObject != null)
            {
                try
                {
                    var helper = DTEHelper.Create(applicationObject);

                    var selectedFiles = helper.GetSelectedFilesFromListForPublish();

                    result = selectedFiles.Any(f => checker(f.FilePath));
                }
                catch (Exception ex)
                {
                    DTEHelper.WriteExceptionToOutput(null, ex);

                    result = false;
                }
            }

            return result;
        }

        internal static void ActionBeforeQueryStatusListForPublishWebResourceTextSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusListForPublishWebResourceTextSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInPublishListSingle(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusListForPublishWebResourceTextAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusListForPublishWebResourceTextAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInPublishListAny(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusListForPublishWebResourceSingle(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusListForPublishWebResourceSingle);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInPublishListSingle(applicationObject, FileOperations.SupportsWebResourceTextType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusListForPublishWebResourceAny(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusListForPublishWebResourceAny);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CheckInPublishListAny(applicationObject, FileOperations.SupportsWebResourceType);

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusFilesToAdd(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusFilesToAdd);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                var helper = DTEHelper.Create(applicationObject);

                visible = helper.GetSelectedFilesAll(FileOperations.SupportsWebResourceType, true).Any();

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static IEnumerable<SelectedFile> GetOpenedDocuments(DTEHelper helper)
        {
            return helper.GetOpenedDocuments(FileOperations.SupportsWebResourceTextType);
        }

        internal static IEnumerable<SelectedFile> GetSelectedFiles(DTEHelper helper)
        {
            return helper.GetSelectedFilesInSolutionExplorer(FileOperations.SupportsWebResourceTextType, false);
        }

        internal static IEnumerable<SelectedFile> GetSelectedFilesRecursive(DTEHelper helper)
        {
            return helper.GetSelectedFilesInSolutionExplorer(FileOperations.SupportsWebResourceTextType, true);
        }

        internal static IEnumerable<SelectedFile> GetSelectedFilesInListForPublish(DTEHelper helper)
        {
            var selectedFiles = helper.GetSelectedFilesFromListForPublish();

            if (selectedFiles.Any())
            {
                helper.ShowListForPublish(null);
            }
            else
            {
                helper.WriteToOutput(null, Properties.OutputStrings.PublishListIsEmpty);
            }

            return selectedFiles;
        }

        internal static void CorrectCommandNameForConnectionName(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand, string name)
        {
            var helper = DTEHelper.Create(applicationObject);
            var connection = helper.GetCurrentConnectionName();

            if (!string.IsNullOrEmpty(connection))
            {
                name = string.Format(Properties.CommandNames.CommandNameWithConnectionFormat2, name, connection);
            }

            menuCommand.Text = name;
        }

        internal static void ActionBeforeQueryStatusTextEditorProgramExists(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusTextEditorProgramExists);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = CommonConfiguration.Get().TextEditorProgramExists();

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }

        internal static void ActionBeforeQueryStatusConnectionIsNotReadOnly(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            ObjectCache cache = MemoryCache.Default;
            const string cacheName = nameof(ActionBeforeQueryStatusConnectionIsNotReadOnly);

            bool visible = false;

            if (cache.Contains(cacheName))
            {
                visible = (bool)cache.Get(cacheName);
            }
            else
            {
                visible = (ConnectionConfiguration.Get().CurrentConnectionData?.IsReadOnly).GetValueOrDefault() == false;

                cache.Set(cacheName, visible, new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheItemSpan),
                });
            }

            if (visible == false)
            {
                menuCommand.Enabled = menuCommand.Visible = false;
            }
        }
    }
}
