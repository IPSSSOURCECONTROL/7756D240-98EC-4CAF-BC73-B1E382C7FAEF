using System.Configuration;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Database;

namespace KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS
{
    public static class UnitTestContext
    {
        public static void Initialize()
        {
            SecurityDatabaseContext context = new SecurityDatabaseContext(
              ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString);
        }
    }
}
