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
            try
            {
                commandSender.Send(command);
            }
            catch (Exception)
            {
                throw new Exception("oh noes, kiddy no playz");
            }
        }

        public void MoveItem(Guid id, int oldIndex, int newIndex)
        {
            switch (newIndex)
            {
                case 0:
                    commandSender.Send(new RollbackFeatureCommand(id));
                    break;

                case 1:
                    commandSender.Send(new TestFeatureCommand(id));
                    break;

                case 2:
                    commandSender.Send(new ReleaseFeatureCommand(id));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}