using chui.Core;

namespace Chocolatey
{
    public class PackageInstaller
    {
        public void InstallPackage(Package package)
        {
            var powershellCommandRunner = new RunSync();
            powershellCommandRunner.Run("cinst " + package.Name);
        } 
    }
}