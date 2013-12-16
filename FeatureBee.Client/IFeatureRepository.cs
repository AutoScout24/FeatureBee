using System.Collections.Generic;

namespace FeatureBee.Client
{
    public interface IFeatureRepository
    {
        List<Feature> GetFeatures();
    }
}