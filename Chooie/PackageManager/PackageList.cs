using System.Collections.Generic;
using Chooie.Core;
using Chooie.Core.PackageManager;
using Chooie.Database;

namespace Chooie.PackageManager
{
    public class PackageList : IPackageList
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly PackageManagerSettings _packageManagerSettings;

        public PackageList(IDatabaseManager databaseManager, PackageManagerSettings packageManagerSettings)
        {
            _databaseManager = databaseManager;
            _packageManagerSettings = packageManagerSettings;
        }

        public IReadOnlyList<Package> Packages
        {
            get
            {
                var dbPackages = _databaseManager.GetDatabaseObject<List<Package>>(CurrentPackageManagerListTableName);
                if (dbPackages == null)
                {
                    return new List<Package>();
                }
                return dbPackages;
            }
            set { _databaseManager.SaveDatabaseObject(CurrentPackageManagerListTableName, value); }
        }

        private string CurrentPackageManagerListTableName
        {
            get { return _packageManagerSettings.PackageManagerType + "_packages"; }
        }
    }
}
