using System.Reflection;
using Architecture.Tests.Infrustructure.Exception;

namespace Architecture.Tests.Infrustructure.Repository
{
    public class EntityAlreadyExistException : KBitException
    {
        public EntityAlreadyExistException(MethodBase methodInfo, string message) : base(methodInfo, message)
        {
        }
    }
}