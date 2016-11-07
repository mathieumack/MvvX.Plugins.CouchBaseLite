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

            var databaseOptions = couchbaselite.CreateDatabaseOptions();
            databaseOptions.Create = true;
            databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;
            database = couchbaselite.CreateConnection(databaseFolderPath, databaseName, databaseOptions);
                
            couchbaselite2 = new CouchBaseLite();
            var databaseOptions2 = couchbaselite2.CreateDatabaseOptions();
            databaseOptions.Create = false;
            databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;
            database2 = couchbaselite2.CreateConnection(databaseFolderPath, databaseName, databaseOptions2);

        }
        
        [TestCleanup()]
        public void Cleanup()
        {
            database.Dispose();
            database2.Dispose();
            couchbaselite.Dispose();
            couchbaselite2.Dispose();
        }


        [TestMethod]
        public void IndexUpdates()
        {
            
            Documents.CreateDocument1(database);

            IView viewAge1 = QueryHelper.CreateAgeView(database, "view_age_1", "1");
            IQuery queryAge1 = viewAge1.CreateQuery();
            queryAge1.StartKey = 0;
            queryAge1.EndKey = 99;

            var enumqueryAge1 = queryAge1.Run();
            Assert.AreEqual(1, enumqueryAge1.Count, "Document should be found even if it has been created before the view");


            Documents.CreateDocument2(database);
            var enumqueryAge2 = queryAge1.Run();
            Assert.AreEqual(2, enumqueryAge2.Count, "2 Documents should be found");
            
            // Create document and try to get it from an other DB connection
            Documents.CreateDocument4(database);


            // Create the view in the other connection
            IView viewAgeDB2 = QueryHelper.CreateAgeView(database2, "view_age_1", "1");
            IQuery queryAgeDB2 = viewAgeDB2.CreateQuery();
            queryAgeDB2.StartKey = 0;
            queryAgeDB2.EndKey = 99;
            queryAgeDB2.IndexUpdateMode = MvvX.Plugins.CouchBaseLite.IndexUpdateMode.Never; // Update View's index if needed before starting the query

            var enumqueryDB2 = queryAgeDB2.Run();
            Assert.AreEqual(2, enumqueryDB2.Count, "3td Document should not be found because they are not yet index and IndexUpdateMode.Never is used");

            viewAgeDB2.UpdateIndex();

            var enumqueryDB2_2 = queryAge1.Run();
            Assert.AreEqual(3, enumqueryDB2_2.Count, "Documents should be found because we ran an UpdateIndex of the view");

            // Concurent insert + read test

            Documents.CreateDocument5(database); // Insert in DB2
            
            IQuery queryAgeDB2_3 = viewAgeDB2.CreateQuery();
            queryAgeDB2_3.StartKey = 0;
            queryAgeDB2_3.EndKey = 99;
            queryAgeDB2_3.IndexUpdateMode = IndexUpdateMode.Before;
            var enumqueryDB2_3 = queryAge1.Run();
            Assert.AreEqual(4, enumqueryDB2_3.Count, "5th document should be visible in DB2 even if it has been insert by the DB1 connection");


        }

        [TestMethod]
        public void QueryIntField()
        {

            IView viewAge = QueryHelper.CreateAgeView(database, "view_age", "1");

            Documents.CreateDocument1(database);
            Documents.CreateDocument2(database);
            Documents.CreateDocument3(database);
            Documents.CreateDocument4(database);


            // Tests viewAge (integer)
            {
                // No filter query
                {
                    IQuery queryAge1 = viewAge.CreateQuery();
                    var queryEnum1 = queryAge1.Run();
                    Assert.AreEqual(3, queryEnum1.Count, "Should return 3 rows because doc 3 doesn't have age field");
                }

                // Test Interval
                {
                    IQuery queryAge1 = viewAge.CreateQuery();
                    queryAge1.StartKey = 10;
                    queryAge1.EndKey = 15;

                    var queryEnum1 = queryAge1.Run();

                    Assert.AreEqual(2, queryEnum1.Count, "Should return 2 row");

                    queryEnum1.MoveNext();
                    Assert.AreEqual("1", queryEnum1.Current.DocumentId);

                    queryEnum1.MoveNext();
                    Assert.AreEqual("2", queryEnum1.Current.DocumentId);
                }

                // Test Exact match
                // Test Interval
                {
                    IQuery queryAge1 = viewAge.CreateQuery();
                    queryAge1.StartKey = 15;
                    queryAge1.EndKey = 15;

                    var queryEnum1 = queryAge1.Run();
                    Assert.AreEqual(1, queryEnum1.Count, "Should return 1 row");
                    queryEnum1.MoveNext();
                    Assert.AreEqual("2", queryEnum1.Current.DocumentId);


                    queryAge1.StartKey = 16;
                    queryAge1.EndKey = 16;
                    queryEnum1 = queryAge1.Run();

                    Assert.AreEqual(0, queryEnum1.Count, "Should return 0 row because no doc with age field = 16");
                }

            }
        }

        
        [TestMethod]
        public void QueryStringField()
        {

            IView viewCity = QueryHelper.CreateCityView(database, "view_city", "1", new PlatformNewtonsoftJsonSerializer());

            Documents.CreateDocument1(database);
            Documents.CreateDocument2(database);
            Documents.CreateDocument3(database);
            Documents.CreateDocument4(database);

            // Test Exact match
            {
                IQuery queryAge1 = viewCity.CreateQuery();
                queryAge1.StartKey = "Grenoble";
                queryAge1.EndKey = "Grenoble";

                var queryEnum1 = queryAge1.Run();
                Assert.AreEqual(2, queryEnum1.Count, "Should return 2 row");
                queryEnum1.MoveNext();
                Assert.IsTrue(queryEnum1.Current.DocumentId.Equals("1") || queryEnum1.Current.DocumentId.Equals("2"));
                queryEnum1.MoveNext();
                Assert.IsTrue(queryEnum1.Current.DocumentId.Equals("1") || queryEnum1.Current.DocumentId.Equals("2"));

                queryAge1 = viewCity.CreateQuery();
                queryAge1.StartKey = "Andernos";
                queryAge1.EndKey = "Andernos";

                queryEnum1 = queryAge1.Run();
                Assert.AreEqual(1, queryEnum1.Count, "Should return 1 row");
                queryEnum1.MoveNext();
                Assert.AreEqual("1", queryEnum1.Current.DocumentId);


                queryAge1 = viewCity.CreateQuery();
                queryAge1.StartKey = "Andernos";

            }

        }
    }
}
