using System.Collections.Generic;
using System.Reflection;
using Chooie.Interface.Logging;
using Chooie.Interface.TinyIoC;
using System.Linq;

namespace Chooie.Plugin
{
    public class PluginContainerBuilder : IPluginContainerProvider, IPluginNameProvider
    {
        private readonly AssemblyLoader _assemblyLoader;
        private readonly IPluginJudge _pluginJudge;
        private readonly IContainerFactory _containerFactory;
        private readonly ILogger _logger;

        private readonly IDictionary<string, TinyIoCContainer> _containers = new Dictionary<string, TinyIoCContainer>(); 

        public PluginContainerBuilder(AssemblyLoader assemblyLoader, IPluginJudge pluginJudge, IContainerFactory containerFactory, ILogger logger)
        {
            _assemblyLoader = assemblyLoader;
            _pluginJudge = pluginJudge;
            _containerFactory = containerFactory;
            _logger = logger;
        }

        public void BuildContainers()
        {
            foreach (Assembly assembly in _assemblyLoader.LoadedAssemblies.Where(_pluginJudge.IsPluginAssembly))
            {
                _containers[assembly.GetName().Name] = _containerFactory.CreateContainerForAssembly(assembly);
                _logger.LogInfo("Loaded plugin: " + assembly.ManifestModule.Name);
            }
        }

        public TinyIoCContainer GetContainerForAssembly(string assemblyName)
        {
            return _containers[assemblyName];
        }

        public IEnumerable<string> PluginNames
        {
            get { return _containers.Keys; }
        }
    }
}
