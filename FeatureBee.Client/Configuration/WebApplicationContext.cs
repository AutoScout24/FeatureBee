using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.Evaluators;

namespace FeatureBee.Configuration
{
    internal class WebApplicationContext : IFeatureBeeContext
    {
        private readonly HttpContextBase _httpContext;

        public List<IConditionEvaluator> Evaluators { get; private set; }
        public IFeatureRepository FeatureRepository { get; private set; }

        public bool IsDebugMode
        {
            get { return _httpContext.IsDebuggingEnabled; }
        }

        public List<string> GodModeFeatures
        {
            get
            {
                var value = "";

                if (_httpContext.Request.Cookies != null)
                {
                    var cookie = _httpContext.Request.Cookies["FeatureBee"];
                    value = cookie == null ? "" : HttpUtility.UrlDecode(cookie.Value);
                }
                return value.Split('#').ToList();
            }
        }

        public WebApplicationContext(HttpContextBase httpContext, List<IConditionEvaluator> evaluators, IFeatureRepository featureRepository)
        {
            _httpContext = httpContext;
            Evaluators = evaluators;
            FeatureRepository = featureRepository;
        }
    }
}