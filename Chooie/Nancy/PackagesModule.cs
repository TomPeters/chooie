﻿using System;
using Nancy;
using Nancy.ModelBinding;
using Chooie.Core;
using Chooie.PackageManager;

namespace Chooie.Nancy
{
    public class PackagesModule : NancyModule
    {
        public PackagesModule(IPackageManagerProxy packageManager)
        {
            Get["/packages"] = _ => Response.AsJson(packageManager.Packages);
            Post["/packages/update"] = _ =>
                {
                    packageManager.UpdatePackages();
                    return HttpStatusCode.OK;
                };
            Post["/packages/install"] = _ =>
                {
                    packageManager.InstallPackage(this.Bind<Package>());
                    return HttpStatusCode.OK;
                };

            Post["/packages/uninstall"] = _ =>
                {
                    packageManager.UninstallPackage(this.Bind<Package>());
                    return HttpStatusCode.OK;
                };
        }
    }
}