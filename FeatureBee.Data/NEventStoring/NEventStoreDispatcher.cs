namespace FeatureBee.Data.NEventStoring
{
    using System;

    using NEventStore;

    internal class NEventStoreDispatcher : IDispatchMessages
    {
        private readonly SubscriberDictionary subscriberDictionary;

        public NEventStoreDispatcher(SubscriberDictionary subscriberDictionary)
        {
            this.subscriberDictionary = subscriberDictionary;
        }

        public void DispatchCommit<TCommit>(TCommit commit)
        {
            var nEventStoreCommit = commit as Commit;
            if (nEventStoreCommit == null)
            {
                throw new Exception("Invalid commit type");
            }

            nEventStoreCommit.Events.ForEach(
                @event =>
                {
                    var key = @event.Body.GetType().FullName;
                    if (this.subscriberDictionary.ContainsKey(key))
                    {
                        this.subscriberDictionary[key].Notify(@event.Body);
                    }
                });
        }
    }
}