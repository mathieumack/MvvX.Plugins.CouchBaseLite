using MvvX.Plugins.CouchBaseLite.Documents;
using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Database
{
    public interface IDatabaseChangeEventArgs
    {
        /// <summary>
        /// List of changes in the database
        /// </summary>
        IEnumerable<IDocumentChange> Changes { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsExternal { get; }

        /// <summary>
        /// Get source of changes
        /// </summary>
        IDatabase Source { get; }
    }
}
