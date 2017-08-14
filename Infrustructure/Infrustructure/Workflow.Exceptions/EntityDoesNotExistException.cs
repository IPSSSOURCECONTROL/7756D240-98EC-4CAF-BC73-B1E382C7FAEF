using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions
{
    public class EntityDoesNotExistException : KBitException
    {
        public EntityDoesNotExistException(MethodBase methodBase, string message) : base(methodBase, message)
        {
        }
    }
}