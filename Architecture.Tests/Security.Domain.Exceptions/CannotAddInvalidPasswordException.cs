using System;
using System.Runtime.Serialization;

namespace Architecture.Tests.Security.Domain.Exceptions
{
    [Serializable]
    internal class CannotAddInvalidPasswordException : Exception
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