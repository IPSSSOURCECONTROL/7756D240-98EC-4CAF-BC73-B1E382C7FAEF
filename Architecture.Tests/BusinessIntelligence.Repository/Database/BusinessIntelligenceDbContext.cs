using Architecture.Tests.Infrustructure.MongoDb;

namespace Architecture.Tests.BusinessIntelligence.Repository.Database
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
