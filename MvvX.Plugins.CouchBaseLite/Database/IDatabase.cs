using MvvX.Plugins.CouchBaseLite.Documents;
using MvvX.Plugins.CouchBaseLite.Queries;
using MvvX.Plugins.CouchBaseLite.Views;
using System;
using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Database
{
    public interface IDatabase : IDisposable
    {
        /// <summary>
        /// Name of the databse
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get document in the document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDocument GetDocument(string id);

        /// <summary>
        /// Get the number of documents
        /// </summary>
        /// <returns></returns>
        int GetDocumentCount();

        /// <summary>
        /// Change from the database
        /// </summary>
        event EventHandler<IDatabaseChangeEventArgs> Changed;

        /// <summary>
        /// Permanently deletes a database's file and all its attachments
        /// </summary>
        void Delete();

        /// <summary>
        /// Delete local document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteLocalDocument(string id);

        IDocument GetExistingDocument(string id);

        IDictionary<string, object> GetExistingLocalDocument(string id);

        long GetLastSequenceNumber();

        int GetMaxRevTreeDepth();

        /// <summary>
        /// Get total size of the database
        /// </summary>
        /// <returns></returns>
        long GetTotalDataSize();

        bool PutLocalDocument(string id, IDictionary<string, object> properties);

        void SetMaxRevTreeDepth(int value);

        void StorageExitedTransaction(bool committed);

        /// <summary>
        /// Compact the database
        /// </summary>
        /// <returns></returns>
        bool Compact();

        /// <summary>
        /// Create a query on all documents
        /// </summary>
        /// <returns></returns>
        IQuery CreateAllDocumentsQuery();

        /// <summary>
        /// Create a new document in the database
        /// </summary>
        /// <returns></returns>
        IDocument CreateDocument();

        IView GetExistingView(string name);

        IView GetView(string name);
    }
}
