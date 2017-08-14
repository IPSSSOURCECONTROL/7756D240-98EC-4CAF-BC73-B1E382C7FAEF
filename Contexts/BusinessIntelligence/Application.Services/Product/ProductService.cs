using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public class ProductService : IProductService
    {
        [ServiceRequestMethod]
        public ProductResponse GetById(ProductServiceRequest request)
        {
            throw new NotImplementedException();
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