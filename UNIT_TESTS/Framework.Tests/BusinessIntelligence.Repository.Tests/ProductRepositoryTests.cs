using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests.BusinessIntelligence.Repository.Tests
{
    [TestClass]
    public class ProductRepositoryTests
    {

        private readonly IAutoResolver _autoResolver;

        public ProductRepositoryTests()
        {
            this._autoResolver = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            UnitTestContext.Initialize();

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IProductRepository));
        }
    }
}
