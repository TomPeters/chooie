using Microsoft.AspNet.SignalR;
using Owin;

namespace Chooie.SignalR
{
    internal class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(new HubConfiguration() { EnableJSONP = true });
        }
    }
}
