namespace FeatureBee.Server.Models
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using FeatureBee.Server.Migrations;

    public class FeatureBeeContext : DbContext
    {
        public FeatureBeeContext()
            : base("FeatureBeeContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<FeatureBeeContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FeatureBeeContext, Configuration>());
        }

        public DbSet<FeatureViewModel> Features { get; set; }

        public List<FeatureViewModel> LoadedFeatures()
        {
            return this.Features.Include("Conditions.Values").ToList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Initialize(bool force)
        {
            Database.Initialize(force);
        }
    }
}