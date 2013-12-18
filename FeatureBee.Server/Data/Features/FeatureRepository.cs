namespace FeatureBee.Server.Data.Features
{
    using System.Collections.Generic;
    using System.Linq;

    using FeatureBee.Server.Models;

    public interface IFeatureRepository
    {
        IQueryable<Feature> Collection();

        void Save(string title, Feature feature);
    }

    public class FeatureRepository : IFeatureRepository
    {
        private static readonly Dictionary<string,Feature> Features = new Dictionary<string,Feature>
        {
            { "PMVM-1150 Booking Overview" , new Feature { name= "PMVM-1150 Booking Overview", team= "asm", index= 0, link="https://jira.as24.local/browse/PMVM-1030", conditions = new List<Condition>{ new Condition {type="culture", values = new List<string> {"de-DE", "de-AT"}}, new Condition { type="browser", values = new List<string> {"chrome", "firefox"}}} }  },
            { "PMVM-442 SalesForce VII: WebForm Regsitration" , new Feature { name= "PMVM-442 SalesForce VII: WebForm Regsitration", team= "dealer", index= 1, link="https://jira.as24.local/browse/PMVM-1030" } },
            {"PMVM-1589 Navigation 2.0" , new Feature { name= "PMVM-1589 Navigation 2.0", team= "asm", index= 0, link="https://jira.as24.local/browse/PMVM-1030", conditions = new List<Condition>{ new Condition {type="culture", values = new List<string> {"de-AT"}}, new Condition { type="trafficDistribution", values = new List<string> {"0%", "50%"}}} } },
            {"PMVM-1238 Rating Comments" , new Feature { name= "PMVM-1238 Rating Comments", team= "asm", index= 2, link="https://jira.as24.local/browse/PMVM-1030" } },
            {"erw" , new Feature { name= "erw", team= "", index= 0, link="https://jira.as24.local/browse/PMVM-1030" } }
        };

        public IQueryable<Feature> Collection()
        {
            return Features.Values.AsQueryable();
        }

        public void Save(string title, Feature feature)
        {
            if (Features.ContainsKey(title)) Features.Remove(title);

            Features.Add(feature.name, feature);
        }
    }
}