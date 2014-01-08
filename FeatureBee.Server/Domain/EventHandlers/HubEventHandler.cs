namespace FeatureBee.Server.Domain.EventHandlers
{
    using FeatureBee.Server.Controllers;
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;

    using Microsoft.AspNet.SignalR;

    using NEventStore;

    public class HubEventHandler: IEventHandler
    {
        public void Handle(EventMessage @event)
        {
            if (@event.Body is FeatureCreatedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureCreated((@event.Body as FeatureCreatedEvent).Name);
            }
            if (@event.Body is FeatureReleasedForEveryoneEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureReleasedForEveryone((@event.Body as FeatureReleasedForEveryoneEvent).Name);
            }
            if (@event.Body is FeatureReleasedWithConditionsEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureReleasedWithConditions((@event.Body as FeatureReleasedWithConditionsEvent).Name);
            }
            if (@event.Body is FeatureRollbackedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureRollbacked((@event.Body as FeatureRollbackedEvent).Name);
            }
            if (@event.Body is FeatureConditionsChangedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<EditPanelHub>();
                hub.Clients.All.conditionsChanged((@event.Body as FeatureConditionsChangedEvent).Name);
            }
            if (@event.Body is FeatureDescriptionUpdatedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.descriptionUpdated((@event.Body as FeatureDescriptionUpdatedEvent).Name);
            }
            if (@event.Body is FeatureLinkedToTicketEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.linkedToTicket((@event.Body as FeatureLinkedToTicketEvent).Name);
            }
        }
    }
}