namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Data;

    public class Feature : IHandleEvents
    {
        public Feature()
        {
            id = Guid.NewGuid();
            conditions = new List<Condition>();
        }

        public Guid id { get; set; }

        public string name { get; set; }

        public string team { get; set; }

        public int index { get; set; }

        public string link { get; set; }

        public List<Condition> conditions { get; set; }

        public void Handle(Type eventType, object eventBody)
        {
            if (eventType == typeof(NewFeatureCreated))
            {
                HandledEvent((NewFeatureCreated)eventBody);
            }
        }

        private void HandledEvent(NewFeatureCreated eventBody)
        {
            id = eventBody.Feature.id;
            name = eventBody.Feature.name;
            team = eventBody.Feature.team;
            index = eventBody.Feature.index;
            link = eventBody.Feature.link;
            conditions = eventBody.Feature.conditions;
        }
    }

    public class NewFeatureCreated
    {
        public Feature Feature { get; set; }
    }

    public class Condition
    {
        public Condition()
        {
            values = new List<string>();
        }

        public string type { get; set; }

        public List<string> values { get; set; }

        public void AddValue(string value)
        {
            if (values.Contains(value)) return;
            values.Add(value);
        }

        public void RemoveValue(string value)
        {
            if (!values.Contains(value)) return;
            values.Remove(value);
        }
    }
}