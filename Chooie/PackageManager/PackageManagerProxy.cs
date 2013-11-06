using System.Collections.Generic;
using System.Linq;
using Chooie.Core;
using Chooie.Core.PackageManager;
using Chooie.Jobs;
using Chooie.SignalR;

namespace Chooie.PackageManager
{
    public class PackageManagerProxy : IPackageManagerProxy
    {
        private readonly IPackageManagerProvider _packageManagerProvider;
        private readonly PackageManagerSettings _packageManagerSettings;
        private readonly IClientMessenger _clientMessenger;
        private readonly IJobQueue _jobQueue;
        private readonly IPackageList _packageList;

        public PackageManagerProxy(IPackageManagerProvider packageManagerProvider, 
            PackageManagerSettings packageManagerSettings, 
            IClientMessenger clientMessenger, 
            IJobQueue jobQueue,
            IPackageList packageList)
        {
            _packageManagerProvider = packageManagerProvider;
            _packageManagerSettings = packageManagerSettings;
            _clientMessenger = clientMessenger;
            _jobQueue = jobQueue;
            _packageList = packageList;
        }

        private IPackageManager PackageManager
        {
            get { return _packageManagerProvider.GetPackageManager(_packageManagerSettings.PackageManagerType); }
        }

        public IEnumerable<Package> Packages
        {
            get
            {
                return _packageList.Packages;
            }
        }

        public void InstallPackage(Package package)
        {
            _jobQueue.EnqueueJob("Installing Package: " + package.Name, () => PackageManager.InstallPackage(package));
        }

        public void UninstallPackage(Package package)
        {
            _jobQueue.EnqueueJob("Uninstalling Package: " + package.Name, () => PackageManager.UninstallPackage(package));
        }

        public void UpdatePackages()
        {
            _jobQueue.EnqueueJob("Update Packages", () =>
                {
                    _packageList.Packages = PackageManager.Packages.ToList();
                    _clientMessenger.SendMessage("Packages", "Updated");
                });
        }
    }
}
