using MongoDB.Driver;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb
{
    public class MongoDbContext
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            if (_client == null)
            {
                _client = new MongoClient(connectionString);
                _database = _client.GetDatabase("KBIT");
            }
        }

        public MongoDbContext(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoCollection<TEntity> Set<TEntity>() where TEntity : class
        {
            string adsas = typeof(TEntity).Name + "s";
            return _database.GetCollection<TEntity>(adsas);
        }
    }
}