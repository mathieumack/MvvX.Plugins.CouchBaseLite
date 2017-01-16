using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MvvX.Plugins.CouchBaseLite.Platform.Documents
{
    public class PlatformUnsavedRevision : PlatformRevision, IUnsavedRevision
    {
        #region Fields

        private readonly UnsavedRevision unsavedRevision;
           
        #endregion

        #region Constructor

        public PlatformUnsavedRevision(UnsavedRevision unsavedRevision)
            : base(unsavedRevision)
        {
            this.unsavedRevision = unsavedRevision;
        }

        #endregion

        #region Methods
        
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
