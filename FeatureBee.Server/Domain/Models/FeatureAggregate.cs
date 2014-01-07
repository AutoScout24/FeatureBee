namespace FeatureBee.Server.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureAggregate : BaseAggregateRoot
    {
        private FeatureAggregate()
        {
            RegisterEvents();
        }

        private FeatureAggregate(Guid id, string name, string description, List<Condition> conditions, string link) : this()
        {
            Id = id;
            Apply(new FeatureCreatedEvent { Name = name, Description = description, Conditions = conditions });

            if (!string.IsNullOrEmpty(link)) { 
                Apply(new FeatureLinkedToTicketEvent { Link = link });
            }
        }

        public FeatureAggregate(IEnumerable<IDomainEvent> domainEvents) : this()
        {
            LoadFromHistory(domainEvents);
        }

        public static FeatureAggregate CreateNew(string name, string description, List<Condition> conditions, string link)
        {
            return new FeatureAggregate(Guid.NewGuid(), name, description, conditions, link);
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

        public void Release()
        {
            Apply(new FeatureReleasedEvent());
        }

        public void UpdateDescription(string description)
        {
            Apply(new FeatureDescriptionUpdatedEvent { Description = description });
        }

        public void LinkToTicket(string link)
        {
            Apply(new FeatureLinkedToTicketEvent { Link = link });
        }

        private void OnFeatureCreated(FeatureCreatedEvent @event)
        {
            Id = @event.AggregateId;
        }

        public void Test()
        {
            Apply(new FeatureTestedEvent());
        }

        public void Rollback()
        {
            Apply(new FeatureRollbackedEvent());
        }

        public void Remove()
        {
            Apply(new FeatureRemovedEvent());
        }

        public void ChangeConditions(List<Condition> conditions)
        {
            Apply(new FeatureConditionsChangedEvent(conditions));
        }
    }
}