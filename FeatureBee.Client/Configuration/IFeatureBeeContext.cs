using System.Collections.Generic;
using FeatureBee.Evaluators;

namespace FeatureBee.Configuration
{
    internal interface IFeatureBeeContext
    {
        List<IConditionEvaluator> Evaluators { get; }
        IFeatureRepository FeatureRepository { get; }
        List<string> GodModeFeatures { get; }
        bool IsDebugMode { get; }
    }
}