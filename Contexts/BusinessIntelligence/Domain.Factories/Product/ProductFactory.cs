using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories.Product
{
    public class ProductFactory : IDomainFactory<Domain.Product.Product, ProductAm>
    {
        private readonly IObjectActivator _objectActivator;

        public ProductFactory(IObjectActivator objectActivator)
        {
            this._objectActivator = objectActivator;
        }

        [ValidateMethodArguments]
        public Domain.Product.Product BuildDomainEntityType(ProductAm applicationModel, bool isNew)
        {
            applicationModel.Validate();
            Vat vat = (Vat)this._objectActivator.CreateInstanceOf<Vat>(applicationModel.Vat.Replace(" ", string.Empty));
            PricingClassification pricingClassification =
                (PricingClassification)this._objectActivator
                .CreateInstanceOf<PricingClassification>(applicationModel.PricingClassification.Replace(" ",string.Empty),
                    applicationModel.Rate, vat);

            if (isNew)
            {
                return new Domain.Product.Product(applicationModel.Description, pricingClassification);
            }
            else
            {
                Domain.Product.Product product = new Domain.Product.Product(applicationModel.Description, pricingClassification);
                product.Id = applicationModel.Id;
                return product;
            }
            
        }

        [ValidateMethodArguments]
        public ProductAm BuildApplicationModelType(Domain.Product.Product domainEntity)
        {
            ProductAm productAm = new ProductAm();

            productAm.Description = domainEntity.Description;
            productAm.PricingClassification =
                MessageFormatter.InsertSpaceAfterCapitalLetter(domainEntity.PricingClassification.GetType().Name);
            productAm.Rate = domainEntity.PricingClassification.Rate;
            productAm.Vat = MessageFormatter.InsertSpaceAfterCapitalLetter(domainEntity.PricingClassification.Vat.GetType().Name);
            productAm.Id = domainEntity.Id;

            return productAm;
        }

        [ValidateMethodArguments]
        public IEnumerable<ProductAm> BuildApplicationModelTypes(IEnumerable<Domain.Product.Product> domainEntities)
        {
            List<ProductAm> applicationModels = new List<ProductAm>();

            foreach (Domain.Product.Product product in domainEntities)
            {
                applicationModels.Add(this.BuildApplicationModelType(product));
            }

            return applicationModels;
        }
    }
}
