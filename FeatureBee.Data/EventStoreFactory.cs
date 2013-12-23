namespace FeatureBee.Data
{
    using System;

    using FeatureBee.Data.NEventStoring;

    using NEventStore;
    using NEventStore.Dispatcher;

    public static class EventStoreFactory
    {
        private static IDispatchMessages dispatcher;

        private static readonly SubscriberDictionary SubscriberDictionary = new SubscriberDictionary();

        public static void SubscribeTo<TEventToSubscribeTo>(ISubscribe subscriber)
        {
            if (!SubscriberDictionary.ContainsKey(typeof(TEventToSubscribeTo).FullName)) SubscriberDictionary.Add(typeof(TEventToSubscribeTo).FullName, subscriber);
            else SubscriberDictionary[typeof(TEventToSubscribeTo).FullName] = subscriber;
        }

        public static IStoreEvents Create(Guid id = default(Guid))
        {
            return CreateNEventStore(id);
        }

        private static IStoreEvents CreateNEventStore(Guid id)
        {
            dispatcher = new NEventStoreDispatcher(SubscriberDictionary);
            var nEventStore =
                Wireup.Init()
                    .LogToOutputWindow()
                    .UsingInMemoryPersistence()
                    .InitializeStorageEngine()
                    .TrackPerformanceInstance("featurebee")
                    .UsingBinarySerialization()
                    .UsingSynchronousDispatchScheduler()
                    .DispatchTo(new DelegateMessageDispatcher(dispatcher.DispatchCommit))
                    .Build();
            var store = new NEventStoreImpl().Init(id, nEventStore);
            return store;
        }
    }
}