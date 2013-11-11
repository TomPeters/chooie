using System.Collections.Generic;

namespace Chooie.Logging
{
    public class MemoryLog : IMemoryLog
    {
        private readonly List<LogMessage> _messages = new List<LogMessage>();

        public void Log(LogMessage message)
        {
            _messages.Add(message);
        }

        public IReadOnlyList<LogMessage> Messages
        {
            get { return _messages; }
        }
    }
}
