namespace FeatureBee.Server
{
    using System.Web.Http;

    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Enable query support for actions with an IQueryable or IQueryable<T> return type.
            config.EnableQuerySupport();
        }
    }
}