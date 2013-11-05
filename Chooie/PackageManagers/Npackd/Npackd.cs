using System.Collections.Generic;
using Chooie.Core;
using Chooie.Core.PackageManager;

namespace Npackd
{
    public class Npackd : IPackageManager
    {
        private readonly PackageListRetriever _packageListRetriever;

        public Npackd(PackageListRetriever packageListRetriever)
        {
            _packageListRetriever = packageListRetriever;
        }

        public IEnumerable<Package> Packages
        {
            get
            {
                return _packageListRetriever.GetPackages();
            }
        }

        public void InstallPackage(Package package)
        {
            throw new System.NotImplementedException();
        }

        public void UninstallPackage(Package package)
        {
            throw new System.NotImplementedException();
        }
    }
}