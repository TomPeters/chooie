using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chooie.Plugin;

namespace Chooie.PackageManager
{
    public class PackageManagerPluginJudge : IPluginJudge
    {
        public bool IsPluginAssembly(Assembly assembly)
        {
            IEnumerable<Type> types = GetAssemblyTypes(assembly);
            return types.Count(t => PackageManagerContainerFactory.PackageManagerModuleType.IsAssignableFrom(t)) == 1;
        }

        private static IEnumerable<Type> GetAssemblyTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (Exception)
            {
                return new List<Type>();
            }
        }
    }
}
