using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Chooie.Interface.Helpers;
using Chooie.Interface.Logging;
using System.Linq;

namespace Chooie.PluginHelpers
{
    public class ShellCommandRunner : IShellCommandRunner
    {
        private readonly ILogger _logger;

        public ShellCommandRunner(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<string> RunCommand(string command)
        {
            Process process = StartProcess(command);
            IEnumerable<string> output = ReadProcessOutput(process);
            process.WaitForExit();
            return output.ToList();
        }

        public IEnumerable<string> RunCommandWihLogging(string command)
        {
            Process process = StartProcess(command);
            IEnumerable<string> output = ReadProcessOutput(process);
            IList<string> outputList = LogOutput(output).ToList();
            process.WaitForExit();
            return outputList;
        }

        private IEnumerable<string> LogOutput(IEnumerable<string> output)
        {
            foreach (var outputLine in output)
            {
                _logger.LogInfo(outputLine);
                yield return outputLine;
            }
        }

        private IEnumerable<string> ReadProcessOutput(Process process)
        {
            using (StreamReader outputStream = process.StandardOutput)
            {
                while(true)
                {
                    string nextLine = outputStream.ReadLine();
                    if(nextLine == null) yield break;
                    yield return nextLine;
                }
            }
        }

        private Process StartProcess(string command)
        {
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C " + command;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _logger.LogInfo("Running: " + command);
            p.Start();
            return p;
        }
    }
}
