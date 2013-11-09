using System.IO;

namespace Chooie.Database
{
    public class DatabaseAccessor : IDatabaseAccessor
    {
        public const string DatabaseFile = "db.txt";

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