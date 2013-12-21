using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Chooie.Interface.Logging;

namespace Chooie.Plugin
{
    public class AssemblyLoader
    {
        private readonly ILogger _logger;
        private readonly IList<Assembly> _loadedAssemblies = new List<Assembly>();

        public AssemblyLoader(ILogger logger)
        {
            _logger = logger;
        }

        public void LoadAllAssembliesInDirectory()
        {
            var alreadyLoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(currentDirectory, "*.dll");
            IEnumerable<Assembly> successfullyLoadedAssemblies = files.Select(LoadAssembly).Where(a => a != null);
            var newlyLoadedAssemblie = successfullyLoadedAssemblies.Where(a => !alreadyLoadedAssemblies.Contains(a));
            foreach (var assembly in newlyLoadedAssemblie)
            {
                _loadedAssemblies.Add(assembly);
            }
        }

        public IEnumerable<Assembly> LoadedAssemblies
        {
            get { return _loadedAssemblies; }
        }

        private Assembly LoadAssembly(string f)
        {
            try
            {
                return Assembly.LoadFile(f);
            }
            catch (Exception ex)
            {
                _logger.LogInfo("Failed to load assembly " + f + ": " + ex.Message);
                return null;
            }
        }
    }
}
