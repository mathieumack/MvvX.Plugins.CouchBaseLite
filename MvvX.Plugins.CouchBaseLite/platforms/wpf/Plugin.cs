using MvvmCross.Plugin;
using System;

namespace MvvX.Plugins.CouchBaseLite
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxConfigurablePlugin
    {
        private string textLogDirectoryPath;

        public void Configure(IMvxPluginConfiguration configuration)
        {
            var config = configuration as PluginConfiguration;
            if (config == null)
                throw new ArgumentNullException(nameof(configuration), "The configuration can't be null.");
            if (!string.IsNullOrWhiteSpace(config.TextLogDirectoryPath))
                textLogDirectoryPath = config.TextLogDirectoryPath;
        }

        public void Load()
        {
            Couchbase.Lite.Support.NetDesktop.Activate();
            if (!string.IsNullOrWhiteSpace(textLogDirectoryPath))
                Couchbase.Lite.Support.NetDesktop.EnableTextLogging(textLogDirectoryPath);
        }
    }
}
