using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;

namespace BusinessIntelligence.Repository.Tests
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
            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IProductRepository));
        }
    }
}
