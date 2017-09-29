using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public class Product: BusinessEntity
    {
        public Product(string productCode, string description, 
            PricingClassification pricingClassification1, string businessId) :base(businessId)
        {
            this.ProductCode = productCode;
            this.Description = description;
            this.PricingClassification = pricingClassification1;
        }

        public string Description { get; private set; }
        public string ProductCode { get; private set; }
        public PricingClassification PricingClassification { get; private set; }

        public void UpdateDescription(string description)
        {
            if(string.IsNullOrWhiteSpace(description))
                throw new InvalidOperationException("Invalid description");

            this.Description = description;
        }

        public void UpdateProductCode(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                throw new InvalidOperationException("Invalid product code");

            this.ProductCode = productCode;
        }

        public void UpdatePricingClassification(PricingClassification pricingClassification)
        {
            if(pricingClassification == null)
                throw new InvalidOperationException("Invalid Pricing Classification");

            this.PricingClassification = pricingClassification;
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }

        public decimal CalculateTotalAmount(int quantity, decimal discount)
        {
            decimal totalWithoutDiscount = quantity * this.PricingClassification.Rate;

            decimal totalDiscount = totalWithoutDiscount * (discount/100);

            decimal totalWithoutVatApplied = totalWithoutDiscount - totalDiscount;

            decimal totalVat = totalWithoutVatApplied * (this.PricingClassification.Vat.Percentage/100);

            return totalWithoutVatApplied + totalVat;
        }
    }
}