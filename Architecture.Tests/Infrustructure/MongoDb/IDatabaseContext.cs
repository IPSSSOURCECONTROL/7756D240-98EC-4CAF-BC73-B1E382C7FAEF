using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.Infrustructure.MongoDb
{
    /// <summary>
    /// Exposes functions to manipulate the datasource.
    /// </summary>
    public interface IDatabaseContext: IDisposable
    {
        /// <summary>
        /// Returns an <see cref="IQueryable{T}"/> which can be filtered with Linq.
        /// Use this function if you intend on just reading entities and you don't intend 
        /// on updating them. 
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="AggregateRoot"/> type.</typeparam>
        /// <returns>A querable collection of <see cref="TEntity"/></returns>
        [CheckIfRepositoryCall]
        IQueryable<TEntity> TableForQuery<TEntity>() where TEntity : class;

        /// <summary>
        /// Returns an <see cref="IQueryable{T}"/> which can be filtered with Linq.
        /// Use this function if you intend on updating entities retreived.
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="AggregateRoot"/> type.</typeparam>
        /// <returns>A querable collection of <see cref="TEntity"/></returns>
        [CheckIfRepositoryCall]
        IQueryable<TEntity> Table<TEntity>() where TEntity : class;

        /// <summary>
        /// Adds a <see cref="TEntity"/> to the wrapped database context.
        /// </summary>
        /// <typeparam name="TEntity">Derivative of <see cref="AggregateRoot"/></typeparam>
        /// <param name="entity">The entity.</param>
        [CheckIfRepositoryCall]
        void Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Adds a collection of <see cref="TEntity"/>'s to the wrapped database context.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        [CheckIfRepositoryCall]
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// Commits made to the database context to the actual underlying datasource.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Removes a <see cref="TEntity"/> from the database context.
        /// </summary>
        /// <typeparam name="TEntity">Derivative of <see cref="AggregateRoot"/></typeparam>
        /// <param name="entity">The entity.</param>
        [CheckIfRepositoryCall]
        void Remove<TEntity>(string entityId) where TEntity : class;
    }
}