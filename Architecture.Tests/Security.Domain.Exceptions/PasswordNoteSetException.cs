using System;
using System.Runtime.Serialization;

namespace Architecture.Tests.Security.Domain.Exceptions
{
    [Serializable]
    internal class PasswordNoteSetException : Exception
    {
        public PasswordNoteSetException()
        {
        }

        public PasswordNoteSetException(string message) : base(message)
        {
        }

        public PasswordNoteSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordNoteSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}