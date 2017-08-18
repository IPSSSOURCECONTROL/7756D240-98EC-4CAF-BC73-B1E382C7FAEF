using System.Configuration;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Database;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.BusinessIntelligence.Repository.Tests
{
    [TestClass]
    public class ProductRepositoryTests
    {

        private readonly IAutoResolver _autoResolver;

        public ProductRepositoryTests()
        {
            this._autoResolver = new IocContainer();
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //will drop and recreate the test database after each test...
            BusinessIntelligenceDatabaseContext context = new BusinessIntelligenceDatabaseContext(
              ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString);
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IProductRepository));
        }
    }
}
