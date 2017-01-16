using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Queries;
using static MvvX.Plugins.CouchBaseLite.Views.Delegates;

namespace MvvX.Plugins.CouchBaseLite.Views
{
    public interface IView
    {
        /// <summary>
        /// Get the <see cref="IDatabase"/> that owns the <see cref="IView"/>.
        /// </summary>
        IDatabase Database { get; }

        /// <summary>
        /// The document "type" property values this view is filtered to (null if none.)
        /// </summary>
        string DocumentType { get; set; }

        /// <summary>
        /// Gets if the <see cref="IView"/>'s indices are currently out of date.
        /// </summary>
        /// <value><c>true</c> if this instance is stale; otherwise, <c>false</c>.</value>
        bool IsStale { get; }

        /// <summary>
        /// Gets the last sequence that there was a change in the view
        /// </summary>
        long LastSequenceChangedAt { get; }

        /// <summary>
        /// Gets the last sequence number indexed so far.
        /// </summary>
        long LastSequenceIndexed { get; }

        /// <summary>
        /// The current map block. Never null.
        /// </summary>
        IMapDelegate Map { get; }

        /// <summary>
        /// The current map version string. If this changes, the storage's SetVersion() method will be
        /// called to notify it, so it can invalidate the index.
        /// </summary>
        string MapVersion { get; }

        /// <summary>
        /// Gets the <see cref="IView"/>'s name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The current reduce block, or null if there is none.
        /// </summary>
        IReduceDelegate Reduce { get; }

        /// <summary>
        /// Gets the total number of rows present in the view
        /// </summary>
        int TotalRows { get; }

        /// <summary>
        /// Creates a new <see cref="IQuery"/> for this view.
        /// </summary>
        /// <returns>A new <see cref="IQuery"/> for this view.</returns>
        IQuery CreateQuery();

        /// <summary>
        /// Deletes the <see cref="IView"/>.
        /// </summary>
        void Delete();

        /// <summary>
        /// Updates the <see cref="IView"/>'s persistent index.  Indexing
        /// scans all documents that have changed since the last time the index was updated.
        /// The body of each document is passed to the view's map callback, and any emitted
        /// rows are added to the index.  Any existing rows previously emitted by those documents,
        /// that weren't re-emitted this time, are removed.
        /// </summary>
        void UpdateIndex();

        /// <summary>
        /// Deletes the <see cref="IView"/>'s persistent index. 
        /// The index is regenerated on the next <see cref="IQuery"/> execution.
        /// </summary>
        void DeleteIndex();

        /// <summary>
        /// Defines the <see cref="IView"/>'s <see cref="IMapDelegate"/> and sets 
        /// its <see cref="IReduceDelegate"/> to null.
        /// </summary>
        /// <returns>
        /// True if the <see cref="IMapDelegate"/> was set, otherwise false. If the values provided are 
        /// identical to the values that are already set, then the values will not be updated and false will be returned.  
        /// In addition, if true is returned, the index was deleted and will be rebuilt on the next 
        /// <see cref="IQuery"/> execution.
        /// </returns>
        /// <param name="mapDelegate">The <see cref="IMapDelegate"/> to set</param>
        /// <param name="version">
        /// The key of the property value to return. The value of this parameter must change when 
        /// the <see cref="IMapDelegate"/> is changed in a way that will cause it to 
        /// produce different results.
        /// </param>
        bool SetMap(IMapDelegate mapDelegate, string version);

        /// <summary>
        /// Defines the View's <see cref="IMapDelegate"/> 
        /// and <see cref="IReduceDelegate"/>.
        /// </summary>
        /// <remarks>
        /// Defines a view's functions.
        /// The view's definition is given as a class that conforms to the Mapper or
        /// Reducer interface (or null to delete the view). The body of the block
        /// should call the 'emit' object (passed in as a paramter) for every key/value pair
        /// it wants to write to the view.
        /// Since the function itself is obviously not stored in the database (only a unique
        /// string idenfitying it), you must re-define the view on every launch of the app!
        /// If the database needs to rebuild the view but the function hasn't been defined yet,
        /// it will fail and the view will be empty, causing weird problems later on.
        /// It is very important that this block be a law-abiding map function! As in other
        /// languages, it must be a "pure" function, with no side effects, that always emits
        /// the same values given the same input document. That means that it should not access
        /// or change any external state; be careful, since callbacks make that so easy that you
        /// might do it inadvertently!  The callback may be called on any thread, or on
        /// multiple threads simultaneously. This won't be a problem if the code is "pure" as
        /// described above, since it will as a consequence also be thread-safe.
        /// </remarks>
        /// <returns>
        /// <c>true</c> if the <see cref="IMapDelegate"/> 
        /// and <see cref="IReduceDelegate"/> were set, otherwise <c>false</c>.
        /// If the values provided are identical to the values that are already set, 
        /// then the values will not be updated and <c>false</c> will be returned. 
        /// In addition, if <c>true</c> is returned, the index was deleted and 
        /// will be rebuilt on the next <see cref="IQuery"/> execution.
        /// </returns>
        /// <param name="map">The <see cref="IMapDelegate"/> to set.</param>
        /// <param name="reduce">The <see cref="IReduceDelegate"/> to set.</param>
        /// <param name="version">
        /// The key of the property value to return. The value of this parameter must change 
        /// when the <see cref="IMapDelegate"/> and/or <see cref="IReduceDelegate"/> 
        /// are changed in a way that will cause them to produce different results.
        /// </param>
        bool SetMapReduce(IMapDelegate map, IReduceDelegate reduce, string version);
    }
}
