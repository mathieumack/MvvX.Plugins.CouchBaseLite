using MvvmCross.Plugin;

namespace MvvX.Plugins.CouchBaseLite
{
    public class PluginConfiguration : IMvxPluginConfiguration
    {
        /// <summary>
        /// Current context used to activate plugin on Xamarin.Android
        /// </summary>
        public Android.Content.Context Context { get; set; }

        /// <summary>
        /// Enable the log folder for couchbase lite
        /// </summary>
        public bool EnableTextLog { get; set; }
    }
}
