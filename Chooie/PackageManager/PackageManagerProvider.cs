using System.Linq;
using Chooie.Interface.PackageManager;
using Chooie.Plugin;

namespace Chooie.PackageManager
{
    public class PackageManagerProvider : IPackageManagerProvider
    {
        private readonly IPluginContainerProvider _pluginContainerProvider;
        private readonly IPluginNameProvider _pluginNameProvider;

        public PackageManagerProvider(IPluginContainerProvider pluginContainerProvider, IPluginNameProvider pluginNameProvider)
        {
            _pluginContainerProvider = pluginContainerProvider;
            _pluginNameProvider = pluginNameProvider;
        }

        public IPackageManager GetPackageManager(string packageManager)
        {
            return _pluginContainerProvider.GetContainerForAssembly(packageManager).Resolve<IPackageManager>();
        }

        public string GetFirstPackageManagerName()
        {
            return _pluginNameProvider.PluginNames.First();
        }
    }
}
