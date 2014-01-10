namespace FeatureBee.Server.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

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