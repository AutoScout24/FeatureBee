namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System;

    public class RollbackFeatureCommand : ICommand
    {
        public RollbackFeatureCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}