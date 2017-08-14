using System.Reflection;

namespace Architecture.Tests.Infrustructure.Exception
{
    public class KBitException : System.Exception
    {
        public readonly MethodBase MethodInfo;

        public KBitException(MethodBase methodInfo, string message): base(message)
        {
            this.MethodInfo = methodInfo;
        }
    }
}