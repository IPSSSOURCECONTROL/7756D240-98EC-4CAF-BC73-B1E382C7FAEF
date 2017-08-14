using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Database
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
