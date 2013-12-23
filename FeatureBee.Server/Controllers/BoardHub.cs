namespace FeatureBee.Server.Controllers
{
    using System;
    using System.Linq;

    using Antlr.Runtime.Misc;

    using FeatureBee.Data;
    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    using Microsoft.AspNet.SignalR;

    public class BoardHub : Hub
    {
        class BoardHubSubscriber : ISubscribe
        {
            private readonly Action<object> onNotify;

            public BoardHubSubscriber(Action<object> onNotify)
            {
                this.onNotify = onNotify;
            }

            public void Notify(object @event)
            {
                onNotify(@event);
            }
        }

        private readonly IFeatureRepository featureRepository;
        
        public BoardHub(IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
            EventStoreFactory.SubscribeTo<NewFeatureCreated>(new BoardHubSubscriber(
                o => this.NewItemAdded(((NewFeatureCreated)o).Feature)));
        }

        public void AddNewItem(Feature feature)
        {
            try
            {
                featureRepository.AddNewItem(feature.name, feature);
            }
            catch (Exception)
            {
                throw new Exception("oh noes, kiddy no playz");
            }
        }

        public void MoveItem(string name, int oldIndex, int newIndex)
        {
            var feature = featureRepository.Collection().FirstOrDefault(f => f.name == name);
            if (feature == null) return;

            feature.index = newIndex;
            featureRepository.Save(name, feature);
            this.ItemMoved(feature);
        }

        public void ItemMoved(Feature item)
        {
            Clients.All.itemMoved(item);
        }

        public void NewItemAdded(Feature item)
        {
            Clients.All.newItemAdded(item);
        }
    }
}