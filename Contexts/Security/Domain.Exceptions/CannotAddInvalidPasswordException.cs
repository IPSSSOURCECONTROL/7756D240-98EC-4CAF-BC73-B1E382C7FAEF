using System;
using System.Runtime.Serialization;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Exceptions
{
    [Serializable]
    public class CannotAddInvalidPasswordException : Exception
    {
        public CannotAddInvalidPasswordException()
        {
        }

        public CannotAddInvalidPasswordException(string message) : base(message)
        {
        }

        public CannotAddInvalidPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotAddInvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}