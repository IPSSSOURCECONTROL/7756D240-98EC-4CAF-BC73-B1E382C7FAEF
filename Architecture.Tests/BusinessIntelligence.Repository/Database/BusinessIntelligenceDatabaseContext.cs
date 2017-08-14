using Architecture.Tests.Infrustructure.MongoDb;

namespace Architecture.Tests.BusinessIntelligence.Repository.Database
{
    public class BusinessIntelligenceDatabaseContext: DatabaseContextWrapper
    {
        public BusinessIntelligenceDatabaseContext(string connectionString) : base(connectionString)
        {
        }

        protected override void SetUpDbContext()
        {
            this.KbitDatabaseContext = new BusinessIntelligenceDbContext(this.ConnectionString);
        }
    }
}
