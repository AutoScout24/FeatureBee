namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class ReleaseFeatureWithConditionsCommand : ICommand
    {
        public string Name { get; private set; }

        public ReleaseFeatureWithConditionsCommand(string name)
        {
            Name = name;
        }
    }
}