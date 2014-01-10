namespace FeatureBee.Server
{
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;

    using FeatureBee.Server.Migrations;
    using FeatureBee.Server.Models;

    using Microsoft.AspNet.SignalR;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Error += (sender, args) => Debug.Write(args);

            (new FeatureBeeContext()).Initialize(true);

            var container = new DIConfiguration().BuildApplicationContainer();

            // Configure MVC with the dependency resolver.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Configure SignalR with the dependency resolver.
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}