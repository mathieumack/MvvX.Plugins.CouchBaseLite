using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public interface IQuery : IDisposable
    {
        QueryAllDocsMode AllDocsMode { get; set; }

        bool Descending { get; set; }

        object EndKey { get; set; }

        string EndKeyDocId { get; set; }

        int GroupLevel { get; set; }

        bool IncludeDeleted { get; set; }

        bool InclusiveEnd { get; set; }

        bool InclusiveStart { get; set; }

        IndexUpdateMode IndexUpdateMode { get; set; }

        IEnumerable<object> Keys { get; set; }

        int Limit { get; set; }

        bool MapOnly { get; set; }

        Func<IQueryRow, bool> PostFilter { get; set; }

        bool Prefetch { get; set; }

        int Skip { get; set; }

        object StartKey { get; set; }

        string StartKeyDocId { get; set; }

        IQueryEnumerator Run();

        Task<IQueryEnumerator> RunAsync();

        ILiveQuery ToLiveQuery();
    }
}
