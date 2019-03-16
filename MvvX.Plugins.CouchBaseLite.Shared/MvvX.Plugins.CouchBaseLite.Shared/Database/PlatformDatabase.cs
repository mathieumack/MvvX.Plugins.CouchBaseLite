using System.Collections.Generic;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Documents;
using MvvX.Plugins.CouchBaseLite.Platform.Documents;
using MvvX.Plugins.CouchBaseLite.Queries;
using MvvX.Plugins.CouchBaseLite.Platform.Queries;
using System;
using MvvX.Plugins.CouchBaseLite.Views;
using MvvX.Plugins.CouchBaseLite.Shared.Views;
using System.Threading.Tasks;
using MvvX.Plugins.CouchBaseLite.Shared.Sync;
using MvvX.Plugins.CouchBaseLite.Sync;

namespace MvvX.Plugins.CouchBaseLite.Platform.Database
{
    public class PlatformDatabase : IDatabase
    {
        #region Fields

        private readonly Couchbase.Lite.Database database;

        #endregion

        #region Constructor

        public PlatformDatabase(Couchbase.Lite.Database database)
        {
            if (database == null)
                throw new ArgumentNullException("database");

            this.database = database;
            this.database.Changed += Database_Changed;
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Change from the database
        /// </summary>
        public event EventHandler<IDatabaseChangeEventArgs> Changed;

        private void Database_Changed(object sender, Couchbase.Lite.DatabaseChangeEventArgs e)
        {
            if(Changed != null)
            {
                this.Changed(sender, new PlatformDatabaseChangeEventArgs(e, this));
            }
        }

        public string Name
        {
            get
            {
                return database.Name;
            }
        }

        public bool Compact()
        {
            return database.Compact();
        }

        public IQuery CreateAllDocumentsQuery()
        {
            var query = this.database.CreateAllDocumentsQuery();
            return new PlatformQuery(query, this);
        }

        public IDocument CreateDocument()
        {
            var document = this.database.CreateDocument();
            if (document != null)
                return new PlatformDocument(document);
            else
                return null;
        }

        public void Dispose()
        {
            this.database.Changed -= Database_Changed;
            database.Dispose();
        }

        public IDocument GetDocument(string id)
        {
            var document = database.GetDocument(id);
            if (document != null)
                return new PlatformDocument(document);
            else
                return null;
        }

        public int GetDocumentCount()
        {
            return database.GetDocumentCount();
        }

        public IDocument GetExistingDocument(string id)
        {
            var document = database.GetExistingDocument(id);
            if (document != null)
                return new PlatformDocument(document);
            else
                return null;
        }

        public IDictionary<string, object> GetExistingLocalDocument(string id)
        {
            return database.GetExistingLocalDocument(id);
        }

        public long GetLastSequenceNumber()
        {
            return database.GetLastSequenceNumber();
        }

        public int GetMaxRevTreeDepth()
        {
            return database.GetMaxRevTreeDepth();
        }

        public long GetTotalDataSize()
        {
            return database.GetTotalDataSize();
        }

        public bool PutLocalDocument(string id, IDictionary<string, object> properties)
        {
            return database.PutLocalDocument(id, properties);
        }

        public void SetMaxRevTreeDepth(int value)
        {
            database.SetMaxRevTreeDepth(value);
        }

        public void StorageExitedTransaction(bool committed)
        {
            database.StorageExitedTransaction(committed);
        }

        public void Delete()
        {
            database.Delete();
        }

        public bool DeleteLocalDocument(string id)
        {
            return database.DeleteLocalDocument(id);
        }

        public IView GetExistingView(string name)
        {
            var view = this.database.GetExistingView(name);
            if (view != null)
                return new PlatformView(view, this);
            else
                return null;
        }

        public IView GetView(string name)
        {
            var view = this.database.GetView(name);
            if (view != null)
                return new PlatformView(view, this);
            else
                return null;
        }

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        /// <returns></returns>
        public Task Close()
        {
            return this.database.Close();
        }

        public IReplication CreatePushReplication(Uri url)
        {
            var replication = this.database.CreatePushReplication(url);
            return new PlatformReplication(replication, this);
        }

        public IReplication CreatePullReplication(Uri url)
        {
            var replication = this.database.CreatePullReplication(url);
            return new PlatformReplication(replication, this);
        }

        #endregion
    }
}
