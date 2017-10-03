using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class IndexCustomerVm: ViewModelBase
    {
        public IEnumerable<ViewEditCustomerVm> ViewModels { get; set; } = new List<ViewEditCustomerVm>();
    }
}