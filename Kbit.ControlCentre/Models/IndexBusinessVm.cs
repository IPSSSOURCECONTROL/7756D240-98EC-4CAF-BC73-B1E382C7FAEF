using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class IndexBusinessVm: ViewModelBase
    {
        public IEnumerable<ViewEditBusinessVm> ViewModels { get; set; }=new List<ViewEditBusinessVm>();
    }
}