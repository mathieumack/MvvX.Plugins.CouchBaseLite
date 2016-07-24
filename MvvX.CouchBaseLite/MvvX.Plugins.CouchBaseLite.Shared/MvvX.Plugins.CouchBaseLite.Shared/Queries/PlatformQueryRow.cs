using System;
using System.Collections.Generic;
using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Documents;
using MvvX.Plugins.CouchBaseLite.Queries;
using MvvX.Plugins.CouchBaseLite.Platform.Documents;
using System.Linq;

namespace MvvX.Plugins.CouchBaseLite.Platform.Queries
{
    public class PlatformQueryRow : IQueryRow
    {
        #region Fields

        private readonly QueryRow queryRow;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformQueryRow(QueryRow queryRow, IDatabase database)
        {
            this.queryRow = queryRow;
            this.database = database;
        }

        #endregion

        #region Implements

        public IDatabase Database
        {
            get
            {
                return database;
            }
        }

        public IDocument Document
        {
            get
            {
                return new PlatformDocument(queryRow.Document);
            }
        }

        public string DocumentId
        {
            get
            {
                return queryRow.DocumentId;
            }
        }

        public IDictionary<string, object> DocumentProperties
        {
            get
            {
                return queryRow.DocumentProperties;
            }
        }

        public string DocumentRevisionId
        {
            get
            {
                return queryRow.DocumentRevisionId;
            }
        }

        public object Key
        {
            get
            {
                return queryRow.Key;
            }
        }

        public long SequenceNumber
        {
            get
            {
                return queryRow.SequenceNumber;
            }
        }

        public string SourceDocumentId
        {
            get
            {
                return queryRow.SourceDocumentId;
            }
        }

        public object Value
        {
            get
            {
                return queryRow.Value;
            }
        }

        public IDictionary<string, object> AsJSONDictionary()
        {
            return queryRow.AsJSONDictionary();
        }

        public IEnumerable<ISavedRevision> GetConflictingRevisions()
        {
            return queryRow.GetConflictingRevisions().Select(e => new PlatformSavedRevision(e));
        }

        public T ValueAs<T>()
        {
            return queryRow.ValueAs<T>();
        }

        #endregion

        #region override

        public override bool Equals(object obj)
        {
            return queryRow.Equals(obj);
        }

        public override int GetHashCode()
        {
            return queryRow.GetHashCode();
        }

        public override string ToString()
        {
            return queryRow.ToString();
        }

        #endregion
    }
}
