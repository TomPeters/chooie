using chui.Core.PackageManager;

namespace chui.PackageManager
{
    public interface IPackageManagerProvider
    {
        IPackageManager GetPackageManager(string packageManager);
    }
}