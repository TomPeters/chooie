using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace Core.Nancy
{
    public class PackagesModule : NancyModule
    {
        private const string Url = @"http://chocolatey.org/api/v2";
        private IList<string> _lines;
        private IList<Package> _packages; 

        public PackagesModule()
        {
            Get["/packages"] = _ =>
                {
                    _lines = new List<string>();
                    _packages = new List<Package>();
                    var powershellCommandRunner = new RunSync();
                    powershellCommandRunner.OutputChanged += OutputChanged;
                    powershellCommandRunner.RunFinished += RunFinished;
                    powershellCommandRunner.Run("clist -source " + Url);
                    return Response.AsJson(_packages);
                };
        }

        private void OutputChanged(string line)
        {
            Console.WriteLine("Output changed: {0}", line);
            foreach (var package in line.Split(new string[] {Environment.NewLine}, StringSplitOptions.None))
            {
                _lines.Add(package);
            }
        }

        private void RunFinished()
        {
            foreach (var packageLine in _lines)
            {
                try
                {
                    var name = packageLine.Split(" ".ToCharArray()[0])[0];
                    var version = packageLine.Split(" ".ToCharArray()[0])[1];
                    _packages.Add(new Package {Name = name, InstalledVersion = version});
                }
                catch
                {
                }
            }
        }
    }
}
