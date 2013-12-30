namespace FeatureBee.Server.Domain.Models
{
    using System.Collections.Generic;

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