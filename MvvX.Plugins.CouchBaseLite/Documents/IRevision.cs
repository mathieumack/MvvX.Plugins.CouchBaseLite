using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface IRevision
    {
        /// <summary>
        /// Gets the <see cref="IRevision"/>'s id.
        /// </summary>
        /// <value>The <see cref="IRevision"/>'s id.</value>
        string Id { get; }

        /// <summary>
        /// Gets if the <see cref="IRevision"/> marks the deletion of its <see cref="IDocument"/>.
        /// </summary>
        /// <value><c>true</c> if the <see cref="IRevision"/> marks the deletion; otherwise, <c>false</c>.</value>
        bool IsDeletion { get; }

        /// <summary>
        /// Does this revision mark the deletion or removal (from available channels) of its document?
        /// (In other words, does it have a "_deleted_ or "_removed" property?)
        /// </summary>
        bool IsGone { get; }

        /// <summary>Gets the properties of the <see cref="IRevision"/>.</summary>
        /// <remarks>
        /// The contents of this revision of the document.
        /// Any keys in the dictionary that begin with "_", such as "_id" and "_rev", contain CouchbaseLite metadata.
        /// </remarks>
        /// <value>The properties of the <see cref="IRevision"/>.</value>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Gets the properties of the <see cref="IRevision"/>. 
        /// without any properties with keys prefixed with '_' (which contain Couchbase Lite data).
        /// </summary>
        /// <value>The properties of the <see cref="IRevision"/>.</value>
        IDictionary<string, object> UserProperties { get; }

        /// <summary>
        /// List of attachement in the revision
        /// </summary>
        IEnumerable<string> AttachmentNames { get; }

        /// <summary>
        /// Get attachments of the revision
        /// </summary>
        IEnumerable<IAttachment> Attachments { get; }

        /// <summary>
        /// Get an attachment by his name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Attachment, null if not found</returns>
        IAttachment GetAttachment(string name);

        /// <summary>
        /// Gets the parent <see cref="IRevision"/>'s id.
        /// </summary>
        /// <value>The parent <see cref="IRevision"/>'s id.</value>
        string ParentId { get; }

        /// <summary>Returns the history of this document as an array of CBLRevisions, in forward order.</summary>
        /// <remarks>
        /// Returns the history of this document as an array of CBLRevisions, in forward order.
        /// Older revisions are NOT guaranteed to have their properties available.
        /// </remarks>
        /// <value>The history of this document as an array of CBLRevisions, in forward order</value>
        IEnumerable<ISavedRevision> RevisionHistory { get; }

        /// <summary>
        /// Returns the value of the property with the specified key.
        /// </summary>
        /// <returns>The value of the property with the specified key.</returns>
        /// <param name="key">The key of the property value to return.</param>
        object GetProperty(string key);
    }
}
