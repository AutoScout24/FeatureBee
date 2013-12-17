using System.Web;
using FeatureBee.Configuration;

namespace FeatureBee.Samples.Web
{
    public class FeatureBeeConfig
    {
        public static void InitFor(HttpApplication app)
        {
            FeatureBeeBuilder
                .Init(app)
                .UsingEvaluatorsFromAssembly()
                .FeaturesPullFrom("http://localhost:")
                .Build();
        }
    }
}