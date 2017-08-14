using System;
using System.Reflection;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Exception
{
    [Serializable]
    public class KbitNullArgumentException : KBitException
    {
        public KbitNullArgumentException(MethodBase methodInfo, string message) : base(methodInfo, message)
        {
        }
    }
}