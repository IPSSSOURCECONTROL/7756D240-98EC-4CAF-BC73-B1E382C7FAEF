using System;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public class ProductService : ApplicationServiceBase<ProductResponse, IProductRepository>,
        IProductService
    {
        [ServiceRequestMethod]
        public ProductResponse GetById(ProductServiceRequest request)
        {
            return null;
        }

        [ServiceRequestMethod]
        public ProductResponse GetAll(ProductServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public ProductResponse Add(ProductServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public ProductResponse Update(ProductServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public ProductResponse Delete(ProductServiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}