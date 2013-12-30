namespace FeatureBee.Server.Domain.Infrastruture
{
    using System;

    public interface IDomainEvent 
    {
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}