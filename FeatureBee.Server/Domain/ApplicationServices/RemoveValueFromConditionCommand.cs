namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class RemoveValueFromConditionCommand : ICommand
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string[] Values { get; set; }

        public RemoveValueFromConditionCommand(string name, string type, string[] values)
        {
            this.Name = name;
            this.Type = type;
            this.Values = values;
        }
    }
}