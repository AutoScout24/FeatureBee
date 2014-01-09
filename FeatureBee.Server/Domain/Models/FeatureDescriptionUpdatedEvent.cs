namespace FeatureBee.Server.Domain.Models
{
    using System;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureDescriptionUpdatedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}