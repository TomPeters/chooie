﻿using Microsoft.AspNet.SignalR;

namespace Chooie.SignalR
{
    public class MessageHub : Hub
    {
        public void Send(string dispatchId, string message)
        {
            Clients.All.sendMessage(dispatchId, message);
        }
    }
}
