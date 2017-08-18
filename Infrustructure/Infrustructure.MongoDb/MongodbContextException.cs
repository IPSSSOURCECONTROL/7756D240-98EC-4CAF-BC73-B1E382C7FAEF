using System;
using System.Runtime.Serialization;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb
{
    [Serializable]
    public class MongodbContextException : Exception
    {
        public MongodbContextException()
        {
        }

        public MongodbContextException(string message) : base(message)
        {
        }

        public MongodbContextException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MongodbContextException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}