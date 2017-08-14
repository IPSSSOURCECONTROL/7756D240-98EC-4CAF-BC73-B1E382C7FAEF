using System;

namespace Architecture.Tests.Security.Domain.Exceptions
{
    public class CannotAddNullApplicationFunctionException : Exception
    {
        public CannotAddNullApplicationFunctionException(string message):base(message)
        {
        }
    }
}