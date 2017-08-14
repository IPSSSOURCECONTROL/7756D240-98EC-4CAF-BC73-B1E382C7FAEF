using System;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Configuration
{
    [Serializable]
    public class Function
    {
        public string Name { get; set; } = string.Empty;
        public string NameSpace { get; set; } = string.Empty;
    }
}