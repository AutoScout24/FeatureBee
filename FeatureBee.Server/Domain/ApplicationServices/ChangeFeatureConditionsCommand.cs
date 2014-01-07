namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Models;

    public class ChangeFeatureConditionsCommand : ICommand
    {
        public ChangeFeatureConditionsCommand(string name, List<Condition> conditions)
        {
            Name = name;
            Conditions = conditions;
        }

        public string Name { get; private set; }
        public List<Condition> Conditions { get; private set; }
    }
}