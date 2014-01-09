namespace FeatureBee.Server.Domain.Infrastruture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseAggregateRoot
    {
        private readonly Dictionary<Type, Action<IDomainEvent>> registeredEvents = new Dictionary<Type, Action<IDomainEvent>>();
        private readonly List<IDomainEvent> appliedEvents = new List<IDomainEvent>();

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, IDomainEvent
        {
            registeredEvents.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, IDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = GetNewEventVersion();
            apply(domainEvent.GetType(), domainEvent);
            appliedEvents.Add(domainEvent);
        }

        protected void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            if (!domainEvents.Any())
                return;

            foreach (var domainEvent in domainEvents)
            {
                apply(domainEvent.GetType(), domainEvent);
            }

            Version = domainEvents.Last().Version;
            EventVersion = Version;
        }

        private void apply(Type eventType, IDomainEvent domainEvent)
        {
            Action<IDomainEvent> handler;

            if (!registeredEvents.TryGetValue(eventType, out handler))
                throw new Exception(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handler(domainEvent);
        }

        public IEnumerable<IDomainEvent> GetChanges()
        {
            return appliedEvents;
        }

        private int GetNewEventVersion()
        {
            return ++EventVersion;
        }
    }
}