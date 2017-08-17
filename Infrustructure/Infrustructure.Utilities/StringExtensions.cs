using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpaces(this string value)
        {
            return value.Trim().Replace(" ", string.Empty);
        }

        public static string InsertSpaceAfterCapitalLetter(this string input)
        {
            // check if string is empty
            if (string.IsNullOrEmpty(input))
                return string.Empty;


            if (input.Contains("_"))
            {
                return input.Replace('_', ' ');
            }
            else
            {
                StringBuilder newString = new StringBuilder();
                foreach (Char char1 in input)
                {
                    if (char.IsUpper(char1))
                        newString.Append(new char[] { ' ', char1 });
                    else
                        newString.Append(char1);
                }

                newString.Remove(0, 1);
                return newString.ToString();
            }
        }
    }
}
