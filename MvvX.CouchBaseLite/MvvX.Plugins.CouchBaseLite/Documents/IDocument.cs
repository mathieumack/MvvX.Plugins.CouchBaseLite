using System;
using System.Collections.Generic;
using System.IO;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    /// <summary>
    /// A Couchbase Lite Document.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Gets the <see cref="IDocument"/>'s id.
        /// </summary>
        /// <value>The <see cref="IDocument"/>'s id.</value>
        string Id { get; }

        /// <summary>
        /// Gets if the <see cref="IDocument"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        bool Deleted { get; }

        /// <summary>
        /// If known, gets the Id of the current <see cref="IRevision"/>, otherwise null.
        /// </summary>
        /// <value>The Id of the current <see cref="IRevision"/> if known, otherwise null.</value>
        string CurrentRevisionId { get; }

        /// <summary>
        /// Gets the current/latest <see cref="IRevision"/>.
        /// </summary>
        /// <value>The current/latest <see cref="IRevision"/>.</value>
        ISavedRevision CurrentRevision { get; }

        /// <summary>
        /// Gets the <see cref="IDocument"/>'s <see cref="IRevision"/> history 
        /// in forward order. Older, ancestor, <see cref="IRevision"/>s are not guaranteed to 
        /// have their properties available.
        /// </summary>
        /// <value>
        /// The <see cref="IDocument"/>'s <see cref="IRevision"/> history 
        /// in forward order.
        /// </value>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an issue occurs while getting the Revision history.
        /// </exception>
        IEnumerable<ISavedRevision> RevisionHistory { get; }

        /// <summary>
        /// Gets all of the current conflicting <see cref="IRevision"/>s for the 
        /// <see cref="IDocument"/>. If the <see cref="IDocument"/> is not in conflict, 
        /// only the single current <see cref="IRevision"/> will be returned.
        /// </summary>
        /// <value>
        /// All of the current conflicting <see cref="IRevision"/>s for the 
        /// <see cref="IDocument"/>.
        /// </value>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an issue occurs while getting the conflicting <see cref="IRevision"/>s.
        /// </exception>
        IEnumerable<ISavedRevision> ConflictingRevisions { get; }

        /// <summary>
        /// Gets all of the leaf <see cref="IRevision"/>s in the <see cref="IDocument"/>'s 
        /// <see cref="IRevision"/> tree.
        /// </summary>
        /// <value>
        /// All of the leaf <see cref="IRevision"/>s in the <see cref="IDocument"/>'s 
        /// <see cref="IRevision"/> tree.
        /// </value>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an issue occurs while getting the leaf <see cref="IRevision"/>s.
        /// </exception>
        IEnumerable<ISavedRevision> LeafRevisions { get; }

        /// <summary>
        /// Gets the properties of the current <see cref="IRevision"/> of 
        /// the <see cref="IDocument"/>.
        /// </summary>
        /// <remarks>
        /// The contents of the current revision of the document.
        /// This is shorthand for self.currentRevision.properties.
        /// Any keys in the dictionary that begin with "_", such as "_id" and "_rev", 
        /// contain CouchbaseLite metadata.
        /// </remarks>
        /// <value>
        /// The properties of the current <see cref="IRevision"/> of 
        /// the <see cref="IDocument"/>
        /// </value>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Gets the properties of the current <see cref="IRevision"/> of the 
        /// <see cref="IDocument"/> without any properties 
        /// with keys prefixed with '_' (which contain Couchbase Lite data).
        /// </summary>
        /// <remarks>
        /// The user-defined properties, without the ones reserved by CouchDB.
        /// This is based on -properties, with every key whose name starts with "_" removed.
        /// </remarks>
        /// <value>
        /// The properties of the current <see cref="IRevision"/> of the 
        /// <see cref="IDocument"/> without any properties 
        /// with keys prefixed with '_'.
        /// </value>
        IDictionary<string, object> UserProperties { get; }

        /// <summary>
        /// Deletes the <see cref="IDocument"/>.
        /// </summary>
        /// <remarks>
        /// Deletes the <see cref="IDocument"/> by adding a deletion <see cref="IRevision"/>.
        /// </remarks>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an issue occurs while deleting the <see cref="IDocument"/>.
        /// </exception>
        void Delete();

        /// <summary>
        /// Completely purges the <see cref="IDocument"/> from the local <see cref="Database.IDatabase"/>. 
        /// This is different from delete in that it completely deletes everything related to the 
        /// <see cref="IDocument"/> and does not replicate the deletes to other <see cref="Database.IDatabase"/>s.
        /// </summary>
        /// <remarks>
        /// Purges this document from the database; this is more than deletion, it forgets entirely about it.
        /// The purge will NOT be replicated to other databases.
        /// </remarks>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an issue occurs while purging the <see cref="IDocument"/>.
        /// </exception>
        void Purge();

        /// <summary>
        /// Returns the <see cref="IRevision"/> with the specified id if it exists, otherwise null.
        /// </summary>
        /// <param name="id">The <see cref="IRevision"/> id.</param>
        /// <returns>The <see cref="IRevision"/> with the specified id if it exists, otherwise null</returns>
        ISavedRevision GetRevision(string id);

        /// <summary>
        /// Creates a new <see cref="IUnsavedRevision"/> whose properties and attachments are initially 
        /// identical to the current <see cref="IRevision"/>.
        /// </summary>
        /// <remarks>
        /// Creates an unsaved new revision whose parent is the currentRevision,
        /// or which will be the first revision if the document doesn't exist yet.
        /// You can modify this revision's properties and attachments, then save it.
        /// No change is made to the database until/unless you save the new revision.
        /// </remarks>
        /// <returns>
        /// A new <see cref="IUnsavedRevision"/> whose properties and attachments are initially 
        /// identical to the current <see cref="IRevision"/>
        /// </returns>
        IUnsavedRevision CreateRevision();

        /// <summary>
        /// Returns the value of the property with the specified key.
        /// </summary>
        /// <returns>The value of the property with the specified key.</returns>
        /// <param name="key">The key of the property value to return.</param>
        object GetProperty(string key);

        /// <summary>
        /// Returns the TValue of the property with the specified key.
        /// </summary>
        /// <returns>The value of the property with the specified key as TValue.</returns>
        /// <param name="key">The key of the property value to return.</param>
        TValue GetProperty<TValue>(string key);

        /// <summary>
        /// Creates and saves a new <see cref="IRevision"/> with the specified properties. 
        /// To succeed the specified properties must include a '_rev' property whose value maches 
        /// the current <see cref="IRevision"/>'s id.
        /// </summary>
        /// <remarks>
        /// Saves a new revision. The properties dictionary must have a "_rev" property
        /// whose ID matches the current revision's (as it will if it's a modified
        /// copy of this document's .properties property.)
        /// </remarks>
        /// <param name="properties">The properties to set on the new Revision.</param>
        /// <returns>The new <see cref="ISavedRevision"/></returns>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an error occurs while creating or saving the new <see cref="IRevision"/>.
        /// </exception>
        ISavedRevision PutProperties(IDictionary<string, object> properties);

        /// <summary>
        /// Creates and saves a new <see cref="IRevision"/> by allowing the caller to update 
        /// the existing properties. Conflicts are handled by calling the delegate again.
        /// </summary>
        /// <remarks>
        /// Saves a new revision by letting the caller update the existing properties.
        /// This method handles conflicts by retrying (calling the block again).
        /// The DocumentUpdater implementation should modify the properties of the new revision and return YES to save or
        /// NO to cancel. Be careful: the DocumentUpdater can be called multiple times if there is a conflict!
        /// </remarks>
        /// <param name="updateAction">
        /// The delegate that will be called to update the new <see cref="IRevision"/>'s properties.
        /// return YES, or just return NO to cancel.
        /// </param>
        /// <returns>The new <see cref="ISavedRevision"/>, or null on error or cancellation.</returns>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an error occurs while creating or saving the new <see cref="IRevision"/>.
        /// </exception>
        void Update(Func<IUnsavedRevision, bool> updateAction);

        /// <summary>
        /// Gets the expiration data of this document, if it has one
        /// </summary>
        /// <returns>The expiration data of this document, if it has one</returns>
        DateTime? GetExpirationDate();

        /// <summary>
        /// Sets an interval to wait before expiring the document.
        /// </summary>
        /// <param name="timeInterval">The time to wait before expiring the document and
        /// making it eligible for auto-purging.</param>
        void ExpireAfter(TimeSpan timeInterval);

        /// <summary> 
        /// Sets an absolute point in time for the document to expire.  Must be 
        /// a DateTime in the future.  Pass a null value to cancel an expiration. 
        /// </summary> 
        /// <param name="expireTime">The time at which the document expires, and is 
        /// eligible to be auto-purged</param> 
        /// <exception cref="System.InvalidOperationException">The expireTime is not in the future</exception> 
        void ExpireAt(DateTime? expireTime);

        // TODO : Add this method in next release
        //event EventHandler<DocumentChangeEventArgs> Change;
                
        /// <summary>
        /// Adds an existing revision copied from another database.  Unlike a normal insertion, this does
        /// not assign a new revision ID; instead the revision's ID must be given.  Ths revision's history
        /// (ancestry) must be given, which can put it anywhere in the revision tree.  It's not an error if
        /// the revision already exists locally; it will just be ignored.
        /// 
        /// This is not an operation that clients normall perform; it's used by the replicator.  You might want
        /// to use it if you're pre-loading a database with canned content, or if you're implementing some new
        /// kind of replicator that transfers revisions from another database
        /// </summary>
        /// <param name="properties">The properties of the revision (_id and _rev will be ignored but _deleted
        /// and _attachments are recognized)</param>
        /// <param name="attachments">A dictionary providing attachment bodies.  The keys are the attachment
        /// names (matching the keys in the properties `_attachments` dictionary) and the values are streams
        /// that contain the attachment bodies.</param>
        /// <param name="revisionHistory">The revision history in the form of an array of revision-ID strings, in
        /// reverse chronological order.  The first item must be the new revision's ID.  Following items are its
        /// parent's ID, etc.</param>
        /// <param name="sourceUri">The URL of the database this revision came from, if any.  (This value
        /// shows up in the Database Changed event triggered by this insertion, and can help clients decide
        /// whether the change is local or not)</param>
        /// <returns><c>true</c> on success, false otherwise</returns>
        bool PutExistingRevision(IDictionary<string, object> properties, IDictionary<string, Stream> attachments, IList<string> revisionHistory, Uri sourceUri);
    }
}