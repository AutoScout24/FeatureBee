namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System;

    public class UpdateDescriptionCommand : ICommand
    {
        public UpdateDescriptionCommand(Guid id, string description)
        {
            Description = description;
            Id = id;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
    }
}