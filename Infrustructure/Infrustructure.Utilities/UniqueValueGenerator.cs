using System;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities
{
    public class UniqueValueGenerator : IUniqueValueGenerator
    {
        public string GenerateUniqueValue()
        {
            return $"{DateTime.Now.ToString("yy-MM-dd").Replace("-", string.Empty)}" +
                   $"{DateTime.Now.ToString("T").Replace(":", string.Empty)}";
        }
    }
}