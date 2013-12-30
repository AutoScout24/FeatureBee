namespace FeatureBee.Server.Models
{
    using System.Linq;

    public interface IFeatureReadRepository
    {
        IQueryable<FeatureViewModel> Collection();
    }
}