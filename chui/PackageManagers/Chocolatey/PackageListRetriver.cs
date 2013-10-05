using System;
using System.Collections.Generic;
using chui.Core;

namespace Chocolatey
{
    public class PackageListRetriver
    {
        private const string Url = @"http://chocolatey.org/api/v2";
        private readonly IList<string> _lines = new List<string>();
        private readonly IList<Package> _packages = new List<Package>();

        public IEnumerable<Package> Packages
        {
            get
            {
                var powershellCommandRunner = new RunSync();
                powershellCommandRunner.OutputChanged += OutputChanged;
                powershellCommandRunner.RunFinished += RunFinished;
                powershellCommandRunner.Run("clist -source " + Url);
                return _packages;
            }
        }

        private void OutputChanged(string line)
        {
            Console.WriteLine("Output changed: {0}", line);
            foreach (var package in line.Split(new [] { Environment.NewLine }, StringSplitOptions.None))
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
                    _packages.Add(new Package { Name = name, CurrentVersion = version });
                }
                catch
                {
                }
            }
        }
    }
}
