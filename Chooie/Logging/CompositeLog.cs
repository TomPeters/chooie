using System.Collections.Generic;

namespace Chooie.Logging
{
    public class CompositeLog : ILog
    {
        private readonly IReadOnlyList<ILog> _logs;

        public CompositeLog(IReadOnlyList<ILog> logs)
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
