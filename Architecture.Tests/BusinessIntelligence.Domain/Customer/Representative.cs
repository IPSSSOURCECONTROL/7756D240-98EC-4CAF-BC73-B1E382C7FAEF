using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.BusinessIntelligence.Domain.Customer
{
    public class Representative: IEntity
    {
        public Representative(string id, string name, string code)
        {
            this.Name = name;
            this.Code = code;
            this.Id = id;
        }

        public string Name { get; }
        public string Code { get; }
        public string Id { get; }
    }
}