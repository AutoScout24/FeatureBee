namespace FeatureBee.Server.Controllers
{
    using System.Linq;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    using Microsoft.AspNet.SignalR;

    public class EditPanelHub : Hub
    {
        private readonly IFeatureRepository featureRepository;

        public EditPanelHub(IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
        }

        public void AddConditionValue(string name, string type, string[] values)
        {
            var feature = this.featureRepository.Collection().FirstOrDefault(_ => _.name == name);
            var condition = feature.conditions.FirstOrDefault(_ => _.type == type);
            if (condition == null)
            {
                condition = new Condition() { type = type };
                feature.conditions.Add(condition);
            }

            condition.AddValue(string.Join(",", values));
            this.ConditionValueAdded(feature);
        }

        public void EditItem(string oldName, Feature feature)
        {
            featureRepository.Save(oldName, feature);
            this.ItemEdited(feature);
        }

        public void ConditionValueAdded(Feature feature)
        {
            Clients.All.conditionValueAddedToFeature(feature);
        }

        public void ItemEdited(Feature item)
        {
            Clients.All.itemEdited(item);
        }
    }
}