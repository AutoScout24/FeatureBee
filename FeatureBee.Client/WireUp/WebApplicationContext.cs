using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.Evaluators;

namespace FeatureBee.WireUp
{
    internal class WebApplicationContext : IFeatureBeeContext
    {
        private readonly Func<HttpContextBase> _httpContextFunc;

        public WebApplicationContext(Func<HttpContextBase> httpContextFunc)
        {
            _httpContextFunc = httpContextFunc;
        }

        public List<IConditionEvaluator> Evaluators { get; set; }
        public IFeatureRepository FeatureRepository { get; set; }
        public bool ShowTrayIconOnPages { get; set; }

        public bool IsDebugMode
        {
            get { return _httpContextFunc.Invoke().IsDebuggingEnabled; }
        }

        public List<string> GodModeFeatures
        {
            get
            {
                var request = _httpContextFunc.Invoke().Request;

                if (request.Cookies == null) 
                { 
                    return new List<string>();
                }

                var cookie = request.Cookies["FeatureBee"];
                var value = cookie == null ? "" : HttpUtility.UrlDecode(cookie.Value) ?? "";

                return value.Split('#').ToList();
            }
        }
    }
}