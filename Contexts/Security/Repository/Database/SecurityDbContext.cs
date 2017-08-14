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

    //public class SecurityDbContext: DbContext
    //{
    //    public SecurityDbContext(string connectionString) : base(connectionString)
    //    {

    //    }


    //    public DbSet<PasswordDM> Passwords { get; set; }
    //    public DbSet<UserDM> Users { get; set; }
    //    public DbSet<LicenseDM> Licenses { get; set; }

    //    public DbSet<ApplicationFunctionDM> ApplicationFunctions { get; set; }

    //    public DbSet<RoleToApplicationFunctionDM> RoleToApplicationFunction { get; set; }

    //    public DbSet<RoleDM> Roles { get; set; }
    //    public DbSet<UserToBusinessDM> UserToBusiness{ get; set; }
    //    public DbSet<FunctionClassificationDM> FunctionClassification { get; set; }
    //    public DbSet<RoleToFunctionClassificationDM> RoleToFunctionClassification { get; set; }
    //    public DbSet<ApplicationFunctionToFunctionClassificationDM> ApplicationFunctionToFunctionClassification { get; set; }

    //}
}
