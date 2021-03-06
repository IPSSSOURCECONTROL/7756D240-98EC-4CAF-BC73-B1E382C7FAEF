using System.Collections.Generic;
using System.Linq;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using MongoDB.Driver;

namespace Architecture.Tests.Infrustructure.MongoDb
{
    public abstract class DatabaseContextWrapper : IDatabaseContext
    {
        private MongoDbContext _kbitDatabaseContext;
        protected string ConnectionString { get; set; }

        protected MongoDbContext KbitDatabaseContext
        {
            get
            {
                if (this._kbitDatabaseContext == null)
                    this.SetUpDbContext();

                return this._kbitDatabaseContext;
            }
            set { this._kbitDatabaseContext = value; }
        }

        protected DatabaseContextWrapper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        [CheckIfRepositoryCall]
        public void Add<TEntity>(TEntity entity) where TEntity : class 
        {
            if (entity == null)
                return;

            this.KbitDatabaseContext.Set<TEntity>().Add(entity);
        }

        [CheckIfRepositoryCall]
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities == null)
                return;

            this.KbitDatabaseContext.Set<TEntity>().AddRange(entities);
        }

        public void Dispose()
        {
            //KbitDatabaseContext.Dispose();
        }

        public void SaveChanges()
        {
            //this.KbitDatabaseContext.SaveChanges();
        }

        [CheckIfRepositoryCall]
        public void Remove<TEntity>(string entityId) where TEntity : class
        {
            this.KbitDatabaseContext.Set<TEntity>().Remove(entityId);
        }

        [CheckIfRepositoryCall]
        public IQueryable<TEntity> Table<TEntity>() where TEntity : class
        {
            return this.KbitDatabaseContext.Set<TEntity>().AsQueryable();
        }

        [CheckIfRepositoryCall]
        public IQueryable<TEntity> TableForQuery<TEntity>() where TEntity : class
        {
            return this.KbitDatabaseContext.Set<TEntity>().AsQueryable();
        }

        protected abstract void SetUpDbContext();
    }
    //public abstract class DatabaseContextWrapper: IDatabaseContext
    //{
    //    private DbContext _kbitDatabaseContext;
    //    protected string ConnectionString { get; set; }

    //    protected DbContext KbitDatabaseContext
    //    {
    //        get
    //        {
    //            if(this._kbitDatabaseContext == null)
    //                this.SetUpDbContext();

    //            return _kbitDatabaseContext;
    //        }
    //        set { _kbitDatabaseContext = value; }
    //    }

    //    protected DatabaseContextWrapper(string connectionString)
    //    {
    //        ConnectionString = connectionString;
    //    }

    //    public void Add<TEntity>(TEntity entity) where TEntity : DataModelBase
    //    {
    //        if(entity == null)
    //            return;

    //        this.KbitDatabaseContext.Set<TEntity>().Add(entity);
    //    }

    //    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : DataModelBase
    //    {
    //        if(entities == null)
    //            return;

    //        this.KbitDatabaseContext.Set<TEntity>().AddRange(entities);
    //    }

    //    public void Dispose()
    //    {
    //        KbitDatabaseContext.Dispose();
    //    }

    //    public void SaveChanges()
    //    {
    //        this.KbitDatabaseContext.SaveChanges();
    //    }

    //    public void Remove<TEntity>(TEntity entity) where TEntity : DataModelBase
    //    {
    //        this.KbitDatabaseContext.Set<TEntity>().Remove(entity);
    //    }

    //    public IQueryable<TEntity> Table<TEntity>() where TEntity: DataModelBase
    //    {
    //        return this.KbitDatabaseContext.Set<TEntity>();
    //    }

    //    public IQueryable<TEntity> TableForQuery<TEntity>() where TEntity : DataModelBase
    //    {
    //        return this.KbitDatabaseContext.Set<TEntity>().AsNoTracking();
    //    }

    //    protected abstract void SetUpDbContext();
    //}
}