using System.Collections.Generic;
using Chooie.Database;
using Chooie.Interface.Logging;
using Chooie.Interface.TinyIoC;
using Chooie.Logging;
using Chooie.Nancy;
using Chooie.PackageManager;
using Chooie.Plugin;
using Chooie.SignalR;
using Chooie.SystemTrayApplication;

namespace Chooie.ApplicationStart
{
    public class StartupContainerFactory
    {
        public TinyIoCContainer CreateStartupContainer()
        {
            var container = new TinyIoCContainer();
            RegisterLoggerTypes(container);
            RegisterPluginTypes(container);
            RegisterSignalRTypes(container);
            RegisterDatabaseTypes(container);
            RegisterNancyTypes(container);
            RegisterSystemTrayApplicationTypes(container);
            RegisterApplicationStarterTypes(container);

            return container;
        }

        private static void RegisterLoggerTypes(TinyIoCContainer container)
        {
            container.Register<IClientMessenger, ClientMessenger>().AsSingleton();
            container.Register<IMemoryLog, MemoryLog>().AsSingleton();
            container.Register<ILog>((c, p) => new CompositeLog(new List<ILog>
                {
                    new FileLog("log.txt"), // TODO: Don't hardcode this here - it should be a part of settings
                    c.Resolve<IMemoryLog>()
                }));
            container.Register<ILogger>(
                (cContainer, overloads) => new Logger(new Context("Chooie.Startup"), cContainer.Resolve<ILog>()));
        }

        private static void RegisterPluginTypes(TinyIoCContainer container)
        {
            container.Register<AssemblyLoader>().AsSingleton();
            container.Register<PluginContainerBuilder>().AsSingleton();
            container.Register<IPluginJudge, PackageManagerPluginJudge>();
            container.Register<IContainerFactory, PackageManagerContainerFactory>();
            container.Register<PluginStarter>();
        }

        private static void RegisterSignalRTypes(TinyIoCContainer container)
        {
            container.Register<SignalRStarter>();
            container.Register<SignalRUriProvider>();
        }

        private static void RegisterDatabaseTypes(TinyIoCContainer container)
        {
            container.Register<DatabaseFileNameProvider>();
            container.Register<IDatabaseAccessor, DatabaseAccessor>();
            container.Register<DatabaseStarter>();
        }

        private static void RegisterNancyTypes(TinyIoCContainer container)
        {
            container.Register<NancyBaseUriProvider>();
            container.Register<HostConfigurationProvider>();
            container.Register<NancyContainerBuilder>((c, p) => new NancyContainerBuilder(c));
            container.Register<Bootstrapper>();
            container.Register<NancyHostProvider>().AsSingleton();
            container.Register<NancyStarter>();
        }

        private static void RegisterSystemTrayApplicationTypes(TinyIoCContainer container)
        {
            container.Register<SystemTrayApplicationStarter>();
        }

        private static void RegisterApplicationStarterTypes(TinyIoCContainer container)
        {
            container.Register<ApplicationStarterProvider>((c, p) => new ApplicationStarterProvider(c));
            container.Register<ApplicationStarter>();
        }
    }
}
