using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;
using System;

namespace MvvX.Plugins.CouchBaseLite.Shared.Documents
{
    public class PlatformDocumentChange : IDocumentChange
    {
        #region Fields

        private readonly DocumentChange documentchange;

        #endregion

        #region Constructor

        public PlatformDocumentChange(DocumentChange documentchange)
        {
            this.documentchange = documentchange;
        }

        #endregion

        #region Implements

        public string DocumentId
        {
            get
            {
                return documentchange.DocumentId;
            }
        }

        public bool IsConflict
        {
            get
            {
                return documentchange.IsConflict;
            }
        }

        public bool IsCurrentRevision
        {
            get
            {
                return documentchange.IsCurrentRevision;
            }
        }

        public string RevisionId
        {
            get
            {
                return documentchange.RevisionId;
            }
        }

        public Uri SourceUrl
        {
            get
            {
                return documentchange.SourceUrl;
            }
        }

        #endregion
    }
}
