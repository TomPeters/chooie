using Chooie.ApplicationStart;
using Chooie.Interface.Logging;

namespace Chooie.Plugin
{
    public class PluginStarter : IStarter
    {
        private readonly AssemblyLoader _assemblyLoader;
        private readonly PluginContainerBuilder _pluginContainerBuilder;
        private readonly ILogger _logger;

        public PluginStarter(AssemblyLoader assemblyLoader, PluginContainerBuilder pluginContainerBuilder, ILogger logger)
        {
            _assemblyLoader = assemblyLoader;
            _pluginContainerBuilder = pluginContainerBuilder;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInfo("Loading plugins");
            _assemblyLoader.LoadAllAssembliesInDirectory();
            _pluginContainerBuilder.BuildContainers();
        }
    }
}
