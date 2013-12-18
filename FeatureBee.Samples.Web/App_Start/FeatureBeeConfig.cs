using System.Web;
using FeatureBee.Configuration;

namespace FeatureBee.Samples.Web
{
    public static class FeatureBeeConfig
    {
        public static void InitFor(HttpApplication app)
        {
            FeatureBeeBuilder.Init(app).BuildFromConfig();
        }
    }
}