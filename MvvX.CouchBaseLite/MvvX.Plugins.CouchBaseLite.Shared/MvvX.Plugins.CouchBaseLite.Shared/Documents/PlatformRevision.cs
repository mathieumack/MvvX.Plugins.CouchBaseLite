using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MvvX.Plugins.CouchBaseLite.Platform.Documents
{
    public class PlatformRevision : IRevision
    {
        private readonly Revision revision;

        public IEnumerable<string> AttachmentNames
        {
            get
            {
                return revision.AttachmentNames;
            }
        }

        public IEnumerable<ISavedRevision> RevisionHistory
        {
            get
            {
                return this.revision.RevisionHistory.Select(e => new PlatformSavedRevision(e));
            }
        }

        public IEnumerable<IAttachment> Attachments
        {
            get
            {
                return this.revision.Attachments.Select(e => new PlatformAttachment(e));
            }
        }

        public IAttachment GetAttachment(string name)
        {
            var attachment = revision.GetAttachment(name);
            if (attachment != null)
                return new PlatformAttachment(attachment);
            else
                return null;
        }

        public bool IsGone
        {
            get
            {
                return revision.IsGone;
            }
        }

        public PlatformRevision(Revision revision)
        {
            this.revision = revision;
        }

        #region overrides

        public override bool Equals(object obj)
        {
            return revision.Equals(obj);
        }
        
        public override int GetHashCode()
        {
            return revision.GetHashCode();
        }
        
        public override string ToString()
        {
            return revision.ToString();
        }

        #endregion
    }
}
