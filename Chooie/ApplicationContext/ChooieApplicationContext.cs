using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Nancy.Hosting.Self;

namespace Chooie.ApplicationContext
{
    public class ChooieApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private const string NotifyIconText = "Chooie";
        private readonly string _uri;
        private readonly NancyHost _nancyHost;
        private NotifyIcon _notifyIcon;

        public ChooieApplicationContext(NancyHost nancyHost, string uri)
        {
            _nancyHost = nancyHost;
            _uri = uri;
        }

        public void Initialize()
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
            System.Diagnostics.Process.Start(_uri);
        }

        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false;
            base.ExitThreadCore();
        }
    }
}
