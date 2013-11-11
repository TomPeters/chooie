using System;
using System.IO;
using System.Windows.Forms;
using Chooie.Database;
using Microsoft.Owin.Hosting;
using Chooie.SignalR;

namespace Chooie
{
    public class Launcher
    {
        private const string SignalRUrl = "http://+:8080/";

        public static void Main(string[] args)
        {
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            try
            {
                dependencyContainerBuilder.Logger.LogInfo("Attempting to configure urls for signal R");
                new UrlAclConfigurer().ConfigureUrl(SignalRUrl);
                dependencyContainerBuilder.Logger.LogInfo("Starting signal R at " + SignalRUrl);
                WebApp.Start<SignalRStartup>(SignalRUrl);

                if (!File.Exists(DatabaseAccessor.DatabaseFile))
                {
                    dependencyContainerBuilder.Logger.LogInfo("Creating database");
                    using (StreamWriter writer = File.AppendText(DatabaseAccessor.DatabaseFile)) { }
                }
                    
                var applicationContext = new ChooieApplicationContext(dependencyContainerBuilder);

                dependencyContainerBuilder.Logger.LogInfo("Starting chooie (entering main loop)");
                Application.Run(applicationContext);
            }

            catch (Exception ex)
            {
                dependencyContainerBuilder.Logger.LogError(ex.Message);
                dependencyContainerBuilder.Logger.LogError(ex.StackTrace);
            }
        }
    }
}
