using MvvX.Plugins.CouchBaseLite.Database;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public interface IQuery : IDisposable
    {
        /// <summary>
        /// Event raised when a query has finished running.
        /// </summary>
        event EventHandler<IQueryCompletedEventArgs> Completed;
        
        /// <summary>
        /// Gets the <see cref="IDatabase"/> that owns 
        /// the <see cref="IQuery"/>'s <see cref="Views.IView"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IDatabase"/> that owns
        /// the <see cref="IQuery"/>'s <see cref="Views.IView"/>.
        /// </value>
        IDatabase Database { get; }
        
        /// <summary>
        /// Gets or sets the maximum number of rows to return. 
        /// The default value is int.MaxValue, meaning 'unlimited'.
        /// </summary>
        int Limit { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Documents.IDocument"/> id of the first value to return. 
        /// A null value has no effect. This is useful if the view contains 
        /// multiple identical keys, making startKey ambiguous.
        /// </summary>
        /// <value>The Document id of the first value to return.</value>
        string StartKeyDocId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Documents.IDocument"/> id of the last value to return. 
        /// A null value has no effect. This is useful if the view contains 
        /// multiple identical keys, making endKey ambiguous.
        /// </summary>
        /// <value>The Document id of the last value to return.</value>
        string EndKeyDocId { get; set; }

        /// <summary>
        /// If true the StartKey (or StartKeyDocID) comparison uses "&gt;=". Else it uses "&gt;"
        /// </summary>
        bool InclusiveStart { get; set; }

        /// <summary>
        /// If true the EndKey (or EndKeyDocID) comparison uses "&lt;=". Else it uses "&lt;".
        /// Default value is <c>true</c>.
        /// </summary>
        /// <value><c>true</c> if InclusiveEnd; otherwise, <c>false</c>.</value>
        bool InclusiveEnd { get; set; }

        /// <summary>
        /// Gets or sets when a <see cref="Views.IView"/> index is updated when running a Query.
        /// </summary>
        /// <value>The index update mode.</value>
        IndexUpdateMode IndexUpdateMode { get; set; }

        /// <summary>Changes the behavior of a query created by -queryAllDocuments.</summary>
        /// <remarks>
        /// Changes the behavior of a query created by -queryAllDocuments.
        /// - In mode kCBLAllDocs (the default), the query simply returns all non-deleted documents.
        /// - In mode kCBLIncludeDeleted, it also returns deleted documents.
        /// - In mode kCBLShowConflicts, the .conflictingRevisions property of each row will return the
        /// conflicting revisions, if any, of that document.
        /// - In mode kCBLOnlyConflicts, _only_ documents in conflict will be returned.
        /// (This mode is especially useful for use with a CBLLiveQuery, so you can be notified of
        /// conflicts as they happen, i.e. when they're pulled in by a replication.)
        /// </remarks>
        QueryAllDocsMode AllDocsMode { get; set; }

        /// <summary>
        /// Gets or sets the keys of the values to return. 
        /// A null value has no effect.
        /// </summary>
        /// <value>The keys of the values to return.</value>
        IEnumerable<Object> Keys { get; set; }

        /// <summary>
        /// Gets or sets whether to only use the map function without using the reduce function.
        /// </summary>
        /// <value><c>true</c> if map only; otherwise, <c>false</c>.</value>
        bool MapOnly { get; set; }

        /// <summary>
        /// Gets or sets whether results will be grouped in <see cref="Views.IView"/>s that have reduce functions.
        /// </summary>
        /// <value>The group level.</value>
        int GroupLevel { get; set; }

        /// <summary>
        /// Gets or sets whether to include the entire <see cref="Documents.IDocument"/> content with the results. 
        /// The <see cref="Documents.IDocument"/>s can be accessed via the <see cref="IQueryRow"/>'s 
        /// documentProperties property.
        /// </summary>
        /// <value><c>true</c> if prefetch; otherwise, <c>false</c>.</value>
        bool Prefetch { get; set; }

        /// <summary>
        /// Gets or sets whether the rows be returned in descending key order. 
        /// Default value is <c>false</c>.
        /// </summary>
        /// <value><c>true</c> if descending; otherwise, <c>false</c>.</value>
        bool Descending { get; set; }

        /// <summary>
        /// Gets or sets the key of the first value to return. 
        /// A null value has no effect.
        /// </summary>
        /// <value>The start key.</value>
        object StartKey { get; set; }

        /// <summary>
        /// Gets or sets the key of the last value to return. 
        /// A null value has no effect.
        /// </summary>
        /// <value>The end key.</value>
        object EndKey { get; set; }

        /// <summary>
        /// Gets or sets whether Queries created via the <see cref="IDatabase"/> createAllDocumentsQuery method 
        /// will include deleted <see cref="Documents.IDocument"/>s. 
        /// This property has no effect in other types of Queries.
        /// </summary>
        /// <value><c>true</c> if include deleted; otherwise, <c>false</c>.</value>
        [Obsolete("Use AllDocsMode.IncludeDeleted")]
        bool IncludeDeleted { get; set; }

        /// <summary>
        /// Gets or sets an optional predicate that filters the resulting query rows.
        /// If present, it's called on every row returned from the query, and if it returnsfalseNO
        /// the row is skipped.
        /// </summary>
        Func<IQueryRow, bool> PostFilter { get; set; }

        /// <summary>
        /// If nonzero, enables prefix matching of string or array keys.
        /// * A value of 1 treats the endKey itself as a prefix: if it's a string, keys in the index that
        ///   come after the endKey, but begin with the same prefix, will be matched. (For example, if the
        ///   endKey is "foo" then the key "foolish" in the index will be matched, but not "fong".) Or if
        ///   the endKey is an array, any array beginning with those elements will be matched. (For
        ///   example, if the endKey is [1], then [1, "x"] will match, but not [2].) If the key is any
        ///   other type, there is no effect.
        /// * A value of 2 assumes the endKey is an array and treats its final item as a prefix, using the
        ///   rules above. (For example, an endKey of [1, "x"] will match [1, "xtc"] but not [1, "y"].)
        /// * A value of 3 assumes the key is an array of arrays, etc.
        ///   Note that if the .descending property is also set, the search order is reversed and the above
        ///   discussion applies to the startKey, _not_ the endKey.
        /// </summary>
        int PrefixMatchLevel { get; set; }

        /// <summary>
        /// Gets or sets the number of initial rows to skip. Default value is 0.
        /// </summary>
        /// <value>
        /// The number of initial rows to skip. Default value is 0
        /// </value>
        int Skip { get; set; }

        /// <summary>
        /// Runs the <see cref="IQuery"/> and returns an enumerator over the result rows.
        /// </summary>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an issue occurs while executing the <see cref="IQuery"/>.
        /// </exception>
        IQueryEnumerator Run();

        /// <summary>
        /// Runs the Query asynchronously and 
        /// will notified <see cref="Completed"/> event handlers on completion.
        /// </summary>
        /// <returns>The async task.</returns>
        Task<IQueryEnumerator> RunAsync();

        /// <summary>
        /// Returns a new LiveQuery with identical properties to the the Query.
        /// </summary>
        /// <returns>The live query.</returns>
        ILiveQuery ToLiveQuery();
    }
}
