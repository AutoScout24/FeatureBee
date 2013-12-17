namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    using Machine.Fakes;
    using Machine.Specifications;

    public class FeatureBeeLiveApiControllerSpecs : WithSubject<FeatureBeeLiveApiController>
    {
        Establish context = () => The<IFeatureRepository>().WhenToldTo(_ => _.Collection()).Return(new List<Feature>()
                                                                                                   {
                                                                                                       new Feature() { index = 0 },
                                                                                                       new Feature() { index = 1 }
                                                                                                   }.AsQueryable<Feature>());

        public class When_Getting_The_Feature
        {
            Because of = () => result = Subject.Get();

            It should_not_return_any_features_in_development = () => result.ShouldNotContain(_ => _.index == 0);

            private static IEnumerable<FeatureResult> result; 
        }
    }
}