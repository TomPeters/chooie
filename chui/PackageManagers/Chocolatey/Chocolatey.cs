using System.Collections.Generic;
using chui.Core;
using chui.Core.PackageManager;

namespace Chocolatey
{
    public class Chocolatey : IPackageManager
    {
        private readonly PackageListRetriver _packageListRetriver;

        public Chocolatey(PackageListRetriver packageListRetriver)
        {
            _packageListRetriver = packageListRetriver;
        }

        public IEnumerable<Package> Packages
        {
            get
            {
                return _packageListRetriver.Packages;
            }
        }
    }
}
