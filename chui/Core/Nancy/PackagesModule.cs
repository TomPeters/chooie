using Core.PackageManager;
using Nancy;
using System.Linq;

namespace Core.Nancy
{
    public class PackagesModule : NancyModule
    {
        public PackagesModule(IPackageManager packageManager)
        {
            Get["/packages"] = _ => Response.AsJson(packageManager.Packages.ToList());
        }
    }
}
