namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class ReleaseFeatureForEveryoneCommand : ICommand
    {
        public string Name { get; private set; }

        public ReleaseFeatureForEveryoneCommand(string name)
        {
            Name = name;
        }
    }
}