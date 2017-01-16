using System;

namespace MvvX.Plugins.CouchBaseLite
{
    /// <summary>
    /// CouchBaseLiteException
    /// Occurs when an error is raised due to a conccurent access to a shared ressource
    /// </summary>
    public class CouchbaseLiteConcurrentException : CouchbaseLiteException
    {
        // TODO : Add status
        //public Status CBLStatus { get; }

        /// <summary>
        /// Instanciate a new CouchbaseLiteConcurrentException
        /// </summary>
        public CouchbaseLiteConcurrentException()
            : base()
        { }

        /// <summary>
        /// Instanciate a new CouchbaseLiteConcurrentException with a message
        /// </summary>
        /// <param name="message"></param>
        public CouchbaseLiteConcurrentException(string message)
            : base(message)
        { }

        /// <summary>
        /// Instanciate a new CouchbaseLiteConcurrentException with a message and a reference to the internal exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public CouchbaseLiteConcurrentException(string message, Exception exception)
            : base(message)
        { }
    }
}
