namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class ReleaseFeatureCommand : ICommand
    {
        public string Name { get; private set; }

        public ReleaseFeatureCommand(string name)
        {
            Name = name;
        }
    }
}