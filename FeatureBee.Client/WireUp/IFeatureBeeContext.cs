using System.Collections.Generic;
using FeatureBee.Evaluators;

namespace FeatureBee.WireUp
{
    internal interface IFeatureBeeContext
    {
        List<IConditionEvaluator> Evaluators { get; set; }
        IFeatureRepository FeatureRepository { get; set; }
        List<string> GodModeFeatures { get; }
        bool IsDebugMode { get; }
        bool ShowTrayIconOnPages { get; set; }
    }
}