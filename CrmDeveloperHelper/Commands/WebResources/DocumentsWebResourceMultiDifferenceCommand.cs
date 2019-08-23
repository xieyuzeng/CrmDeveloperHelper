﻿using Microsoft.VisualStudio.Shell;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Commands.WebResources
{
    internal sealed class DocumentsWebResourceMultiDifferenceCommand : AbstractCommand
    {
        private readonly OpenFilesType _openFilesType;

        private DocumentsWebResourceMultiDifferenceCommand(
            OleMenuCommandService commandService
            , int idCommand
            , OpenFilesType openFilesType
        ) : base(commandService, idCommand)
        {
            this._openFilesType = openFilesType;
        }

        private static TupleList<int, OpenFilesType> _commandsDocuments = new TupleList<int, OpenFilesType>()
        {
              { PackageIds.DocumentsWebResourceMultiDifferenceFilesNotEqualByTextCommandId, OpenFilesType.NotEqualByText }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesNotExistsInCrmWithLinkCommandId, OpenFilesType.NotExistsInCrmWithLink }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesEqualByTextCommandId, OpenFilesType.EqualByText }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesWithInsertsCommandId, OpenFilesType.WithInserts }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesWithDeletesCommandId, OpenFilesType.WithDeletes }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesWithComplexCommandId, OpenFilesType.WithComplex }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesWithMirrorCommandId, OpenFilesType.WithMirror }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesWithMirrorInsertsCommandId, OpenFilesType.WithMirrorInserts }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesWithMirrorDeletesCommandId, OpenFilesType.WithMirrorDeletes }
            , { PackageIds.DocumentsWebResourceMultiDifferenceFilesWithMirrorComplexCommandId, OpenFilesType.WithMirrorComplex }
        };

        private static ConcurrentDictionary<OpenFilesType, DocumentsWebResourceMultiDifferenceCommand> _instances = new ConcurrentDictionary<OpenFilesType, DocumentsWebResourceMultiDifferenceCommand>();

        public static DocumentsWebResourceMultiDifferenceCommand Instance(OpenFilesType openFilesType)
        {
            if (_instances.ContainsKey(openFilesType))
            {
                return _instances[openFilesType];
            }

            return null;
        }

        public static void Initialize(OleMenuCommandService commandService)
        {
            foreach (var item in _commandsDocuments)
            {
                var command = new DocumentsWebResourceMultiDifferenceCommand(commandService, item.Item1, item.Item2);

                _instances.TryAdd(item.Item2, command);
            }
        }

        protected override void CommandAction(DTEHelper helper)
        {
            List<SelectedFile> selectedFiles = helper.GetOpenedDocuments(FileOperations.SupportsWebResourceTextType).ToList();

            if (selectedFiles.Count > 0)
            {
                helper.HandleMultiDifferenceFiles(selectedFiles, _openFilesType);
            }
        }

        protected override void CommandBeforeQueryStatus(EnvDTE80.DTE2 applicationObject, OleMenuCommand menuCommand)
        {
            CommonHandlers.ActionBeforeQueryStatusOpenedDocumentsWebResourceText(applicationObject, menuCommand);
        }
    }
}
