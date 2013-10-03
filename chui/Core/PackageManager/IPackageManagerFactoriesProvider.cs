namespace Core.PackageManager
{
    public interface IPackageManagerFactoriesProvider
    {
        IPackageManagerFactory GetPackageManagerFactory(string packageManager);
    }
}