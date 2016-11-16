using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvX.Plugins.CouchBaseLite.Platform;
using System.IO;
using Clients.Tests;
using MvvX.Plugins.CouchBaseLite.Views;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Queries;
using System.Linq;
using MvvX.Plugins.CouchBaseLite;
using MvvX.Plugins.CouchBaseLite.Managers;

namespace Client.Tests.UnitTest
{
    [TestClass]
    public class ViewTests
    {
        private string databaseName = "MvvXPluginTest";
        private string databaseFolderPath = Path.Combine(Path.GetTempPath(), "testCouchDbDefault");

        private ICouchBaseLite couchbaselite;
        private IDatabase database;

        private ICouchBaseLite couchbaselite2;
        private IDatabase database2;

        [TestInitialize()]
        public void Initialize()
        {
            if (Directory.Exists(databaseFolderPath))
                Directory.Delete(databaseFolderPath, true);

            couchbaselite = new CouchBaseLite();
            couchbaselite.Initialize(databaseFolderPath);

            var databaseOptions = couchbaselite.CreateDatabaseOptions();
            databaseOptions.Create = true;
            databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;
            database = couchbaselite.CreateConnection(databaseName, databaseOptions);
                
            couchbaselite2 = new CouchBaseLite();
            var databaseOptions2 = couchbaselite2.CreateDatabaseOptions();
            databaseOptions.Create = false;
            databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;
            database2 = couchbaselite2.CreateConnection(databaseName, databaseOptions2);

        }
        
        [TestCleanup()]
        public void Cleanup()
        {
            database.Dispose();
            database2.Dispose();
            couchbaselite.Dispose();
            couchbaselite2.Dispose();
        }


    }
}
