﻿using Microsoft.AspNet.SignalR;
using Nancy;

namespace chui.SignalR
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
