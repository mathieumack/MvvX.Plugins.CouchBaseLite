using System;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public interface IQueryCompletedEventArgs
    {
        /// <summary>
        /// The result rows from the Query.
        /// </summary>
        /// <value>he result rows.</value>
        IQueryEnumerator Rows { get; }

        /// <summary>
        /// The error, if any, that occured during the execution of the Query
        /// </summary>
        /// <value>The error info if any.</value>
        Exception ErrorInfo { get; }
    }
}