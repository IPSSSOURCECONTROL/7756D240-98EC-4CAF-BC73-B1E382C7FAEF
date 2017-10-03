namespace Kbit.ControlCentre.Models
{
    public class ProductListingFooterTotalsVm : ViewModelBase
    {
        public decimal SubTotal { get; set; } = 0.00m;
        public decimal TotalDiscount { get; set; } = 0.00m;
        public decimal TotalVat { get; set; } = 0.00m;
    }
}