using System.Collections.Generic;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities
{
    public interface IPolymorphicTyeNameProvider
    {
        IEnumerable<string> GetPolymorphicTypeNamesForBaseType<TBaseType>() where TBaseType : class;
        IEnumerable<string> GetPolymorphicTypeNamesForBaseType(string baseTypeName);
    }
}
