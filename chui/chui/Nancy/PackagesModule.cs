using Nancy;
using Nancy.ModelBinding;
using System.Linq;
using chui.Core;
using chui.Core.PackageManager;

namespace chui.Nancy
{
    public class PackagesModule : NancyModule
    {
        public PackagesModule(IPackageManager packageManager)
        {
            Get["/packages"] = _ => Response.AsJson(packageManager.Packages.ToList());
            Post["/packages/install"] = _ =>
                {
                    packageManager.InstallPackage(this.Bind<Package>());
                    return HttpStatusCode.OK;
                };
        }
    }
}
