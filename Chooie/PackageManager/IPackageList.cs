using System.Collections.Generic;
using Chooie.Interface;

namespace Chooie.PackageManager
{
    public interface IPackageList
    {
        IReadOnlyList<Package> Packages { get; set; }
    }
}