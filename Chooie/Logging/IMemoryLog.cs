using System.Collections.Generic;

namespace Chooie.Logging
{
    public interface IMemoryLog : ILog
    {
        IReadOnlyList<LogMessage> Messages { get; }
    }
}