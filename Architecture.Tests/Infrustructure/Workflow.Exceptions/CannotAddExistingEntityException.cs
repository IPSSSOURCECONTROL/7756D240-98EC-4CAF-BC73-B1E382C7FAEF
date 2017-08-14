using System.Reflection;
using Architecture.Tests.Infrustructure.Exception;

namespace Architecture.Tests.Infrustructure.Workflow.Exceptions
{
    public class CannotAddExistingEntityException : KBitException
    {
        public CannotAddExistingEntityException(MethodBase methodInfo, string message) : base(methodInfo, message)
        {
        }
    }
}