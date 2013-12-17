namespace FeatureBee.Server.App_Start
{
    using System;
    using System.Reflection;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.SignalR;
    using Autofac.Integration.WebApi;

    using Module = Autofac.Module;

    public class DIConfiguration : Module
    {
        internal IContainer BuildApplicationContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(this);

            return builder.Build();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register the SignalR hubs.
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Repository", StringComparison.Ordinal))
                .AsImplementedInterfaces();
        }
    }
}