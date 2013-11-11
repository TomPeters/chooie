using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using Chooie.Core.Logging;
using Chooie.Core.PackageManager;
using Chooie.Core.TinyIoC;

namespace Chooie.PackageManager
{
    public class ContainerFactory
    {
        private readonly ILogger _logger;
        public static readonly Type PackageManagerModuleType = typeof(IPackageManagerModule);
        private static readonly Type PackageManagerType = typeof (IPackageManager);

        public ContainerFactory(ILogger logger)
        {
            _logger = logger;
        }

        public TinyIoCContainer BuildContainer(Assembly assembly)
        {
            IPackageManagerModule module = CreateModule(assembly);
            Type packageManagerType = module.PackageManagerType;

            Debug.Assert(PackageManagerType.IsAssignableFrom(packageManagerType));

            var container = new TinyIoCContainer();
            module.RegisterDependencies(container);
            if (container.CanResolve<IPackageManager>())
            {
                throw new ConfigurationException("Package Manager should not be manually configured in the dependency injection container");
            }
            container.Register(PackageManagerType, packageManagerType);
            container.Register(_logger);
            return container;
        }

        private static IPackageManagerModule CreateModule(Assembly assembly)
        {
            Type packageManagerModuleTypes = assembly.GetTypes().Where(PackageManagerModuleType.IsAssignableFrom).Single();
            var module = (IPackageManagerModule) Activator.CreateInstance(packageManagerModuleTypes);
            return module;
        }
    }
}