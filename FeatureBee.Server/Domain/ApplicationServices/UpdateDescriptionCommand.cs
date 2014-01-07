namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class UpdateDescriptionCommand : ICommand
    {
        public UpdateDescriptionCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}