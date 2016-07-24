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
        
        public string Id
        {
            get
            {
                return savedRevision.Id;
            }
        }

        public string ParentId
        {
            get
            {
                return savedRevision.ParentId;
            }
        }

        public IDictionary<string, object> Properties
        {
            get
            {
                return savedRevision.Properties;
            }
        }

        #endregion

        #region Constructor

        public PlatformSavedRevision(SavedRevision savedRevision)
            : base(savedRevision)
        {
            this.savedRevision = savedRevision;
        }

        #endregion
    }
}
