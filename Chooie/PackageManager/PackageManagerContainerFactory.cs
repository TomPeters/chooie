﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using Chooie.Interface.Helpers;
using Chooie.Interface.Logging;
using Chooie.Interface.PackageManager;
using Chooie.Interface.TinyIoC;
using Chooie.Logging;
using Chooie.Plugin;
using Chooie.PluginHelpers;

namespace Chooie.PackageManager
{
    public class PackageManagerContainerFactory : IContainerFactory
    {
        private readonly ILog _log;
        public static readonly Type PackageManagerModuleType = typeof(IPackageManagerModule);
        private static readonly Type PackageManagerType = typeof (IPackageManager);

        public PackageManagerContainerFactory(ILog log)
        {
            _log = log;
        }

        public TinyIoCContainer CreateContainerForAssembly(Assembly assembly)
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
            container.Register<ILogger>((c, p) => new Logger(new Context(packageManagerType.ToString()), _log));
            container.Register<IShellCommandRunner, ShellCommandRunner>();
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