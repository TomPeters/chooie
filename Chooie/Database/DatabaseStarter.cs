using Chooie.ApplicationStart;

namespace Chooie.Database
{
    public class DatabaseStarter : IStarter
    {
        private readonly IDatabaseAccessor _databaseAccessor;

        public DatabaseStarter(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public void Start()
        {
            _databaseAccessor.SetupDatabase();
        }
    }
}
