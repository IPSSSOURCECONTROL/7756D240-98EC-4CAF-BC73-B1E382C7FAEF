using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class IndexProductVm: ViewModelBase
    {
        public IEnumerable<ViewEditProductVm> ViewModels { get; set; } = new List<ViewEditProductVm>();
    }
}