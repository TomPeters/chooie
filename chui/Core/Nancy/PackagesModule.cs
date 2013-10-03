using Core.PackageManager;
using Nancy;
using System.Linq;

namespace Core.Nancy
{
    public class PackagesModule : NancyModule
    {
        public PackagesModule(IPackageManagerProvider packageManagerProvider)
        {
            Get["/packages"] = _ => Response.AsJson(packageManagerProvider.GetPackageManager().Packages.ToList());
        }
    }
}
