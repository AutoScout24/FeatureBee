namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Models;

    public class CreateFeatureCommand : ICommand
    {
        public CreateFeatureCommand(string name, string team, string link, List<Condition> conditions)
        {
            Conditions = conditions;
            Team = team;
            Link = link;
            Name = name;
        }

        public string Name { get; private set; }
        public string Team { get; private set; }
        public string Link { get; private set; }
        public List<Condition> Conditions { get; private set; }
    }
}