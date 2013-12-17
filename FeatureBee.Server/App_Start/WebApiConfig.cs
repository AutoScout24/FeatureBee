using System.Web.Http;

namespace FeatureBee.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Staging",
                routeTemplate: "api/features/staging/{id}",
                defaults: new { controller = "FeatureBeeStagingApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Live",
                routeTemplate: "api/features/live/{id}",
                defaults: new { controller = "FeatureBeeLiveApi", id = RouteParameter.Optional }
            );
        }
    }
}
