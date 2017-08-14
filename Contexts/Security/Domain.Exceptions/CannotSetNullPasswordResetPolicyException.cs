using System;
using System.Runtime.Serialization;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Exceptions
{
    [Serializable]
    public class CannotSetNullPasswordResetPolicyException : Exception
    {
        public CannotSetNullPasswordResetPolicyException()
        {
        }

        public CannotSetNullPasswordResetPolicyException(string message) : base(message)
        {
        }

        public CannotSetNullPasswordResetPolicyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotSetNullPasswordResetPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}