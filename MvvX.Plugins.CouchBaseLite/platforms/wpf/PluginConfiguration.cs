using MvvmCross.Plugin;

namespace MvvX.Plugins.CouchBaseLite
{
    public class PluginConfiguration : IMvxPluginConfiguration
    {
        /// <summary>
        /// Define the log folder for couchbase lite
        /// </summary>
        public string TextLogDirectoryPath { get; set; }
    }
}
