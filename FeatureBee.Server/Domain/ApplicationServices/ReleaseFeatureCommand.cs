namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System;

    public class ReleaseFeatureCommand : ICommand
    {
        public ReleaseFeatureCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}