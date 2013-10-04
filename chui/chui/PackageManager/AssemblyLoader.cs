using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace chui.PackageManager
{
    public class AssemblyLoader
    {
        public IEnumerable<Assembly> LoadAllAssembliesInDirectory()
        {
            var currentlyLoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(currentDirectory, "*.dll");
            IEnumerable<Assembly> loadedAssemblies = files.Select(Assembly.LoadFile);
            IEnumerable<Assembly> newlyLoadedAssemblies = loadedAssemblies.Where(a => !currentlyLoadedAssemblies.Contains(a));
            return newlyLoadedAssemblies;
        }
    }
}
