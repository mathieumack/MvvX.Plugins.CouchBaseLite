using System;

namespace MvvX.Plugins.CouchBaseLite.Sync
{
    public interface IReplicationChangeEventArgs
    {
        IReplication Source { get; }

        //
        // Summary:
        //     Gets the number of changes scheduled for the replication at the time the event
        //     was created.
        int ChangesCount { get; }

        //
        // Summary:
        //     Gets the number of changes completed by the replication at the time the event
        //     was created.
        int CompletedChangesCount { get; }

        //
        // Summary:
        //     Gets the status of the replication at the time the event was created
        ReplicationStatus Status { get; }
        
        //
        // Summary:
        //     Gets the most recent error that occured at the time of this change
        Exception LastError { get; }

        //
        // Summary:
        //     The current username assigned to the replication
        string Username { get; }
    }
}
