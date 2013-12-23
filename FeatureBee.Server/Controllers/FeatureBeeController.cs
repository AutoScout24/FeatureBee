using System.Web.Mvc;

namespace FeatureBee.Server.Controllers
{
    using System;
    using System.Linq;

    using FeatureBee.Server.Data.Features;
    using FeatureBee.Server.Models;

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
            try
            {
                return Json(featureRepository.Collection().ToArray());
            }
            catch (Exception)
            {
                return Json(new Feature[0]);
            }
        }
    }
}
