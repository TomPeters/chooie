using System;
using System.IO;
using System.Windows.Forms;

namespace chui
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            try
            {
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
