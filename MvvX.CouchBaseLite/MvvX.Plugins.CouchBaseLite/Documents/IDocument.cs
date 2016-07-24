using System;
using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface IDocument
    {
        /// <summary>
        /// Id of the document
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Id of the current revision
        /// </summary>
        string CurrentRevisionId { get; }

        /// <summary>
        /// Indicate if the document is deleted
        /// </summary>
        bool Deleted { get; }

        /// <summary>
        /// Get the current revision
        /// </summary>
        ISavedRevision CurrentRevision { get; }

        /// <summary>
        /// Properties of the document
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// User properties
        /// </summary>
        IDictionary<string, object> UserProperties { get; }

        /// <summary>
        /// Get all leaf revisions
        /// </summary>
        IEnumerable<ISavedRevision> LeafRevisions { get; }

        /// <summary>
        /// Get all revisions in history
        /// </summary>
        IEnumerable<ISavedRevision> RevisionHistory { get; }

        /// <summary>
        /// Get an object on a document
        /// </summary>
        /// <param name="key">Key of a property</param>
        /// <returns>Property, default(T) if not found</returns>
        T GetProperty<T>(string key) where T : class;

        /// <summary>
        /// Get an object on a document
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetProperty(string key);

        /// <summary>
        /// Delete the document
        /// </summary>
        void Delete();

        /// <summary>
        /// The purge method removes all trace of a document (and all its revisions and their attachments) from the local database.
        /// </summary>
        void Purge();

        /// <summary>
        /// Add properties to the document.
        /// It creates a new revision
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        ISavedRevision PutProperties(IDictionary<string, object> properties);

        /// <summary>
        /// Allow updating the document
        /// This create a new revision
        /// </summary>
        /// <param name="updateAction"></param>
        void Update(Func<IUnsavedRevision, bool> updateAction);
    }
}
