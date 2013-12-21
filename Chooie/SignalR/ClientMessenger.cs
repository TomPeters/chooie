using Microsoft.AspNet.SignalR;

namespace Chooie.SignalR
{
    internal class ClientMessenger : IClientMessenger
    {
        public void SendMessage(string dispatchId, string message)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            hubContext.Clients.All.sendMessage(dispatchId, message);
        }
    }
}
