namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System;
    using System.Linq;

    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    public class FeatureApplicationServices : 
        ICommandHandler<CreateFeatureCommand>,
        ICommandHandler<ReleaseFeatureForEveryoneCommand>,
        ICommandHandler<ReleaseFeatureWithConditionsCommand>,
        ICommandHandler<RollbackFeatureCommand>,
        ICommandHandler<ChangeFeatureConditionsCommand>,
        ICommandHandler<UpdateDescriptionCommand>,
        ICommandHandler<LinkToTicketCommand>
    {
        private readonly IDomainRepository repository;
        private readonly FeatureBeeContext featureBeeContext = new FeatureBeeContext();

        public FeatureApplicationServices(IDomainRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(CreateFeatureCommand command)
        {
            var aggregate = FeatureAggregate.CreateNew(command.Name, command.Description, command.Team, command.Link, command.Conditions);
            repository.Save(aggregate);
        }

        public void Execute(ReleaseFeatureForEveryoneCommand command)
        {
            var aggregate = LoadAggregate(command.Name);
            aggregate.ReleaseForEveryone();
            repository.Save(aggregate);
        }

        public void Execute(ReleaseFeatureWithConditionsCommand command)
        {
            var aggregate = LoadAggregate(command.Name);
            aggregate.ReleaseWithConditions();
            repository.Save(aggregate);
        }

        public void Execute(RollbackFeatureCommand command)
        {
            var aggregate = LoadAggregate(command.Name);
            aggregate.Rollback();
            repository.Save(aggregate);

        }

        public void Execute(ChangeFeatureConditionsCommand command)
        {
            var aggregate = LoadAggregate(command.Name);
            aggregate.ChangeConditions(command.Conditions);
            repository.Save(aggregate);
        }

        public void Execute(UpdateDescriptionCommand command)
        {
            var aggregate = LoadAggregate(command.Name);
            aggregate.UpdateDescription(command.Description);
            repository.Save(aggregate);
        }

        public void Execute(LinkToTicketCommand command)
        {
            var aggregate = LoadAggregate(command.Name);
            aggregate.LinkToTicket(command.Link);
            repository.Save(aggregate);
        }

        private FeatureAggregate LoadAggregate(string name)
        {
            var feature = featureBeeContext.Features.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (feature == null)
                return null;

            var events = repository.GetById(feature.Id);

            return new FeatureAggregate(events);
        }
    }
}