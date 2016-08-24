using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Queries;
using static MvvX.Plugins.CouchBaseLite.Views.Delegates;

namespace MvvX.Plugins.CouchBaseLite.Views
{
    public interface IView
    {
        IDatabase Database { get; }

        string DocumentType { get; set; }

        bool IsStale { get; }

        long LastSequenceChangedAt { get; }

        long LastSequenceIndexed { get; }

        IMapDelegate Map { get; }

        string MapVersion { get; }

        string Name { get; }

        IReduceDelegate Reduce { get; }

        int TotalRows { get; }

        IQuery CreateQuery();

        void Delete();

        void DeleteIndex();

        bool SetMap(IMapDelegate mapDelegate, string version);

        bool SetMapReduce(IMapDelegate map, IReduceDelegate reduce, string version);
    }
}
