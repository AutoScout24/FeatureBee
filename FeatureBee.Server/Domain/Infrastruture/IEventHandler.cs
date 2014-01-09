namespace FeatureBee.Server.Domain.Infrastruture
{
    using NEventStore;

    public interface IEventHandler
    {
        void Handle(EventMessage @event);
    }
}