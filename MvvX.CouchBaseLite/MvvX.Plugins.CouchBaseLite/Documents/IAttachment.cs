using System;
using System.Collections.Generic;
using System.IO;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface IAttachment : IDisposable
    {
        /// <summary>
        /// Contennt of the attachment
        /// </summary>
        IEnumerable<byte> Content { get; }

        /// <summary>
        /// Content stream
        /// </summary>
        Stream ContentStream { get; }

        /// <summary>
        /// Content Type
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Document of the attachment
        /// </summary>
        IDocument Document { get; }

        /// <summary>
        /// Lenght of the attachment
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Metadatas
        /// </summary>
        IDictionary<string, object> Metadata { get; }

        /// <summary>
        /// Name of the attachment
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Revision of the attachment
        /// </summary>
        IRevision Revision { get; }
    }
}
