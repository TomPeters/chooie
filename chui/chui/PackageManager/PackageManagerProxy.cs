using System.Collections.Generic;
using chui.Core;
using chui.Core.PackageManager;

namespace chui.PackageManager
{
    public class PackageManagerProxy : IPackageManager
    {
        private readonly IPackageManagerProvider _packageManagerProvider;
        private readonly PackageManagerSettings _packageManagerSettings;


        public PackageManagerProxy(IPackageManagerProvider packageManagerProvider, PackageManagerSettings packageManagerSettings)
        {
            _packageManagerProvider = packageManagerProvider;
            _packageManagerSettings = packageManagerSettings;
        }

        private IPackageManager PackageManager
        {
            get { return _packageManagerProvider.GetPackageManager(_packageManagerSettings.PackageManagerType); }
        }

        private IEnumerable<Package> _packages; 

        public IEnumerable<Package> Packages
        {
            get
            {
                if (_packages == null)
                {
                    _packages = PackageManager.Packages;
                }
                return _packages;
            }
        }

        public void InstallPackage(Package package)
        {
            PackageManager.InstallPackage(package);
        }
    }
}
