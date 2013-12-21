using System.IO;
using Chooie.Interface.Logging;

namespace Chooie.Database
{
    public class DatabaseAccessor : IDatabaseAccessor
    {
        private readonly ILogger _logger;
        public const string DatabaseFile = "db.txt";

        public DatabaseAccessor(ILogger logger)
        {
            _logger = logger;
        }

        public void SetupDatabase()
        {
            if (!File.Exists(DatabaseFile))
            {
                _logger.LogInfo("Creating database");
                using (File.AppendText(DatabaseFile)) {}
            }
        }

        public string ReadDatabase()
        {
            var file = new StreamReader(DatabaseFile);
            var json = file.ReadToEnd();
            file.Close();
            return json;
        }

        public void WriteToDatabase(string databaseJson)
        {
            File.WriteAllText(DatabaseFile, databaseJson);
        }
    }
}