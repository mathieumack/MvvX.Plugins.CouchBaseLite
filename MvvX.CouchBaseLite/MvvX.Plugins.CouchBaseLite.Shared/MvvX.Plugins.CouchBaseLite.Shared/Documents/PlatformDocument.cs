using System;
using System.Collections.Generic;
using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;
using System.Linq;
using System.IO;

namespace MvvX.Plugins.CouchBaseLite.Platform.Documents
{
    public class PlatformDocument : IDocument
    {
        #region Fields

        private readonly Document document;

        #endregion

        #region Constructor

        public PlatformDocument(Document document)
        {
            this.document = document;
        }

        #endregion

        #region Interface

        public string Id
        {
            get
            {
                return document.Id;
            }
        }

        public string CurrentRevisionId
        {
            get
            {
                return document.CurrentRevisionId;
            }
        }

        public bool Deleted
        {
            get
            {
                return document.Deleted;
            }
        }

        public ISavedRevision CurrentRevision
        {
            get
            {
                if (document.CurrentRevision != null)
                    return new PlatformSavedRevision(document.CurrentRevision);
                else
                    return null;
            }
        }

        public IDictionary<string, object> Properties
        {
            get
            {
                return document.Properties;
            }
        }

        public IDictionary<string, object> UserProperties
        {
            get
            {
                return document.UserProperties;
            }
        }

        public IEnumerable<ISavedRevision> LeafRevisions
        {
            get
            {
                try
                {
                    return this.document.LeafRevisions.Select(e => new PlatformSavedRevision(e));
                }
                catch(Couchbase.Lite.CouchbaseLiteException ex)
                {
                    throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
                }
            }
        }

        public IEnumerable<ISavedRevision> RevisionHistory
        {
            get
            {
                try
                { 
                    return this.document.RevisionHistory.Select(e => new PlatformSavedRevision(e));
                }
                catch (Couchbase.Lite.CouchbaseLiteException ex)
                {
                    throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
                }
            }
        }

        public IEnumerable<ISavedRevision> ConflictingRevisions
        {
            get
            {
                try
                {
                    return this.document.ConflictingRevisions.Select(e => new PlatformSavedRevision(e));
                }
                catch (Couchbase.Lite.CouchbaseLiteException ex)
                {
                    throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
                }
            }
        }

        public TValue GetProperty<TValue>(string key)
        {
            return document.GetProperty<TValue>(key);
        }

        public object GetProperty(string key)
        {
            return document.GetProperty(key);
        }

        public void Delete()
        {
            try
            { 
                document.Delete();
            }
            catch (Couchbase.Lite.CouchbaseLiteException ex)
            {
                throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }
        }

        public void Purge()
        {
            try
            { 
                document.Purge();
            }
            catch (Couchbase.Lite.CouchbaseLiteException ex)
            {
                throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }
        }

        public ISavedRevision PutProperties(IDictionary<string, object> properties)
        {
            try
            {
                var revision = document.PutProperties(properties);
                return new PlatformSavedRevision(revision);
            }
            catch (Couchbase.Lite.CouchbaseLiteException ex)
            {
                throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }
        }

        public void Update(Func<IUnsavedRevision, bool> updateAction)
        {
            try
            { 
                document.Update((UnsavedRevision newRevision) =>
                {
                    return updateAction.Invoke(new PlatformUnsavedRevision(newRevision));
                });
            }
            catch (Couchbase.Lite.CouchbaseLiteException ex)
            {
                throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }
        }

        public void ExpireAfter(TimeSpan timeInterval)
        {
            document.ExpireAfter(timeInterval);
        }

        public void ExpireAt(DateTime? expireTime)
        {
            document.ExpireAt(expireTime);
        }

        public DateTime? GetExpirationDate()
        {
            return document.GetExpirationDate();
        }

        public ISavedRevision GetRevision(string id)
        {
            var revision = document.GetRevision(id);
            if (revision != null)
                return new PlatformSavedRevision(revision);
            else
                return null;
        }

        public IUnsavedRevision CreateRevision()
        {
            var revision = document.CreateRevision();
            if (revision != null)
                return new PlatformUnsavedRevision(revision);
            else
                return null;
        }

        public bool PutExistingRevision(IDictionary<string, object> properties, IDictionary<string, Stream> attachments, IList<string> revisionHistory, Uri sourceUri)
        {
            try
            {
                return document.PutExistingRevision(properties, attachments, revisionHistory, sourceUri);
            }
            catch (Couchbase.Lite.CouchbaseLiteException ex)
            {
                throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }
        }

        //private static void addAttachment(Database database, String documentId)
        //{
        //    Document document = database.GetDocument(documentId);
        //    try
        //    {
        //        /* Add an attachment with sample data as POC */
        //        Stream inputStream = new MemoryStream(new byte[] { 0, 0, 0, 0 });
        //        var revision = document.CurrentRevision.CreateRevision();
        //        revision.SetAttachment("binaryData", "application/octet-stream", inputStream);
        //        /* Save doc & attachment to the local DB */
        //        revision.Save();
        //    }
        //    catch (CouchbaseLiteException e)
        //    {
        //        Console.WriteLine("Error putting");
        //    }
        //}

        #endregion
    }
}
