using System;
using System.Linq;
using FeatureBee.Configuration;

namespace FeatureBee
{
    public static class Feature
    {
        public static bool IsEnabled(string featureName)
        {
            if (FeatureBeeBuilder.Context == null)
            {
                throw new InvalidOperationException("FeatureBeeConfing.Init needs to be called first!");
            }

            var godModeFeatures = FeatureBeeBuilder.Context.GodModeFeatures;
            if (godModeFeatures.Any(x => x.Equals(featureName, StringComparison.InvariantCultureIgnoreCase)))
            {
                return true;
            }

            var evaluators = FeatureBeeBuilder.Context.Evaluators;
            var features = FeatureBeeBuilder.Context.FeatureRepository.GetFeatures();

            var feature = features.FirstOrDefault(x => string.Equals(x.Name, featureName));

            if (feature == null)
            {
                return false;
            }

            if (feature.State == "In Development" && !FeatureBeeBuilder.Context.IsDebugMode)
            {
                return false;
            }

            if (feature.State == "Released")
            {
                return true;
            }

            var isFulfilled = false;
            foreach (var condition in feature.Conditions)
            {
                var evaluator = evaluators.FirstOrDefault(x => string.Equals(x.Name, condition.Type));
                if (evaluator == null)
                {
                    return false;
                }

                isFulfilled = evaluator.IsFulfilled(condition.Values);
                if (!isFulfilled) {
                    return false;
                }
            }
            return isFulfilled;
        }
    }
}
