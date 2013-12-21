namespace Chooie.Database
{
    public interface IDatabaseAccessor
    {
        void SetupDatabase();
        string ReadDatabase();
        void WriteToDatabase(string databaseJson);
    }
}
