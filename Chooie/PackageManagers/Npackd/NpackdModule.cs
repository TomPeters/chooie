using System;
using Chooie.Core.PackageManager;
using Chooie.Core.TinyIoC;

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