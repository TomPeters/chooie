using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Chooie.PackageManager
{
    public class AssemblyLoader
    {
        public IEnumerable<Assembly> LoadAllAssembliesInDirectory()
        {
            var currentlyLoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(currentDirectory, "*.dll");
            IEnumerable<Assembly> loadedAssemblies = files.Select(LoadAssembly).Where(a => a != null);
            IEnumerable<Assembly> newlyLoadedAssemblies = loadedAssemblies.Where(a => !currentlyLoadedAssemblies.Contains(a));
            return newlyLoadedAssemblies;
        }

        private static Assembly LoadAssembly(string f)
        {
            try
            {
                return Assembly.LoadFile(f);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
