using System.Configuration;

namespace FeatureBee.Configuration
{
    public class FeatureBeeTrayConfig: ConfigurationElement
    {
        [ConfigurationProperty("showTrayIconOnPages", DefaultValue = false, IsRequired = false)]
        public bool ShowTrayIconOnPages
        {
            get { return (bool)this["showTrayIconOnPages"]; }
            set { this["showTrayIconOnPages"] = value; }
        }
    }
}