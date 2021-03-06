﻿using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Threading;
using Nav.Common.VSPackages.CrmDeveloperHelper.Helpers;
using Nav.Common.VSPackages.CrmDeveloperHelper.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Nav.Common.VSPackages.CrmDeveloperHelper.Intellisense
{
    public class UrlCommandFilter : IOleCommandTarget
    {
        public const string PrefixOpenInVisualStudio = "openinvisualstudio";
        public const string PrefixOpenInVisualStudioRelativePath = "openinvisualstudiopath";
        public const string PrefixOpenInTextEditor = "openintexteditor";
        public const string PrefixOpenInExcel = "openinexcel";
        public const string PrefixShowDifference = "showdifference";
        public const string PrefixSelectFileInFolder = "selectfileinfolder";
        public const string PrefixOpenSolution = "opensolution";
        public const string PrefixOpenSolutionList = "opensolutionlist";

        private IOleCommandTarget _nextCommandHandler;
        private ITextView _textView;
        private UrlCommandFilterProvider _provider;

        private readonly ITagAggregator<IUrlTag> _urlTagAggregator;

        public UrlCommandFilter(IVsTextView textViewAdapter, ITextView textView, UrlCommandFilterProvider provider)
        {
            this._textView = textView;
            this._provider = provider;
            this._urlTagAggregator = provider.ViewTagAggregatorFactoryService.CreateTagAggregator<IUrlTag>(textView);

            var hresult = textViewAdapter.AddCommandFilter(this, out _nextCommandHandler);
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            if (pguidCmdGroup == VSConstants.VSStd2K)
            {
                var any = prgCmds.Any(e =>
                    e.cmdID == (uint)VSConstants.VSStd2KCmdID.OPENURL
                );

                if (any)
                {
                    return VSConstants.S_OK;
                }
            }

            ThreadHelper.ThrowIfNotOnUIThread();

            var ret = _nextCommandHandler.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);

            return ret;
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            if (pguidCmdGroup == typeof(VSConstants.VSStd2KCmdID).GUID)
            {
                if (nCmdID == (uint)VSConstants.VSStd2KCmdID.OPENURL)
                {
                    if (pvaIn != IntPtr.Zero)
                    {
                        int line = (int)Marshal.GetObjectForNativeVariant(pvaIn);
                        int column = (int)Marshal.GetObjectForNativeVariant(new IntPtr(pvaIn.ToInt32() + 16));

                        if (TryOpenUrlAtPoint(line, column))
                        {
                            return VSConstants.S_OK;
                        }
                    }

                    if (TryOpenUrlAtCaret())
                    {
                        return VSConstants.S_OK;
                    }
                }
            }

            ThreadHelper.ThrowIfNotOnUIThread();

            int retVal = _nextCommandHandler.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);

            return retVal;
        }

        private bool TryOpenUrlAtCaret()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            SnapshotPoint bufferPosition = _textView.Caret.Position.BufferPosition;
            ITextSnapshotLine containingLine = bufferPosition.GetContainingLine();
            int line = containingLine.LineNumber;
            int column = bufferPosition - containingLine.Start;

            return TryOpenUrlAtPoint(line, column);
        }

        private bool TryOpenUrlAtPoint(int line, int column)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (!TryGetUrlSpan(line, column, out ITagSpan<IUrlTag> tagSpan))
                return false;

            if (!IsUrlSpanValid(tagSpan))
                return false;

            return OpenUri(tagSpan.Tag.Url);
        }

        private bool TryGetUrlSpan(int line, int column, out ITagSpan<IUrlTag> urlSpan)
        {
            urlSpan = null;

            if (!TryToSnapshotPoint(_textView.TextSnapshot, line, column, out SnapshotPoint point))
                return false;

            SnapshotSpan span = new SnapshotSpan(point, 0);
            foreach (IMappingTagSpan<IUrlTag> current in _urlTagAggregator.GetTags(span))
            {
                NormalizedSnapshotSpanCollection spans = current.Span.GetSpans(_textView.TextSnapshot);
                if (spans.Count == 1 && spans[0].Length == current.Span.GetSpans(current.Span.AnchorBuffer)[0].Length && spans[0].Contains(span.Start))
                {
                    urlSpan = new TagSpan<IUrlTag>(spans[0], current.Tag);
                    return true;
                }
            }

            return false;
        }

        private static bool TryToSnapshotPoint(ITextSnapshot snapshot, int startLine, int startIndex, out SnapshotPoint result)
        {
            if (snapshot == null)
                throw new ArgumentNullException("snapshot");

            result = default(SnapshotPoint);
            if (snapshot == null || startLine < 0 || startLine >= snapshot.LineCount || startIndex < 0)
                return false;

            ITextSnapshotLine startSnapshotLine = snapshot.GetLineFromLineNumber(startLine);
            if (startIndex > startSnapshotLine.Length)
                return false;

            result = startSnapshotLine.Start + startIndex;
            return true;
        }

        private static bool IsUrlSpanValid(ITagSpan<IUrlTag> urlTagSpan)
        {
            return urlTagSpan != null
                && urlTagSpan.Tag != null
                && urlTagSpan.Tag.Url != null;
        }

        private bool OpenUri(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            if (string.Equals(uri.Scheme, PrefixOpenInVisualStudio, StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(uri.LocalPath))
                {
                    DTEHelper.Singleton?.OpenFileInVisualStudio(null, uri.LocalPath);
                }

                return true;
            }

            if (string.Equals(uri.Scheme, PrefixOpenInVisualStudioRelativePath, StringComparison.InvariantCultureIgnoreCase))
            {
                DTEHelper.Singleton?.OpenFileInVisualStudioRelativePath(null, uri.LocalPath);

                return true;
            }

            if (string.Equals(uri.Scheme, PrefixOpenInTextEditor, StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(uri.LocalPath))
                {
                    DTEHelper.Singleton?.OpenFileInTextEditor(null, uri.LocalPath);
                }

                return true;
            }

            if (string.Equals(uri.Scheme, PrefixOpenInExcel, StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(uri.LocalPath))
                {
                    DTEHelper.Singleton?.OpenFileInExcel(null, uri.LocalPath);
                }

                return true;
            }

            if (string.Equals(uri.Scheme, PrefixShowDifference, StringComparison.InvariantCultureIgnoreCase))
            {
                DTEHelper.Singleton?.ShowDifference(uri);

                return true;
            }

            if (string.Equals(uri.Scheme, PrefixOpenSolution, StringComparison.InvariantCultureIgnoreCase))
            {
                DTEHelper.Singleton?.OpenSolution(uri);

                return true;
            }

            if (string.Equals(uri.Scheme, PrefixOpenSolutionList, StringComparison.InvariantCultureIgnoreCase))
            {
                DTEHelper.Singleton?.OpenSolutionList(uri);

                return true;
            }

            if (string.Equals(uri.Scheme, PrefixSelectFileInFolder, StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(uri.LocalPath))
                {
                    DTEHelper.Singleton?.SelectFileInFolder(null, uri.LocalPath);
                }

                return true;
            }

            if (_provider.ServiceProvider.GetService(typeof(SVsWebBrowsingService)) is IVsWebBrowsingService service)
            {
                var createFlags = __VSCREATEWEBBROWSER.VSCWB_AutoShow;
                var resolution = VSPREVIEWRESOLUTION.PR_Default;

                int result = ErrorHandler.CallWithCOMConvention(() =>
                {
                    ThreadHelper.ThrowIfNotOnUIThread();

                    service.CreateExternalWebBrowser((uint)createFlags, resolution, uri.AbsoluteUri);
                });

                if (ErrorHandler.Succeeded(result))
                    return true;
            }

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                return false;

            try
            {
                Process.Start(uri.AbsoluteUri);
                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}