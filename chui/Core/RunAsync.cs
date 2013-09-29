using System;
using System.Management.Automation;
using System.Text;

namespace Core
{
    public delegate void ResultsHandler(String result);
    public delegate void EmptyHandler();

    public class RunSync
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
                    Console.WriteLine("Result: {0}", result.ToString());
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