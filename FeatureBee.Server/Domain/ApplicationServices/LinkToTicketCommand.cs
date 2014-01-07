namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System;

    public class LinkToTicketCommand : ICommand
    {
        public LinkToTicketCommand(Guid id, string link)
        {
            Link = link;
            Id = id;
        }

        public Guid Id { get; private set; }
        public string Link { get; private set; }
    }
}