using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.Client.Evaluators;

namespace FeatureBee.Client
{
    public static class FeatureBeeConfigEvaluators
    {
        public static FeatureBeeConfig UsingEvaluators(this FeatureBeeConfig config, List<IConditionEvaluator> evaluators)
        {
            config.Evaluators = evaluators;
            return config;
        }

        public static FeatureBeeConfig UsingEvaluatorsFromAssembly(this FeatureBeeConfig config)
        {
            var types = typeof(FeatureBeeConfig).Assembly.GetTypes().Where(TypeIsConditionEvaluator<WindowsApplicationContext>).ToList();
            var evaluators = types.Select(_ =>
            {
                var constructor = _.GetConstructor(Type.EmptyTypes);
                return constructor != null ? (IConditionEvaluator)constructor.Invoke(null) : null;
            }).ToList();

            config.Evaluators = evaluators;
            return config;
        }

        private static bool TypeIsConditionEvaluator<T>(Type type)
        {
            return type.IsSubclassOf(typeof(IConditionEvaluator<T>)) && !type.IsAbstract;
        }
    }

    public static class FeatureBeeConfigFeatures
    {
        public static FeatureBeeConfig UsingRepository(this FeatureBeeConfig config, IFeatureRepository featureRepository)
        {
            config.FeatureRepository = featureRepository;
            return config;
        }

        public static FeatureBeeConfig UsingRepositoryFromConfig(this FeatureBeeConfig config)
        {
            return config;
        }
    }

    public class FeatureBeeConfig
    {
        private FeatureBeeConfig()
        {
            
        }

        public void Build()
        {
            Context = new WindowsApplicationContext(Evaluators, FeatureRepository);
        }

        public static FeatureBeeConfig Init()
        {
            return new FeatureBeeConfig();
        }

        internal static IFeatureBeeContext Context { get; private set; }
        internal List<IConditionEvaluator> Evaluators { get; set; }
        internal IFeatureRepository FeatureRepository { get; set; }

        ///// <summary>
        ///// Initializes FeatureBee. Use this method from a Web Application.
        ///// </summary>
        ///// <param name="app">The reference to your Web Application.</param>
        //public static void Init(HttpApplication app)
        //{
        //    var types = typeof(FeatureBeeConfig).Assembly.GetTypes().Where(TypeIsConditionEvaluator<WebApplicationContext>).ToList();
        //    var evaluators = types.Select(_ =>
        //    {
        //        var constructor = _.GetConstructor(Type.EmptyTypes);
        //        return constructor != null ? (IConditionEvaluator)constructor.Invoke(null) : null;
        //    }).ToList();
        //    Context = new WebApplicationContext(app.Context, evaluators);
        //}

        ///// <summary>
        ///// Initializes FeatureBee. Use this method from a Windows Forms or Windows Service Application.
        ///// </summary>
        //public static void Init()
        //{
        //    var types = typeof(FeatureBeeConfig).Assembly.GetTypes().Where(TypeIsConditionEvaluator<WindowsApplicationContext>).ToList();
        //    var evaluators = types.Select(_ =>
        //    {
        //        var constructor = _.GetConstructor(Type.EmptyTypes);
        //        return constructor != null ? (IConditionEvaluator)constructor.Invoke(null) : null;
        //    }).ToList();

        //    Context = new WindowsApplicationContext(evaluators, featureRepository);
        //}

    }
}