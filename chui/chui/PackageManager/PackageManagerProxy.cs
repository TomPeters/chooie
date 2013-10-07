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
        private readonly IJobFactory _jobFactory;

        public PackageManagerProxy(IPackageManagerProvider packageManagerProvider, 
            PackageManagerSettings packageManagerSettings, 
            IClientMessenger clientMessenger, 
            IJobFactory jobFactory)
        {
            _packageManagerProvider = packageManagerProvider;
            _packageManagerSettings = packageManagerSettings;
            _clientMessenger = clientMessenger;
            _jobFactory = jobFactory;
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
            PackageManager.InstallPackage(package);
        }

        public void UpdatePackages(string dispatchId)
        {
            _jobFactory.CreateJob("Update Packages", () =>
                {
                    _packages = PackageManager.Packages.ToList();
                    _clientMessenger.SendMessage(dispatchId, "Packages Updated");
                });
        }
    }
}
