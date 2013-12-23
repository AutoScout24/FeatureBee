namespace FeatureBee.Data.NEventStoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;

    using global::NEventStore;

    public class NEventStoreImpl : IStoreEvents<IStoreEvents>
    {
        private readonly TransactionScope scope;

        private Guid id;

        private IStoreEvents store;

        public NEventStoreImpl()
        {
            scope = new TransactionScope();
        }

        public void Dispose()
        {
            scope.Complete();
            this.store.Dispose();
        }

        public IStoreEvents<IStoreEvents> Init(Guid streamId, IStoreEvents underlyingStorage)
        {
            this.id = streamId;
            this.store = underlyingStorage;
            return this;
        }

        public T Get<T>() where T : IHandleEvents
        {
            var invokedType = Create<T>();

            var commits = this.store.Advanced.GetFrom(this.id, 0, int.MaxValue);
            foreach (var @event in commits.SelectMany(commit => commit.Events))
            {
                invokedType.Handle(@event.GetType(), @event.Body);
            }

            return invokedType;
        }

        private static T Create<T>() where T : IHandleEvents
        {
            var constructorInfo = typeof(T).GetConstructor(Type.EmptyTypes);
            if (constructorInfo == null)
            {
                throw new Exception("Missing default constructor for type");
            }

            var invokedType = (IHandleEvents)constructorInfo.Invoke(null);
            return (T)invokedType;
        }

        public IEnumerable<T> All<T>() where T : IHandleEvents
        {
            var dict = new Dictionary<Guid, T>();

            var commits = this.store.Advanced.GetFrom(DateTime.MinValue);
            foreach (var commit in commits)
            {
                T currentType;
                if (!dict.ContainsKey(commit.StreamId))
                {
                    currentType = Create<T>();
                    dict.Add(commit.StreamId, currentType);
                }
                else
                {
                    currentType = dict[commit.StreamId];
                }

                foreach (var @event in commit.Events)
                {
                    currentType.Handle(@event.GetType(), @event.Body);
                }
            }

            return dict.Values;
        }

        public void AddEvent<T>(T @event)
        {
            using (var stream = this.store.OpenStream(this.id, 0, int.MaxValue))
            {
                stream.Add(new EventMessage { Body = @event });
                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}