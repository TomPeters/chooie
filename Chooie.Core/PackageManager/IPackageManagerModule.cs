using System;
using Chooie.Core.TinyIoC;

namespace Chooie.Core.PackageManager
{
    public interface IPackageManagerModule
    {
        Type PackageManagerType { get; }
        void RegisterDependencies(TinyIoCContainer container);
    }
}
