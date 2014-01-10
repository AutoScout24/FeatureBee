namespace FeatureBee.Server.Controllers
{
    using FeatureBee.Server.Domain.ApplicationServices;
    using FeatureBee.Server.Domain.EventHandlers;
    using FeatureBee.Server.Domain.Infrastruture;

    using Microsoft.AspNet.SignalR;

    public class EditPanelHub : Hub
    {
        private readonly ICommandSender commandSender;

        public EditPanelHub(ICommandSender commandSender)
        {
            this.commandSender = commandSender;
        }

        public void CreateCondition(string name, string type)
        {
            commandSender.Send(new NewConditionCommand(name, type));
        }

        public void AddConditionValue(string name, string type, string[] values)
        {
            commandSender.Send(new AddValueToConditionCommand(name, type, values));
        }

        public void RemoveConditionValue(string name, string type, string[] values)
        {
            commandSender.Send(new RemoveValueFromConditionCommand(name, type, values));
        }

        public void EditItem(EditItemDto changes)
        {
            commandSender.Send(new UpdateDescriptionCommand(changes.Name, changes.Description));
            commandSender.Send(new LinkToTicketCommand(changes.Name, changes.Link));
        }
    }

    public class EditItemDto
    {
        public EditItemDto(string name, string description, string link)
        {
            Link = link;
            Description = description;
            Name = name;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Link { get; private set; }
    }
}