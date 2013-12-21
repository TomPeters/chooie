using System.Collections.Generic;

namespace Chooie.Interface.Helpers
{
    public interface IShellCommandRunner
    {
        IEnumerable<string> RunCommand(string command);
        IEnumerable<string> RunCommandWihLogging(string command);
    }
}
