using Chooie.Interface.PackageManager;

namespace Chooie.PackageManager
{
    public interface IPackageManagerProxy : IPackageManager
    {
        void UpdatePackages();
    }
}
