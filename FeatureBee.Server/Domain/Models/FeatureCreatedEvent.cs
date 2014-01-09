namespace FeatureBee.Server.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureCreatedEvent : IDomainEvent
    {
        public FeatureCreatedEvent()
        {
            Conditions = new List<Condition>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Team { get; set; }
        public List<Condition> Conditions { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}