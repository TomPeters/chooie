using System.Collections.Generic;

namespace Chooie.Logging
{
    public class CompositeLog : ILog
    {
        private readonly IEnumerable<ILog> _logs;

        public CompositeLog(IEnumerable<ILog> logs)
        {
            _logs = logs;
        }

        public void Log(LogMessage message)
        {
            foreach (ILog log in _logs)
            {
                log.Log(message);
            }
        }
    }
}
