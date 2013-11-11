using System;

namespace Chooie.Logging
{
    public class LogMessage
    {
        public LogMessage(string message, IContext context, Severity severity)
        {
            Message = message;
            Severity = severity;
            Context = context;
            Time = DateTime.Now;
        }

        public string Message { get; private set; }
        public Severity Severity { get; private set; }
        public IContext Context { get; private set; }
        public DateTime Time { get; private set; }
    }
}