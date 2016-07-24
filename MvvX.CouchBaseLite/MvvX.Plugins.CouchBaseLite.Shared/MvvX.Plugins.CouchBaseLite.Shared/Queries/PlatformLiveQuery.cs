using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Platform.Queries;
using MvvX.Plugins.CouchBaseLite.Queries;
using System;

namespace MvvX.Plugins.CouchBaseLite.Shared.Queries
{
    public class PlatformLiveQuery : PlatformQuery, ILiveQuery
    {
        #region Fields

        private readonly LiveQuery liveQuery;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformLiveQuery(LiveQuery liveQuery, IDatabase database)
            : base(liveQuery, database)
        {
            this.liveQuery = liveQuery;
            this.database = database;
        }

        #endregion

        #region Impkements

        public Exception LastError
        {
            get
            {
                return liveQuery.LastError;
            }
        }
        
        public new IQueryEnumerator Run()
        {
            return new PlatformQueryEnumerator(this.liveQuery.Run(), this.database);
        }

        public IQueryEnumerator Rows
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public TimeSpan UpdateInterval
        {
            get
            {
                return liveQuery.UpdateInterval;
            }

            set
            {
                liveQuery.UpdateInterval = value;
            }
        }

        public void QueryOptionsChanged()
        {
            liveQuery.QueryOptionsChanged();
        }

        public void Start()
        {
            liveQuery.Start();
        }

        public void Stop()
        {
            liveQuery.Stop();
        }

        public void WaitForRows()
        {
            liveQuery.WaitForRows();
        }

        #endregion
    }
}
