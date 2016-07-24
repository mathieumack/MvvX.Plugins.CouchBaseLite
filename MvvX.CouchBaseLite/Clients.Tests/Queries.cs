using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var query = database.CreateAllDocumentsQuery();

            query.AllDocsMode = QueryAllDocsMode.AllDocs;
            query.Limit = 5;
            query.PostFilter = (row) =>
            {
                return row.DocumentProperties.Values.Any(e => e.ToString().Contains("Note 1"));
            };

            var result = query.Run();

            Debug.WriteLine("Result search : " + result.Count + " item(s).");
            var i = 1;
            foreach(var resultItem in result)
            {
                Debug.WriteLine("Item " + i + ". Document id : " + resultItem.DocumentId);
                i++;
            }
        }

    }
}
