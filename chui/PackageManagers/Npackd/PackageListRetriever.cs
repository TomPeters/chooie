using System;
using System.Collections.Generic;
using System.Diagnostics;
using chui.Core;
using System.Linq;

namespace Npackd
{
    public class PackageListRetriever
    {
        private const int HeaderLines = 4;

        public IEnumerable<Package> GetPackages()
        {
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = @"C:\Program Files (x86)\NpackdCL\npackdcl.exe";
            p.StartInfo.Arguments = "list";
            p.StartInfo.CreateNoWindow = true;
            Console.WriteLine("Running Command");
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            Console.WriteLine("Parsing output");
            return ConvertCommandOutputToPackages(output);
        }

        private static IEnumerable<Package> ConvertCommandOutputToPackages(string output)
        {
            return output.Split('\n').Skip(HeaderLines).Select(ConvertPackageLineToPackage).Where(p => p != null);
        } 

        private static Package ConvertPackageLineToPackage(string line)
        {
            var words = line.Split(' ');
            if (words.Length < 3) return null;
            var packageNameWords = words.Take(words.Length - 2).ToArray();
            var packageVersion = words.ToArray()[words.Length - 2];
            return new Package()
                {
                    Name = String.Join(" ", packageNameWords),
                    InstalledVersion = packageVersion
                };
        }
    }
}
