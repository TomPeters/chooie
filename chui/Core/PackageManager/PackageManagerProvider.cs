using System.Collections.Generic;

namespace Core.PackageManager
{
    public class PackageManagerProvider : IPackageManagerProvider
    {
        private readonly PackageManagerSettings _packageManagerSettings;
        private readonly IPackageManagerFactoriesProvider _packageManagerFactoriesProvider;
        private readonly IDictionary<string, IPackageManager> _packageManagers = new Dictionary<string, IPackageManager>(); 

        public PackageManagerProvider(PackageManagerSettings packageManagerSettings, IPackageManagerFactoriesProvider packageManagerFactoriesProvider)
        {
            _packageManagerSettings = packageManagerSettings;
            _packageManagerFactoriesProvider = packageManagerFactoriesProvider;
        }

        public IPackageManager GetPackageManager()
        {
            if (!_packageManagers.ContainsKey(SelectedPackageManagerType))
            {
                _packageManagers[SelectedPackageManagerType] = _packageManagerFactoriesProvider.GetPackageManagerFactory(_packageManagerSettings.PackageManagerType).CreatePackageManager();
            }
            return _packageManagers[SelectedPackageManagerType];
        }

        private string SelectedPackageManagerType
        {
            get { return _packageManagerSettings.PackageManagerType; }
        }
    }
}
