using System.Collections.Generic;

namespace Chooie.Interface.PackageManager
{
    public interface IPackageManager
    {
        IEnumerable<Package> Packages { get; }
        void InstallPackage(Package package);
        void UninstallPackage(Package package);
    }
}
