using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Chooie.Core.PackageManager;
using Chooie.Core.TinyIoC;

namespace Chooie.PackageManager
{
    public class PackageManagerProvider : IPackageManagerProvider
    {
        private readonly ContainerFactory _containerFactory;
        private readonly AssemblyLoader _assemblyLoader;
        private readonly IDictionary<string, TinyIoCContainer> _containers = new Dictionary<string, TinyIoCContainer>(); 

        public PackageManagerProvider(ContainerFactory containerFactory, AssemblyLoader assemblyLoader)
        {
            _containerFactory = containerFactory;
            _assemblyLoader = assemblyLoader;
        }

        public void BuildContainers()
        {
            IEnumerable<Assembly> assemblies = _assemblyLoader.LoadAllAssembliesInDirectory();

            IEnumerable<Assembly> packageManagerAssemblies = assemblies.Where(IsPackageManagerAssembly);
            foreach (Assembly assembly in packageManagerAssemblies)
            {
                _containers[assembly.GetName().Name] = _containerFactory.BuildContainer(assembly);
            }
        }

        private bool IsPackageManagerAssembly(Assembly assembly)
        {
            IEnumerable<Type> types = GetAssemblyTypes(assembly);
            return types.Count(t => ContainerFactory.PackageManagerModuleType.IsAssignableFrom(t)) == 1;
        }

        private static IEnumerable<Type> GetAssemblyTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        public IPackageManager GetPackageManager(string packageManager)
        {
            return _containers[packageManager].Resolve<IPackageManager>();
        }
    }
}
