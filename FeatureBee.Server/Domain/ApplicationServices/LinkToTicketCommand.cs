namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class LinkToTicketCommand : ICommand
    {
        public LinkToTicketCommand(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public string Name { get; private set; }
        public string Link { get; private set; }
    }
}