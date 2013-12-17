using System.Collections.Generic;

namespace FeatureBee.Client
{
    public class FeatureDto
    {
        public string Name { get; set; }
        public List<ConditionDto> Conditions { get; set; }
        public string State { get; set; }
    }
}