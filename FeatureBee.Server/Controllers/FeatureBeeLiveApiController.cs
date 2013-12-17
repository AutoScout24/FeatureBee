namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    public class FeatureBeeLiveApiController : ApiController
    {
        private readonly IFeatureRepository repository;

        public FeatureBeeLiveApiController(IFeatureRepository repository)
        {
            this.repository = repository;
        }

        // GET api/features
        public IEnumerable<dynamic> Get()
        {
            return this.repository.Collection().Where(_ => _.index > 0).Select(
                feature => new
                           {
                               name = feature.name,
                               conditions = feature.conditions
                           });
        }

        // GET api/features/myfeature
        public Feature Get(string id)
        {
            return this.repository.Collection().FirstOrDefault(_ => _.name == id);
        }
    }
}