using System.Web.Mvc;

namespace FeatureBee.Server.Controllers
{
    using System.Linq;

    using FeatureBee.Server.Data.Features;

    public class FeatureBeeController : Controller
    {
        private readonly IFeatureRepository featureRepository;

        public FeatureBeeController(IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Features()
        {
            return Json(featureRepository.Collection().ToArray());
        }
    }
}
