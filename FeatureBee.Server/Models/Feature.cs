namespace FeatureBee.Server.Models
{
    using System.Collections.Generic;

    public class Feature
    {
        public string name { get; set; }

        public string team { get; set; }

        public int index { get; set; }

        public string link { get; set; }

        public List<Condition> conditions { get; set; }
    }

    public class Condition
    {
        public string type { get; set; }

        public string[] values { get; set; }
    }
}