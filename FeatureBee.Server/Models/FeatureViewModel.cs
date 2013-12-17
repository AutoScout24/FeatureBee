namespace FeatureBee.Server.Models
{
    using System.Collections.Generic;

    public class FeatureViewModel
    {
        public string Name { get; set; }
        public List<Condition> Conditions { get; set; }
        public string State { get; set; }
    }
}