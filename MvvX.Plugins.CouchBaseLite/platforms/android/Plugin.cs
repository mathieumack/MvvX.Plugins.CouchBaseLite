using MvvmCross.Plugin;
using System;

namespace MvvX.Plugins.CouchBaseLite
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxConfigurablePlugin
    {
        private Android.Content.Context context;
        private bool enableTextLog;

        public void Configure(IMvxPluginConfiguration configuration)
        {
            var config = configuration as PluginConfiguration;
            if (config == null)
                throw new ArgumentNullException(nameof(configuration), "The configuration must not be null.");
            if (config.Context == null)
                throw new ArgumentNullException(nameof(configuration), "The configuration context must not be null.");

            context = config.Context;
            enableTextLog = config.EnableTextLog;
        }

        public void Load()
        {
            Couchbase.Lite.Support.Droid.Activate(context);
            if (enableTextLog)
                Couchbase.Lite.Support.Droid.EnableTextLogging();
        }
    }
}
