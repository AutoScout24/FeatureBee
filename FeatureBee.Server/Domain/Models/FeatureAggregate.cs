namespace FeatureBee.Server.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureAggregate : BaseAggregateRoot
    {
        private string name;

        private FeatureAggregate()
        {
            RegisterEvents();
        }

        private FeatureAggregate(Guid id, string name, string description, string team, string link, List<Condition> conditions) : this()
        {
            Id = id;
            Apply(new FeatureCreatedEvent { Name = name, Description = description, Conditions = conditions, Team = team });

            if (!string.IsNullOrEmpty(link)) { 
                Apply(new FeatureLinkedToTicketEvent { Link = link });
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
            Apply(new FeatureDescriptionUpdatedEvent { Name = name, Description = description });
        }

        public void LinkToTicket(string link)
        {
            Apply(new FeatureLinkedToTicketEvent { Name = name, Link = link });
        }

        public void Test()
        {
            Apply(new FeatureTestedEvent { Name = name });
        }

        public void Release()
        {
            Apply(new FeatureReleasedEvent {Name = name});
        }

        public void Rollback()
        {
            Apply(new FeatureRollbackedEvent { Name = name });
        }

        public void Remove()
        {
            Apply(new FeatureRemovedEvent { Name = name });
        }

        public void ChangeConditions(List<Condition> conditions)
        {
            Apply(new FeatureConditionsChangedEvent(name, conditions));
        }

        private void RegisterEvents()
        {
            RegisterEvent<FeatureCreatedEvent>(OnFeatureCreated);
            RegisterEvent<FeatureTestedEvent>(OnFeatureTested);
            RegisterEvent<FeatureReleasedEvent>(OnFeatureRelased);
            RegisterEvent<FeatureRollbackedEvent>(OnFeatureRollbacked);
            RegisterEvent<FeatureConditionsChangedEvent>(OnFeatureConditionsChanged);
            RegisterEvent<FeatureDescriptionUpdatedEvent>(OnFeatureDescriptionUpdated);
            RegisterEvent<FeatureLinkedToTicketEvent>(OnFeatureLinkedToTicket);
            RegisterEvent<FeatureRemovedEvent>(OnFeatureRemoved);
        }

        private void OnFeatureCreated(FeatureCreatedEvent @event)
        {
            Id = @event.AggregateId;
            name = @event.Name;
        }

        private void OnFeatureDescriptionUpdated(FeatureDescriptionUpdatedEvent obj)
        {
        }

        private void OnFeatureLinkedToTicket(FeatureLinkedToTicketEvent obj)
        {
        }

        private void OnFeatureConditionsChanged(FeatureConditionsChangedEvent @event)
        {
        }

        private void OnFeatureRollbacked(FeatureRollbackedEvent @event)
        {
        }

        private void OnFeatureTested(FeatureTestedEvent @event)
        {
        }

        private void OnFeatureRelased(FeatureReleasedEvent @event)
        {
        }

        private void OnFeatureRemoved(FeatureRemovedEvent @event)
        {
        }
    }
}