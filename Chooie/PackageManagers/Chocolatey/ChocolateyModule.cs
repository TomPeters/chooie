using System;
using Chooie.Core.PackageManager;
using Chooie.Core.TinyIoC;

namespace Chocolatey
{
    public class ChocolateyModule : IPackageManagerModule
    {
        public Type PackageManagerType
        {
            get { return typeof (Chocolatey); }
        }

        public void RegisterDependencies(TinyIoCContainer container)
        {
            container.Register<PackageListRetriver>();
        }
    }
}
