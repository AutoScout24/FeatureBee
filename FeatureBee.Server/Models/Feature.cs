namespace FeatureBee.Server.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Feature
    {
        public Feature()
        {
            conditions = new List<Condition>();
        }

        public string name { get; set; }

        public string team { get; set; }

        public int index { get; set; }

        public string link { get; set; }

        public List<Condition> conditions { get; set; }
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
    }
}