namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class TestFeatureCommand : ICommand
    {
        public string Name { get; private set; }

        public TestFeatureCommand(string name)
        {
            Name = name;
        }
    }
}