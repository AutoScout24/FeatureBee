namespace FeatureBee.Server.Controllers
{
    using FeatureBee.Server.Domain.ApplicationServices;
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
            //var feature = this.domainRepository.Collection().Single(_ => _.name == name);
            //var condition = feature.conditions.FirstOrDefault(_ => _.type == type);

            //if (condition == null)
            //{
            //    feature.conditions.Add(new Condition() { type = type });
            //}

            //this.ConditionCreated(feature);
        }

        public void AddConditionValue(string name, string type, string[] values)
        {
            //var feature = this.domainRepository.Collection().Single(_ => _.name == name);
            //var condition = feature.conditions.FirstOrDefault(_ => _.type == type);
            //if (condition == null)
            //{
            //    condition = new Condition() { type = type };
            //    feature.conditions.Add(condition);
            //}

            //condition.AddValue(string.Join("-", values));
            //this.ConditionValueAdded(feature);
        }

        public void RemoveConditionValue(string name, string type, string[] values)
        {
            //var feature = this.domainRepository.Collection().Single(_ => _.name == name);
            //var condition = feature.conditions.FirstOrDefault(_ => _.type == type);
            //if (condition != null)
            //{
            //    condition.RemoveValue(string.Join("-", values));
            //    if (!condition.values.Any())
            //    {
            //        feature.conditions.Remove(condition);
            //    }
            //}

            //this.ConditionValueRemoved(feature);
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