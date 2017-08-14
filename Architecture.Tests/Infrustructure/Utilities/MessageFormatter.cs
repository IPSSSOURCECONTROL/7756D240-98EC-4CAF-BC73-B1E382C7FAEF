using System;
using System.Reflection;
using System.Text;
using Architecture.Tests.Infrustructure.Domain;
using Architecture.Tests.Infrustructure.Validation;

namespace Architecture.Tests.Infrustructure.Utilities
{
    public class MessageFormatter
    {
        public static object FormatException(System.Exception exception)
        {
            StringBuilder errorMessage = new StringBuilder();

            errorMessage.AppendLine($"[EXCEPTION: {exception.Message}] [STACKTRACE: {exception.StackTrace}]");

            if (exception.InnerException != null)
            {
                errorMessage.AppendLine($"[INNER-EXCEPTION: {exception.InnerException.Message}] [INNER-STACKTRACE: {exception.InnerException.StackTrace}]");
            }

            return errorMessage.ToString();
        }

        public static object ExecutingMethodTrace(string methodName, string targetTypeName)
        {
            return
                $"[EXECUTING: {methodName}()] [TYPE: {targetTypeName}]";
        }

        public static object ExitingMethodTrace(string methodName, string targetTypeName, TimeSpan stopwatchElapsed)
        {
            return
                $"[EXITING: {methodName}()] [TYPE: {targetTypeName}] [EXECUTION TIME: {stopwatchElapsed}]";
        }

        public static string RecordWithIdAlreadyExists(string value)
        {
            return $"Record with Id '{value}' already exists.";
        }

        public static string RecordWithNameAlreadyExists(string value)
        {
            return $"Record with name '{value}' already exists.";
        }

        public static string NotSupported(MethodBase getCurrentMethod)
        {
            return $"Function '{getCurrentMethod.Name}' not supported.";
        }

        public static string InvalidConfigurationFilePath(string path)
        {
            return $"Invalid configuration file path. Path: {path}";
        }

        public static string RecordWithIdDoesNotExist(string value)
        {
            return $"Record with Id '{value}' does not exist.";
        }

        public static string EntityCanNotBeNull()
        {
            return "Entity can not be null";
        }

        public static string IsARequiredField(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
                return string.Empty;

            return $"{InsertSpaceAfterCapitalLetter(field)} is a required field.";
        }

        private static string InsertSpaceAfterCapitalLetter(string input)
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

        public static string EntitySuccessfullyAdded<T>(string id) where T: AggregateRoot
        {
            Validator.IsNullEmptyOrWhitespace(id, nameof(id), MethodBase.GetCurrentMethod(), typeof(MessageFormatter));

            return $"{InsertSpaceAfterCapitalLetter(typeof(T).Name)} with Id '{id}' sucessfully saved.";
        }

        public static string EntitySuccessfullyUpdated<T>(string id) where T : AggregateRoot
        {
            Validator.IsNullEmptyOrWhitespace(id, nameof(id), MethodBase.GetCurrentMethod(), typeof(MessageFormatter));

            return $"{InsertSpaceAfterCapitalLetter(typeof(T).Name)} with Id '{id}' sucessfully saved.";
        }

        public static string EntitySuccessfullyRemoved<T>(string id) where T : AggregateRoot
        {
            Validator.IsNullEmptyOrWhitespace(id, nameof(id), MethodBase.GetCurrentMethod(), typeof(MessageFormatter));

            return $"{InsertSpaceAfterCapitalLetter(typeof(T).Name)} with Id '{id}' sucessfully saved.";
        }
    }
}
