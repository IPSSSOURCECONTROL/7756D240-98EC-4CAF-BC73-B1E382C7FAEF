using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using MongoDB.Driver;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors
{
    public class OwnDatabaseContextInterceptor: IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            OwnDatabaseContextAttribute attribute =
                invocation.Method.GetAttribute<OwnDatabaseContextAttribute>();

            if (attribute == null)
                invocation.Proceed();

            TestDbContext testDbContext = new TestDbContext(
                ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString);
        }

        private class TestDbContext
        {
            protected static IMongoClient _client;
            protected static IMongoDatabase _database;

            public TestDbContext(string connectionString)
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
                        throw new System.Exception("Invalid configuration");
                    }
                }
            }

            public TestDbContext(string connectionString, string databaseName)
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
}
