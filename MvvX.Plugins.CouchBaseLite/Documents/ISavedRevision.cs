using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface ISavedRevision : IRevision
    {
        /// <summary>
        /// Creates a new <see cref="IUnsavedRevision"/> whose properties and attachments are initially identical to this one.
        /// </summary>
        /// <remarks>
        /// Creates a new mutable child revision whose properties and attachments are initially identical
        /// to this one's, which you can modify and then save.
        /// </remarks>
        /// <returns>
        /// A new child <see cref="IUnsavedRevision"/> whose properties and attachments 
        /// are initially identical to this one.
        /// </returns>
        IUnsavedRevision CreateRevision();

        /// <summary>
        /// Gets whether the <see cref="IRevision"/>'s properties are available. 
        /// Older, ancestor, <see cref="IRevision"/>s are not guaranteed to have their properties available.
        /// </summary>
        /// <value><c>true</c> if properties available; otherwise, <c>false</c>.</value>
        bool PropertiesAvailable { get; }

        /// <summary>
        /// Creates and saves a new <see cref="IRevision"/> with the specified properties. 
        /// To succeed the specified properties must include a '_rev' property whose value maches the current Revision's id.
        /// </summary>
        /// <returns>
        /// The new <see cref="ISavedRevision"/>.
        /// </returns>
        /// <param name="properties">
        /// The properties to set on the new Revision.
        /// </param>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an error occurs while creating or saving the new <see cref="ISavedRevision"/>.
        /// </exception>
        ISavedRevision CreateRevision(IDictionary<string, object> properties);

        /// <summary>
        /// Creates and saves a new deletion <see cref="IRevision"/> 
        /// for the associated <see cref="IDocument"/>.
        /// </summary>
        /// <returns>
        /// A new deletion Revision for the associated <see cref="IDocument"/>
        /// </returns>
        /// <exception cref="CouchbaseLiteException">
        /// Throws if an issue occurs while creating a new deletion <see cref="ISavedRevision"/>.
        /// </exception>
        ISavedRevision DeleteDocument();
    }
}
