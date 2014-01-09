namespace FeatureBee.Server.Domain.Infrastruture
{
    using System.Collections.Generic;

    using NEventStore;

    public class NEventStoreDispatcher
    {
        private readonly IEnumerable<IEventHandler> eventHandlers;

        public NEventStoreDispatcher(IEnumerable<IEventHandler> eventHandlers)
        {
            this.eventHandlers = eventHandlers;
        }

        public void DispatchCommit(ICommit commit)
        {
            foreach (var @event in commit.Events)
            {
                foreach (var eventHandler in eventHandlers)
                {
                    eventHandler.Handle(@event);
                }
            }
        }
    }
}