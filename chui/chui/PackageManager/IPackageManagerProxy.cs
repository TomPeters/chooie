using chui.Core.PackageManager;

namespace chui.PackageManager
{
    public interface IPackageManagerProxy : IPackageManager
    {
        void UpdatePackages();
    }
}
