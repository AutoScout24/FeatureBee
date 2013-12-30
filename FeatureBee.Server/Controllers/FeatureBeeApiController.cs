namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Models;

    [Route("api/features")]
    public class FeatureBeeApiController : ApiController
    {
        private readonly IFeatureReadRepository repository;

        public FeatureBeeApiController(IFeatureReadRepository repository)
        {
            this.repository = repository;
        }

        // GET api/features
        public IEnumerable<FeatureViewModel> Get()
        {
            return repository.Collection();
        }

        // GET api/features/myfeature
        public FeatureViewModel Get(string id)
        {
            return repository.Collection().FirstOrDefault(_ => _.Name == id);
        }
    }
}