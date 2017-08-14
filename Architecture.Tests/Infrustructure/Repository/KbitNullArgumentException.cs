using System;
using System.Reflection;
using Architecture.Tests.Infrustructure.Exception;

namespace Architecture.Tests.Infrustructure.Repository
{
    [Serializable]
    internal class KbitNullArgumentException : KBitException
    {
        public KbitNullArgumentException(MethodBase methodInfo, string message) : base(methodInfo, message)
        {
        }
    }
}