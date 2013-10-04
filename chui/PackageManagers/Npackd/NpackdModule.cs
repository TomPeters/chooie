using System;
using chui.Core.PackageManager;
using chui.Core.TinyIoC;

namespace Npackd
{
    public class NpackdModule : IPackageManagerModule
    {
        public Type PackageManagerType
        {
            get { return typeof (Npackd); }
        }
        public void RegisterDependencies(TinyIoCContainer container)
        {
        }
    }
}