namespace FeatureBee.Server.Domain.EventHandlers
{
    using System;
    using System.Diagnostics;
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
                try
                {
                    var body = @event.Body as FeatureCreatedEvent;
                    var conditions = body.Conditions.Select(c => new ConditionViewModel {Type = c.Type, Values = c.Values.Select(_ => new ConditionValueViewModel(_)).ToList()});

                    var conditionList = conditions.ToList();

                    context.Features.Add(new FeatureViewModel
                    {
                        Id = body.AggregateId,
                        Name = body.Name,
                        Description = body.Description,
                        Team = body.Team,
                        Index = 0,
                        Conditions = conditionList
                    });
                }
                catch (Exception e)
                {
                    Debug.Write(e);
                }
            }

            var domainEvent = (@event.Body as IDomainEvent);
            if (@event.Body is FeatureDescriptionUpdatedEvent)
            {
                var body = @event.Body as FeatureDescriptionUpdatedEvent;
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Description = body.Description;
            }

            if (@event.Body is FeatureLinkedToTicketEvent)
            {
                var body = @event.Body as FeatureLinkedToTicketEvent;
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Link = body.Link;
            }

            if (@event.Body is FeatureConditionsChangedEvent)
            {
                var body = @event.Body as FeatureConditionsChangedEvent;
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Conditions = body.Conditions.Select(c => new ConditionViewModel { Type = c.Type, Values = c.Values.Select(_ => new ConditionValueViewModel(_)).ToList() }).ToList();
            }

            if (@event.Body is FeatureReleasedForEveryoneEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Index = 2;
            }

            if (@event.Body is FeatureReleasedWithConditionsEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Index = 1;
            }

            if (@event.Body is FeatureRollbackedEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                feature.Index = 0;
            }

            if (@event.Body is FeatureRemovedEvent)
            {
                var feature = context.Features.Find(domainEvent.AggregateId);
                context.Features.Remove(feature);
            }

            if (@event.Body is FeatureConditionCreatedEvent)
            {
                try
                {
                    var condition = (@event.Body as FeatureConditionCreatedEvent).Condition;
                    var feature = context.Features.FirstOrDefault(f => f.Id == domainEvent.AggregateId);
                    feature.AddCondition(condition.Type);
                }
                catch (Exception e0)
                {
                    Console.WriteLine();
                    throw;
                }
            }
            if (@event.Body is FeatureConditionValuesAddedEvent)
            {
                var featureConditionValuesAddedEvent = (@event.Body as FeatureConditionValuesAddedEvent);
                var feature = context.Features.First(f => f.Id == domainEvent.AggregateId);
                feature.Conditions.First(_ => _.Type == featureConditionValuesAddedEvent.Type).AddValue(featureConditionValuesAddedEvent.Value);
            }
            if (@event.Body is FeatureConditionValuesRemovedEvent)
            {
                var featureConditionValuesAddedEvent = (@event.Body as FeatureConditionValuesRemovedEvent);
                var feature = context.Features.First(f => f.Id == domainEvent.AggregateId);
                feature.Conditions.First(_ => _.Type == featureConditionValuesAddedEvent.Type).RemoveValue(featureConditionValuesAddedEvent.Value);
            }

            context.SaveChanges();
        }
    }
}