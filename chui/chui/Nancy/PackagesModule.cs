using Nancy;
using System.Linq;
using chui.Core.PackageManager;

namespace chui.Nancy
{
    public class PackagesModule : NancyModule
    {
        public PackagesModule(IPackageManager packageManager)
        {
            Get["/packages"] = _ => Response.AsJson(packageManager.Packages.ToList());
        }
    }
}
