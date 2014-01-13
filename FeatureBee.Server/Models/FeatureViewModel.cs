namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<ConditionViewModel> Conditions { get; set; }
        public string State { get; set; }
        public int Index { get; set; }
        public string Link { get; set; }
        public string Team { get; set; }
        public string Description { get; set; }

        public void AddCondition(string type)
        {
            if (Conditions == null) Conditions = new List<ConditionViewModel>();
            if (Conditions.Any(_ => _.Type == type)) return;
            Conditions.Add(new ConditionViewModel() { Type = type, Values = new List<ConditionValueViewModel>() });
        }
    }

    public class ConditionViewModel
    {
        [Key]
        public string Type { get; set; }
        public virtual List<ConditionValueViewModel> Values { get; set; }
        
        public void AddValue(string value)
        {
            var model = new ConditionValueViewModel(value);
            if (Values.All(v => v.Value != model.Value)) return;
            Values.Add(model);
        }

        public void RemoveValue(string value)
        {
            var model = new ConditionValueViewModel(value);
            if (Values.All(v => v.Value != model.Value)) return;
            Values.Remove(model);
        }
    }

    public class ConditionValueViewModel
    {
        public ConditionValueViewModel()
        {
        }

        public ConditionValueViewModel(string value)
        {
            this.Value = value;
        }

        [Key]
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}