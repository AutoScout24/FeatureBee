using System;
using System.Collections.Generic;
using FeatureBee.Client.Evaluators;

namespace FeatureBee.Client
{
    internal interface IFeatureBeeContext
    {
        List<IConditionEvaluator> Evaluators { get; }
        IFeatureRepository FeatureRepository { get; }
        List<string> GodModeFeatures { get; }
        bool IsDebugMode { get; }
    }
}