using Couchbase.Lite.Auth;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Sync;
using System;
using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Shared.Sync
{
    public class PlatformReplication : IReplication
    {
        #region Fields

        private readonly Couchbase.Lite.Replication replication;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformReplication(Couchbase.Lite.Replication replication, IDatabase database)
        {
            if (replication == null)
                throw new ArgumentNullException("replication");
            if (database == null)
                throw new ArgumentNullException("database");

            this.replication = replication;
            this.database = database;
            this.replication.Changed += Replication_Changed;
        }

        #endregion

        #region Implementation

        public bool Continuous
        {
            get
            {
                return this.replication.Continuous;
            }
            set
            {
                this.replication.Continuous = value;
            }
        }

        public IEnumerable<string> DocIds
        {
            get
            {
                return this.replication.DocIds;
            }
            set
            {
                this.replication.DocIds = value;
            }
        }

        public IEnumerable<string> Channels
        {
            get
            {
                return this.replication.Channels;
            }
            set
            {
                this.replication.Channels = value;
            }
        }

        public IDictionary<string, object> FilterParams
        {
            get
            {
                return this.replication.FilterParams;
            }
            set
            {
                this.replication.FilterParams = value;
            }
        }

        public string Filter
        {
            get
            {
                return this.replication.Filter;
            }
            set
            {
                this.replication.Filter = value;
            }
        }
        
        public IDatabase LocalDatabase
        {
            get
            {
                return this.database;
            }
        }

        public bool IsPull
        {
            get
            {
                return this.replication.IsPull;
            }
        }
        
        public Uri RemoteUrl
        {
            get
            {
                return this.replication.RemoteUrl;
            }
        }
        
        public IDictionary<string, string> Headers
        {
            get
            {
                return this.replication.Headers;
            }
            set
            {
                this.replication.Headers = value;
            }
        }

        public bool CreateTarget
        {
            get
            {
                return this.replication.CreateTarget;
            }
            set
            {
                this.replication.CreateTarget = value;
            }
        }
        
        public ReplicationStatus Status
        {
            get
            {
                return (ReplicationStatus)(int)this.replication.Status;
            }
        }

        public int ChangesCount
        {
            get
            {
                return this.replication.ChangesCount;
            }
        }

        public Exception LastError
        {
            get
            {
                return this.replication.LastError;
            }
            set
            {
                this.replication.LastError = value;
            }
        }
        
        public int CompletedChangesCount
        {
            get
            {
                return this.replication.CompletedChangesCount;
            }
        }
        
        public IDictionary<string, object> ActiveTaskInfo
        {
            get
            {
                return this.replication.ActiveTaskInfo;
            }
        }
        
        public bool IsRunning
        {
            get
            {
                return this.replication.IsRunning;
            }
        }
        
        public string Username
        {
            get
            {
                return this.replication.Username;
            }
        }
        
        /// <summary>
        /// Change from the database
        /// </summary>
        public event EventHandler<IReplicationChangeEventArgs> Changed;

        private void Replication_Changed(object sender, Couchbase.Lite.ReplicationChangeEventArgs e)
        {
            if (Changed != null)
            {
                this.Changed(sender, new PlatformReplicationChangeEventArgs(e, this));
            }
        }

        public void Start()
        {
            this.replication.Start();
        }

        public bool ClearAuthenticationStores()
        {
            return this.replication.ClearAuthenticationStores();
        }

        public void DeleteCookie(string name)
        {
            this.replication.DeleteCookie(name);
        }

        public ICollection<string> GetPendingDocumentIDs()
        {
            return this.replication.GetPendingDocumentIDs();
        }

        public void Restart()
        {
            this.replication.Restart();
        }

        public void SetCookie(string name, string value, string path, DateTime expirationDate, bool secure, bool httpOnly)
        {
            this.replication.SetCookie(name, value, path, expirationDate, secure, httpOnly);
        }

        public void Stop()
        {
            this.replication.Stop();
        }

        public void SetBasicAuthenticator(string username, string password)
        {
            var auth = AuthenticatorFactory.CreateBasicAuthenticator(username, password);
            this.replication.Authenticator = auth;
        }

        public void SetDigestAuthenticator(string username, string password)
        {
            var auth = AuthenticatorFactory.CreateDigestAuthenticator(username, password);
            this.replication.Authenticator = auth;
        }

        public void SetFacebookAuthenticator(string token)
        {
            var auth = AuthenticatorFactory.CreateFacebookAuthenticator(token);
            this.replication.Authenticator = auth;
        }

        public void SetPersonaAuthenticator(string assertion, string email)
        {
            var auth = AuthenticatorFactory.CreatePersonaAuthenticator(assertion, email);
            this.replication.Authenticator = auth;
        }

        #endregion
    }
}
