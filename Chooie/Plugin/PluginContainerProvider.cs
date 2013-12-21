using Chooie.Interface.TinyIoC;

namespace Chooie.Plugin
{
    public interface IPluginContainerProvider
    {
        TinyIoCContainer GetContainerForAssembly(string assemblyName);
    }
}