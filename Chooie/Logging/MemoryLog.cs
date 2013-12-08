using System.Collections.Generic;
using Chooie.SignalR;
using Nancy.Responses;
using Newtonsoft.Json;

namespace Chooie.Logging
{
    public class MemoryLog : IMemoryLog
    {
        private readonly IClientMessenger _clientMessenger;
        private readonly List<LogMessage> _messages = new List<LogMessage>();

        public MemoryLog(IClientMessenger clientMessenger)
        {
            _clientMessenger = clientMessenger;
        }

        public void Log(LogMessage message)
        {
            _messages.Add(message);
            _clientMessenger.SendMessage("Log Message", ConvertLogMessageToJson(message));
        }

        private string ConvertLogMessageToJson(LogMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public IReadOnlyList<LogMessage> Messages
        {
            get { return _messages; }
        }
    }
}
