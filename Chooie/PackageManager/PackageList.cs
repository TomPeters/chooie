using System.Collections.Generic;
using Chooie.Core;

namespace Chooie.PackageManager
{
    public class PackageList : IPackageList
    {
        private IReadOnlyList<Package> _packages = new List<Package>();

        public IReadOnlyList<Package> Packages
        {
            get { return _packages; }
            set { _packages = value; }
        }
    }
}
