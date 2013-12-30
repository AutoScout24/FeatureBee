namespace FeatureBee.Server.Controllers
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.ApplicationServices;
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;

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
            try
            {
                //var command = new CreateFeatureCommand(name, team, link, conditions);
                commandSender.Send(command);
            }
            catch (Exception)
            {
                throw new Exception("oh noes, kiddy no playz");
            }
        }

        public void MoveItem(Guid id, int oldIndex, int newIndex)
        {
            ICommand command;
            switch (newIndex)
            {
                case 0:
                    command = new RollbackFeatureCommand(id);
                    break;

                case 1:
                    command = new TestFeatureCommand(id);
                    break;

                case 2:
                    command = new ReleaseFeatureCommand(id);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            commandSender.Send(command);
        }
    }
}