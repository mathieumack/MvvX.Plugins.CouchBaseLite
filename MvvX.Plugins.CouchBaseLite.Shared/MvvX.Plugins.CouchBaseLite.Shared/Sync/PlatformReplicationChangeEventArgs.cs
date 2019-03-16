using System;
using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Sync;

namespace MvvX.Plugins.CouchBaseLite.Shared.Sync
{
    public class PlatformReplicationChangeEventArgs : IReplicationChangeEventArgs
    {
        #region Fields

        private readonly ReplicationChangeEventArgs replicationChangeEventArgs;
        private readonly IReplication replication;

        #endregion

        #region Constructor

        public PlatformReplicationChangeEventArgs(ReplicationChangeEventArgs replicationChangeEventArgs, IReplication replication)
        {
            this.replicationChangeEventArgs = replicationChangeEventArgs;
            this.replication = replication;
        }

        #region Implements

        public IReplication Source
        {
            get
            {
                return replication;
            }
        }

        public int ChangesCount
        {
            get
            {
                return replicationChangeEventArgs.ChangesCount;
            }
        }

        public int CompletedChangesCount
        {
            get
            {
                return replicationChangeEventArgs.CompletedChangesCount;
            }
        }

        public ReplicationStatus Status
        {
            get
            {
                return (ReplicationStatus)(int)replicationChangeEventArgs.Status;
            }
        }

        public Exception LastError
        {
            get
            {
                return replicationChangeEventArgs.LastError;
            }
        }

        public string Username
        {
            get
            {
                return replicationChangeEventArgs.Username;
            }
        }

        #endregion

        #endregion
    }
}
