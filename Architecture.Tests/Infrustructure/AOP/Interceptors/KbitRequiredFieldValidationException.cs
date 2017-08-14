using System;
using System.Runtime.Serialization;

namespace Architecture.Tests.Infrustructure.AOP.Interceptors
{
    [Serializable]
    internal class KbitRequiredFieldValidationException : System.Exception
    {
        public KbitRequiredFieldValidationException()
        {
        }

        public KbitRequiredFieldValidationException(string message) : base(message)
        {
        }

        public KbitRequiredFieldValidationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected KbitRequiredFieldValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}