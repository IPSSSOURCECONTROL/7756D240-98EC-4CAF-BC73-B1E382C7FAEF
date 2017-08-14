using Architecture.Tests.Infrustructure.MongoDb;

namespace Architecture.Tests.Infrustructure.Repository
{
    public interface IDatabaseContextAvailable
    {
        IDatabaseContext DatabaseContext { get; set; }

    }
}
