using System.Reflection;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Exception
{
    public class EntityAlreadyExistException : KBitException
    {
        public EntityAlreadyExistException(MethodBase methodInfo, string message) : base(methodInfo, message)
        {
        }
    }
}