using System;

namespace Architecture.Tests.Infrustructure.Repository
{
    public class DataModelBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreationDateTime { get; set; } = DateTime.Now;
    }
}