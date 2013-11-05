using System.Collections.Generic;

namespace Chooie.Core.PackageManager
{
    public interface IPackageManager
    {
        IEnumerable<Package> Packages { get; }
        void InstallPackage(Package package);
        void UninstallPackage(Package package);
    }
}
