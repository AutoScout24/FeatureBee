namespace FeatureBee.Server.Domain.Models
{
    using System;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureTestedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}