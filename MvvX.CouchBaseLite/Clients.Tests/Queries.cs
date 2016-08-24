using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Queries;
using System.Diagnostics;
using System.Linq;

namespace Clients.Tests
{
    public static class Queries
    {
        /// <summary>
        /// Create a new document in the database and populate some fields
        /// </summary>
        /// <param name="database"></param>
        public static void CreateQuery(IDatabase database)
        {
            using (var query = database.CreateAllDocumentsQuery())
            {
                query.AllDocsMode = QueryAllDocsMode.AllDocs;
                query.Limit = 5;
                query.Completed += Query_Completed;
                query.PostFilter = (row) =>
                {
                    return row.DocumentProperties.Values.Any(e => e.ToString().Contains("Note 1"));
                };

                var result = query.Run();

                Debug.WriteLine("Result search : " + result.Count + " item(s).");
                var i = 1;
                foreach (var resultItem in result)
                {
                    Debug.WriteLine("Item " + i + ". Document id : " + resultItem.DocumentId);
                    i++;
                }
            }
        }

        private static void Query_Completed(object sender, IQueryCompletedEventArgs e)
        {
            Debug.WriteLine("Query completed");
        }
    }
}
