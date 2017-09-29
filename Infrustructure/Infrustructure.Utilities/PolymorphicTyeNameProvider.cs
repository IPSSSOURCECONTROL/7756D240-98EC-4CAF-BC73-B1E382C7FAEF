using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Stack;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities
{
    public class PolymorphicTyeNameProvider : IPolymorphicTyeNameProvider
    {
        private readonly IStackInspector _stackInspector;

        public PolymorphicTyeNameProvider(IStackInspector stackInspector)
        {
            this._stackInspector = stackInspector;
        }

        public IEnumerable<string> GetPolymorphicTypeNamesForBaseType<TBaseType>() where TBaseType : class
        {
            List<string> nameList = new List<string>();

            Assembly assembly =
                this._stackInspector.GetAllStackAssemblies()
                    .FirstOrDefault(x => x.GetTypes().Any(b => b.Namespace == typeof(TBaseType).Namespace));

            if (assembly == null)
                return nameList;

            IEnumerable<Type> types = assembly.GetTypes().Where(x => x.BaseType == typeof(TBaseType));

            if (!types.Any())
                return nameList;

            foreach (Type type in types)
            {
                nameList.Add(type.Name.InsertSpaceAfterCapitalLetter());
            }

            return nameList;
        }

        public IEnumerable<string> GetPolymorphicTypeNamesForBaseType(string baseTypeName)
        {
            List<string> nameList = new List<string>();

            if (string.IsNullOrWhiteSpace(baseTypeName))
                return nameList;

            Assembly assembly =
                this._stackInspector.GetAllStackAssemblies()
                    .FirstOrDefault(x => x.GetTypes().Any(b => b.Name == baseTypeName && b.IsAbstract));

            if (assembly == null)
                return nameList;

            IEnumerable<Type> types = assembly.GetTypes().Where(x => x.BaseType != null && 
                    x.BaseType.Name == baseTypeName);

            if (!types.Any())
                return nameList;

            foreach (Type type in types)
            {
                nameList.Add(type.Name.InsertSpaceAfterCapitalLetter());
            }

            return nameList;
        }
    }
}