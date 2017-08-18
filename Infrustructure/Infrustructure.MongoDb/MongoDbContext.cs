using System.Configuration;
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

                if (ConfigurationManager.AppSettings["IS_DEV_ENV"] == "N")
                {
                    _database = _client.GetDatabase("KBIT");
                }
                else if (ConfigurationManager.AppSettings["IS_DEV_ENV"] == "Y")
                {
                    _client.DropDatabase("KBIT_TEST");
                    _database = _client.GetDatabase("KBIT_TEST");
                }
                else
                {
                    throw new MongodbContextException("Invalid configuration");
                }
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

        private void DropDatabase()
        {
            _client.DropDatabase("KBIT_TEST");
        }
    }
}