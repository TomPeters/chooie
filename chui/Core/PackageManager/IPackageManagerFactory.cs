namespace Core.PackageManager
{
    public interface IPackageManagerFactory
    {
        IPackageManager CreatePackageManager();
    }
}
