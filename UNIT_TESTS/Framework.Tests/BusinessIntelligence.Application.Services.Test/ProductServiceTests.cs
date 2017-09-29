using System;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.Tests.BusinessIntelligence.Application.Services.Test
{
    [TestClass]
    public class ProductServiceTests
    {
        private readonly IAutoResolver _iocContainer;

        public ProductServiceTests()
        {
            _iocContainer = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IProductService target = _iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IProductService));
        }

        #region Test Add
        [TestMethod]
        public void TestProductServiceAdd_MustFailGIvenNullRequest()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            ProductResponse response = service.Add(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestCanAddProduct_MustFailGIvenInvalidApplicationModel()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            ProductResponse response = service.Add(new ProductServiceRequest() { ApplicationModel = null });

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Product is a required field.", response.Message);
        }

        [TestMethod]
        public void TestProductServiceAdd_MustAddSuccessfully()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            IProductRepository repository = this._iocContainer.Resolve<IProductRepository>();
            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());
            ProductResponse response = service.Add(new ProductServiceRequest()
            {
                ApplicationModel = new ProductAm
                {
                    Description = "Some Description 123",
                    PricingClassification = pricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(),
                    Rate = pricingClassification.Rate,
                    VatClassification = pricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter()
                }
            });
            
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }

        [TestMethod]
        public void TestProductServiceAdd_MustNotCreateANewProductDueToNullProductObject()
        {
            ProductServiceRequest productServiceRequest = new ProductServiceRequest();
            ProductAm productAm = null;
            productServiceRequest.ApplicationModel = productAm;

            IProductService target = _iocContainer.Resolve<IProductService>();
            target.Add(productServiceRequest);

            ProductResponse productResponse = target.GetById(productServiceRequest);

            Assert.IsNotNull(productResponse);
            Assert.AreEqual(ServiceResult.Error, productResponse.ServiceResult);
            Assert.AreEqual(null, productResponse.ApplicationModel);
        }

        [TestMethod]
        public void TestCanAddProduct_MustFailGIvenInvalidRequireProductField_Description()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());
            ProductResponse response = service.Add(new ProductServiceRequest()
            {
                ApplicationModel = new ProductAm()
                {
                    Description = null,
                    PricingClassification = pricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(),
                    Rate = pricingClassification.Rate,
                    VatClassification = pricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter()
                }
            });

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Description is a required field.", response.Message);
        }

        #endregion

        #region Test Update
        [TestMethod]
        public void TestCanUpdateProduct_MustFailGIvenNullRequest()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            ProductResponse response = service.Update(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateProduct_MustFailGIvenInvalidApplicationModel()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            ProductResponse response = service.Update(new ProductServiceRequest() { ApplicationModel = null });

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Product is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateProduct_MustFailGIvenInvalidRequireProductField_Description()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());
            ProductResponse response = service.Add(new ProductServiceRequest()
            {
                ApplicationModel = new ProductAm()
                {
                    Description = null,
                    PricingClassification = pricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(),
                    Rate = pricingClassification.Rate,
                    VatClassification = pricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter()
                }
            });

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Description is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateBusiness_MustUpdateSuccessfully()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            Assert.IsNotNull(service);

            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());
            ProductResponse response = service.Update(new ProductServiceRequest()
            {
                ApplicationModel = new ProductAm()
                {
                    Description = "Description One",
                    PricingClassification = pricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(),
                    Rate = pricingClassification.Rate,
                    VatClassification = pricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter(),
                    Id = "59aeebffe34973364cb43a31"
                }
            });
            
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }

        #endregion

        #region Test GetAll
        [TestMethod]
        public void TestCanGeAllProduct_MustGetAllSuccessfully()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();

            ProductServiceRequest serviceRequest = new ProductServiceRequest();

            ProductResponse customerResponse = service.GetAll(serviceRequest);

            Assert.IsNotNull(customerResponse);
            Assert.IsNotNull(customerResponse.ApplicationModels);
            Assert.AreNotEqual(0, customerResponse.ApplicationModels.Count());
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Description);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().PricingClassification);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Rate);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().VatClassification);

            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Description);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().PricingClassification);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Rate);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().VatClassification);
        }


        #endregion

        #region Test GetById

        [TestMethod]
        public void TestCanGetCustomerById_MustGetByIdSuccessfully()
        {
            UnitTestContext.Initialize();
            IProductService service = this._iocContainer.Resolve<IProductService>();
            IDomainFactory<Product, ProductAm> factory = this._iocContainer.Resolve<IDomainFactory<Product, ProductAm>>();
            IProductRepository repository = this._iocContainer.Resolve<IProductRepository>();

            Assert.IsNotNull(service);

            PricingClassification pricingClassification = new PerUnitClassification(100, new Vat());
            ProductAm productAm = new ProductAm()
            {
                Description = "Some Description 123",
                PricingClassification = pricingClassification.GetType().Name.InsertSpaceAfterCapitalLetter(),
                Rate = pricingClassification.Rate,
                VatClassification = pricingClassification.Vat.GetType().Name.InsertSpaceAfterCapitalLetter()
            };

            Product product = factory.BuildDomainEntityType(productAm);
            repository.Add(product);

            ProductServiceRequest serviceRequest = new ProductServiceRequest();
            serviceRequest.EntityId = product.Id;

            ProductResponse businessResponse = service.GetById(serviceRequest);

            Assert.IsNotNull(businessResponse);
            Assert.IsNotNull(businessResponse.ApplicationModel.Description);
            Assert.IsNotNull(businessResponse.ApplicationModel.PricingClassification);
            Assert.IsNotNull(businessResponse.ApplicationModel.Rate);
            Assert.IsNotNull(businessResponse.ApplicationModel.VatClassification);
            Assert.IsNotNull(businessResponse.ApplicationModel.Id);
        }

        #endregion
    }
}
