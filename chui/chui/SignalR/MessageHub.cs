using Microsoft.AspNet.SignalR;

namespace chui.SignalR
{
    public class MessageHub : Hub
    {
        public void Send(string message)
        {
            //Clients.All.addMessage(message);
        }
    }
}
