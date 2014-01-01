namespace FeatureBee.Server.Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Condition
    {
        public Condition()
        {
            Values = new List<string>();
        }

        [Key]
        public string Type { get; set; }

        public List<string> Values { get; set; }
    }
}