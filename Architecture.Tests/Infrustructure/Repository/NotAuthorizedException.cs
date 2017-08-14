using System;
using System.Runtime.Serialization;

namespace Architecture.Tests.Infrustructure.Repository
{
    [Serializable]
    internal class NotAuthorizedException : System.Exception
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