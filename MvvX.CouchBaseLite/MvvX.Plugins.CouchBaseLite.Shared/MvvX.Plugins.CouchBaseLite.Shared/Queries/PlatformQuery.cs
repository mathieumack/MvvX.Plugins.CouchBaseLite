using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Queries;
using MvvX.Plugins.CouchBaseLite.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Platform.Queries
{
    public class PlatformQuery : IQuery
    {
        #region Fields

        private readonly Query query;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformQuery(Query query, IDatabase database)
        {
            this.query = query;
            this.database = database;
            this.settedPostFilter = null;
        }

        #endregion

        #region Implements

        public IndexUpdateMode IndexUpdateMode
        {
            get
            {
                return (IndexUpdateMode)(int)this.query.IndexUpdateMode;
            }

            set
            {
                this.query.IndexUpdateMode = (Couchbase.Lite.IndexUpdateMode)(int)value;
            }
        }

        public QueryAllDocsMode AllDocsMode
        {
            get
            {
                return (QueryAllDocsMode)(int)this.query.AllDocsMode;
            }

            set
            {
                this.query.AllDocsMode = (AllDocsMode)(int)value;
            }
        }

        public bool Descending
        {
            get
            {
                return this.query.Descending;
            }

            set
            {
                this.query.Descending = value;
            }
        }

        public object EndKey
        {
            get
            {
                return this.query.EndKey;
            }

            set
            {
                this.query.EndKey = value;
            }
        }

        public string EndKeyDocId
        {
            get
            {
                return this.query.EndKeyDocId;
            }

            set
            {
                this.query.EndKeyDocId = value;
            }
        }

        public int GroupLevel
        {
            get
            {
                return this.query.GroupLevel;
            }

            set
            {
                this.query.GroupLevel = value;
            }
        }

        public bool IncludeDeleted
        {
            get
            {
                return this.query.IncludeDeleted;
            }

            set
            {
                this.query.IncludeDeleted = value;
            }
        }

        public bool InclusiveEnd
        {
            get
            {
                return this.query.InclusiveEnd;
            }

            set
            {
                this.query.InclusiveEnd = value;
            }
        }

        public bool InclusiveStart
        {
            get
            {
                return this.query.InclusiveStart;
            }

            set
            {
                this.query.InclusiveStart = value;
            }
        }

        public IEnumerable<object> Keys
        {
            get
            {
                return this.query.Keys;
            }

            set
            {
                this.query.Keys = value;
            }
        }

        public int Limit
        {
            get
            {
                return this.query.Limit;
            }

            set
            {
                this.query.Limit = value;
            }
        }

        public bool MapOnly
        {
            get
            {
                return this.query.MapOnly;
            }

            set
            {
                this.query.MapOnly = value;
            }
        }

        private Func<IQueryRow, bool> settedPostFilter = null;
        public Func<IQueryRow, bool> PostFilter
        {
            get
            {
                return settedPostFilter;
            }

            set
            {
                this.settedPostFilter = value;
                this.query.PostFilter = (row) =>
                {
                    var platformRow = new PlatformQueryRow(row, database);
                    return value(platformRow);
                };
            }
        }

        public bool Prefetch
        {
            get
            {
                return this.query.Prefetch;
            }

            set
            {
                this.query.Prefetch = value;
            }
        }

        public int Skip
        {
            get
            {
                return this.query.Skip;
            }

            set
            {
                this.query.Skip = value;
            }
        }

        public object StartKey
        {
            get
            {
                return this.query.StartKey;
            }

            set
            {
                this.query.StartKey = value;
            }
        }

        public string StartKeyDocId
        {
            get
            {
                return this.query.StartKeyDocId;
            }

            set
            {
                this.query.StartKeyDocId = value;
            }
        }
        
        public void Dispose()
        {
            this.query.Dispose();
        }

        public IQueryEnumerator Run()
        {
            return new PlatformQueryEnumerator(this.query.Run(), this.database);
        }

        public async Task<IQueryEnumerator> RunAsync()
        {
            var result = await this.query.RunAsync();
            return new PlatformQueryEnumerator(result, this.database);
        }

        public ILiveQuery ToLiveQuery()
        {
            var result = this.query.ToLiveQuery();
            return new PlatformLiveQuery(result, this.database);
        }

        #endregion
    }
}
