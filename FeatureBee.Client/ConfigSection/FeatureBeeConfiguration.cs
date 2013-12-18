using System.Configuration;

namespace FeatureBee.ConfigSection
{
    internal class FeatureBeeConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("server", IsRequired = false)]
        public FeatureBeeServerConfig Server
        {
            get { return (FeatureBeeServerConfig)this["server"]; }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("tray", IsRequired = false)]
        public FeatureBeeTrayConfig Tray
        {
            get { return (FeatureBeeTrayConfig)this["tray"]; }
            set { this["tray"] = value; }
        }

        public static FeatureBeeConfiguration GetSection()
        {
            return ConfigurationManager.GetSection("featureBee") as FeatureBeeConfiguration;
        }
    }
}