using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public interface IQueryRow
    {
        /// <summary>
        /// Database
        /// </summary>
        IDatabase Database { get; }

        /// <summary>
        /// Current document
        /// </summary>
        IDocument Document { get; }

        /// <summary>
        /// ID of the document
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        /// Properties of the document
        /// </summary>
        IDictionary<string, object> DocumentProperties { get; }

        /// <summary>
        /// Revision id of the document
        /// </summary>
        string DocumentRevisionId { get; }

        object Key { get; }

        long SequenceNumber { get; }

        string SourceDocumentId { get; }

        object Value { get; }

        IDictionary<string, object> AsJSONDictionary();
        
        IEnumerable<ISavedRevision> GetConflictingRevisions();
        
        T ValueAs<T>();
    }
}
