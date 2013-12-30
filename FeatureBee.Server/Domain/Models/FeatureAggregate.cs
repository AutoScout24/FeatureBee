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

        private FeatureAggregate(Guid id, string name, string team, List<Condition> conditions) : this()
        {
            Id = id;
            Apply(new FeatureCreatedEvent { Name = name, Team = team, Conditions = conditions });
        }

        public FeatureAggregate(IEnumerable<IDomainEvent> domainEvents) : this()
        {
            LoadFromHistory(domainEvents);
        }

        public static FeatureAggregate CreateNew(string name, string team, List<Condition> conditions)
        {
            return new FeatureAggregate(Guid.NewGuid(), name, team, conditions);
        }

        private void RegisterEvents()
        {
            RegisterEvent<FeatureCreatedEvent>(OnFeatureCreated);
            RegisterEvent<FeatureReleasedEvent>(OnFeatureRelased);
            RegisterEvent<FeatureTestedEvent>(OnFeatureTested);
            RegisterEvent<FeatureRollbackedEvent>(OnFeatureRollbacked);
            RegisterEvent<FeatureConditionsChangedEvent>(OnFeatureConditionsChanged);
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

        public void Release()
        {
            Apply(new FeatureReleasedEvent());
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

        public void ChangeConditions(List<Condition> conditions)
        {
            Apply(new FeatureConditionsChangedEvent(conditions));
        }
    }
}