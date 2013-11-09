namespace Chooie.Database
{
    public interface IDatabaseManager
    {
        T GetDatabaseObject<T>(string table);
        void SaveDatabaseObject(string table, object databaseObject);
    }
}
