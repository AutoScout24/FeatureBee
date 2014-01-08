namespace FeatureBee.Server.Domain.Models
{
    using System;

    using FeatureBee.Server.Domain.Infrastruture;

    // TODO: Find a better name
    public class FeatureReleasedWithConditionsEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
    }
}