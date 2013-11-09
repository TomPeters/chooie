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
            try
            {
                new UrlAclConfigurer().ConfigureUrl(SignalRUrl);
                WebApp.Start<SignalRStartup>(SignalRUrl);

                // Create Database File if required
                if(!File.Exists(DatabaseAccessor.DatabaseFile))
                    using (StreamWriter writer = File.AppendText(DatabaseAccessor.DatabaseFile)) { }

                var applicationContext = new ChooieApplicationContext();
                Application.Run(applicationContext);
            }
            catch (Exception ex)
            {
                var file = new StreamWriter("log.txt");
                file.WriteLine(ex.Message);
                file.WriteLine(ex.StackTrace);
                file.Close();
            }
        }
    }
}
