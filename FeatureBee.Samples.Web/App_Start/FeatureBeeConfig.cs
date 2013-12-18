using System.Web;
using FeatureBee.WireUp;

namespace FeatureBee.Samples.Web
{
    public static class FeatureBeeConfig
    {
        public static void InitFor(HttpApplication app)
        {
            FeatureBeeBuilder.ForWebApp().UseConfig();
        }
    }
}