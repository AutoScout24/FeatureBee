namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FeatureReadRepository : IFeatureReadRepository
    {
        public IQueryable<FeatureViewModel> Collection()
        {
            var x = new List<FeatureViewModel>
            {
                new FeatureViewModel {Id = Guid.NewGuid(), Name = "XY-1871", State = "In Development"}
            };
            return x.AsQueryable();

            //.Select(
            //    feature => new FeatureViewModel
            //    {
            //        Name = feature.name,
            //        Conditions = feature.conditions,
            //        State = new StateMapper().Map(feature.index)
            //    });
            // return eventStore.All<Feature>().AsQueryable();
        }
    }
}