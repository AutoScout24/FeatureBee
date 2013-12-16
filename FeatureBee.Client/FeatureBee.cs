﻿using System;
using System.Linq;

namespace FeatureBee.Client
{
    public static class FeatureBee
    {
        public static bool IsFeatureEnabled(string featureName)
        {
            if (FeatureBeeConfig.Context == null)
            {
                throw new InvalidOperationException("FeatureBeeConfing.Init needs to be called first!");
            }

            var evaluators = FeatureBeeConfig.Context.Evaluators;
            var features = FeatureBeeConfig.Context.FeatureRepository.GetFeatures();

            var feature = features.FirstOrDefault(x => string.Equals(x.Name, featureName));

            if (feature == null)
            {
                return false;
            }

            if (feature.State == "In Development")
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
                var evaluator = evaluators.FirstOrDefault(x => string.Equals(x.Name, condition.Evaluator));
                if (evaluator == null)
                {
                    return false;
                }

                isFulfilled = evaluator.IsFulfilled(condition.Value);
                if (!isFulfilled) {
                    return false;
                }
            }
            return isFulfilled;
        }
    }
}
