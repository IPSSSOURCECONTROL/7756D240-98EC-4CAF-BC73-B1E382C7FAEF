using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class IndexUserVm : ViewModelBase
    {
        public IEnumerable<ViewEditUserVm> ViewModels { get; set; } = new List<ViewEditUserVm>();
    }
}