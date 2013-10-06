using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Owin.Hosting;
using chui.SignalR;

namespace chui
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            try
            {
                //Launch SignalR
                WebApp.Start<SignalRStartup>();

                var applicationContext = new ChuiApplicationContext();
                Application.Run(applicationContext);
            }
            catch (Exception ex)
            {
                var file = new StreamWriter("log.txt");
                file.WriteLine(ex.Message);
                file.Close();
            }
        }
    }
}
