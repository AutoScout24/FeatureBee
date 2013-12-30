namespace FeatureBee.Server.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureConditionsChangedEvent : IDomainEvent
    {
        public FeatureConditionsChangedEvent(List<Condition> conditions)
        {
            Conditions = conditions;
        }

        public List<Condition> Conditions { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}