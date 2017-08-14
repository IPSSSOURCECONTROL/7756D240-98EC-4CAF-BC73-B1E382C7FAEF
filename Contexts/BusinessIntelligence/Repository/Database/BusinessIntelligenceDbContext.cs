using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Database
{
    public class BusinessIntelligenceDbContext: MongoDbContext
    {
        public BusinessIntelligenceDbContext(string connectionString) : base(connectionString)
        {
        }

        public BusinessIntelligenceDbContext(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }
    }
}
