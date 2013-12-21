using Chooie.Interface.Logging;
using Chooie.Interface.PackageManager;
using Chooie.Database;
using Chooie.Jobs;
using Chooie.Logging;
using Chooie.PackageManager;
using Chooie.Plugin;
using Chooie.SignalR;
using Nancy.TinyIoc;

namespace Chooie.Nancy
{
    public class NancyContainerBuilder
    {
        private readonly Interface.TinyIoC.TinyIoCContainer _startupContainer;

        public NancyContainerBuilder(Interface.TinyIoC.TinyIoCContainer startupContainer)
        {
            _startupContainer = startupContainer;
        }

        public void ConfigureContainer(TinyIoCContainer container)
        {
            RegisterLoggerTypes(container);
            RegisterJobTypes(container);
            RegisterSignalRTypes(container);
            RegisterDatabaseTypes(container);
            RegisterPackageManagerTypes(container);
        }

        private void RegisterLoggerTypes(TinyIoCContainer container)
        {
            container.Register<ILogger>((c, p) => new Logger(new Context("Chooie"), _startupContainer.Resolve<ILog>()));
            container.Register<IMemoryLog>((c, p) => _startupContainer.Resolve<IMemoryLog>());
        }

        private static void RegisterJobTypes(TinyIoCContainer container)
        {
            container.Register<IJobFactory, JobFactory>();
            container.Register<IJobListUpdater, JobListUpdater>();
            container.Register<IJobQueue, JobQueue>().AsSingleton();
        }

        private void RegisterSignalRTypes(TinyIoCContainer container)
        {
            container.Register<IClientMessenger>((c, p) => _startupContainer.Resolve<IClientMessenger>());
        }

        private void RegisterDatabaseTypes(TinyIoCContainer container)
        {
            container.Register<IDatabaseManager, DatabaseManager>().AsSingleton();
            container.Register<IDatabaseAccessor>((c, p) => _startupContainer.Resolve<IDatabaseAccessor>());
        }

        private void RegisterPackageManagerTypes(TinyIoCContainer container)
        {
            container.Register<IPluginContainerProvider>((c, p) => _startupContainer.Resolve<PluginContainerBuilder>());
            container.Register<IPluginNameProvider>((c, p) => _startupContainer.Resolve<PluginContainerBuilder>());
            container.Register<IPackageManagerProvider, PackageManagerProvider>();
            container.Register<IPackageManagerSettings, PackageManagerSettings>().AsSingleton();
            container.Register<IPackageList, PackageList>().AsSingleton();
            container.Register<IPackageManager, PackageManagerProxy>().AsSingleton();
            container.Register<IPackageManagerProxy, PackageManagerProxy>().AsSingleton();
        }
    }
}
