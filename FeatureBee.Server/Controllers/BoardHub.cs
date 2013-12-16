namespace FeatureBee.Server.Controllers
{
    using System.Linq.Expressions;

    using Microsoft.AspNet.SignalR;

    public class BoardHub : Hub
    {
        public void AddNewItem(string name, string team)
        {
            this.NewItemAdded(new { title = name, team = team, index = 0 });
        }

        public void EditItem(string name, string team, int index)
        {
            this.ItemEdited(new { title = name, team = team, index = index });
        }

        public void ItemEdited(dynamic item)
        {
            Clients.All.itemEdited(item);
        }

        public void NewItemAdded(dynamic item)
        {
            Clients.All.newItemAdded(item);
        }

        public void Move(string name, int oldIndex, int newIndex)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastItemMoved(new { title = name, oldIndex = oldIndex, newIndex = newIndex });
        }
    }
}