using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.Evaluators;

namespace FeatureBee.WireUp
{
    internal class WebApplicationContext : IFeatureBeeContext
    {
        private readonly HttpContextBase _httpContext;

        public List<IConditionEvaluator> Evaluators { get; set; }
        public IFeatureRepository FeatureRepository { get; set; }

        public bool IsDebugMode
        {
            get { return _httpContext.IsDebuggingEnabled; }
        }

        public bool ShowTrayIconOnPages { get; set; }

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
            set
            {
                
            }
        }

        public WebApplicationContext(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }
    }
}