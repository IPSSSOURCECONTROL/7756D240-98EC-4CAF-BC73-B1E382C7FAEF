using KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction.ApplicationFunctions;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction
{
    public class NoFunction : FunctionClassification
    {
        public override string GetName()
        {
            return this.GetType().Name;
        }
    }
}