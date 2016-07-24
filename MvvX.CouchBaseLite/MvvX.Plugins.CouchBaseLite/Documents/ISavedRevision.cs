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
    }
}
