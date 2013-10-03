using System.Collections.Generic;

namespace Core.PackageManager
{
    public interface IPackageManager
    {
        IEnumerable<Package> Packages { get; }
    }
}
