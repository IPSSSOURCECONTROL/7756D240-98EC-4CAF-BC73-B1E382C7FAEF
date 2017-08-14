using System;
using System.Runtime.Serialization;

namespace Architecture.Tests.Security.Domain.Exceptions
{
    [Serializable]
    internal class CannotAddNullRoleException : Exception
    {
        public CannotAddNullRoleException()
        {
        }

        public CannotAddNullRoleException(string message) : base(message)
        {
        }

        public CannotAddNullRoleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotAddNullRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}