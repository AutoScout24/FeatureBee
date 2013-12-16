using System.Web.Mvc;

namespace FeatureBee.Server.Controllers
{
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

        public JsonResult Features()
        {
            return Json(featureRepository.Collection());
        }
    }
}
