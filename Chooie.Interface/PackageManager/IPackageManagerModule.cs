using System;
using Chooie.Interface.TinyIoC;

namespace Chooie.Interface.PackageManager
{
    public interface IPackageManagerModule
    {
        Type PackageManagerType { get; }
        void RegisterDependencies(TinyIoCContainer container);
    }
}
