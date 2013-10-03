using System;
using System.Management.Automation;
using System.Text;

namespace Chocolatey
{
    internal delegate void ResultsHandler(String result);
    internal delegate void EmptyHandler();

    internal class RunSync
    {
        public void Run(String command)
        {
            Console.WriteLine("Running command: {0}", command);
            var result = new StringBuilder();
            var results = PowerShell.Create()
                .AddScript(command)
                .AddCommand("out-String")
                .Invoke<String>();
            if (results != null && results.Count > 0 && (results.Count == 1 && !String.IsNullOrWhiteSpace(results[0])))
            {
                foreach (var line in results)
                {
                    Console.WriteLine("Line: {0}", line);
                    result.AppendLine(line);
                }
                if (OutputChanged != null)
                {
                    Console.WriteLine("Result: {0}", result);
                    OutputChanged(result.ToString());
                }
            }
            else
            {
                if (OutputChanged != null)
                {
                    Console.WriteLine("No output");
                    OutputChanged("No output");
                }
            }
            if (RunFinished == null) return;
            Console.WriteLine("Run finished");
            RunFinished();
        }

        public event ResultsHandler OutputChanged;

        public event EmptyHandler RunFinished;
    }
}