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
            // TODO: Hub previously sent whole "feature"
            if (@event.Body is FeatureCreatedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureCreated(@event.Body as FeatureCreatedEvent);
            }
            if (@event.Body is FeatureReleasedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureReleased(@event.Body as FeatureReleasedEvent);
            }
            if (@event.Body is FeatureTestedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureTested(@event.Body as FeatureTestedEvent);
            }
            if (@event.Body is FeatureRollbackedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.featureRollbacked(@event.Body as FeatureRollbackedEvent);
            }
            if (@event.Body is FeatureConditionsChangedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<EditPanelHub>();
                hub.Clients.All.conditionsChanged(@event.Body as FeatureConditionsChangedEvent);
            }
            if (@event.Body is FeatureDescriptionUpdatedEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.descriptionUpdated(@event.Body as FeatureDescriptionUpdatedEvent);
            }
            if (@event.Body is FeatureLinkedToTicketEvent)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<BoardHub>();
                hub.Clients.All.linkedToTicket(@event.Body as FeatureLinkedToTicketEvent);
            }
        }
    }
}