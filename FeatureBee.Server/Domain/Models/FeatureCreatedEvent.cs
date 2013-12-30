namespace FeatureBee.Server.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureCreatedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        public string Name { get; set; }
        public string Team { get; set; }
        public string Link { get; set; }
        public List<Condition> Conditions { get; set; }
    }
}