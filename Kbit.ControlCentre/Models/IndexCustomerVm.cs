using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kbit.ControlCentre.Models
{
    public class IndexCustomerVm: ViewModelBase
    {
        public IEnumerable<ViewEditCustomerVm> ViewModels { get; set; } = new List<ViewEditCustomerVm>();
    }
}