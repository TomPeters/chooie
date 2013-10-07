using System.Collections.Generic;
using System.Linq;
using chui.Core;
using chui.Core.PackageManager;
using chui.SignalR;

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

        private IList<Package> _packages; 

        public IEnumerable<Package> Packages
        {
            get
            {
                if (_packages == null)
                {
                    _packages = PackageManager.Packages.ToList();
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
