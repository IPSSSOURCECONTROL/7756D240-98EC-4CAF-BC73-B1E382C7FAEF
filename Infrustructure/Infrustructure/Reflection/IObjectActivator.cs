using System;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection
{
    /// <summary>
    /// Creates an object based on given criteria.
    /// </summary>
    public interface IObjectActivator
    {
        object CreateInstanceOf<T>(string className, params object[] constructorArguments) where T : class;
        object CreateInstanceOf(Type type, params object[] constructorArguments);
        object CreateInstanceOfBaseType(Type baseType, string derivedTypeName, params object[] constructorArguments);
    }
}
