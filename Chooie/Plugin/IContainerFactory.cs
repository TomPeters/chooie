using System.Reflection;
using Chooie.Interface.TinyIoC;

namespace Chooie.Plugin
{
    public interface IContainerFactory
    {
        TinyIoCContainer CreateContainerForAssembly(Assembly assembly);
    }
}
