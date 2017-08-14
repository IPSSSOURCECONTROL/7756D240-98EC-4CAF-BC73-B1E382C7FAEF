using System.Collections.Generic;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.Infrustructure.Repository
{
    /// <summary>
    /// Exposes general functions fro a basic repository without a workflow
    /// </summary>
    /// <typeparam name="TEntity">Target Domain Model type.</typeparam>
    public interface IBasicRepository<TEntity>  where TEntity : IEntity
    {
        /// <summary>
        /// Adds an instance of <see cref="TEntity"/> to 
        /// the data source.
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/></param>
        [Transactional]
        [ValidateMethodArguments]
        void Add(TEntity entity);

        /// <summary>
        /// Updates an instance of <see cref="TEntity"/> in 
        /// the data source.
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/></param>
        [Transactional]
        [ValidateMethodArguments]
        void Update(TEntity entity);

        /// <summary>
        /// Removes an instance of <see cref="TEntity"/> in 
        /// the data source.
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/></param>
        [Transactional]
        [ValidateMethodArguments]
        void Delete(TEntity entity);


        /// <summary>
        /// Retreives a valid instance of <see cref="TEntity"/>
        /// </summary>
        /// <param name="id">The unique Id of the <see cref="TEntity"/></param>
        /// <returns><see cref="TEntity"/></returns>
        [ValidateMethodArguments]
        TEntity GetById(string id);

        /// <summary>
        /// Returns a collection of <see cref="TEntity"/>'s
        /// </summary>
        /// <returns><see cref="TEntity"/>'s</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Checks if the given entity <see cref="TEntity"/> exists in 
        /// the data source.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True if exists, false if not.</returns>
        [ValidateMethodArguments]
        bool IsExist(TEntity entity);
    }
}