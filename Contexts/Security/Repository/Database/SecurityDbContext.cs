using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Database
{
    public class SecurityDbContext: MongoDbContext
    {
        public SecurityDbContext(string connectionString) : base(connectionString)
        {
        }

        public SecurityDbContext(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }
    }
}
