using System.Collections.Generic;
using chui.Core;
using chui.Core.PackageManager;

namespace Chocolatey
{
    public class Chocolatey : IPackageManager
    {
        private readonly PackageListRetriver _packageListRetriver;

        private IEnumerable<Package> _packages;

        public Chocolatey(PackageListRetriver packageListRetriver)
        {
            _packageListRetriver = packageListRetriver;
        }

        public IEnumerable<Package> Packages
        {
            get
            {
                if (_packages == null)
                {
                    _packages = _packageListRetriver.Packages;
                }
                return _packages;
            }
        }
    }
}
