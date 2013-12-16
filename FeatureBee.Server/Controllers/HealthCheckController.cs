using System.Web.Mvc;

namespace FeatureBee.Server.Controllers
{
    using System;

    public class HealthCheckController : Controller
    {
        public ActionResult Index()
        {
            return View(new XRayModel { Status = "Ok" });
        }

        public ActionResult Exception()
        {
            throw new Exception("I'm siiiiinging in the rain. Just siiiiinging in the rain.");
        }

        public ActionResult XRay()
        {
            var result = new XRayModel
                             {
                                 Status = "Ok",
                                 Read = new TimeSpan(),
                                 Write = new TimeSpan(),
                                 Delete = new TimeSpan()
                             };
            return this.View(result);
        }

        public class XRayModel
        {
            public string Status { get; set; }

            public TimeSpan Read { get; set; }

            public TimeSpan Write { get; set; }

            public TimeSpan Delete { get; set; }
        }
    }
}
