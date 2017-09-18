using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction.ApplicationFunctions;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction
{
    public class ApplicationFunction : AggregateRoot, IBusinessLink
    {
        private string _description;

        public ApplicationFunction(string name, string description = null)
        {
            this.Name = name;
            this._description = description;
        }

        public ApplicationFunction(string id, string name, IEnumerable<FunctionClassification> functionClassifications, string description = null)
        {
            this.Id = id;
            this.Name = name;
            this._description = description;
        }

        public string BusinessId { get; set; }

        public string Name { get; set; }

        public IList<FunctionClassification> FunctionClassification { get; private set; }= new List<FunctionClassification>();

        public string Description
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._description))
                    return "No description";

                return this._description;
            }
            set { this._description = value; }
        }

        public void Add(FunctionClassification function)
        {
            if(function == null)
                throw new KbitNullArgumentException(MethodBase.GetCurrentMethod(), nameof(function));

            this.FunctionClassification.Add(function);
        }

        public void ScanAppDomain(string inAssembliesLike)
        {
            List<Type> subTypes = new List<Type>();
            
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var foundTypes = assembly.GetTypes().Where(x => x.BaseType != null &&
                x.BaseType == typeof(FunctionClassification) && x.AssemblyQualifiedName.Contains(inAssembliesLike));

                subTypes.AddRange(foundTypes);
            }

            foreach (Type type in subTypes)
            {
                this.FunctionClassification.Add((FunctionClassification)this.CreateInstanceOf(type));
            }

        }

        private object CreateInstanceOf(Type type, params object[] constructorArguments)
        {

            if (type != null)
            {
                if (constructorArguments != null)
                {
                    return Activator.CreateInstance(type, constructorArguments);
                }
                return Activator.CreateInstance(type);
            }

            return null;
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}