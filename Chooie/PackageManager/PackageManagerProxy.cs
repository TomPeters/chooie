using System;
using System.Collections.Generic;
using System.Linq;
using Chooie.Interface;
using Chooie.Interface.PackageManager;
using Chooie.Jobs;
using Chooie.SignalR;

namespace Chooie.PackageManager
{
    public class PackageManagerProxy : IPackageManagerProxy
    {
        private readonly IPackageManagerProvider _packageManagerProvider;
        private readonly IPackageManagerSettings _packageManagerSettings;
        private readonly IClientMessenger _clientMessenger;
        private readonly IJobQueue _jobQueue;
        private readonly IJobFactory _jobFactory;
        private readonly IPackageList _packageList;

        public PackageManagerProxy(IPackageManagerProvider packageManagerProvider, 
            IPackageManagerSettings packageManagerSettings, 
            IClientMessenger clientMessenger, 
            IJobQueue jobQueue,
            IJobFactory jobFactory,
            IPackageList packageList)
        {
            _packageManagerProvider = packageManagerProvider;
            _packageManagerSettings = packageManagerSettings;
            _clientMessenger = clientMessenger;
            _jobQueue = jobQueue;
            _jobFactory = jobFactory;
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
            var threadSafePackage = package.Clone();
            _jobQueue.EnqueueJob(_jobFactory.CreateJob("Installing Package: " + package.Name, () => PackageManager.InstallPackage(threadSafePackage)));
        }

        public void UninstallPackage(Package package)
        {
            var threadSafePackage = package.Clone();
            _jobQueue.EnqueueJob(_jobFactory.CreateJob("Uninstalling Package: " + package.Name, () => PackageManager.UninstallPackage(threadSafePackage)));
        }

        public void UpdatePackages()
        {
            List<Package> packageList = null;
            Action threadedPackageRetrievalAction = () => { packageList = PackageManager.Packages.ToList(); };
            Action postRunAction = () =>
                {
                    _packageList.Packages = packageList;
                    _clientMessenger.SendMessage("Packages", "Updated");
                };
            var job = _jobFactory.CreateJobWithPostRunAction("Update Packages", threadedPackageRetrievalAction, postRunAction);
            _jobQueue.EnqueueJob(job);
        }
    }
}
