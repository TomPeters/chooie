using System;
using System.Diagnostics;
using Chooie.Core;

namespace Chocolatey
{
    public class PackageUninstaller
    {
        public void UninstallPackage(Package package)
        {
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C cuninst " + package.Name;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Console.WriteLine("Running Command");
            p.Start();
            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        } 
    }
}
