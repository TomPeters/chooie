using Chooie.Interface.PackageManager;

namespace Chooie.PackageManager
{
    public class PackageManagerSettings : IPackageManagerSettings
    {
        public PackageManagerSettings(IPackageManagerProvider packageManagerProvider)
        {
            PackageManagerType = packageManagerProvider.GetFirstPackageManagerName();
        }

        public string PackageManagerType { get; set; }
    }
}
