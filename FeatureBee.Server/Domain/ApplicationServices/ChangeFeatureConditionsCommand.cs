namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Models;

    public class ChangeFeatureConditionsCommand : ICommand
    {
        public ChangeFeatureConditionsCommand(Guid id, List<Condition> conditions)
        {
            Conditions = conditions;
            Id = id;
        }

        public Guid Id { get; private set; }
        public List<Condition> Conditions { get; private set; }
    }
}