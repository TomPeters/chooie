using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using chui.Core;

namespace Chocolatey
{
    public class PackageListRetriver
    {
        private const string Url = @"http://chocolatey.org/api/v2";
        public IEnumerable<Package> Packages
        {
            get
            {
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C clist -source " + Url;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Console.WriteLine("Running Command");
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.WriteLine("Parsing output");
                return ConvertCommandOutputToPackages(output);
            }
        }

        private IEnumerable<Package> ConvertCommandOutputToPackages(string output)
        {
            return output.Split('\n').Select(ConvertPackageLineToPackage).Where(p => p != null);
        }

        private Package ConvertPackageLineToPackage(string packageLine)
        {
            string[] words = packageLine.Split(' ');
            if (words.Length != 2) return null;
            return new Package()
                {
                    Name = words[0],
                    CurrentVersion = words[1]
                };
        }
    }
}
