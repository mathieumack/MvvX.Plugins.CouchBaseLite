using System;
using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Platform.Queries;
using MvvX.Plugins.CouchBaseLite.Queries;
using MvvX.Plugins.CouchBaseLite.Views;
using static MvvX.Plugins.CouchBaseLite.Views.Delegates;

namespace MvvX.Plugins.CouchBaseLite.Shared.Views
{
    public class PlatformView : IView
    {
        #region Fields

        private readonly Couchbase.Lite.View view;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformView(Couchbase.Lite.View view, IDatabase database)
        {
            this.view = view;
            this.database = database;
        }

        #endregion

        #region Implements

        public IDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        public string DocumentType
        {
            get
            {
                return this.view.DocumentType;
            }

            set
            {
                this.view.DocumentType = value;
            }
        }

        public bool IsStale
        {
            get
            {
                return this.view.IsStale;
            }
        }

        public long LastSequenceChangedAt
        {
            get
            {
                return this.view.LastSequenceChangedAt;
            }
        }

        public long LastSequenceIndexed
        {
            get
            {
                return this.view.LastSequenceIndexed;
            }
        }

        private IMapDelegate map;
        public IMapDelegate Map
        {
            get
            {
                return map;
            }
        }

        public string MapVersion
        {
            get
            {
                return this.view.MapVersion;
            }
        }

        public string Name
        {
            get
            {
                return this.view.Name;
            }
        }

        private IReduceDelegate reduce;
        public IReduceDelegate Reduce
        {
            get
            {
                return reduce;
            }
        }

        public int TotalRows
        {
            get
            {
                return this.view.TotalRows;
            }
        }

        public IQuery CreateQuery()
        {
            var query = this.view.CreateQuery();
            return new PlatformQuery(query, this.database);
        }

        public void Delete()
        {
            this.view.Delete();
        }

        public void DeleteIndex()
        {
            this.view.DeleteIndex();
        }

        public bool SetMap(IMapDelegate mapDelegate, string version)
        {
            this.map = mapDelegate;

            return this.view.SetMap((MapDelegate)((x, y) => {
                mapDelegate.Invoke(x, (t, z) =>
                {
                    y.Invoke(t, z);
                });
            }), version);
        }
        
        public bool SetMapReduce(IMapDelegate mapDelegate, IReduceDelegate reduceDelegate, string version)
        {
            this.map = mapDelegate;
            this.reduce = reduceDelegate;

            return this.view.SetMapReduce((MapDelegate)((x, y) => {
                mapDelegate.Invoke(x, (t, z) =>
                {
                    y.Invoke(t, z);
                });
            }),
                (ReduceDelegate)((x, y, z) => {
                return reduceDelegate.Invoke(x, y, z);
            }), version);
        }

        public void UpdateIndex()
        {
            this.view.UpdateIndex();
        }

        #endregion
    }
}
