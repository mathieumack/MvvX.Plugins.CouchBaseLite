using System;
using System.Collections.Generic;
using System.IO;

namespace MvvX.Plugins.CouchBaseLite.Documents
{
    public interface IUnsavedRevision : IRevision, IDisposable
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
        /// Delete an attchment from the current revision
        /// </summary>
        /// <param name="name">Name of the attachment</param>
        void RemoveAttachment(string name);

        /// <summary>
        /// Save the new revision
        /// </summary>
        /// <returns></returns>
        ISavedRevision Save();

        /// <summary>
        /// Save the new revision
        /// </summary>
        /// <param name="allowConflict"></param>
        /// <returns></returns>
        ISavedRevision Save(bool allowConflict);

        void SetAttachment(string name, string contentType, IEnumerable<byte> content);

        void SetAttachment(string name, string contentType, Stream content);

        void SetAttachment(string name, string contentType, Uri contentUrl);

        void SetProperties(IDictionary<string, object> newProperties);

        void SetUserProperties(IDictionary<string, object> userProperties);
    }
}
