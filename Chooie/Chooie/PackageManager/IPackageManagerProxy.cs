using Chooie.Core.PackageManager;

namespace Chooie.PackageManager
{
    public interface IPackageManagerProxy : IPackageManager
    {
        void UpdatePackages();
    }
}
