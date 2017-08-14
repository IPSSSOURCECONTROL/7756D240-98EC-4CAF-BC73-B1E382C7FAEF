using System.Collections.Generic;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using MongoDB.Driver;

namespace Architecture.Tests.Infrustructure.MongoDb
{
    public static class MongoCollectionExtensions
    {
        [CheckIfRepositoryCall]
        public static void Add<TEntity>(this IMongoCollection<TEntity> entityCollection, TEntity entity)
            where TEntity : class
        {
            entityCollection.InsertOne(entity);
        }

        [CheckIfRepositoryCall]
        public static void Remove<TEntity>(this IMongoCollection<TEntity> entityCollection, string entityId)
            where TEntity : class
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", entityId);
            entityCollection.DeleteOne(filter);
        }

        [CheckIfRepositoryCall]
        public static void AddRange<TEntity>(this IMongoCollection<TEntity> entityCollection,
            IEnumerable<TEntity> entities) 
            where TEntity : class
        {
            entityCollection.InsertMany(entities);
        }


    }
}