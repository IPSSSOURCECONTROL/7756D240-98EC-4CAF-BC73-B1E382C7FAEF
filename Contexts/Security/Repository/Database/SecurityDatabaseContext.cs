using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Database
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