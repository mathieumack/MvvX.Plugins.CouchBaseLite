using MvvmCross.Plugin;

namespace MvvX.Plugins.CouchBaseLite
{
    public class PluginConfiguration : IMvxPluginConfiguration
    {
        /// <summary>
        /// Enable the log folder for couchbase lite
        /// </summary>
        public bool EnableTextLog { get; set; }
    }
}
