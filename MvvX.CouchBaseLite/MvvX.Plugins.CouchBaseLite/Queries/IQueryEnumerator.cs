using System;
using System.Collections;
using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Queries
{
    public interface IQueryEnumerator : IEnumerator<IQueryRow>, IEnumerable<IQueryRow>, IDisposable, IEnumerator, IEnumerable
    {
        int Count { get; }

        long SequenceNumber { get; }

        bool Stale { get; }
    }
}
