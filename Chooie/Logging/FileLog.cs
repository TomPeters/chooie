using System.IO;

namespace Chooie.Logging
{
    public class FileLog : ILog
    {
        private readonly string _fileLocation;

        public FileLog(string fileLocation)
        {
            _fileLocation = fileLocation;
        }

        public void Log(LogMessage message)
        {
            using (var streamWriter = File.AppendText(_fileLocation))
            {
                streamWriter.WriteLine(GetStringFromMessage(message));
            }
        }

        private string GetStringFromMessage(LogMessage message)
        {
            return message.Time.ToShortDateString() + " " + 
                message.Time.ToShortTimeString() + ": " + 
                message.Context.Caption + ": " + 
                GetSeverityCaptionFromSeverity(message.Severity) + ": " + 
                message.Message;
        }

        private string GetSeverityCaptionFromSeverity(Severity severity)
        {
            switch (severity)
            {
                case Severity.Error:
                    return "Error";
                case Severity.Warning:
                    return "Warning";
                case Severity.Info:
                    return "Info";
            }
            return "Error";
        }
    }
}
