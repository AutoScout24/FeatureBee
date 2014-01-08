namespace FeatureBee.Server.Controllers
{
    using System;

    using FeatureBee.Server.Domain.ApplicationServices;
    using FeatureBee.Server.Domain.Infrastruture;

    using Microsoft.AspNet.SignalR;

    public class BoardHub : Hub
    {
        private readonly ICommandSender commandSender;

        public BoardHub(ICommandSender commandSender)
        {
            this.commandSender = commandSender;
        }

        public void AddNewItem(CreateFeatureCommand command)
        {
            commandSender.Send(command);
        }

        public void MoveItem(string name, int newIndex)
        {
            switch (newIndex)
            {
                case 0:
                    commandSender.Send(new RollbackFeatureCommand(name));
                    break;

                case 1:
                    commandSender.Send(new ReleaseFeatureWithConditionsCommand(name));
                    break;

                case 2:
                    commandSender.Send(new ReleaseFeatureForEveryoneCommand(name));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}