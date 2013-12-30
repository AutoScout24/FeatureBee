namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System;

    public class TestFeatureCommand : ICommand
    {
        public TestFeatureCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}