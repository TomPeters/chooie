using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Nancy.Hosting.Self;
using chui.Core.PackageManager;
using chui.PackageManager;


namespace chui
{
    public class ChuiApplicationContext : ApplicationContext
    {
        private const string NotifyIconText = "Chui";
        private readonly Uri _uri = new Uri("http://localhost:9876");
        private readonly NancyHost _nancyHost;
        private NotifyIcon _notifyIcon;

        public ChuiApplicationContext()
        {
            Initialize();
            var packageManagerProvider = new PackageManagerProvider(new ContainerFactory(), new AssemblyLoader());
            packageManagerProvider.BuildContainers();
            var packageManagerSettings = new PackageManagerSettings {PackageManagerType = "Chocolatey"};
            var hostConfiguration = new HostConfiguration();
            hostConfiguration.UrlReservations.CreateAutomatically = true;
            _nancyHost = new NancyHost(new ChuiBootstrapper(packageManagerSettings, packageManagerProvider), hostConfiguration, _uri);
            _nancyHost.Start();
        }

        private void Initialize()
        {
            var components = new Container();
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(new ToolStripButton("Exit", null, exitItem_Click));
            _notifyIcon = new NotifyIcon(components)
                {
                    ContextMenuStrip = contextMenuStrip,
                    Visible = true,
                    Text = NotifyIconText,
                    Icon = new Icon(Assembly.GetEntryAssembly().GetManifestResourceStream("chui.assets.chui.ico"))
                };
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            _nancyHost.Stop();
            ExitThread();
        }

        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false;
            base.ExitThreadCore();
        }
    }
}
