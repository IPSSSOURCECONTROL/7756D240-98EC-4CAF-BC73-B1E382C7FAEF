using System;
using System.Runtime.Serialization;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Exception
{
    [Serializable]
    public class NotAuthorizedException : System.Exception
    {
        public NotAuthorizedException()
        {
        }

        public NotAuthorizedException(string message) : base(message)
        {
        }

        public NotAuthorizedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected NotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}