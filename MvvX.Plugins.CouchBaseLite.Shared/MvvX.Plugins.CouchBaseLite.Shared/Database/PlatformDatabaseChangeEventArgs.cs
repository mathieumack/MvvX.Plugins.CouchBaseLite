using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Database;
using System.Collections.Generic;
using System.Linq;
using MvvX.Plugins.CouchBaseLite.Documents;
using MvvX.Plugins.CouchBaseLite.Shared.Documents;

namespace MvvX.Plugins.CouchBaseLite.Platform.Database
{
    public class PlatformDatabaseChangeEventArgs : IDatabaseChangeEventArgs
    {
        #region Fields

        private readonly DatabaseChangeEventArgs databaseChangeEventArgs;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformDatabaseChangeEventArgs(DatabaseChangeEventArgs databaseChangeEventArgs, IDatabase database)
        {
            this.databaseChangeEventArgs = databaseChangeEventArgs;
            this.database = database;
        }

        #endregion

        #region Implements

        public IEnumerable<IDocumentChange> Changes
        {
            get
            {
                return databaseChangeEventArgs.Changes.Select(e => new PlatformDocumentChange(e));
            }
        }

        public bool IsExternal
        {
            get
            {
                return databaseChangeEventArgs.IsExternal;
            }
        }

        public IDatabase Source
        {
            get
            {
                return database;
            }
        }
        
        #endregion
    }
}
