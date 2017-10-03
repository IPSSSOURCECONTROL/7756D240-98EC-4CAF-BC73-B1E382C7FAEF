using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.Tests.BusinessIntelligence.Domain.Factories.Tests
{
    [TestClass]
    public class ProductFactoryTests
    {
        private IAutoResolver _iocContainer;

        public ProductFactoryTests()
        {
            this._iocContainer = new IocContainer();

        }
        [TestMethod]
        public void TestCanResolveDependency()
        {
            IDomainFactory<Product, ProductAm> target = this._iocContainer.Resolve<IDomainFactory<Product, ProductAm>>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IDomainFactory<Product, ProductAm>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildDomainEntityType_MustFailGivenNullArgument()
        {
            IDomainFactory<Product, ProductAm> target = this._iocContainer.Resolve<IDomainFactory<Product, ProductAm>>();
            target.BuildDomainEntityType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(KbitRequiredFieldValidationException))]
        public void TestBuildDomainEntityType_MustFailGivenInvalidApplicationModelProperty()
        {
            IDomainFactory<Product, ProductAm> target = this._iocContainer.Resolve<IDomainFactory<Product, ProductAm>>();

            ProductAm productAm = new ProductAm();
            //All properties are not set, it will fail.

            target.BuildDomainEntityType(productAm);
        }
        
        [TestMethod]
        public void TestBuildDomainEntityType_MustSuccessfullyCreateDomainEntity()
        {
            IDomainFactory<Product, ProductAm> target = this._iocContainer.Resolve<IDomainFactory<Product, ProductAm>>();

            IProductRepository repository = this._iocContainer.Resolve<IProductRepository>();
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());

            ProductAm productAm = new ProductAm
            {
                Description = "Some Description",
                PricingClassification = pricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(),
                Rate = pricingClassification.Rate,
                VatClassification = pricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter()
            };

            Product product = target.BuildDomainEntityType(productAm);

            Assert.IsNotNull(product);
            Assert.AreEqual(productAm.Description, product.Description);
            Assert.AreEqual(productAm.PricingClassification, product.PricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter());
            Assert.AreEqual(productAm.VatClassification, product.PricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter());
            Assert.AreEqual(productAm.Rate, product.PricingClassification.Rate);
        }

        [TestMethod]
        public void TestBuildDomainEntityType_MustSuccessfullyCreateApplicationModelFromDomainEntity()
        {
            IDomainFactory<Product, ProductAm> target = this._iocContainer.Resolve<IDomainFactory<Product, ProductAm>>();

            IProductRepository repository = this._iocContainer.Resolve<IProductRepository>();
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());
            ProductAm productAm = new ProductAm
            {
                Description = "Some Description 123",
                PricingClassification = pricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(),
                Rate = pricingClassification.Rate,
                VatClassification = pricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter()
            };

            Product product = target.BuildDomainEntityType(productAm);

            Assert.IsNotNull(product);
            Assert.AreEqual(productAm.Description, product.Description);

            ProductAm applicationModel = target.BuildApplicationModelType(product);

            Assert.IsNotNull(applicationModel);
            Assert.AreEqual(product.PricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(), applicationModel.PricingClassification);
            Assert.AreEqual(product.PricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter(), applicationModel.VatClassification);
            Assert.AreEqual(product.PricingClassification.Rate, applicationModel.Rate);
            Assert.AreEqual(product.Description, applicationModel.Description);
        }
    }
}
