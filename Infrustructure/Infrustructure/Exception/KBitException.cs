using System.Reflection;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Exception
{
    /// <summary>
    /// Base KBIT exception.
    /// </summary>
    public class KBitException : System.Exception
    {
        public readonly MethodBase MethodInfo;

        public KBitException(MethodBase methodInfo, string message): base(message)
        {
            this.MethodInfo = methodInfo;
        }
    }
}