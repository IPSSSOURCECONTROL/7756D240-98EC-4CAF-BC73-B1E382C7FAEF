using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions
{
    public class CannotAddExistingEntityException : KBitException
    {
        public CannotAddExistingEntityException(MethodBase methodInfo, string message) : base(methodInfo, message)
        {
        }
    }
}