using MvvmCross.Plugin;

namespace MvvX.Plugins.CouchBaseLite
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxConfigurablePlugin
    {
        private bool enableTextLog;

        public void Configure(IMvxPluginConfiguration configuration)
        {
            var config = configuration as PluginConfiguration;
            if (config != null)
                enableTextLog = config.EnableTextLog;
        }

        public void Load()
        {
            Couchbase.Lite.Support.iOS.Activate();
            if (enableTextLog)
                Couchbase.Lite.Support.iOS.EnableTextLogging();
        }
    }
}
