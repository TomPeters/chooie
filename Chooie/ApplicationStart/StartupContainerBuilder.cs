using System;
using Chooie.ApplicationContext;
using Chooie.Database;
using Chooie.Interface.Logging;
using Chooie.Interface.TinyIoC;
using Chooie.Logging;
using Chooie.Nancy;
using Chooie.SignalR;
using Nancy.Hosting.Self;

namespace Chooie.ApplicationStart
{
    public class StartupContainerFactory
    {
        public TinyIoCContainer CreateStartupContainer()
        {
            var container = new TinyIoCContainer();

            // Logger
            container.Register<ILog>((cContainer, overloads) => new FileLog("log.txt"));  // TODO: Don't hardcode this here - it should be a part of settings
            container.Register<ILogger>((cContainer, overloads) => new Logger(new Context("Chooie.Startup"), cContainer.Resolve<ILog>()));

            // SignalR
            container.Register<SignalRStarter>();
            container.Register<SignalRUriProvider>();

            // Database
            container.Register<DatabaseStarter>();
            container.Register<IDatabaseAccessor, DatabaseAccessor>();

            // Nancy
            container.Register<NancyBaseUriProvider>();
            container.Register<ChooieHostConfigurationProvider>();
            container.Register<NancyDependencyContainerBuilder>();
            container.Register<ChooieBootstrapper>();
            var nancyHost = new NancyHost(
                container.Resolve<ChooieBootstrapper>(),
                container.Resolve<ChooieHostConfigurationProvider>().HostConfiguration,
                container.Resolve<NancyBaseUriProvider>().Uri);
            container.Register<NancyHost>(nancyHost);
            container.Register<NancyStarter>();
            
            // Application Context
            container.Register<ChooieSystemTrayApplicationStarter>();

            // Starter
            container.Register<ApplicationStarterProvider>((c,p) => new ApplicationStarterProvider(c));
            container.Register<ApplicationStarter>();

            return container;
        }
    }
}
