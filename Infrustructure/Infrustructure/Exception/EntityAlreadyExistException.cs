using System.Reflection;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Exception
{
    /// <summary>
    /// Throw this exception if an Entity does not exist.
    /// </summary>
    public class EntityAlreadyExistException : KBitException
    {
        public EntityAlreadyExistException(MethodBase methodInfo, string message) : base(methodInfo, message)
        {
        }
    }
}