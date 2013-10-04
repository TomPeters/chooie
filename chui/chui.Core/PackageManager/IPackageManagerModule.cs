using System;
using chui.Core.TinyIoC;

namespace chui.Core.PackageManager
{
    public interface IPackageManagerModule
    {
        Type PackageManagerType { get; }
        void RegisterDependencies(TinyIoCContainer container);
    }
}
