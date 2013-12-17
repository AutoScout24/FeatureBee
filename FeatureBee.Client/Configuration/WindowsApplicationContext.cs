using System.Collections.Generic;
using FeatureBee.Evaluators;

namespace FeatureBee.Configuration
{
    internal class WindowsApplicationContext : IFeatureBeeContext
    {
        public WindowsApplicationContext(List<IConditionEvaluator> evaluators, IFeatureRepository featureRepository)
        {
            Evaluators = evaluators;
            FeatureRepository = featureRepository;
            GodModeFeatures = new List<string>(); // Not supported, yet
            IsDebugMode = false; // Not supported, yet
        }

        public List<IConditionEvaluator> Evaluators { get; private set; }
        public IFeatureRepository FeatureRepository { get; private set; }
        public List<string> GodModeFeatures { get; private set; }
        public bool IsDebugMode { get; private set; }
    }
}