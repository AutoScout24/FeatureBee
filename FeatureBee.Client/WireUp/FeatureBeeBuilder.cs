using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.ConfigSection;
using FeatureBee.Evaluators;

namespace FeatureBee.WireUp
{
    public class FeatureBeeBuilder
    {
        internal static IFeatureBeeContext Context { get; private set; }

        private FeatureBeeBuilder()
        {
            Context = new WindowsApplicationContext {Evaluators = LoadConditionEvaluators()};
        }
        private FeatureBeeBuilder(HttpContextBase httpContext)
        {
            Context = new WebApplicationContext(httpContext) {Evaluators = LoadConditionEvaluators()};
        }

        public static FeatureBeeBuilder ForWebApp(HttpApplication app)
        {
            return new FeatureBeeBuilder(new HttpContextWrapper(app.Context));
        }

        public static FeatureBeeBuilder ForWebApp(HttpContextBase httpContext)
        {
            return new FeatureBeeBuilder(httpContext);
        }

        public static FeatureBeeBuilder ForWindowsService()
        {
            return new FeatureBeeBuilder();
        }

        public void Use(IFeatureRepository featureRepository = null, List<IConditionEvaluator> conditionEvaluators = null)
        {
            if (featureRepository != null)
            {
                Context.FeatureRepository = featureRepository;
            }

            if (conditionEvaluators != null)
            {
                Context.Evaluators = conditionEvaluators;
            }
        }

        public void UseConfig()
        {
            var config = FeatureBeeConfiguration.GetSection();
            Context.FeatureRepository = new PullFeatureRepository(config.Server.Url);

            Context.ShowTrayIconOnPages = config.Tray.ShowTrayIconOnPages;
        }

        private static List<IConditionEvaluator> LoadConditionEvaluators()
        {
            var types = typeof(FeatureBeeBuilder).Assembly.GetTypes().Where(TypeIsConditionEvaluator).ToList();
            return types.Select(_ =>
            {
                var constructor = _.GetConstructor(Type.EmptyTypes);
                return constructor != null ? (IConditionEvaluator)constructor.Invoke(null) : null;
            }).ToList();            
        }

        private static bool TypeIsConditionEvaluator(Type type)
        {
            return type.GetInterface(typeof(IConditionEvaluator).Name) != null && !type.IsAbstract;
        }
    }
}