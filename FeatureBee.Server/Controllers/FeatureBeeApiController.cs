using System.Collections.Generic;
using System.Web.Http;

namespace FeatureBee.Server.Controllers
{
    using System.Linq;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

    public class FeatureBeeApiController : ApiController
    {
        private readonly IFeatureRepository repository;

        public FeatureBeeApiController(IFeatureRepository repository)
        {
            this.repository = repository;
        }

        // GET api/features
        public IEnumerable<Feature> Get()
        {
            return repository.Collection();
        }

        // GET api/features/myfeature
        public Feature Get(string id)
        {
            return repository.Collection().FirstOrDefault(_ => _.title == id);
        }
    }
}
