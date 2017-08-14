using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.ProductListing
{
    public class ProductListingService : IProductListingService
    {
        [ServiceRequestMethod]
        public ProductListingResponse GetById(ProductListingServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public ProductListingResponse GetAll(ProductListingServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public ProductListingResponse Add(ProductListingServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public ProductListingResponse Update(ProductListingServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public ProductListingResponse Delete(ProductListingServiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}