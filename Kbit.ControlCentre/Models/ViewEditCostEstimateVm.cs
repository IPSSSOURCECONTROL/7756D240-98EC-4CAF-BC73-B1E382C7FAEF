using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class ViewEditCostEstimateVm:ViewModelBase
    {
        public ViewEditCostEstimateVm()
        {
            this.Quantity = 1;
        }

        public ViewEditBusinessVm Business { get; set; }
        public ViewEditCustomerVm Customer { get; set; }
        public IEnumerable<ViewEditProductVm> Products { get; set; }=new List<ViewEditProductVm>();
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string ProductListingNumber { get; set; }
    }
}