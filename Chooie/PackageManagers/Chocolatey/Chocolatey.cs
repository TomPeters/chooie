using System.Collections.Generic;
using Chooie.Core;
using Chooie.Core.PackageManager;

namespace Chocolatey
{
    public class Chocolatey : IPackageManager
    {
        private readonly PackageListRetriver _packageListRetriver;
        private readonly PackageInstaller _packageInstaller;
        private readonly PackageUninstaller _packageUninstaller;

        public Chocolatey(PackageListRetriver packageListRetriver, PackageInstaller packageInstaller, PackageUninstaller packageUninstaller)
        {
            _packageListRetriver = packageListRetriver;
            _packageInstaller = packageInstaller;
            _packageUninstaller = packageUninstaller;
        }

        public IEnumerable<Package> Packages
        {
            get
            {
                return _packageListRetriver.Packages;
            }
        }

        public void InstallPackage(Package package)
        {
            _packageInstaller.InstallPackage(package);
        }

        public void UninstallPackage(Package package)
        {
            _packageUninstaller.UninstallPackage(package);
        }
    }
}
