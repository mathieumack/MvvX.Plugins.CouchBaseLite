using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;
using System.Collections.Generic;
using System.IO;

namespace MvvX.Plugins.CouchBaseLite.Platform.Documents
{
    public class PlatformAttachment : IAttachment
    {
        private readonly Attachment attachment;
        
        public IEnumerable<byte> Content
        {
            get
            {
                try
                { 
                return attachment.Content;
                }
                catch (Couchbase.Lite.CouchbaseLiteException ex)
                {
                    throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
                }
            }
        }

        public Stream ContentStream
        {
            get
            {
                try
                { 
                return attachment.ContentStream;
                }
                catch (Couchbase.Lite.CouchbaseLiteException ex)
                {
                    throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
                }
            }
        }

        public string ContentType
        {
            get
            {
                return attachment.ContentType;
            }
        }

        public IDocument Document
        {
            get
            {
                try
                { 
                return new PlatformDocument(attachment.Document);
                }
                catch (Couchbase.Lite.CouchbaseLiteException ex)
                {
                    throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
                }
            }
        }

        public long Length
        {
            get
            {
                return attachment.Length;
            }
        }

        public IDictionary<string, object> Metadata
        {
            get
            {
                return attachment.Metadata;
            }
        }

        public string Name
        {
            get
            {
                return attachment.Name;
            }
        }

        public IRevision Revision
        {
            get
            {
                return new PlatformRevision(attachment.Revision);
            }
        }

        public PlatformAttachment(Attachment attachment)
        {
            this.attachment = attachment;
        }

        public void Dispose()
        {
            attachment.Dispose();
        }
    }
}
