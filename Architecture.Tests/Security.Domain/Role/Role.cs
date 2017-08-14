using System;
using System.Collections.Generic;
using System.Text;
using Architecture.Tests.Infrustructure.Domain;
using Architecture.Tests.Security.Domain.Exceptions;

namespace Architecture.Tests.Security.Domain.Role
{
    /// <summary>
    /// Represents an application Role.
    /// </summary>
    //[BsonKnownTypes(typeof(SupermanRole), typeof(AdministratorRole))]

    public abstract class Role: AggregateRoot
    {
        protected Role()
        {
        }

        protected Role(string id)
        {
            this.Id = id;
        }

        public IList<ApplicationFunction.ApplicationFunction> Functions { get; } = new List<ApplicationFunction.ApplicationFunction>();

        public void AddFunction(ApplicationFunction.ApplicationFunction function)
        {
            if (function == null)
                throw new CannotAddNullApplicationFunctionException(nameof(function));

            this.Functions.Add(function);
        }

        public string GetDescription()
        {
            string typeName = this.GetType().Name;

            StringBuilder descriptiveName = new StringBuilder();

            foreach (char c in typeName)
            {
                if (Char.IsUpper(c))
                {
                    descriptiveName.Append(" ");
                }
                descriptiveName.Append(c);
            }

            if (typeName != null && typeName.Length > 0 && Char.IsUpper(typeName[0]))
            {
                descriptiveName.Remove(0, 1);
            }
            return descriptiveName.ToString();
        }
    }
}