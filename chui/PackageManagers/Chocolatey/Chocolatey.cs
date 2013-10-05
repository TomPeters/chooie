using System.Collections.Generic;
using chui.Core;
using chui.Core.PackageManager;

namespace Chocolatey
{
    public class Chocolatey : IPackageManager
    {
        private readonly PackageListRetriver _packageListRetriver;
        private readonly PackageInstaller _packageInstaller;

        public Chocolatey(PackageListRetriver packageListRetriver, PackageInstaller packageInstaller)
        {
            _packageListRetriver = packageListRetriver;
            _packageInstaller = packageInstaller;
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
    }
}
