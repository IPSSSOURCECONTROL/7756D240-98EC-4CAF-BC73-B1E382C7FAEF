using System.Reflection;
using Architecture.Tests.Infrustructure.Exception;

namespace Architecture.Tests.Infrustructure.Workflow.Exceptions
{
    public class EntityDoesNotExistException : KBitException
    {
        public EntityDoesNotExistException(MethodBase methodBase, string message) : base(methodBase, message)
        {
        }
    }
}