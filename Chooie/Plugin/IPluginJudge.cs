using System.Reflection;

namespace Chooie.Plugin
{
    public interface IPluginJudge
    {
        bool IsPluginAssembly(Assembly assembly);
    }
}
