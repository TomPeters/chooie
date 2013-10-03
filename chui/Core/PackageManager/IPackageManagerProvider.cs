namespace Core.PackageManager
{
    public interface IPackageManagerProvider
    {
        IPackageManager GetPackageManager();
    }
}
