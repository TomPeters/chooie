using Chooie.Core.Logging;

namespace Chooie.Logging
{
    public class Logger : ILogger
    {
        private readonly IContext _context;
        private readonly ILog _log;

        public Logger(IContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public void LogInfo(string message)
        {
            _log.Log(CreateLogMessage(message, Severity.Info));
        }

        public void LogWarn(string message)
        {
            _log.Log(CreateLogMessage(message, Severity.Warn));
        }

        public void LogError(string message)
        {
            _log.Log(CreateLogMessage(message, Severity.Error));
        }

        private LogMessage CreateLogMessage(string message, Severity severity)
        {
            return new LogMessage(message, _context, severity);
        }
    }
}
