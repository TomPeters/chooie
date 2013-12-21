using System.Collections.Generic;

namespace Chooie.Plugin
{
    public interface IPluginNameProvider
    {
        IEnumerable<string> PluginNames { get; } 
    }
}