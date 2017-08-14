using System;
using System.Runtime.Serialization;

namespace Architecture.Tests.Security.Domain.Exceptions
{
    [Serializable]
    internal class CannotAddNullLicenseException : Exception
    {
        public CannotAddNullLicenseException()
        {
        }

        public CannotAddNullLicenseException(string message) : base(message)
        {
        }

        public CannotAddNullLicenseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotAddNullLicenseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}