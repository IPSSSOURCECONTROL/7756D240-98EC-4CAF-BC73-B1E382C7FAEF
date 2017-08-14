using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

namespace Architecture.Tests.Infrustructure.Configuration
{
    [Serializable]
    public class ApplicationFunctionsConfiguration
    {
        public List<Function> Functions { get; set; } = new EditableList<Function>();
        public List<BoundedContext> BoundedContexts { get; set; } = new List<BoundedContext>();
    }

    [Serializable]
    public class BoundedContext
    {
        public string Name { get; set; } = string.Empty;
    }
}
