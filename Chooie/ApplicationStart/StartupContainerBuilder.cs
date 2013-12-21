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
            TinyIoCContainer logContainer = CreateHelperLogBuilderContainer(container);
            var threadSafeLog = new ThreadSafeLog(logContainer.Resolve<CompositeLog>());
            container.Register<ILog, ThreadSafeLog>(threadSafeLog);
            container.Register<ILogger>(
                (c, p) => new Logger(new Context("Chooie.Startup"), c.Resolve<ILog>()));
        }

        private static TinyIoCContainer CreateHelperLogBuilderContainer(TinyIoCContainer container)
        {
            var logContainer = new TinyIoCContainer();
            logContainer.Register<IClientMessenger>((c, p) => container.Resolve<IClientMessenger>());
            logContainer.Register<ILog>((c, p) => container.Resolve<IMemoryLog>());
            logContainer.Register<FileLogFileNameProvider>();
            logContainer.Register<ILog, FileLog>();
            logContainer.Register<CompositeLog>().AsSingleton();
            return logContainer;
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
