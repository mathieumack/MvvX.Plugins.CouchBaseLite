using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Documents;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Clients.Tests
{
    public static class Documents
    {
        /// <summary>
        /// Create a new document in the database and populate some fields
        /// </summary>
        /// <param name="database"></param>
        public static IDocument CreateDocument(IDatabase database, string name)
        {
            var document = database.CreateDocument();
            if (document != null)
            {
                // New document created !
                //Debug.WriteLine("New document created : " + document.Id);

                // 2. Adding properties :
                var properties = new Dictionary<string, object>();
                properties["title"] = "Happy coding !";
                properties["name"] = name;
                properties["age"] = 10;
                properties["CustomContent"] = new
                {
                    Field1 = "Hello.",
                    SubField = new
                    {
                        Field1 = "He !",
                        Field2 = "What's your name ?"
                    }
                };
                document.PutProperties(properties);

                //Debug.WriteLine("Document created.");

                // Show document properties :
                //PrintDocumentProperties(document);

                return document;
            }

            return null;
        }


        /// <summary>
        /// Create a new document in the database and populate some fields
        /// </summary>
        /// <param name="database"></param>
        public static IDocument CreateDocument1(IDatabase database)
        {
            string id = "1";
            var document = database.GetDocument(id);

            var properties = new Dictionary<string, object>();
            properties["id"] = id;
            properties["name"] = "Alexandre";
            properties["age"] = 10;
            properties["city"] = new List<string> { "Andernos", "Grenoble" };
            properties["CustomContent"] = new
            {
                Field1 = "Hello1.",
                SubField = new
                {
                    Field1 = "He 1 !",
                    Field2 = "What's your name 1 ?"
                }
            };
            document.PutProperties(properties);
            return document;
        }


        /// <summary>
        /// Create a new document in the database and populate some fields
        /// </summary>
        /// <param name="database"></param>
        public static IDocument CreateDocument2(IDatabase database)
        {
            string id = "2";
            var document = database.GetDocument(id);

            var properties = new Dictionary<string, object>();
            properties["id"] = id;
            properties["age"] = 15;
            properties["name"] = "Vincent";
            properties["city"] = new List<string> { "Grenoble" };
            properties["CustomContent"] = new
            {
                Field1 = "Hello1.",
                SubField = new
                {
                    Field1 = "He 2 !",
                    Field2 = "What's your name 2 ?"
                }
            };
            document.PutProperties(properties);
            return document;
        }


        /// <summary>
        /// Create a new document in the database and populate some fields
        /// </summary>
        /// <param name="database"></param>
        public static IDocument CreateDocument3(IDatabase database)
        {
            string id = "3";
            var document = database.GetDocument(id);
            
            var properties = new Dictionary<string, object>();
            properties["id"] = id;
            properties["name"] = "Monder";
            properties["city"] = new List<string> { "Paris" };
            document.PutProperties(properties);
            return document;
        }



        /// <summary>
        /// Create a new document in the database and populate some fields
        /// </summary>
        /// <param name="database"></param>
        public static IDocument CreateDocument4(IDatabase database)
        {
            string id = "4";
            var document = database.GetDocument(id);

            var properties = new Dictionary<string, object>();
            properties["id"] = id;
            properties["age"] = 20;
            properties["name"] = "Qiane";
            properties["city"] = new List<string> { "NewYork" };
            document.PutProperties(properties);
            return document;
        }



        /// <summary>
        /// Create a new document in the database and populate some fields
        /// </summary>
        /// <param name="database"></param>
        public static IDocument CreateDocument5(IDatabase database)
        {
            string id = "5";
            var document = database.GetDocument(id);

            var properties = new Dictionary<string, object>();
            properties["id"] = id;
            properties["age"] = 19;
            properties["name"] = "Paula";
            properties["city"] = new List<string> { "Madrid" };
            document.PutProperties(properties);
            return document;
        }

        /// <summary>
        /// Get document from database and update some fields
        /// </summary>
        /// <param name="database"></param>
        /// <param name="id"></param>
        public static void UpdateDocument(IDatabase database, string id)
        {
            // Retrieve the document from the database :
            var document = database.GetDocument(id);
            if (document != null)
            {
                Debug.WriteLine("Get document from database :");

                // Show document properties :
                PrintDocumentProperties(document);

                // Update some properties :
                document.Update((IUnsavedRevision newRevision) =>
                {
                    var documentProperties = newRevision.Properties;

                    documentProperties["note"] = "Note 1 : Hello !. Note 2 : Hi !";
                    documentProperties["CustomContent"] = new
                    {
                        Field1 = "He !",
                        Field2 = "What's your name ?"
                    };
                    documentProperties.Remove("title");

                    return true;
                });

                Debug.WriteLine("Document updated.");

                // Show document properties :
                PrintDocumentProperties(document);
            }
        }

        /// <summary>
        /// Show properies of the document to the console
        /// </summary>
        /// <param name="document"></param>
        private static void PrintDocumentProperties(IDocument document)
        {
            Debug.WriteLine(string.Format("Show properties of the document {0}. {1} Properties :", document.Id, document.Properties.Count));

            foreach (var propertyKey in document.Properties.Keys)
            {
                Debug.WriteLine(string.Format("Property name        : {0}.", propertyKey));
                Debug.WriteLine(string.Format("Content (serialized) : {0}.", JsonConvert.SerializeObject(document.Properties[propertyKey])));
            }

            Debug.WriteLine(" ");
        }

        /// <summary>
        /// Insert many documents in the database
        /// </summary>
        /// <param name="database"></param>
        /// <param name="count"></param>
        public static void InsertManyDocuments(IDatabase database, int count)
        {
            Debug.WriteLine("Starting insert many documents.");
            Stopwatch watch = new Stopwatch();
            watch.Start();

            IList<int> ids = new List<int>();
            for (int i = 1; i < count; i++)
            {
                ids.Add(i);
            }
            string name = "Insert many documents";

            var documents = new ConcurrentBag<IDocument>();

            Parallel.ForEach(ids, (i) =>
            {
                // Create the document
                var document = CreateDocument(database, name + ". " + i);
                //Debug.WriteLine("Document " + document.Id.ToString() + " added.");
                documents.Add(document);
            });

            watch.Stop();
            Debug.WriteLine("End. Time to execute : " + watch.Elapsed.Minutes + "m " + watch.Elapsed.Seconds + "s " + watch.Elapsed.Milliseconds + "ms");
        }
    }
}
