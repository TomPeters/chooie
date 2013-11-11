using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Nancy.Hosting.Self;
using Chooie.Core.PackageManager;
using Chooie.PackageManager;


namespace Chooie
{
    public class ChooieApplicationContext : ApplicationContext
    {
        private const string NotifyIconText = "Chooie";
        private const string Uri = "http://localhost:9876/";
        private readonly Uri _uri = new Uri(Uri);
        private readonly NancyHost _nancyHost;
        private NotifyIcon _notifyIcon;

        public ChooieApplicationContext(DependencyContainerBuilder dependencyContainerBuilder)
        {
            dependencyContainerBuilder.Logger.LogInfo("Configuring system tray icon");
            Initialize();

            dependencyContainerBuilder.Logger.LogInfo("Configuring web server");
            var hostConfiguration = new HostConfiguration();
            hostConfiguration.UrlReservations.CreateAutomatically = true;
            _nancyHost = new NancyHost(new ChooieBootstrapper(dependencyContainerBuilder), hostConfiguration, _uri);

            dependencyContainerBuilder.Logger.LogInfo("Starting web server");
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
                    Icon = new Icon(Assembly.GetEntryAssembly().GetManifestResourceStream("Chooie.assets.Chooie.ico"))
                };
            _notifyIcon.DoubleClick += OnDoubleClick;
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            _nancyHost.Stop();
            ExitThread();
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Uri);
        }

        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false;
            base.ExitThreadCore();
        }
    }
}
