namespace FeatureBee.Server.Domain.Infrastruture
{
    using System;
    using System.Collections.Generic;

    using NEventStore;

    public class DomainRepository : IDomainRepository
    {
        private readonly IStoreEvents eventStore;

        public DomainRepository(IStoreEvents eventStore)
        {
            this.eventStore = eventStore;
        }

        public IEnumerable<IDomainEvent> GetById(Guid id)
        {
            var list = new List<IDomainEvent>();
            using (var stream = eventStore.OpenStream(id, 0))
            {
                var committedEvents = stream.CommittedEvents;
                foreach (var eventMessage in committedEvents)
                {
                    list.Add(eventMessage.Body as IDomainEvent);
                }
            }
            return list;
        }

        public void Save<T>(T aggregateRoot) where T : BaseAggregateRoot
        {
            using (var stream = eventStore.OpenStream(aggregateRoot.Id, 0))
            {
                if (stream.StreamRevision != aggregateRoot.Version)
                {
                    throw new ConcurrencyException();
                }

                foreach (IDomainEvent @event in aggregateRoot.GetChanges())
                {
                    stream.Add(new EventMessage { Body = @event });
                    stream.CommitChanges(Guid.NewGuid());
                }
            }
        }
    }
}