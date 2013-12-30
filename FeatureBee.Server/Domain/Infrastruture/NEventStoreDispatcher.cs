namespace FeatureBee.Server.Domain.Infrastruture
{
    using System;

    using FeatureBee.Server.Controllers;
    using FeatureBee.Server.Domain.Models;

    using Microsoft.AspNet.SignalR;

    using NEventStore;
    using NEventStore.Persistence;

    public class NEventStoreDispatcher
    {
        public void DispatchCommit(ICommit commit)
        {
            foreach (var @event in commit.Events)
            {
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
            }

            //nEventStoreCommit.Events.ForEach(
            //    @event =>
            //    {
            //        var key = @event.Body.GetType().FullName;
            //        if (this.subscriberDictionary.ContainsKey(key))
            //        {
            //            this.subscriberDictionary[key].Notify(@event.Body);
            //        }
            //    });
        }
    }
}