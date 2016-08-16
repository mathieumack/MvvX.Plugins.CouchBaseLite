using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Queries;
using System.Collections.Generic;
using System.Collections;
using MvvX.Plugins.CouchBaseLite.Database;
using System;

namespace MvvX.Plugins.CouchBaseLite.Platform.Queries
{
    public class PlatformQueryEnumerator : IQueryEnumerator
    {
        #region Fields

        private readonly QueryEnumerator queryEnumerator;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformQueryEnumerator(QueryEnumerator queryEnumerator, IDatabase database)
        {
            this.queryEnumerator = queryEnumerator;
            this.database = database;
        }

        #endregion

        #region Implements

        public int Count
        {
            get
            {
                return queryEnumerator.Count;
            }
        }

        public IQueryRow Current
        {
            get
            {
                return new PlatformQueryRow(queryEnumerator.Current, database);
            }
        }

        public long SequenceNumber
        {
            get
            {
                return queryEnumerator.SequenceNumber;
            }
        }

        public bool Stale
        {
            get
            {
                return queryEnumerator.Stale;
            }
        }

        public bool IsStale()
        {
            return queryEnumerator.IsStale();
        }

        object IEnumerator.Current
        {
            get
            {
                // TODO : Check this value.
                return queryEnumerator.Current;
            }
        }

        public void Dispose()
        {
            this.queryEnumerator.Dispose();
        }

        public IQueryRow GetRow(int index)
        {
            return new PlatformQueryRow(this.queryEnumerator.GetRow(index), this.database);
        }

        public IEnumerator<IQueryRow> GetEnumerator()
        {
            return CastEnumerator(this.queryEnumerator.GetEnumerator());
        }

        private IEnumerator<IQueryRow> CastEnumerator(IEnumerator<QueryRow> iterator)
        {
            while (iterator.MoveNext())
            {
                yield return new PlatformQueryRow(iterator.Current, database);
            }
        }

        public bool MoveNext()
        {
            return this.queryEnumerator.MoveNext();
        }

        public void Reset()
        {
            this.queryEnumerator.Reset();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.queryEnumerator.GetEnumerator();
        }

        #endregion

        #region overrides

        public override bool Equals(object obj)
        {
            return this.queryEnumerator.Equals(obj);
        }
        
        public override int GetHashCode()
        {
            return this.queryEnumerator.GetHashCode();
        }

        #endregion

    }
}
