using System.Collections.Generic;
using Chooie.Core;

namespace Chooie.PackageManager
{
    public interface IPackageList
    {
        IReadOnlyList<Package> Packages { get; set; }
    }
}