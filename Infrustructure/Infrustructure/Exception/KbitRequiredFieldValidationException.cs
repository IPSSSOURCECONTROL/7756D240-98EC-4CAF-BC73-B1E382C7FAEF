using System;
using System.Runtime.Serialization;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Exception
{
    [Serializable]
    public class KbitRequiredFieldValidationException : System.Exception
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