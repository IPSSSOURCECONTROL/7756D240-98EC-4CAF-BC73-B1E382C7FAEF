using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kbit.ControlCentre.Models
{
    public class ViewEditCostEstimateVm:ViewModelBase
    {
        public ViewEditCostEstimateVm()
        {
            this.Discount = 0.00m;
            this.Quantity = 1;
        }

        public ViewEditBusinessVm Business { get; set; }
        public ViewEditCustomerVm Customer { get; set; }
        public IEnumerable<ViewEditProductVm> Products { get; set; }=new List<ViewEditProductVm>();
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        [Range(0.0,100.00)]
        public decimal Discount { get; set; }
    }
}