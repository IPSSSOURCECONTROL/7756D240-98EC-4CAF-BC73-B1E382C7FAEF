using System;
using System.Runtime.Serialization;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Exceptions
{
    [Serializable]
    public class CannotAddNullRoleException : Exception
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