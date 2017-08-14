using System;
using System.Collections.Generic;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Configuration
{
    [Serializable]
    public class ApplicationFunctionsConfiguration
    {
        public List<Function> Functions { get; set; } = new List<Function>();
        public List<BoundedContext> BoundedContexts { get; set; } = new List<BoundedContext>();
    }
}
