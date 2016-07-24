using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface IRevision
    {
        /// <summary>
        /// ???
        /// </summary>
        bool IsGone { get; }

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
        /// Get all revision history
        /// </summary>
        IEnumerable<ISavedRevision> RevisionHistory { get; }
    }
}
