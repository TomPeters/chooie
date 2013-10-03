using Chocolatey;
using Core.PackageManager;

namespace chui
{
    public class PackageManagerFactoriesProvider : IPackageManagerFactoriesProvider
    {
        public IPackageManagerFactory GetPackageManagerFactory(string packageManager)
        {
            // TODO: This should do some sort of reflection/assembly loading stuff
            return new ChocolateyFactory();
        }
    }
}
