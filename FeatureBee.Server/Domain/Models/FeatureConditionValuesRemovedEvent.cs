namespace FeatureBee.Server.Domain.Models
{
    using System;

    using FeatureBee.Server.Domain.Infrastruture;

    public class FeatureConditionValuesRemovedEvent : IDomainEvent
    {
        public FeatureConditionValuesRemovedEvent(string featureName, string type, string[] values)
        {
            this.FeatureName = featureName;
            this.Type = type;
            this.Value = FeatureConditionValueHandler.Concat(values);
        }
        
        public string FeatureName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}