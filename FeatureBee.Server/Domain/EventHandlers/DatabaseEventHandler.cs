namespace FeatureBee.Server.Domain.EventHandlers
{
    using System.Linq;

    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    using NEventStore;

    public class DatabaseEventHandler : IEventHandler
    {
        public void Handle(EventMessage @event)
        {
            var context = new FeatureBeeContext();

            if (@event.Body is FeatureCreatedEvent)
            {
                var body = @event.Body as FeatureCreatedEvent;
                context.Features.Add(new FeatureViewModel
                {
                    Id = body.AggregateId,
                    Name = body.Name,
                    Description = body.Description,
                    Conditions = body.Conditions.Select(c => new ConditionViewModel { Type = c.Type, Values = c.Values }).ToList(),
                    Link = body.Link,
                    Index = 0
                });
            }

            if (@event.Body is FeatureDescriptionUpdatedEvent)
            {
                var body = @event.Body as FeatureDescriptionUpdatedEvent;
                var feature = context.Features.Find((@event.Body as IDomainEvent).AggregateId);
                feature.Description = body.Description;
            }

            if (@event.Body is FeatureLinkedToTicketEvent)
            {
                var body = @event.Body as FeatureLinkedToTicketEvent;
                var feature = context.Features.Find((@event.Body as IDomainEvent).AggregateId);
                feature.Link = body.Link;
            }

            if (@event.Body is FeatureConditionsChangedEvent)
            {
                var body = @event.Body as FeatureConditionsChangedEvent;
                var feature = context.Features.Find((@event.Body as IDomainEvent).AggregateId);
                feature.Conditions = body.Conditions.Select(c => new ConditionViewModel {Type = c.Type, Values = c.Values}).ToList();
            }

            if (@event.Body is FeatureReleasedEvent)
            {
                var feature = context.Features.Find((@event.Body as IDomainEvent).AggregateId);
                feature.Index = 2;
            }

            if (@event.Body is FeatureTestedEvent)
            {
                var feature = context.Features.Find((@event.Body as IDomainEvent).AggregateId);
                feature.Index = 1;
            }

            if (@event.Body is FeatureRollbackedEvent)
            {
                var feature = context.Features.Find((@event.Body as IDomainEvent).AggregateId);
                feature.Index = 0;
            }

            if (@event.Body is FeatureRemovedEvent)
            {
                var feature = context.Features.Find((@event.Body as IDomainEvent).AggregateId);
                context.Features.Remove(feature);
            }

            context.SaveChanges();
        }
    }
}