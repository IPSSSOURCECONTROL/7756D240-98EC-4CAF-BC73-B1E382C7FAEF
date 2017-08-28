using System;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Validation
{
    public static class ValidationUtility
    {
        public static void IsNullEmptyOrWhitespace(string target, string message, 
            MethodBase method = null, Type type = null)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                ThrowError(message, method, type);
            }
        }

        public static void CheckReferenceTypeForNull(object target, string message, 
            MethodBase method = null,Type type = null)
        {
            if(target == null)
                ThrowError(message, method, type);
        }

        public static void CheckField(object target, string message,
                MethodBase method = null, Type type = null)
        {
            if (target == null)
                throw new KbitRequiredFieldValidationException(message);

            if (target is string)
            {
                if (string.IsNullOrWhiteSpace((string) target))
                {
                     throw new KbitRequiredFieldValidationException(message);
                }
            }
        }

        public static void ThrowInvalidOperationError(string message,
            MethodBase method = null, Type type = null)
        {
            ThrowError(message, method, type);
        }

        private static void ThrowError(string message, MethodBase method, Type type)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new InvalidOperationException("Can not call validation function with an invalid message parameter.");

            if (method != null && type != null)
                throw new ArgumentException(
                    $"Exception: {message} | Origin: {type.AssemblyQualifiedName} | Method: {method.Name} | Declarying Type: {method.DeclaringType.Name}");

            if (method != null && type == null)
                throw new ArgumentException($"Exception: {message} | Method: {method.Name} | Declarying Type: {method.DeclaringType.Name}");

            if (method == null && type != null)
                throw new ArgumentException($"Exception: {message} | Origin: {type.AssemblyQualifiedName} | Declarying Type: {method.DeclaringType.Name}");

            throw new ArgumentException($"Exception: {message}");
        }

    }
}
