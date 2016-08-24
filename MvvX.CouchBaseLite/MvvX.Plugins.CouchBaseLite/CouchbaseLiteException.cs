using System;

namespace MvvX.Plugins.CouchBaseLite
{
    /// <summary>
    /// CouchBaseLiteException
    /// </summary>
    public class CouchbaseLiteException : Exception
    {
        // TODO : Add status
        //public Status CBLStatus { get; }

        /// <summary>
        /// Instanciate a new CouchbaseLiteException
        /// </summary>
        public CouchbaseLiteException()
            : base()
        { }

        /// <summary>
        /// Instanciate a new CouchbaseLiteException with a message
        /// </summary>
        /// <param name="message"></param>
        public CouchbaseLiteException(string message)
            : base(message)
        { }

        /// <summary>
        /// Instanciate a new CouchbaseLiteException with a message and a reference to the internal exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public CouchbaseLiteException(string message, Exception exception)
            : base(message)
        { }
    }
}
