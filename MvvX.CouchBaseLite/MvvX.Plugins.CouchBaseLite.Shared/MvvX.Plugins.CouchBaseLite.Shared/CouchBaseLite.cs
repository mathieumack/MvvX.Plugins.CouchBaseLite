using Couchbase.Lite;
using System;
using System.IO;
using MvvX.Plugins.CouchBaseLite.Platform.Database;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvmCross.Platform;
using Couchbase.Lite.Store;
using MvvX.Plugins.CouchBaseLite.Store;
using MvvX.Plugins.CouchBaseLite.Shared.Database;

namespace MvvX.Plugins.CouchBaseLite.Platform
{
    public class CouchBaseLite : ICouchBaseLite
    {
        /// <summary>
        /// Folder path to the database
        /// </summary>
        private string workingFolderPath = null;

        private Manager manager = null;
        /// <summary>
        /// Manager (singleton)
        /// </summary>
        private Manager Manager
        { 
            get {
                if(manager == null)
                {
                    if (string.IsNullOrEmpty(workingFolderPath))
                        throw new InvalidOperationException("The method Initialize() has not been called");

                    var directoryInfo = new DirectoryInfo(workingFolderPath);
                    manager = new Manager(directoryInfo, Manager.DefaultOptions);
                }
                return manager;
            }
        }

        #region Constructor

        public CouchBaseLite()
        {
        }

        #endregion

        #region Implementations

        public IDatabaseOptions CreateDatabaseOptions()
        {
            return new PlatformDatabaseOptions();
        }

        public void Initialize(string workingFolderPath)
        {
            if(string.IsNullOrEmpty(workingFolderPath))
                throw new ArgumentNullException("workingFolderPath");
            if (this.workingFolderPath != null)
                throw new InvalidOperationException("Initialze method already called");

            this.workingFolderPath = workingFolderPath;
        }

        public IDatabase CreateConnection(string databaseName, IDatabaseOptions databaseOptions)
        {
            try
            {
                var options = new DatabaseOptions();

                options.Create = databaseOptions.Create;

                if (databaseOptions.StorageType == Storages.StorageTypes.ForestDb)
                {
                    options.StorageType = StorageEngineTypes.ForestDB;
                    Couchbase.Lite.Storage.ForestDB.Plugin.Register();
                }
                else
                    options.StorageType = StorageEngineTypes.SQLite;

                switch (databaseOptions.SymmetricKeyType)
                {
                    case SymmetricKeyType.Password:
                        options.EncryptionKey = new SymmetricKey(databaseOptions.Password);
                        break;
                    case SymmetricKeyType.KeyData:
                        options.EncryptionKey = new SymmetricKey(databaseOptions.KeyData);
                        break;
                    case SymmetricKeyType.PasswordWithSalt:
                        options.EncryptionKey = new SymmetricKey(databaseOptions.Password, databaseOptions.Salt, databaseOptions.Rounds);
                        break;
                    default:
                        break;
                }

                var database = Manager.OpenDatabase(databaseName.ToLower(), options);

                if (database != null)
                    return new PlatformDatabase(database);
                else
                    return null;
            }
            catch (Exception e)
            {
                Mvx.Trace("Error getting database : " + e.Message);
                throw;
            }
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
