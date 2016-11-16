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
using System.Collections.Generic;
using MvvX.Plugins.CouchBaseLite.Documents;
using MvvX.Plugins.CouchBaseLite.Storages;

namespace Client.Tests.UnitTest
{
    public class CBLTests
    {
        protected string databaseName = "MvvXPluginTest";

        protected ICouchBaseLite iCouchBaseLite;
        protected IDatabase db1;
        protected IDatabase db2;

        protected static string dBFolderPath = null;
        protected static StorageTypes storage;

        #region Construct & Dispose

        public virtual void Initialize()
        {
            if (Directory.Exists(dBFolderPath))
                Directory.Delete(dBFolderPath, true);

            iCouchBaseLite = new CouchBaseLite();
            iCouchBaseLite.Initialize(dBFolderPath);
            var databaseOptions = iCouchBaseLite.CreateDatabaseOptions();
            databaseOptions.Create = true;
            databaseOptions.StorageType = storage;

            db1 = iCouchBaseLite.CreateConnection(databaseName, databaseOptions);
            db2 = iCouchBaseLite.CreateConnection(databaseName, databaseOptions);
        }


        public virtual void Cleanup()
        {
            db1.Dispose();
            db2.Dispose();
            iCouchBaseLite.Dispose();

        }

        #endregion


        #region Tests Methods

        public virtual void CRUD()
        {
            // Test 2 concurent insert of the same doc Id
            InsertDoc(db1, "1", "User1");
            IDocument doc1DB1_1 = db1.GetDocument("1");


            Exception exRes = null;
            try
            {
                InsertDoc(db2, "1", "User1_T2");
            }
            catch (Exception ex)
            {
                exRes = ex;
            }

            // Get doc in both repo

            IDocument doc1DB1 = db1.GetDocument("1");
            IDocument doc1DB2 = db2.GetDocument("1");
            IDocument doc1DB2_2ndCall = db2.GetDocument("1");
            //Assert.IsTrue(doc1DB2 == doc1DB2_2ndCall, "Object references are equals because the Manager return always the same object references for a same id");

            Assert.IsFalse(doc1DB1 == doc1DB2, "Object references not equals because they come from two differents Manager");

            // Update doc in repo1
            UpdateDoc(db1, "1", "User1Renamed");
            IDocument doc1DB1_updated = db1.GetDocument("1"); // rev-2
            IDocument doc1DB2_updated = db2.GetDocument("1"); // rev-1
            
            //Assert.AreEqual("User1Renamed", doc1DB2_updated.GetProperty("Name"), "Update of the name in one repo should be visible in others repo");

            // Update doc in repo2
            UpdateDoc(db2, "1", "User1Renamedx2");


            IDocument doc1DB1_update2 = db1.GetDocument("1"); // rev-2
            IDocument doc1DB2_update2 = db2.GetDocument("1"); // rev-3

            Assert.AreEqual("User1Renamedx2", doc1DB1_update2.GetProperty("Name"), "Update of the name in one repo should be visible in others repo");

        }
        
        public virtual void ViewIndexUpdates()
        {

            Documents.CreateDocument1(db1);

            IView viewAge1 = QueryHelper.CreateAgeView(db1, "view_age_1", "1");
            IQuery queryAge1 = viewAge1.CreateQuery();
            queryAge1.StartKey = 0;
            queryAge1.EndKey = 99;

            var enumqueryAge1 = queryAge1.Run();
            Assert.AreEqual(1, enumqueryAge1.Count, "Document should be found even if it has been created before the view");


            Documents.CreateDocument2(db1);
            var enumqueryAge2 = queryAge1.Run();
            Assert.AreEqual(2, enumqueryAge2.Count, "2 Documents should be found");

            // Create document and try to get it from an other DB connection
            Documents.CreateDocument4(db1);


            // Create the view in the other connection
            IView viewAgeDB2 = QueryHelper.CreateAgeView(db2, "view_age_1", "1");
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

            Documents.CreateDocument5(db1); // Insert in DB2

            IQuery queryAgeDB2_3 = viewAgeDB2.CreateQuery();
            queryAgeDB2_3.StartKey = 0;
            queryAgeDB2_3.EndKey = 99;
            queryAgeDB2_3.IndexUpdateMode = IndexUpdateMode.Before;
            var enumqueryDB2_3 = queryAge1.Run();
            Assert.AreEqual(4, enumqueryDB2_3.Count, "5th document should be visible in DB2 even if it has been insert by the DB1 connection");


        }

        public virtual void VieweQueryIntField()
        {
            var serializer = new PlatformNewtonsoftJsonSerializer();

            IView viewAge = QueryHelper.CreateAgeView(db1, "view_age", "1");

            Documents.CreateDocument1(db1);
            Documents.CreateDocument2(db1);
            Documents.CreateDocument3(db1);
            Documents.CreateDocument4(db1);


            // Tests viewAge (integer)
            {
                // No filter query
                using (IQuery queryAge1 = viewAge.CreateQuery())
                {
                    using (var queryEnum1 = queryAge1.Run())
                    {
                        queryAge1.Prefetch = false;
                        Assert.AreEqual(3, queryEnum1.Count, "Should return 3 rows because doc 3 doesn't have age field");

                        queryEnum1.MoveNext();
                        IQueryRow row = queryEnum1.Current;
                    }
                }

                // Test Interval
                using (IQuery queryAge1 = viewAge.CreateQuery())
                {
                    queryAge1.StartKey = 10;
                    queryAge1.EndKey = 15;

                    using (var queryEnum1 = queryAge1.Run())
                    {
                        Assert.AreEqual(2, queryEnum1.Count, "Should return 2 row");

                        queryEnum1.MoveNext();
                        Assert.AreEqual("1", queryEnum1.Current.DocumentId);

                        queryEnum1.MoveNext();
                        Assert.AreEqual("2", queryEnum1.Current.DocumentId);
                    }
                }


                // Test Exact match
                // Test Interval
                using (IQuery queryAge1 = viewAge.CreateQuery())
                {
                    queryAge1.StartKey = 15;
                    queryAge1.EndKey = 15;

                    using (var queryEnum1 = queryAge1.Run())
                    {
                        Assert.AreEqual(1, queryEnum1.Count, "Should return 1 row");
                        queryEnum1.MoveNext();
                        Assert.AreEqual("2", queryEnum1.Current.DocumentId);
                    }

                    queryAge1.StartKey = 16;
                    queryAge1.EndKey = 16;

                    using (var queryEnum1 = queryAge1.Run())
                    {
                        Assert.AreEqual(0, queryEnum1.Count, "Should return 0 row because no doc with age field = 16");
                    }
                }


            }
        }

        public virtual void ViewQueryStringField()
        {

            IView viewCity = QueryHelper.CreateCityView(db1, "view_city", "1", new PlatformNewtonsoftJsonSerializer());

            Documents.CreateDocument1(db1);
            Documents.CreateDocument2(db1);
            Documents.CreateDocument3(db1);
            Documents.CreateDocument4(db1);

            // Test Exact match
            using (IQuery queryAge1 = viewCity.CreateQuery())
            {
                queryAge1.StartKey = "Grenoble";
                queryAge1.EndKey = "Grenoble";

                using (var queryEnum1 = queryAge1.Run())
                {

                    Assert.AreEqual(2, queryEnum1.Count, "Should return 2 row");
                    queryEnum1.MoveNext();
                    Assert.IsTrue(queryEnum1.Current.DocumentId.Equals("1") || queryEnum1.Current.DocumentId.Equals("2"));
                    queryEnum1.MoveNext();
                    Assert.IsTrue(queryEnum1.Current.DocumentId.Equals("1") || queryEnum1.Current.DocumentId.Equals("2"));

                }
            }

            using (var queryAge1 = viewCity.CreateQuery())
            {
                queryAge1.StartKey = "Andernos";
                queryAge1.EndKey = "Andernos";

                using (var queryEnum1 = queryAge1.Run())
                {
                    Assert.AreEqual(1, queryEnum1.Count, "Should return 1 row");
                    queryEnum1.MoveNext();
                    Assert.AreEqual("1", queryEnum1.Current.DocumentId);
                }
            }
        }


        #endregion 

        #region Utils methods

        private void InsertDoc(IDatabase database, string id, string name)
        {
            var doc = database.GetDocument(id);
            if (doc.CurrentRevisionId != null) throw new Exception("alreadyexists");

            IDictionary<string, object> prop1 = new Dictionary<string, object> {
                { "Id", id},
                { "Name", name},
                { "_rev", doc.CurrentRevisionId}
            };
            var res = doc.PutProperties(prop1);

        }

        private void UpdateDoc(IDatabase database, string id, string newName)
        {
            var doc = database.GetDocument(id);
            doc.Update((IUnsavedRevision newRevision) =>
            {
                var properties = newRevision.Properties;
                properties["Name"] = newName;
                return true;
            });

        }

        #endregion



    }
}
