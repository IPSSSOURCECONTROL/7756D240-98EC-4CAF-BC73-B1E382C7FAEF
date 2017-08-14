using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Exceptions
{
    public class CannotAddNullApplicationFunctionException : Exception
    {
        public CannotAddNullApplicationFunctionException(string message):base(message)
        {
        }
    }
}