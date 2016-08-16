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
        private string workingFolderPath;

        /// <summary>
        /// Name of the database
        /// </summary>
        private string databaseName;

        /// <summary>
        /// Manager
        /// </summary>
        private Manager manager;

        #region Constructor

        public CouchBaseLite()
        {
            this.manager = null;
        }

        #endregion

        #region Implementations

        public IDatabaseOptions CreateDatabaseOptions()
        {
            return new PlatformDatabaseOptions();
        }

        public IDatabase CreateConnection(string workingFolderPath, string databaseName, IDatabaseOptions databaseOptions)
        {
            this.workingFolderPath = workingFolderPath;
            this.databaseName = databaseName;

            try
            {
                var directoryInfo = new DirectoryInfo(workingFolderPath);
                this.manager = new Manager(directoryInfo, Manager.DefaultOptions);

                var options = new DatabaseOptions();

                options.Create = databaseOptions.Create;

                if (databaseOptions.StorageType == Storages.StorageTypes.ForestDb)
                    options.StorageType = StorageEngineTypes.ForestDB;
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

                var database = manager.OpenDatabase(databaseName.ToLower(), options);
                return new PlatformDatabase(database);
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
