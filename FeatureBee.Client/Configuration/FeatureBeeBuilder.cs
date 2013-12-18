using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.Evaluators;

namespace FeatureBee.Configuration
{
    public static class FeatureBeeBuilderEvaluators
    {
        public static FeatureBeeBuilder UsingEvaluators(this FeatureBeeBuilder config, List<IConditionEvaluator> evaluators)
        {
            config.Evaluators = evaluators;
            return config;
        }

        public static FeatureBeeBuilder UsingEvaluatorsFromAssembly(this FeatureBeeBuilder config)
        {
            var types = typeof(FeatureBeeBuilder).Assembly.GetTypes().Where(TypeIsConditionEvaluator).ToList();
            var evaluators = types.Select(_ =>
            {
                var constructor = _.GetConstructor(Type.EmptyTypes);
                return constructor != null ? (IConditionEvaluator)constructor.Invoke(null) : null;
            }).ToList();

            config.Evaluators = evaluators;
            return config;
        }

        private static bool TypeIsConditionEvaluator(Type type)
        {
            return type.GetInterface(typeof(IConditionEvaluator).Name) != null && !type.IsAbstract;
        }
    }

    public static class FeatureBeeBuilderFeatures
    {
        public static FeatureBeeBuilder FeaturesPullFrom(this FeatureBeeBuilder config, string featureBeeUri)
        {
            config.FeatureRepository = new PullFeatureRepository(featureBeeUri);
            return config;
        }

        public static FeatureBeeBuilder FeaturesProvidedBy(this FeatureBeeBuilder config, IFeatureRepository featureRepository)
        {
            config.FeatureRepository = featureRepository;
            return config;
        }

        //public static FeatureBeeBuilder UsingRepositoryFromConfig(this FeatureBeeBuilder config)
        //{
        //    return config;
        //}
    }

    public class FeatureBeeBuilder
    {
        private readonly HttpContextBase _httpContext;

        private FeatureBeeBuilder(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        public void Build()
        {
            if (_httpContext == null)
                Context = new WindowsApplicationContext(Evaluators, FeatureRepository);
            Context = new WebApplicationContext(_httpContext, Evaluators, FeatureRepository);
        }

        public static FeatureBeeBuilder Init(HttpApplication app)
        {
            return new FeatureBeeBuilder(new HttpContextWrapper(app.Context));
        }

        public static FeatureBeeBuilder Init(HttpContextBase httpContext)
        {
            return new FeatureBeeBuilder(httpContext);
        }

        public static FeatureBeeBuilder Init()
        {
            return new FeatureBeeBuilder(null);
        }

        internal static IFeatureBeeContext Context { get; private set; }
        internal List<IConditionEvaluator> Evaluators { get; set; }
        internal IFeatureRepository FeatureRepository { get; set; }

        public void BuildFromConfig()
        {
            var config = FeatureBeeConfiguration.GetSection();
        }
    }
}