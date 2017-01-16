using System;
using System.Collections.Generic;
using System.Text;

namespace MvvX.Plugins.CouchBaseLite.Utils
{
    public static class ExceptionExtensions
    {
        public static CouchbaseLiteException GetCouchbaseLiteException(this Couchbase.Lite.CouchbaseLiteException ex)
        {
            CouchbaseLiteException res = null;

            if (!string.IsNullOrEmpty(ex.Message))
            {
                res = new CouchbaseLiteException(ex.Message, ex);
            }
            else if (ex.Message.Equals("Error beginning begin transaction"))
            {
                res = new CouchbaseLiteConcurrentException(ex.Message, ex);
            }
            else
            {
                res = new CouchbaseLiteException("An exception occured, see inner exception.", ex);
            }

            return res;
        }
    }
}
