using System.Web;
using FeatureBee.Client;

namespace FeatureBee.Sample.Web
{
    public static class FeatureBeeConfig
    {
        public static void Initialize(HttpApplication app)
        {
            FeatureBeeBuilder
                .Init(app)
                .UsingEvaluatorsFromAssembly()
                .FeaturesPullFrom("http://localhost:57189/")
                .Build();
        }
    }
}