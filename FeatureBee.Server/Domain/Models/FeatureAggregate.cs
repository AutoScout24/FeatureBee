namespace FeatureBee.Server.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureAggregate : BaseAggregateRoot
    {
        private string featureDescription;
        private string featureLinkToTicket;
        private string featureName;

        private FeatureAggregate()
        {
            RegisterEvents();
        }

        private FeatureAggregate(Guid id, string name, string description, string team, string link, List<Condition> conditions) : this()
        {
            Id = id;
            Apply(new FeatureCreatedEvent {Name = name, Description = description, Conditions = conditions, Team = team});

            if (!string.IsNullOrEmpty(link))
            {
                Apply(new FeatureLinkedToTicketEvent {Link = link});
            }
        }

        public FeatureAggregate(IEnumerable<IDomainEvent> domainEvents) : this()
        {
            LoadFromHistory(domainEvents);
        }

        public static FeatureAggregate CreateNew(string name, string description, string team, string link, List<Condition> conditions)
        {
            return new FeatureAggregate(Guid.NewGuid(), name, description, team, link, conditions);
        }

        public void UpdateDescription(string description)
        {
            if ((featureDescription ?? "").Equals(description ?? ""))
            {
                return;
            }

            Apply(new FeatureDescriptionUpdatedEvent {Name = featureName, Description = description});
        }

        public void LinkToTicket(string link)
        {
            if (featureLinkToTicket.Equals(link))
            {
                return;
            }

            Apply(new FeatureLinkedToTicketEvent {Name = featureName, Link = link});
        }

        public void ReleaseWithConditions()
        {
            Apply(new FeatureReleasedWithConditionsEvent {Name = featureName});
        }

        public void ReleaseForEveryone()
        {
            Apply(new FeatureReleasedForEveryoneEvent {Name = featureName});
        }

        public void Rollback()
        {
            Apply(new FeatureRollbackedEvent {Name = featureName});
        }

        public void Remove()
        {
            Apply(new FeatureRemovedEvent {Name = featureName});
        }

        public void ChangeConditions(List<Condition> conditions)
        {
            Apply(new FeatureConditionsChangedEvent(featureName, conditions));
        }

        private void RegisterEvents()
        {
            RegisterEvent<FeatureCreatedEvent>(OnFeatureCreated);
            RegisterEvent<FeatureReleasedWithConditionsEvent>(OnFeatureTested);
            RegisterEvent<FeatureReleasedForEveryoneEvent>(OnFeatureRelased);
            RegisterEvent<FeatureRollbackedEvent>(OnFeatureRollbacked);
            RegisterEvent<FeatureConditionsChangedEvent>(OnFeatureConditionsChanged);
            RegisterEvent<FeatureDescriptionUpdatedEvent>(OnFeatureDescriptionUpdated);
            RegisterEvent<FeatureLinkedToTicketEvent>(OnFeatureLinkedToTicket);
            RegisterEvent<FeatureRemovedEvent>(OnFeatureRemoved);
        }

        private void OnFeatureCreated(FeatureCreatedEvent @event)
        {
            Id = @event.AggregateId;
            featureName = @event.Name;
            featureDescription = @event.Description;
        }

        private void OnFeatureDescriptionUpdated(FeatureDescriptionUpdatedEvent @event)
        {
            featureDescription = @event.Description;
        }

        private void OnFeatureLinkedToTicket(FeatureLinkedToTicketEvent @event)
        {
            featureLinkToTicket = @event.Link;
        }

        private void OnFeatureConditionsChanged(FeatureConditionsChangedEvent @event)
        {
        }

        private void OnFeatureRollbacked(FeatureRollbackedEvent @event)
        {
        }

        private void OnFeatureTested(FeatureReleasedWithConditionsEvent @event)
        {
        }

        private void OnFeatureRelased(FeatureReleasedForEveryoneEvent @event)
        {
        }

        private void OnFeatureRemoved(FeatureRemovedEvent @event)
        {
        }
    }
}