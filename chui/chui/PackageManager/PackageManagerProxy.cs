using System.Collections.Generic;
using System.Linq;
using System.Threading;
using chui.Core;
using chui.Core.PackageManager;
using chui.Jobs;
using chui.SignalR;

namespace chui.PackageManager
{
    public class PackageManagerProxy : IPackageManagerProxy
    {
        private readonly IPackageManagerProvider _packageManagerProvider;
        private readonly PackageManagerSettings _packageManagerSettings;
        private readonly IClientMessenger _clientMessenger;
        private readonly IJobQueue _jobQueue;

        public PackageManagerProxy(IPackageManagerProvider packageManagerProvider, 
            PackageManagerSettings packageManagerSettings, 
            IClientMessenger clientMessenger, 
            IJobQueue jobQueue)
        {
            _packageManagerProvider = packageManagerProvider;
            _packageManagerSettings = packageManagerSettings;
            _clientMessenger = clientMessenger;
            _jobQueue = jobQueue;
        }

        private IPackageManager PackageManager
        {
            get { return _packageManagerProvider.GetPackageManager(_packageManagerSettings.PackageManagerType); }
        }

        private IList<Package> _packages = new List<Package>(); 

        public IEnumerable<Package> Packages
        {
            get
            {
                return _packages;
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
                    _packages = PackageManager.Packages.ToList();
                    _clientMessenger.SendMessage("Packages", "Updated");
                });
        }
    }
}
