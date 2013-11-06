using Chooie.Core.PackageManager;

namespace Chooie.PackageManager
{
    public interface IPackageManagerProvider
    {
        IPackageManager GetPackageManager(string packageManager);
    }
}