using Core.PackageManager;

namespace Chocolatey
{
    public class ChocolateyFactory : IPackageManagerFactory
    {
        public IPackageManager CreatePackageManager()
        {
            return new Chocolatey(new PackageListRetriver());
        }
    }
}
