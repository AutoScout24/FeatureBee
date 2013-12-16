using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace FeatureBee.Server
{
    using System.Web.Optimization;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static dynamic[] Features = new []
        {
            new { title= "a", team= "asm", index= 0 },
            new { title= "b", team= "dealer", index= 1 },
            new { title= "lala", team= "asm", index= 0 },
            new { title= "tata", team= "asm", index= 2 },
            new { title= "erw", team= "", index= 0 }
        };

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}