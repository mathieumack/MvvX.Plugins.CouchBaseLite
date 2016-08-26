using System;
using System.Collections.Generic;
using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Documents;

namespace MvvX.Plugins.CouchBaseLite.Platform.Documents
{
    public class PlatformSavedRevision : PlatformRevision, ISavedRevision
    {
        #region Fields

        private readonly SavedRevision savedRevision;

        #endregion

        #region Constructor

        public PlatformSavedRevision(SavedRevision savedRevision)
            : base(savedRevision)
        {
            this.savedRevision = savedRevision;
        }

        public bool PropertiesAvailable
        {
            get
            {
                return this.savedRevision.PropertiesAvailable;
            }
        }

        #endregion


        #region Interface

        public IUnsavedRevision CreateRevision()
        {
            var unsavedRevision = savedRevision.CreateRevision();
            return new PlatformUnsavedRevision(unsavedRevision);
        }

        public ISavedRevision CreateRevision(IDictionary<string, object> properties)
        {
            try
            {
                var retSavedRevision = savedRevision.CreateRevision(properties);
                return new PlatformSavedRevision(retSavedRevision);
            }
            catch (Couchbase.Lite.CouchbaseLiteException ex)
            {
                throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }
        }

        public ISavedRevision DeleteDocument()
        {
            try
            {
                var retSavedRevision = savedRevision.DeleteDocument();
                return new PlatformSavedRevision(retSavedRevision);
            }
            catch (Couchbase.Lite.CouchbaseLiteException ex)
            {
                throw new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }
        }


        #endregion
    }
}