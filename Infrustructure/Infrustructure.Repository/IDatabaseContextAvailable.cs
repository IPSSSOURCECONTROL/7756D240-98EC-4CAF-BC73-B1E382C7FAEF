using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository
{
    public interface IDatabaseContextAvailable
    {
        IDatabaseContext DatabaseContext { get; set; }

    }
}
