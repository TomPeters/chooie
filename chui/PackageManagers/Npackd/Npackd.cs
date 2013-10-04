using System.Collections.Generic;
using chui.Core;
using chui.Core.PackageManager;

namespace Npackd
{
    public class Npackd : IPackageManager
    {
        private readonly PackageListRetriever _packageListRetriever;
        private IEnumerable<Package> _packages;

        public Npackd(PackageListRetriever packageListRetriever)
        {
            _packageListRetriever = packageListRetriever;
        }

        public IEnumerable<Package> Packages
        {
            get
            {
                if (_packages == null)
                {
                    _packages = _packageListRetriever.GetPackages();
                }
                return _packages;
            }
        }
    }
}