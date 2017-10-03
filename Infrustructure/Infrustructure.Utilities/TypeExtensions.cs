using System;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Converts a strongly type object name into a string. 
        /// Inserts a space after every capital letter.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFrontEndPresentation(this object obj)
        {
            ValidationUtility.CheckReferenceTypeForNull(obj, 
                "Can not convert null type to a Front end presentation.", 
                MethodBase.GetCurrentMethod(), typeof(TypeExtensions));
            Type type = obj.GetType();
            return type.Name.InsertSpaceAfterCapitalLetter();
        }

        public static string Abbreviate(this string name)
        {
            return null;
        }
    }
}
