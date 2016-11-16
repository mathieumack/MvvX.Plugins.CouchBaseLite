using MvvX.Plugins.CouchBaseLite.Database;
using System;

namespace MvvX.Plugins.CouchBaseLite
{
    /// <summary>
    /// Interface of the CouchBaseLite plugin
    /// </summary>
    public interface ICouchBaseLite : IDisposable
    {
        /// <summary>
        /// Initialize the ICouchBaseLite instance 
        /// </summary>
        /// <param name="workingFolderPath">Folder on the disk where saving the database</param>
        void Initialize(string workingFolderPath);

        /// <summary>
        /// Generate a databaseoptions object to be used in CreateConnection method
        /// </summary>
        /// <returns></returns>
        IDatabaseOptions CreateDatabaseOptions();

        /// <summary>
        /// Returns the Couchbase.Lite.Database with the given name. 
        /// If the Couchbase.Lite.Database does not already exist, it is created
        /// Caution:For compatibility reasons, database names cannot contain uppercase letters! The only legal characters are lowercase ASCII letters, digits, and the special characters _$()+-/ 
        /// </summary>
        /// <param name="databaseName">Name of the database</param>
        /// <param name="databaseOptions">Options for the database manager. Use CreateDatabaseOptions() to generate object</param>
        /// <exception cref="CouchbaseLiteException">Thrown if an issue occurs while gettings or createing the Couchbase.Lite.Database.</exception>
        IDatabase CreateConnection(string databaseName, IDatabaseOptions databaseOptions);
    }
}
