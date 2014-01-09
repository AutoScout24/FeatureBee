namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class RollbackFeatureCommand : ICommand
    {
        public string Name { get; private set; }

        public RollbackFeatureCommand(string name)
        {
            Name = name;
        }
    }
}