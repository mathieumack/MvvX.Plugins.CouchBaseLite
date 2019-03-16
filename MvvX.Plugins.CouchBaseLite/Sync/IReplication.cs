using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Documents;
using System;
using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Sync
{
    /// <summary>
    /// A Couchbase Lite pull or push <see cref="IReplication"/>
    /// between a local and a remote <see cref="IDatabase"/>.
    /// </summary>
    public interface IReplication
    {
        /// <summary>
        /// Adds or Removed a <see cref="IDatabase"/> change delegate
        /// that will be called whenever the <see cref="IReplication"/>
        /// changes.
        /// </summary>
        event EventHandler<IReplicationChangeEventArgs> Changed;

        /// <summary>
        /// Gets or sets the ids of the <see cref="IDocument"/>s to replicate.
        /// </summary>
        /// <value>The ids of the <see cref="IDocument"/>s to replicate.</value>
        IEnumerable<string> DocIds { get; set; }

        /// <summary>
        /// Gets or sets the list of Sync Gateway channel names to filter by for pull <see cref="IReplication"/>.
        /// </summary>
        /// <remarks>
        /// Gets or sets the list of Sync Gateway channel names to filter by for pull <see cref="IReplication"/>.
        /// A null value means no filtering, and all available channels will be replicated.
        /// Only valid for pull replications whose source database is on a Couchbase Sync Gateway server.
        /// This is a convenience property that just sets the values of filter and filterParams.
        /// </remarks>
        IEnumerable<string> Channels { get; set; }

        /// <summary>
        /// Gets or sets the parameters to pass to the filter function.
        /// </summary>
        /// <value>The parameters to pass to the filter function.</value>
        IDictionary<string, object> FilterParams { get; set; }

        /// <summary>
        /// Gets or sets the name of an optional filter function to run on the source
        /// <see cref="IDatabase"/>. Only documents for which the function
        /// returns true are replicated.
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// Gets or sets whether the <see cref="IReplication"/> operates continuously,
        /// replicating changes as the source <see cref="IDatabase"/> is modified.
        /// </summary>
        bool Continuous { get; set; }

        /// <summary>
        /// Gets the local <see cref="IDatabase"/> being replicated to/from.
        /// </summary>
        IDatabase LocalDatabase { get; }

        /// <summary>
        /// Gets whether the <see cref="IReplication"/> pulls from,
        /// as opposed to pushes to, the target.
        /// </summary>
        bool IsPull { get; }

        /// <summary>
        /// Gets the remote URL being replicated to/from.
        /// </summary>
        Uri RemoteUrl { get; }

        /// <summary>
        /// Gets or sets the extra HTTP headers to send in <see cref="IReplication"/>
        /// requests to the remote <see cref="IDatabase"/>.
        /// </summary>
        /// <value>
        /// the extra HTTP headers to send in <see cref="IReplication"/> requests
        /// to the remote <see cref="IDatabase"/>.
        /// </value>
        IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets whether the target <see cref="IDatabase"/> should be created
        /// if it doesn't already exist. This only has an effect if the target supports it.
        /// </summary>
        bool CreateTarget { get; set; }

        /// <summary>
        /// Gets the <see cref="IReplication"/>'s current status.
        /// </summary>
        /// <value>The <see cref="IReplication"/>'s current status.</value>
        ReplicationStatus Status { get; }

        /// <summary>
        /// If the <see cref="IReplication"/> is active, gets the number of changes to be processed, otherwise 0.
        /// </summary>
        /// <value>The changes count.</value>
        int ChangesCount { get; }

        /// <summary>
        /// Gets the last error, if any, that occurred since the <see cref="IReplication"/> was started.
        /// </summary>
        Exception LastError { get; set; }

        /// <summary>
        /// If the <see cref="IReplication"/> is active, gets the number of completed changes that have been processed, otherwise 0.
        /// </summary>
        /// <value>The completed changes count.</value>
        int CompletedChangesCount { get; }

        /// <summary>
        /// Gets the active task info for thie replication
        /// </summary>
        IDictionary<string, object> ActiveTaskInfo { get; }

        //ReplicationOptions ReplicationOptions { get; set; }

        /// <summary>
        /// Gets whether the <see cref="IReplication"/> is running.
        /// Continuous <see cref="IReplication"/>s never actually stop,
        /// instead they go idle waiting for new data to appear.
        /// </summary>
        /// <value>
        /// <c>true</c> if <see cref="IReplication"/> is running; otherwise, <c>false</c>.
        /// </value>
        bool IsRunning { get; }

        /// <summary>
        /// If applicable, will store the username of the logged in user once
        /// they are authenticated
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Deletes any persistent credentials (passwords, auth tokens...) associated with this 
        /// replication's Authenticator. Also removes session cookies from the cookie store. 
        /// </summary>
        /// <returns><c>true</c> on success, <c>false</c> otherwise</returns>
        bool ClearAuthenticationStores();

        /// <summary>
        /// Deletes a cookie specified by name
        /// </summary>
        /// <param name="name">The name of the cookie</param>
        void DeleteCookie(string name);

        /// <summary>
        /// Gets a collection of document IDs that have been scheduled for replication
        /// but not yet completed.
        /// </summary>
        /// <returns>The pending document IDs.</returns>
        ICollection<string> GetPendingDocumentIDs();

        /// <summary>
        /// Restarts the <see cref="IReplication"/>.
        /// </summary>
        void Restart();

        /// <summary>Sets an HTTP cookie for the Replication.</summary>
        /// <param name="name">The name of the cookie.</param>
        /// <param name="value">The value of the cookie.</param>
        /// <param name="path">The path attribute of the cookie.  If null or empty, will use remote.getPath()
        ///     </param>
        /// <param name="expirationDate">The expiration date of the cookie.</param>
        /// <param name="secure">Whether the cookie should only be sent using a secure protocol (e.g. HTTPS).
        ///     </param>
        /// <param name="httpOnly">(ignored) Whether the cookie should only be used when transmitting HTTP, or HTTPS, requests thus restricting access from other, non-HTTP APIs.
        ///     </param>
        void SetCookie(string name, string value, string path, DateTime expirationDate, bool secure, bool httpOnly);

        /// <summary>
        /// Starts the <see cref="IReplication"/>.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the <see cref="IReplication"/>.
        /// </summary>
        void Stop();

        /// <summary>
        /// Sets an object for handling HTTP Basic authentication
        /// </summary>
        /// <returns>The authenticator</returns>
        /// <param name="username">The username to use</param>
        /// <param name="password">The password to use</param>
        void SetBasicAuthenticator(string username, string password);

        /// <summary>
        /// Sets an object for handling HTTP Digest authentication (experimental)
        /// </summary>
        /// <param name="username">The username to use</param>
        /// <param name="password">The password to use</param>
        void SetDigestAuthenticator(string username, string password);

        /// <summary>
        /// Sets an object for handling Facebook authentication
        /// </summary>
        /// <param name="token">The facebook auth token</param>
        void SetFacebookAuthenticator(string token);

        //void SetOpenIDAuthenticator(Manager manager, OIDCCallback callback);

        /// <summary>
        /// Sets an object for handling Persona authentication
        /// </summary>
        /// <param name="assertion">The assertion object created by Persona</param>
        /// <param name="email">The email used in the assertion</param>
        void SetPersonaAuthenticator(string assertion, string email);
    }
}
