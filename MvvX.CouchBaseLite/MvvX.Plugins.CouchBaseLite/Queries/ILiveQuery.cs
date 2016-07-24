using System;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public interface ILiveQuery : IQuery
    {
        Exception LastError { get; }

        IQueryEnumerator Rows { get; }

        TimeSpan UpdateInterval { get; set; }

        //public event EventHandler<QueryChangeEventArgs> Changed;

        void QueryOptionsChanged();
        
        void Start();

        void Stop();

        void WaitForRows();
    }
}
