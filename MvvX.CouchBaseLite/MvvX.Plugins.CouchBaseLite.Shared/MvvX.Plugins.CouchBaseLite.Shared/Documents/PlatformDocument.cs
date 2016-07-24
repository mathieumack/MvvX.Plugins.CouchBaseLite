using System;
using System.Collections.Generic;
using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;
using System.Linq;

namespace MvvX.Plugins.CouchBaseLite.Platform.Documents
{
    public class PlatformDocument : IDocument
    {
        #region Fields

        private readonly Document document;
        
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
                return this.document.LeafRevisions.Select(e => new PlatformSavedRevision(e));
            }
        }

        public IEnumerable<ISavedRevision> RevisionHistory
        {
            get
            {
                return this.document.RevisionHistory.Select(e => new PlatformSavedRevision(e));
            }
        }

        #endregion

        public PlatformDocument(Document document)
        {
            this.document = document;
        }

        #region Interface

        public T GetProperty<T>(string key) where T : class
        {
            return document.GetProperty<T>(key);
        }

        public object GetProperty(string key)
        {
            return document.GetProperty(key);
        }

        public void Delete()
        {
            document.Delete();
        }

        public void Purge()
        {
            document.Purge();
        }

        public ISavedRevision PutProperties(IDictionary<string, object> properties)
        {
            var revision = document.PutProperties(properties);
            return new PlatformSavedRevision(revision);
        }

        public void Update(Func<IUnsavedRevision, bool> updateAction)
        {
            document.Update((UnsavedRevision newRevision) =>
            {
                return updateAction.Invoke(new PlatformUnsavedRevision(newRevision));
            });
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
