namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class NewConditionCommand : ICommand
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public NewConditionCommand(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}