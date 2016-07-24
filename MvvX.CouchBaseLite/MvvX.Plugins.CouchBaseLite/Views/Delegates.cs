using System.Collections.Generic;

namespace MvvX.Plugins.CouchBaseLite.Views
{
    public class Delegates
    {
        public delegate void IEmitDelegate(object key, object value);

        public delegate void IMapDelegate(IDictionary<string, object> document, IEmitDelegate emitDelegate);

        public delegate object IReduceDelegate(IEnumerable<object> keys, IEnumerable<object> values, bool rereduce);

    }
}
