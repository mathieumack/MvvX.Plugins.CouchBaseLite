using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MvvX.Plugins.CouchBaseLite.Platform.Documents
{
    public class PlatformUnsavedRevision : IUnsavedRevision
    {
        #region Fields

        private readonly UnsavedRevision unsavedRevision;

        public string Id
        {
            get
            {
                return unsavedRevision.Id;
            }
        }

        public bool IsGone
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string ParentId
        {
            get
            {
                return unsavedRevision.ParentId;
            }
        }

        public IEnumerable<string> AttachmentNames
        {
            get
            {
                return unsavedRevision.AttachmentNames;
            }
        }

        public IDictionary<string, object> Properties
        {
            get
            {
                return unsavedRevision.Properties;
            }
        }

        public IEnumerable<IAttachment> Attachments
        {
            get
            {
                return this.unsavedRevision.Attachments.Select(e => new PlatformAttachment(e));
            }
        }

        public IEnumerable<ISavedRevision> RevisionHistory
        {
            get
            {
                return this.unsavedRevision.RevisionHistory.Select(e => new PlatformSavedRevision(e));
            }
        }

        #endregion

        #region Constructor

        public PlatformUnsavedRevision(UnsavedRevision unsavedRevision)
        {
            this.unsavedRevision = unsavedRevision;
        }

        #endregion

        #region Methods

        public IAttachment GetAttachment(string name)
        {
            var attachment = unsavedRevision.GetAttachment(name);
            if (attachment != null)
                return new PlatformAttachment(attachment);
            else
                return null;
        }

        public void Dispose()
        {
            unsavedRevision.Dispose();
        }

        public void RemoveAttachment(string name)
        {
            unsavedRevision.RemoveAttachment(name);
        }

        public ISavedRevision Save()
        {
            var savedRevision = unsavedRevision.Save();
            return new PlatformSavedRevision(savedRevision);
        }

        public ISavedRevision Save(bool allowConflict)
        {
            var savedRevision = unsavedRevision.Save(allowConflict);
            return new PlatformSavedRevision(savedRevision);
        }

        public void SetAttachment(string name, string contentType, IEnumerable<byte> content)
        {
            unsavedRevision.SetAttachment(name, contentType, content);
        }

        public void SetAttachment(string name, string contentType, Stream content)
        {
            unsavedRevision.SetAttachment(name, contentType, content);
        }

        public void SetAttachment(string name, string contentType, Uri contentUrl)
        {
            unsavedRevision.SetAttachment(name, contentType, contentUrl);
        }

        public void SetProperties(IDictionary<string, object> newProperties)
        {
            unsavedRevision.SetProperties(newProperties);
        }

        public void SetUserProperties(IDictionary<string, object> userProperties)
        {
            unsavedRevision.SetProperties(userProperties);
        }

        #endregion
    }
}
