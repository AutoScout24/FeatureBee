namespace FeatureBee.Server.Migrations
{
    using System.Data.Entity.Migrations;

    using FeatureBee.Server.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FeatureBeeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FeatureBeeContext context)
        {
            //  This method will be called after migrating to the latest version.
            //context.Features.AddOrUpdate(
            //    f => f.Name,
            //    new FeatureViewModel { Id = Guid.NewGuid(), Index = 0, Name = "XY-1780", State = "In Development"},
            //    new FeatureViewModel { Id = Guid.NewGuid(), Index = 1, Name = "XY-1781", State = "Under Test"},
            //    new FeatureViewModel { Id = Guid.NewGuid(), Index = 2, Name = "XY-1782", State = "Released"}
            //    );
        }
    }
}
