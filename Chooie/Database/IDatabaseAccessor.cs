namespace Chooie.Database
{
    public interface IDatabaseAccessor
    {
        string ReadDatabase();
        void WriteToDatabase(string databaseJson);
    }
}
