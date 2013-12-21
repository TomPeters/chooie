namespace Chooie.Logging
{
    public class ThreadSafeLog : ILog
    {
        private readonly object _lock = new object();
        private readonly ILog _wrappedLog;

        public ThreadSafeLog(ILog wrappedLog)
        {
            _wrappedLog = wrappedLog;
        }

        public void Log(LogMessage message)
        {
            lock (_lock)
            {
                _wrappedLog.Log(message);
            }
        }
    }
}
