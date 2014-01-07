namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ConditionViewModel> Conditions { get; set; }
        public string State { get; set; }
        public int Index { get; set; }
        public string Link { get; set; }
        public string Team { get; set; }
        public string Description { get; set; }
    }

    public class ConditionViewModel
    {
        [Key]
        public string Type { get; set; }
        public List<string> Values { get; set; }
    } 
}