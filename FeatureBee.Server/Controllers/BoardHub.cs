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

        public void AddNewItem(string name, string team)
        {
            var feature = new Feature{ title = name, team = team, index = 0 };
            featureRepository.Update(name, feature);
            this.NewItemAdded(feature);
        }

        public void EditItem(string oldName, string name, string team, int index)
        {
            var feature = new Feature { title = name, team = team, index = index };
            featureRepository.Update(oldName, feature);
            this.ItemEdited(feature);
        }

        public void MoveItem(string name, int oldIndex, int newIndex)
        {
            var feature = featureRepository.Collection().FirstOrDefault(f => f.title == name);
            if (feature == null) return;

            feature.index = newIndex;
            featureRepository.Update(name, feature);
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

        public void NewItemAdded(Feature item)
        {
            Clients.All.newItemAdded(item);
        }
    }
}