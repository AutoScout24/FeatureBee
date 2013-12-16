namespace FeatureBee.Server
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using System.Web.Optimization;

    using Autofac;
    using Autofac.Integration.Mvc;

    using FeatureBee.Server.App_Start;

    using Microsoft.AspNet.SignalR;

    using IDependencyResolver = Microsoft.AspNet.SignalR.IDependencyResolver;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IContainer container = new DIConfiguration().BuildApplicationContainer();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // Configure SignalR with the dependency resolver.
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}