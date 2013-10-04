using System;
using chui.Core.PackageManager;
using chui.Core.TinyIoC;

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
