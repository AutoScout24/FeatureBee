namespace FeatureBee.Server.Domain.Infrastruture
{
    using Autofac;

    using FeatureBee.Server.Domain.ApplicationServices;

    public interface ICommandSender
    {
        void Send<T>(T command) where T: ICommand;
    }

    public class CommandSender : ICommandSender
    {
        private readonly IComponentContext container;

        public CommandSender(IComponentContext container)
        {
            this.container = container;
        }

        public void Send<T>(T command) where T:ICommand
        {
            ICommandHandler<T> commandHandler;
            if (container.TryResolve(out commandHandler))
            {
                commandHandler.Execute(command);
            }
        }
    }
}