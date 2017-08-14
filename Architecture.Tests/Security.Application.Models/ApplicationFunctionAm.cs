using Architecture.Tests.Infrustructure.Application.Model;

namespace Architecture.Tests.Security.Application.Models
{
    public class ApplicationFunctionAm: ApplicationModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string FunctionClassification { get; set; }

        public bool RequiresDualAuthorization { get; set; }

    }
}
