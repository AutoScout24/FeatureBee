namespace FeatureBee.Server.Controllers
{
    using System.Linq;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    using Microsoft.AspNet.SignalR;

    public class BoardHub : Hub
    {
        private readonly IFeatureRepository featureRepository;
        
        public BoardHub(IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
        }

        public void AddNewItem(Feature feature)
        {
            featureRepository.Save(feature.title, feature);
            this.NewItemAdded(feature);
        }

        public void EditItem(string oldName, Feature feature)
        {
            featureRepository.Save(oldName, feature);
            this.ItemEdited(feature);
        }

        public void MoveItem(string name, int oldIndex, int newIndex)
        {
            var feature = featureRepository.Collection().FirstOrDefault(f => f.title == name);
            if (feature == null) return;

            feature.index = newIndex;
            featureRepository.Save(name, feature);
            this.ItemMoved(feature);
        }

        public void ItemMoved(Feature item)
        {
            Clients.All.itemMoved(item);
        }

        public void ItemEdited(Feature item)
        {
            Clients.All.itemEdited(item);
        }

        public virtual void NewItemAdded(Feature item)
        {
            Clients.All.newItemAdded(item);
        }
    }
}