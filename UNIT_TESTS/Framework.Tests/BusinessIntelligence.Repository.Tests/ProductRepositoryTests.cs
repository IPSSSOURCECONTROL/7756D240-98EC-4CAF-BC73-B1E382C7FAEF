using System;
using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
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
            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IProductRepository));
        }

        #region Test Add
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProductRepositoryAdd_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(null);
        }

        [TestMethod]
        public void TestProductRepositoryAdd_MustCreateANewProduct()
        {
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());

            Product expectedProduct = new Product("Test description", pricingClassification, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(expectedProduct);

            Product actualProduct = target.GetById(expectedProduct.Id);

            Assert.IsNotNull(actualProduct);
            Assert.AreEqual(expectedProduct.Description, actualProduct.Description);
            Assert.IsInstanceOfType(actualProduct.PricingClassification, typeof(PerUnitClassification));
            Assert.IsInstanceOfType(actualProduct.PricingClassification.Vat, typeof(Vat));
            Assert.AreEqual(actualProduct.PricingClassification.Rate, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestProductRepositoryAdd_MustNotCreateANewProductDueToProductAlreadyExisting()
        {
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());

            Product expectedProduct = new Product("Test description2", pricingClassification, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(expectedProduct);
            target.Add(expectedProduct);
        }
        #endregion

        #region Test Delete
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProductRepositoryDelete_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Delete(null);
        }

        [TestMethod]
        public void TestProductRepositoryDelete_MustDeleteExistingProduct()
        {
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());

            Product testProduct = new Product("Deleted Product", pricingClassification, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(testProduct);
            target.Delete(testProduct);

            Product actualProduct = target.GetById(testProduct.Id);

            Assert.IsNull(actualProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestProductRepositoryDelete_MustNotDeleteProductDueToProductNotExisting()
        {
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());

            Product expectedProduct = new Product("Non-Existent Product", pricingClassification,null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Delete(expectedProduct);
        }
        #endregion

        #region Test GetAll
        [TestMethod]
        public void TestProductRepositoryGetAll_MustReturnAllExistingProducts()
        {
            PricingClassification pricingClassification1 = new PerUnitClassification(100, new Vat());
            PricingClassification pricingClassification2 = new PerDayClassification(200, new NoVat());

            Product testProduct1 = new Product("Test Get All Products 1", pricingClassification1, null);
            Product testProduct2 = new Product("Test Get All Products 2", pricingClassification2, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(testProduct1);
            target.Add(testProduct2);

            IEnumerable<Product> actualProductList = target.GetAll();

            Assert.IsNotNull(actualProductList);
            Assert.AreNotEqual(0, actualProductList.Count());
            Assert.IsInstanceOfType(actualProductList.Select(x => x.PricingClassification).FirstOrDefault(), typeof(PerUnitClassification));
            Assert.IsInstanceOfType(actualProductList.Select(x => x.PricingClassification).LastOrDefault(), typeof(PerDayClassification));
            Assert.IsInstanceOfType(actualProductList.Select(x => x.PricingClassification.Vat).FirstOrDefault(), typeof(Vat));
            Assert.IsInstanceOfType(actualProductList.Select(x => x.PricingClassification.Vat).LastOrDefault(), typeof(NoVat));
            Assert.AreEqual(actualProductList.Select(x => x.PricingClassification.Rate).FirstOrDefault(), 100);
            Assert.AreEqual(actualProductList.Select(x => x.PricingClassification.Rate).LastOrDefault(), 200);

            target.Delete(testProduct1);
            target.Delete(testProduct2);
        }
        #endregion

        #region Test GetByID
        [TestMethod]
        public void TestProductRepositoryGetByID_MustReturnAnExistingProduct()
        {
            PricingClassification pricingClassification1 = new PerUnitClassification(100, new Vat());

            Product testProduct = new Product("Test Get By ID", pricingClassification1, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(testProduct);

            Product actualProduct = target.GetById(testProduct.Id);

            Assert.IsNotNull(actualProduct);
            Assert.AreEqual(testProduct.Id, actualProduct.Id);
            Assert.IsInstanceOfType(actualProduct.PricingClassification, typeof(PerUnitClassification));
            Assert.IsInstanceOfType(actualProduct.PricingClassification.Vat, typeof(Vat));
            Assert.AreEqual(actualProduct.PricingClassification.Rate, 100);
            
        }
        #endregion

        #region Test Is Exists
        [TestMethod]
        public void TestProductRepositoryIsExists_MustReturnTrueGivenAnExistingProduct()
        {
            PricingClassification pricingClassification1 = new PerUnitClassification(100, new Vat());

            Product testProduct = new Product("Test Is Exists True", pricingClassification1, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(testProduct);

            bool actualResult = target.IsExist(testProduct);

            Assert.AreEqual(true, actualResult);

        }

        [TestMethod]
        public void TestProductRepositoryIsExists_MustReturnFalseGivenANonExistingProduct()
        {
            PricingClassification pricingClassification1 = new PerUnitClassification(100, new Vat());

            Product testProduct = new Product("Test Is Exists False", pricingClassification1, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();

            bool actualResult = target.IsExist(testProduct);
            
            Assert.AreEqual(false, actualResult);

        }
        #endregion

        #region Test Update
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProductRepositoryUpdate_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Update(null);
        }

        [TestMethod]
        public void TestProductRepositoryUpdate_MustUpdateAnExistingProduct()
        {
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());

            Product initialProduct = new Product("Test New Product To Be Updated", pricingClassification, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Add(initialProduct);
            
            initialProduct.PricingClassification.Rate = 300;
            initialProduct.PricingClassification.Vat = new NoVat();

            target.Update(initialProduct);
            Product updatedProduct = target.GetById(initialProduct.Id);

            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual("Test New Product To Be Updated", updatedProduct.Description);
            Assert.IsInstanceOfType(updatedProduct.PricingClassification, typeof(PerUnitClassification));
            Assert.IsInstanceOfType(updatedProduct.PricingClassification.Vat, typeof(NoVat));
            Assert.AreEqual(updatedProduct.PricingClassification.Rate, 300);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestProductRepositoryUpdate_MustThrowExceptionDueToProductNotExisting()
        {
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());

            Product expectedProduct = new Product("Another Non-Existent Product", pricingClassification, null);

            IProductRepository target = this._autoResolver.Resolve<IProductRepository>();
            target.Update(expectedProduct);
        }
        #endregion

    }
}
