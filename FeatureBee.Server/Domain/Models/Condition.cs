namespace FeatureBee.Server.Domain.Models
{
    using System.Collections.Generic;

    public class Condition
    {
        public Condition()
        {
            Values = new List<string>();
        }

        public string Type { get; set; }

        public List<string> Values { get; set; }
    }

    public class ConditionEditor
    {
        public void AddValue(Condition condition, string value)
        {
            if (condition.Values.Contains(value)) return;
            condition.Values.Add(value);
        }

        public void RemoveValue(Condition condition, string value)
        {
            if (!condition.Values.Contains(value)) return;
            condition.Values.Remove(value);
        }
    }
}