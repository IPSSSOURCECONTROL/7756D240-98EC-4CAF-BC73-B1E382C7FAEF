using System;

namespace Architecture.Tests.Infrustructure.Reflection
{
    public interface IObjectActivator
    {
        object CreateInstanceOf<T>(string className, params object[] constructorArguments) where T : class;

        object CreateInstanceOf(Type type, params object[] constructorArguments);
        object CreateInstanceOfBaseType(Type baseType, string derivedTypeName, params object[] constructorArguments);
    }
}
