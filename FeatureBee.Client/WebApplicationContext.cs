using System;
using System.Collections.Generic;
using System.Web;
using FeatureBee.Client.Evaluators;

namespace FeatureBee.Client
{
    internal class WebApplicationContext : IFeatureBeeContext
    {
        public HttpContext HttpContext { get; private set; }
        public List<IConditionEvaluator> Evaluators { get; private set; }
        public IFeatureRepository FeatureRepository { get; private set; }

        public WebApplicationContext(HttpContext httpContext, List<IConditionEvaluator> evaluators, IFeatureRepository featureRepository)
        {
            HttpContext = httpContext;
            Evaluators = evaluators;
            FeatureRepository = featureRepository;
        }
    }

    internal class WindowsApplicationContext : IFeatureBeeContext
    {
        public WindowsApplicationContext(List<IConditionEvaluator> evaluators, IFeatureRepository featureRepository)
        {
            Evaluators = evaluators;
            FeatureRepository = featureRepository;
        }

        public List<IConditionEvaluator> Evaluators { get; private set; }
        public IFeatureRepository FeatureRepository { get; private set; }
    }
}