using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kbit.ControlCentre.Models
{
    public class IndexProductVm: ViewModelBase
    {
        public IEnumerable<ViewEditProductVm> ViewModels { get; set; } = new List<ViewEditProductVm>();
    }
}