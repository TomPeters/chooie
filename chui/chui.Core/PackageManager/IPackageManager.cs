using System.Collections.Generic;

namespace chui.Core.PackageManager
{
    public interface IPackageManager
    {
        IEnumerable<Package> Packages { get; }
        void InstallPackage(Package package);
    }
}
