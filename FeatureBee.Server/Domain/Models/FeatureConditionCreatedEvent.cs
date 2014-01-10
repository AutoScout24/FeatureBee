namespace FeatureBee.Server.Domain.Models
{
    using System;
    using FeatureBee.Server.Domain.Infrastruture;
    
    public class FeatureConditionCreatedEvent : IDomainEvent
    {
        public FeatureConditionCreatedEvent(string featureName, Condition condition)
        {
            this.FeatureName = featureName;
            this.Condition = condition;
        }

        public string FeatureName { get; set; }
        public Condition Condition { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}