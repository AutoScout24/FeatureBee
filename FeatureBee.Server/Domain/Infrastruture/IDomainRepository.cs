namespace FeatureBee.Server.Domain.Infrastruture
{
    using System;
    using System.Collections.Generic;

    public interface IDomainRepository
    {
        IEnumerable<IDomainEvent> GetById(Guid id);
        void Save<T>(T aggregateRoot) where T : BaseAggregateRoot;
    }
}