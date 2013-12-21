using System.IO;
using Chooie.Interface.Logging;

namespace Chooie.Database
{
    public class DatabaseAccessor : IDatabaseAccessor
    {
        private readonly DatabaseFileNameProvider _databaseFileNameProvider;
        private readonly ILogger _logger;

        public DatabaseAccessor(DatabaseFileNameProvider databaseFileNameProvider, ILogger logger)
        {
            _databaseFileNameProvider = databaseFileNameProvider;
            _logger = logger;
        }

        public void SetupDatabase()
        {
            if (!File.Exists(_databaseFileNameProvider.FileName))
            {
                _logger.LogInfo("Creating database");
                using (File.AppendText(_databaseFileNameProvider.FileName)) {}
            }
        }

        public string ReadDatabase()
        {
            var file = new StreamReader(_databaseFileNameProvider.FileName);
            var json = file.ReadToEnd();
            file.Close();
            return json;
        }

        public void WriteToDatabase(string databaseJson)
        {
            File.WriteAllText(_databaseFileNameProvider.FileName, databaseJson);
        }
    }
}