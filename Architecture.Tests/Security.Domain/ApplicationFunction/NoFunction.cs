using Architecture.Tests.Security.Domain.ApplicationFunction.ApplicationFunctions;

namespace Architecture.Tests.Security.Domain.ApplicationFunction
{
    public class NoFunction : FunctionClassification
    {
        public override string GetName()
        {
            return this.GetType().Name;
        }
    }
}