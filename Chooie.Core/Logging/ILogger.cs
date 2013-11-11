namespace Chooie.Core.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogError(string message);
    }
}
