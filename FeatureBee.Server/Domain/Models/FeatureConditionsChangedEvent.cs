namespace FeatureBee.Server.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureConditionsChangedEvent : IDomainEvent
    {
        public FeatureConditionsChangedEvent(string name, List<Condition> conditions)
        {
            Name = name;
            Conditions = conditions;
        }

        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public List<Condition> Conditions { get; set; }
    }
}