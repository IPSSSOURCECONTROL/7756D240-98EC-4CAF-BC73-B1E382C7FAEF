namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    //public class LicenseSpecificationRepository : BasicRepositoryBase, ILicenseSpecificationRepository
    //{
    //    private readonly IObjectActivator _objectActivator;

    //    public LicenseSpecificationRepository(IDatabaseContext databaseContext, IObjectActivator objectActivator) : base(databaseContext)
    //    {
    //        this._objectActivator = objectActivator;
    //    }

    //    [Transactional]
    //    [ValidateMethodArguments]
    //    public void Add(LicenseSpecification entity)
    //    {
    //        Validator.CheckReferenceTypeForNull(entity, nameof(entity), MethodBase.GetCurrentMethod(), this.GetType());

    //        if (this.DatabaseContext.TableForQuery<LicenseSpecification>()
    //            .Any(x => x.Id == entity.Id))
    //        {
    //            throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
    //                MessageFormatter.RecordWithIdAlreadyExists(entity.Id));
    //        }
            
    //        this.DatabaseContext.Add(entity);          
    //    }

    //    [Transactional]
    //    [ValidateMethodArguments]
    //    public void Update(LicenseSpecification entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [Transactional]
    //    [ValidateMethodArguments]
    //    public void Delete(LicenseSpecification entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [ValidateMethodArguments]
    //    public LicenseSpecification GetById(string id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<LicenseSpecification> GetAll()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [ValidateMethodArguments]
    //    public bool IsExist(LicenseSpecification entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
