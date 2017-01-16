using System;
using System.Collections.Generic;
using System.IO;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    /// <summary>
    /// A Couchbase Lite Document Attachment.
    /// </summary>
    public interface IAttachment : IDisposable
    {
        /// <summary>
        /// Gets the owning <see cref="IRevision"/>.
        /// </summary>
        /// <value>the owning <see cref="IRevision"/>.</value>
        IRevision Revision { get; }

        /// <summary>
        /// Name of the attachment
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the owning <see cref="IDocument"/>.
        /// </summary>
        /// <value>The owning <see cref="IDocument"/></value>
        /// <exception cref="CouchbaseLiteException"></exception>
        IDocument Document { get; }

        /// <summary>
        /// Gets the content-type.
        /// </summary>
        /// <value>The content-type.</value>
        string ContentType { get; }


        /// <summary>
        /// Get the <see cref="IAttachment"/> content stream.  The caller must not
        /// dispose it.
        /// </summary>
        /// <value>The <see cref="IAttachment"/> content stream.</value>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an error occurs when getting the content stream.
        /// </exception>
        Stream ContentStream { get; }
        
        /// <summary>Gets the <see cref="IAttachment"/> content.</summary>
        /// <value>The <see cref="IAttachment"/> content</value>
        /// <exception cref="CouchbaseLiteException">
        /// Thrown if an error occurs when getting the content.
        /// </exception>
        IEnumerable<byte> Content { get; }

        /// <summary>
        /// Gets the length in bytes of the content.
        /// </summary>
        /// <value>The length in bytes of the content.</value>
        long Length { get; }

        /// <summary>
        /// The CouchbaseLite metadata about the attachment, that lives in the document.
        /// </summary>
        IDictionary<string, object> Metadata { get; }
    }
}
