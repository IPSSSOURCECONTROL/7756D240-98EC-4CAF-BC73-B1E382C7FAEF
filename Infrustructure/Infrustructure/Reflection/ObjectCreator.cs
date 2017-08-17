using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Stack;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection
{
    public class ObjectCreator : IObjectActivator
    {
        private readonly IStackInspector _stackInspector;

        public ObjectCreator(IStackInspector stackInspector)
        {
            this._stackInspector = stackInspector;
        }

        public object CreateInstanceOf<T>(string className, params object[] constructorArguments) where T : class
        {
            string fullyQualifiedName = $"{typeof(T).Namespace}.{className}";

            Type createdType = Type.GetType(fullyQualifiedName);

            if (createdType != null)
            {
                if (constructorArguments != null)
                {
                    return Activator.CreateInstance(createdType, constructorArguments);
                }
                return Activator.CreateInstance(createdType);
            }


            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                createdType = asm.GetType(fullyQualifiedName);
                if (createdType != null)
                {
                    if (constructorArguments != null)
                    {
                        return Activator.CreateInstance(createdType, constructorArguments);
                    }
                    return Activator.CreateInstance(createdType);
                }
            }

            return null;
        }

        public object CreateInstanceOf(Type type, params object[] constructorArguments)
        {

            if (type != null)
            {
                if (constructorArguments != null)
                {
                    return Activator.CreateInstance(type, constructorArguments);
                }
                return Activator.CreateInstance(type);
            }


            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(type.AssemblyQualifiedName);
                if (type != null)
                {
                    if (constructorArguments != null)
                    {
                        return Activator.CreateInstance(type, constructorArguments);
                    }
                    return Activator.CreateInstance(type);
                }
            }

            return null;
        }

        public object CreateInstanceOfBaseType(Type baseType, string derivedTypeName, params object[] constructorArguments)
        {
            List<Type> subTypes = new List<Type>();

            IEnumerable<Assembly> assemblies = this._stackInspector.GetAllStackAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                var foundTypes = assembly.GetTypes().Where(x => x.BaseType != null &&
                                                                x.BaseType == baseType && x.Name == derivedTypeName);

                subTypes.AddRange(foundTypes);
            }

           return subTypes.IsNullOrEmpty() ? null :  this.CreateInstanceOf(subTypes[0]);
        }
    }
}