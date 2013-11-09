using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chooie.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IDatabaseAccessor _databaseAccessor;

        public DatabaseManager(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public T GetDatabaseObject<T>(string table)
        {
            var jsonRoot = GetFullDatabaseJsonObject();
            JToken databaseObjectAsJson = jsonRoot[table];
            if (databaseObjectAsJson == null) return default(T);
            return JsonConvert.DeserializeObject<T>(jsonRoot[table].ToString());
        }

        public void SaveDatabaseObject(string table, object databaseObject)
        {
            var jsonRoot = GetFullDatabaseJsonObject();
            if (databaseObject == null)
            {
                JToken jsonObjectNode = jsonRoot.Property(table);
                if (jsonObjectNode != null)
                {
                    jsonObjectNode.Remove();
                }
            }
            else
            {
                jsonRoot[table] = JsonConvert.SerializeObject(databaseObject);
            }
            _databaseAccessor.WriteToDatabase(jsonRoot.ToString());
        }

        private JObject GetFullDatabaseJsonObject()
        {
            var rawJsonString = _databaseAccessor.ReadDatabase();
            var parsedJsonString = rawJsonString == string.Empty ? @"{}" : rawJsonString;
            JObject jsonRoot = JObject.Parse(parsedJsonString);
            return jsonRoot;
        }
    }
}