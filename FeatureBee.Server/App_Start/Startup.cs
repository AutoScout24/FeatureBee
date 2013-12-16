using Microsoft.Owin;

[assembly: OwinStartup(typeof(FeatureBee.Server.Startup))]
namespace FeatureBee.Server
{
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}