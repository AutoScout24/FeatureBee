using System.Collections.Generic;

namespace FeatureBee.Client
{
    public class Feature
    {
        public string Name { get; set; }
        public List<Condition> Conditions { get; set; }
        public string State { get; set; }
    }
}