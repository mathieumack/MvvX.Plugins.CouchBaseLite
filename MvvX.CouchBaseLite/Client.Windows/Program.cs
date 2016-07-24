using Clients.Tests;
using MvvX.Plugins.CouchBaseLite.Platform;
using System;
using System.IO;
using System.Linq;

namespace Client.Windows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start connection.");

            var databaseName = "MvvXPluginTest";
            var databaseFolderPath = Path.Combine(Path.GetTempPath(), "testCouchDbDefault");

            // Connect to the database

            using (var service = new CouchBaseLite())
            {
                var databaseOptions = service.CreateDatabaseOptions();

                databaseOptions.Create = true;
                databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;

                var database = service.CreateConnection(databaseFolderPath, databaseName, databaseOptions);

                if (database != null)
                {
                    database.Changed += Database_Changed;
                    // 1. Create document :
                    Console.WriteLine("Add document ...");
                    var document = Documents.CreateDocument(database, "Test insert");
                    Console.WriteLine("Ok");
                    Console.ReadLine();

                    // 2. Update document
                    Console.WriteLine("Update document ...");
                    Documents.UpdateDocument(database, document.Id);
                    Console.WriteLine("Ok");
                    Console.ReadLine();

                    //// 3. Insert many documents
                    //Console.WriteLine("Insert multiple documents (Default)...");
                    //Documents.InsertManyDocuments(database, 10);
                    //Console.WriteLine("Ok");
                    //Console.ReadLine();

                    //// 4. Query
                    Console.WriteLine("Query database...");
                    Queries.CreateQuery(database);
                    Console.WriteLine("Ok");

                    //Console.ReadLine();
                    // 4. Delete database :
                    //database.Delete();
                }
            }

            // We compare with ForestDb inserts :
            //databaseFolderPath = Path.Combine(Path.GetTempPath(), "testCouchDbForestDb");
            //using (var service = new CouchBaseLite())
            //{
            //    var databaseOptions = service.CreateDatabaseOptions();

            //    databaseOptions.Create = true;
            //    databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.ForestDb;

            //    var database = service.CreateConnection(databaseFolderPath, databaseName, databaseOptions);

            //    if (database != null)
            //    {
            //        // 1. Create document :

            //        //Console.WriteLine("Add document ...");
            //        //var document = Documents.CreateDocument(database, "Test insert");
            //        //Console.WriteLine("Ok");
            //        //Console.ReadLine();

            //        //// 2. Update document
            //        //Console.WriteLine("Update document ...");
            //        //Documents.UpdateDocument(database, document.Id);
            //        //Console.WriteLine("Ok");
            //        //Console.ReadLine();

            //        // 3. Insert many documents
            //        Console.WriteLine("Insert multiple documents (ForestDb) ...");
            //        Documents.InsertManyDocuments(database, 5000);
            //        Console.WriteLine("Ok");
            //        Console.ReadLine();

            //        // 4. Delete database :
            //        //database.Delete();
            //    }
            //}

            // We compare with ForestDb inserts :
            //databaseFolderPath = Path.Combine(Path.GetTempPath(), "testCouchDbSQLCipher");
            //using (var service = new CouchBaseLite())
            //{
            //    var databaseOptions = service.CreateDatabaseOptions();

            //    databaseOptions.Create = true;
            //    databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;

            //    databaseOptions.SetSymmetricKey("TotoBonjourPa@ssword");

            //    var database = service.CreateConnection(databaseFolderPath, databaseName, databaseOptions);

            //    if (database != null)
            //    {
            //        // 1. Create document :

            //        //Console.WriteLine("Add document ...");
            //        //var document = Documents.CreateDocument(database, "Test insert");
            //        //Console.WriteLine("Ok");
            //        //Console.ReadLine();

            //        //// 2. Update document
            //        //Console.WriteLine("Update document ...");
            //        //Documents.UpdateDocument(database, document.Id);
            //        //Console.WriteLine("Ok");
            //        //Console.ReadLine();

            //        // 3. Insert many documents
            //        Console.WriteLine("Insert multiple documents (SQLCipher) ...");
            //        Documents.InsertManyDocuments(database, 5000);
            //        Console.WriteLine("Ok");
            //        Console.ReadLine();

            //        // 4. Delete database :
            //        //database.Delete();
            //    }
            //}

            Console.WriteLine("End.");
            Console.ReadLine();
        }

        private static void Database_Changed(object sender, MvvX.Plugins.CouchBaseLite.Database.IDatabaseChangeEventArgs e)
        {
            var changes = string.Empty;
            
            Console.WriteLine("Database changed. " + e.Changes.Count() + " change(s) on database " + e.Source.Name + ".");
            foreach (var change in e.Changes)
            {
                changes += change.ToString();
                Console.WriteLine("   Id : " + change.DocumentId + " - Rev : " + change.RevisionId);
            }
        }
    }
}
