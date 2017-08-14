using Architecture.Tests.Infrustructure.MongoDb;

namespace Architecture.Tests.Security.Repository.Database
{
    public class SecurityDatabaseContext: DatabaseContextWrapper
    {

        protected override void SetUpDbContext()
        {
            this.KbitDatabaseContext = new SecurityDbContext(this.ConnectionString);
            
        }

        public SecurityDatabaseContext(string connectionString) : base(connectionString)
        {
        }
    }
}