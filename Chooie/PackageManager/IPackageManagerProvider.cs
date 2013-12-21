using Chooie.Interface.PackageManager;

namespace Chooie.PackageManager
{
    public interface IPackageManagerProvider
    {
        IPackageManager GetPackageManager(string packageManager);
    }
}