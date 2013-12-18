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
        
        public void CreateCondition(string name, string type)
        {
            var feature = this.featureRepository.Collection().Single(_ => _.name == name);
            var condition = feature.conditions.FirstOrDefault(_ => _.type == type);

            if (condition == null)
            {
                feature.conditions.Add(new Condition() { type = type });
            }

            this.ConditionCreated(feature);
        }

        public void AddConditionValue(string name, string type, string[] values)
        {
            var feature = this.featureRepository.Collection().Single(_ => _.name == name);
            var condition = feature.conditions.FirstOrDefault(_ => _.type == type);
            if (condition == null)
            {
                condition = new Condition() { type = type };
                feature.conditions.Add(condition);
            }

            condition.AddValue(string.Join("-", values));
            this.ConditionValueAdded(feature);
        }

        public void RemoveConditionValue(string name, string type, string[] values)
        {
            var feature = this.featureRepository.Collection().Single(_ => _.name == name);
            var condition = feature.conditions.FirstOrDefault(_ => _.type == type);
            if (condition != null)
            {
                condition.RemoveValue(string.Join("-", values));
                if (!condition.values.Any())
                {
                    feature.conditions.Remove(condition);
                }
            }

            this.ConditionValueRemoved(feature);
        }

        public void EditItem(string oldName, Feature feature)
        {
            featureRepository.Save(oldName, feature);
            this.ItemEdited(feature);
        }

        public void ConditionCreated(Feature feature)
        {
            Clients.All.conditionCreatedForFeature(feature);
        }

        public void ConditionValueAdded(Feature feature)
        {
            Clients.All.conditionValueAddedToFeature(feature);
        }

        public void ConditionValueRemoved(Feature feature)
        {
            Clients.All.conditionValueRemovedFromFeature(feature);
        }

        public void ItemEdited(Feature item)
        {
            Clients.All.itemEdited(item);
        }
    }
}