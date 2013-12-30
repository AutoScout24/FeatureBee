namespace FeatureBee.Server.Domain.ApplicationServices
{
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;

    public class FeatureApplicationServices : 
        ICommandHandler<CreateFeatureCommand>,
        ICommandHandler<ReleaseFeatureCommand>,
        ICommandHandler<TestFeatureCommand>,
        ICommandHandler<RollbackFeatureCommand>,
        ICommandHandler<ChangeFeatureConditionsCommand>
    {
        private readonly IDomainRepository repository;

        public FeatureApplicationServices(IDomainRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(CreateFeatureCommand command)
        {
            var aggregate = FeatureAggregate.CreateNew(command.Name, command.Team, command.Conditions);
            repository.Save(aggregate);
        }

        public void Execute(ReleaseFeatureCommand command)
        {
            var events = repository.GetById(command.Id);

            var aggregate = new FeatureAggregate(events);
            aggregate.Release();
            repository.Save(aggregate);
        }

        public void Execute(TestFeatureCommand command)
        {
            var events = repository.GetById(command.Id);

            var aggregate = new FeatureAggregate(events);
            aggregate.Test();
            repository.Save(aggregate);
        }

        public void Execute(RollbackFeatureCommand command)
        {
            var events = repository.GetById(command.Id);

            var aggregate = new FeatureAggregate(events);
            aggregate.Rollback();
            repository.Save(aggregate);

        }

        public void Execute(ChangeFeatureConditionsCommand command)
        {
            var events = repository.GetById(command.Id);

            var aggregate = new FeatureAggregate(events);
            aggregate.ChangeConditions(command.Conditions);
            repository.Save(aggregate);
        }
    }
}