namespace FeatureBee.Data
{
    using System;
    using System.Collections.Generic;

    public interface IStoreEvents : IDisposable
    {
        void AddEvent<TEvent>(TEvent @event);

        IEnumerable<T> All<T>() where T : IHandleEvents;
    }

    public interface IHandleEvents
    {
        void Handle(Type eventType, object eventBody);
    }

    public interface IStoreEvents<in TStorage> : IStoreEvents
    {
        IStoreEvents<TStorage> Init(Guid streamId, TStorage underlyingStorage);
    }
}
