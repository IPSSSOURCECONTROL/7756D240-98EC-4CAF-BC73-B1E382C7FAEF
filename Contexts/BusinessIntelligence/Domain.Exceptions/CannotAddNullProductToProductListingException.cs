using System;
using System.Runtime.Serialization;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Exceptions
{
    [Serializable]
    public class CannotAddNullProductToProductListingException : Exception
    {
        public CannotAddNullProductToProductListingException()
        {
        }

        public CannotAddNullProductToProductListingException(string message) : base(message)
        {
        }

        public CannotAddNullProductToProductListingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotAddNullProductToProductListingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}