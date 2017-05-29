using System;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface IDocumentChange
    {
        /// <summary>
        /// ID of the doucment
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        /// DOcument is in conflict
        /// </summary>
        bool IsConflict { get; }

        /// <summary>
        /// Change is linked to the current revision
        /// </summary>
        bool IsCurrentRevision { get; }

        /// <summary>
        /// Gets whether or not this change is a deletion
        /// </summary>
        bool IsDeletion { get; }

        /// <summary>
        /// ID of linked revision
        /// </summary>
        string RevisionId { get; }

        /// <summary>
        /// Source URI
        /// </summary>
        Uri SourceUrl { get; }
    }
}
