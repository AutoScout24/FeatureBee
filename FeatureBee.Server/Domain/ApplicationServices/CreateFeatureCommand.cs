namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Models;

    public class CreateFeatureCommand : ICommand
    {
        public CreateFeatureCommand(string name, string description, string link, string team, List<Condition> conditions)
        {
            Conditions = conditions ?? new List<Condition>();
            Description = description;
            Link = link;
            Team = team;
            Name = name;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Link { get; private set; }
        public string Team { get; private set; }
        public List<Condition> Conditions { get; private set; }
    }
}