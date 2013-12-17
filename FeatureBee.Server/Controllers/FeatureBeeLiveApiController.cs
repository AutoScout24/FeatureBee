namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    public class FeatureResult
    {
        public string name { get; set; }
        public List<Condition> conditions { get; set; }
        public int index { get; set; }
    }

    public class FeatureBeeLiveApiController : ApiController
    {
        private readonly IFeatureRepository repository;

        public FeatureBeeLiveApiController(IFeatureRepository repository)
        {
            this.repository = repository;
        }

        // GET api/features
        public IEnumerable<FeatureResult> Get()
        {
            return this.repository.Collection().Where(_ => _.index > 0).Select(
                feature => new FeatureResult
                           {
                               name = feature.name,
                               conditions = feature.conditions,
                               index = feature.index
                           });
        }

        // GET api/features/myfeature
        public Feature Get(string id)
        {
            return this.repository.Collection().FirstOrDefault(_ => _.name == id);
        }
    }
}