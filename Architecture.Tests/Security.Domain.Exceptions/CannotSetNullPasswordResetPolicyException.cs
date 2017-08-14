using System;
using System.Runtime.Serialization;

namespace Architecture.Tests.Security.Domain.Exceptions
{
    [Serializable]
    internal class CannotSetNullPasswordResetPolicyException : Exception
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