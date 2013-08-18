using System.Windows.Forms;

namespace chui
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            var applicationContext = new ChuiApplicationContext();
            Application.Run(applicationContext);
        }
    }
}
