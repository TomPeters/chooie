using System.Collections.Generic;
using Chooie.Interface.Logging;
using Chooie.Interface.PackageManager;
using Chooie.Database;
using Chooie.Jobs;
using Chooie.Logging;
using Chooie.PackageManager;
using Chooie.SignalR;
using Nancy.TinyIoc;

namespace Chooie.Nancy
{
    public class NancyDependencyContainerBuilder
    {
        private PackageManagerSettings _packageManagerSettings;

        private PackageManagerSettings PackageManagerSettings
        {
            get
            {
                if (_packageManagerSettings == null)
                {
                    _packageManagerSettings = new PackageManagerSettings {PackageManagerType = PackageManagerProvider.GetInitialPackageManagerType() };
                }
                return _packageManagerSettings;
            }
        }

        private PackageManagerProvider _packageManagerProvider;

        private PackageManagerProvider PackageManagerProvider
        {
            get
            {
                if (_packageManagerProvider == null)
                {
                    var packageManagerProvider = new PackageManagerProvider(new ContainerFactory(Log), new AssemblyLoader());
                    packageManagerProvider.BuildContainers();
                    _packageManagerProvider = packageManagerProvider;
                }
                return _packageManagerProvider;
            }
        }

        private IMemoryLog _memoryLog;

        private IMemoryLog MemoryLog
        {
            get
            {
                if (_memoryLog == null)
                {
                    _memoryLog = new MemoryLog(ClientMessenger);
                }
                return _memoryLog;
            }
        }

        private ILogger _logger;

        public ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new Logger(new Context("Chooie"), Log);
                }
                return _logger;
            }
        }

        private ILog _log;

        public ILog Log
        {
            get
            {
                if (_log == null)
                {
                    _log = new CompositeLog(new List<ILog>
                        {
                            new FileLog("log.txt"), // TODO: Don't hardcode this here - it should be a part of settings
                            MemoryLog
                        });
                }
                return _log;
            }
        }

        private IClientMessenger _clientMessenger;

        private IClientMessenger ClientMessenger
        {
            get
            {
                if (_clientMessenger == null)
                {
                    _clientMessenger = new ClientMessenger();
                }
                return _clientMessenger;
            }
        }

        public void ConfigureContainer(TinyIoCContainer container)
        {
            container.Register<ILogger>(Logger);
            container.Register<IMemoryLog>(MemoryLog);
            container.Register<IJobFactory, JobFactory>();
            container.Register<IJobListUpdater, JobListUpdater>();
            container.Register<IPackageList, PackageList>().AsSingleton();
            container.Register<IJobQueue, JobQueue>().AsSingleton();

            container.Register<IClientMessenger>(ClientMessenger);
            container.Register(PackageManagerSettings);
            container.Register<IDatabaseManager, DatabaseManager>().AsSingleton();
            container.Register<IDatabaseAccessor, DatabaseAccessor>().AsSingleton();
            var packageManagerProxy = new PackageManagerProxy(PackageManagerProvider, PackageManagerSettings, container.Resolve<IClientMessenger>(), container.Resolve<IJobQueue>(), container.Resolve<IPackageList>());
            container.Register<IPackageManager>(packageManagerProxy);
            container.Register<IPackageManagerProxy>(packageManagerProxy);
        }
    }
}
