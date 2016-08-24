using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Platform.Queries;
using MvvX.Plugins.CouchBaseLite.Queries;
using System;

namespace MvvX.Plugins.CouchBaseLite.Shared.Queries
{
    public class PlatformQueryCompletedEventArgs : IQueryCompletedEventArgs
    {
        #region Fields

        private readonly QueryCompletedEventArgs queryCompletedEventArgs;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformQueryCompletedEventArgs(QueryCompletedEventArgs queryCompletedEventArgs, IDatabase database)
        {
            this.queryCompletedEventArgs = queryCompletedEventArgs;
            this.database = database;
        }

        #endregion

        #region Implements

        public Exception ErrorInfo
        {
            get
            {
                return queryCompletedEventArgs.ErrorInfo;
            }
        }

        public IQueryEnumerator Rows
        {
            get
            {
                if (queryCompletedEventArgs.Rows != null)
                    return new PlatformQueryEnumerator(queryCompletedEventArgs.Rows, database);
                else
                    return null;
            }
        }

        #endregion
    }
}
