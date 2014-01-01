namespace FeatureBee.Server.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using FeatureBee.Server.Models;

    [Route("api/features")]
    public class FeatureBeeApiController : ApiController
    {
        private readonly FeatureBeeContext context;

        public FeatureBeeApiController()
        {
            context = new FeatureBeeContext();
        }

        // GET api/features
        public IQueryable<FeatureViewModel> Get()
        {
            return context.Features;
        }

        // GET api/features/myfeature
        public FeatureViewModel Get(string id)
        {
            return context.Features.FirstOrDefault(x => x.Name.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}