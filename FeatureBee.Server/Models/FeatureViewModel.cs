namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Models;

    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Condition> Conditions { get; set; }
        public string State { get; set; }
    }
}