using System;
using Nancy;
using Nancy.ModelBinding;
using System.Linq;
using chui.Core;
using chui.Core.PackageManager;
using chui.PackageManager;

namespace chui.Nancy
{
    public class PackagesModule : NancyModule
    {
        public PackagesModule(IPackageManagerProxy packageManager)
        {
            Get["/packages"] = _ => Response.AsJson(packageManager.Packages.ToList());
            Post["/packages/update"] = _ =>
                {
                    var dispatchId = Guid.NewGuid().ToString();
                    packageManager.UpdatePackages(dispatchId);
                    return Response.AsJson(dispatchId);
                };
            Post["/packages/install"] = _ =>
                {
                    packageManager.InstallPackage(this.Bind<Package>());
                    return HttpStatusCode.OK;
                };
        }
    }
}
