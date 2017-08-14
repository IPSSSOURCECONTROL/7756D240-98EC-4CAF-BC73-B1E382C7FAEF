using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Models
{
    public class ApplicationFunctionAm: ApplicationModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string FunctionClassification { get; set; }

        public bool RequiresDualAuthorization { get; set; }

    }
}
