using System.Collections.Generic;
using FeatureBee.Evaluators;

namespace FeatureBee.WireUp
{
    internal class WindowsApplicationContext : IFeatureBeeContext
    {
        public WindowsApplicationContext()
        {
            GodModeFeatures = new List<string>(); // Not supported, yet
            IsDebugMode = false; // Not supported, yet
        }

        public List<IConditionEvaluator> Evaluators { get; set; }
        public IFeatureRepository FeatureRepository { get; set; }
        public List<string> GodModeFeatures { get; private set; }
        public bool IsDebugMode { get; private set; }
        public bool ShowTrayIconOnPages { get; set; }
    }
}