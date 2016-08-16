using System;
using System.Collections;
using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public interface IQueryEnumerator : IEnumerator<IQueryRow>, IEnumerable<IQueryRow>, IDisposable, IEnumerator, IEnumerable
    {
        int Count { get; }

        long SequenceNumber { get; }

        [Obsolete("This property is heavy and will be replaced by IsStale()")]
        bool Stale { get; }

        bool IsStale();

        [Obsolete("Use LINQ ElementAt")]
        IQueryRow GetRow(int index);
    }
}
