namespace FeatureBee.Server.Data.Features
{
    using System.Collections.Generic;
    using System.Linq;

    using FeatureBee.Server.Models;

    public interface IFeatureRepository
    {
        IQueryable<Feature> Collection();

        void Update(string title, Feature feature);
    }

    public class FeatureRepository : IFeatureRepository
    {
        private static readonly Dictionary<string,Feature> Features = new Dictionary<string,Feature>
        {
            { "a" , new Feature { title= "a", team= "asm", index= 0, link="https://jira.as24.local/browse/PMVM-1030" } },
            { "b" , new Feature { title= "b", team= "dealer", index= 1, link="https://jira.as24.local/browse/PMVM-1030" } },
            {"lala" , new Feature { title= "lala", team= "asm", index= 0, link="https://jira.as24.local/browse/PMVM-1030" } },
            {"tata" , new Feature { title= "tata", team= "asm", index= 2, link="https://jira.as24.local/browse/PMVM-1030" } },
            {"erw" , new Feature { title= "erw", team= "", index= 0, link="https://jira.as24.local/browse/PMVM-1030" } }
        };

        public IQueryable<Feature> Collection()
        {
            return Features.Values.AsQueryable();
        }

        public void Update(string title, Feature feature)
        {
            if (Features.ContainsKey(title)) Features.Remove(title);

            Features.Add(feature.title, feature);
        }
    }
}