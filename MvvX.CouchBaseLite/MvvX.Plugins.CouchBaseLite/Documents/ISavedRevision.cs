using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface ISavedRevision : IRevision
    {
        /// <summary>
        /// Id of the revision
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Id of the parent
        /// </summary>
        string ParentId { get; }

        /// <summary>
        /// Properties of the revision
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Create a new unsaved revision
        /// </summary>
        /// <returns></returns>
        IUnsavedRevision CreateRevision();

        /// <summary>
        /// Create a new saved revision from a properties set
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        ISavedRevision CreateRevision(IDictionary<string, object> properties);

        /// <summary>
        /// Delete the document and returned the new saved revision
        /// </summary>
        /// <returns></returns>
        ISavedRevision DeleteDocument();
    }
}
