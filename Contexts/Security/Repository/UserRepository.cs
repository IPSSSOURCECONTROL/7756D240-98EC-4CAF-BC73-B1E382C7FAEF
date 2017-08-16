using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class UserRepository : BasicRepositoryBase, IUserRepository
    {
        public UserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Add(User entity)
        {
            if (this.DatabaseContext.Table<User>().Any(x => x.Id == entity.Id))
            {
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(), 
                    MessageFormatter.RecordWithIdAlreadyExists(entity.Id));
            }

            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Update(User entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity),
                MethodBase.GetCurrentMethod(), this.GetType());

            if (!this.DatabaseContext.Table<User>().Any(x => x.Id == entity.Id))
            {
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));
            }

            this.DatabaseContext.Remove<User>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(User entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity),
                MethodBase.GetCurrentMethod(), this.GetType());

            this.DatabaseContext.Remove<User>(entity.Id);
        }

        public User GetById(string id)
        {
            Validator.IsNullEmptyOrWhitespace(id, nameof(id), 
                MethodBase.GetCurrentMethod(), this.GetType());

            return this.DatabaseContext.Table<User>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return this.DatabaseContext.Table<User>();
        }

        [ValidateMethodArguments]
        public bool IsExist(User entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity));
            return this.DatabaseContext.Table<User>().Any(x => x.Id == entity.Id);
        }
    }

    //public class UserRepository : BasicRepositoryBase, IUserRepository
    //{
    //    private readonly IObjectActivator _objectActivator;

    //    public UserRepository(IDatabaseContext databaseContext, 
    //        IObjectActivator objectActivator) : base(databaseContext)
    //    {
    //        _objectActivator = objectActivator;
    //    }
    //    public void Add(User entity)
    //    {
    //        if (entity == null)
    //            throw new ArgumentNullException(nameof(entity));

    //        if (this.DatabaseContext.TableForQuery<UserDM>().Any(x=>x.Id == entity.Id))
    //            throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(), "Record already exists");

    //        if (this.DatabaseContext.TableForQuery<UserDM>().Any(x => x.Code == entity.Code))
    //            throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(), $"User with code {entity.Code} already exists.");

    //        PasswordDM passwordDa = new PasswordDM();

    //        UserDM userDa = new UserDM();

    //        LicenseDM licenseDa = new LicenseDM();

    //        this.Map(entity, passwordDa, userDa, licenseDa);

    //        userDa.LRole = entity.Role.Id;

    //        this.DatabaseContext.Add(userDa);
    //        this.DatabaseContext.SaveChanges();

    //        this.DatabaseContext.Add(passwordDa);
    //        this.DatabaseContext.SaveChanges();

    //        this.DatabaseContext.Add(licenseDa);
    //        this.DatabaseContext.SaveChanges();
    //    }

    //    public void Update(User entity)
    //    {
    //        if(entity == null)
    //            throw new ArgumentNullException(nameof(entity));

    //        if (!this.DatabaseContext.TableForQuery<UserDM>().Any(x => x.Id == entity.Id))
    //            throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(), 
    //                $"Record with id {entity.Id} does not exist.");

    //        UserDM userDa = this.DatabaseContext.Table<UserDM>().First(x => x.Id == entity.Id);

    //        PasswordDM passwordDa = this.DatabaseContext.Table<PasswordDM>().First(x => x.LUser == userDa.Id);

    //        LicenseDM licenseDa = this.DatabaseContext.Table<LicenseDM>().First(x => x.LUser == userDa.Id);

    //        this.Map(entity, passwordDa, userDa, licenseDa);

    //        //this.DatabaseContext.Add(licenseDa);
    //        //this.DatabaseContext.Add(passwordDa);
    //        //this.DatabaseContext.Add(userDa);

    //        this.DatabaseContext.SaveChanges();
    //    }

    //    public void Delete(User entity)
    //    {
    //        this.Authorize();

    //        if (entity == null)
    //            throw new ArgumentNullException(nameof(entity));

    //        if (!this.DatabaseContext.TableForQuery<UserDM>().Any(x => x.Id == entity.Id))
    //            throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
    //                $"Record with id {entity.Id} does not exist.");

    //        UserDM userDa = this.DatabaseContext.Table<UserDM>().First(x => x.Id == entity.Id);

    //        LicenseDM licenseDa = this.DatabaseContext.Table<LicenseDM>().First(x => x.LUser == userDa.Id);

    //        PasswordDM passwordDa = this.DatabaseContext.Table<PasswordDM>()
    //            .First(x => x.LUser == userDa.Id);


    //        this.DatabaseContext.Remove(passwordDa);
    //        this.DatabaseContext.Remove(licenseDa);
    //        this.DatabaseContext.Remove(userDa);


    //        this.DatabaseContext.SaveChanges();
    //    }

    //    public  User GetById(string id)
    //    {
    //        UserDM userDataModel = this.DatabaseContext.TableForQuery<UserDM>().FirstOrDefault(x => x.Id == id);

    //        if (userDataModel == null)
    //            return null;

    //        User user = this.BuildDomainUser(userDataModel);

    //        return user;
    //    }

    //    public IEnumerable<User> GetAll()
    //    {
    //        var usersFromBusinessId = from users in this.DatabaseContext.TableForQuery<UserDM>()
    //            join userToBusinessDa in this.DatabaseContext.TableForQuery<UserToBusinessDM>() on users.Id equals
    //            userToBusinessDa.LUser
    //            where userToBusinessDa.LBusiness == this.AuthorizationContext.BusinessId
    //            select users;

    //        var userDomainModels = new List<User>();

    //        foreach (UserDM userDa in usersFromBusinessId)
    //        {
    //            //userDomainModels.Add(this.BuildDomainUser(userDa));
    //        }

    //        return userDomainModels;
    //    }

    //    public bool IsExist(User entity)
    //    {
    //        return this.DatabaseContext.TableForQuery<UserDM>().Any(x => x.Id == entity.Id);
    //    }

    //    #region Private Methods
    //    private void Map(User entity, PasswordDM passwordDa, UserDM userDa, LicenseDM licenseDa)
    //    {
    //        licenseDa.Expires = entity.License.Expires;
    //        licenseDa.Key = entity.License.Key;
    //        licenseDa.Type = entity.License.GetType().Name;
    //        licenseDa.ValidFrom = entity.License.ValidFrom;
    //        licenseDa.LUser = entity.Id;

    //        passwordDa.PasswordResetPolicy = entity.Password.PasswordResetPolicy.GetType().Name;
    //        passwordDa.Value = entity.Password.Value;
    //        passwordDa.NextResetDate = entity.Password.PasswordResetPolicy.NextResetDate;
    //        passwordDa.LUser = entity.Id;

    //        userDa.Id = entity.Id;
    //        userDa.Name = entity.Name;
    //        userDa.Code = entity.Code;
    //        userDa.Email = entity.Email;
    //        userDa.LRole = entity.Role.Id;
    //        userDa.AccountStatus = entity.AccountStatus.GetType().Name;
    //    }
    //    private PasswordResetPolicy GetPasswordResetPolicy(PasswordDM passwordDa)
    //    {
    //        if (passwordDa.PasswordResetPolicy == "DailyPasswordResetPolicy")
    //        {
    //            return new DailyPasswordResetPolicy();
    //        }

    //        if (passwordDa.PasswordResetPolicy == "MonthlyPasswordResetPolicy")
    //        {
    //            return new MonthlyPasswordResetPolicy();
    //        }

    //        if (passwordDa.PasswordResetPolicy == "WeeklyPasswordResetPolicy")
    //        {
    //            return new WeeklyPasswordResetPolicy();
    //        }

    //        if (passwordDa.PasswordResetPolicy == "NeverResetPasswordResetPolicy")
    //        {
    //            return new NeverResetPasswordResetPolicy();
    //        }

    //        return new NeverResetPasswordResetPolicy();
    //    }

    //    private Role GetRole(RoleDM roleDa)
    //    {
    //        Role role = (Role) this._objectActivator.CreateInstanceOf<Role>(roleDa.Name, roleDa.Id);

    //        if (role == null)
    //            return new NoRole(roleDa.Id);

    //        return role;
    //    }

    //    private LicenseSpecification GetLicenseSpecification(LicenseDM licenseDa)
    //    {
    //        LicenseSpecification licenseSpecification = (LicenseSpecification) this._objectActivator
    //            .CreateInstanceOf<LicenseSpecification>(licenseDa.Type, licenseDa.Id,licenseDa.Expires, licenseDa.ValidFrom,
    //                licenseDa.Key);

    //        if (licenseSpecification == null)
    //        {
    //            return new ExpiredLicenseSpecification(licenseDa.Id,DateTime.Now, DateTime.Now, string.Empty);
    //        }

    //        return licenseSpecification;
    //    }
    //    private User BuildDomainUser(UserDM userDataModel)
    //    {
    //        LicenseDM licenseDa = this.DatabaseContext.TableForQuery<LicenseDM>()
    //            .FirstOrDefault(x => x.LUser == userDataModel.Id);

    //        PasswordDM passwordDa = this.DatabaseContext.TableForQuery<PasswordDM>()
    //            .First(x => x.LUser == userDataModel.Id);

    //        RoleDM roleDa = this.DatabaseContext.TableForQuery<RoleDM>()
    //            .FirstOrDefault(x => x.Id == userDataModel.LRole);

    //        User user = new User(userDataModel.Id, userDataModel.Name, userDataModel.Code, userDataModel.Email,
    //            new Password(passwordDa.Value, GetPasswordResetPolicy(passwordDa)),
    //            GetRole(roleDa), GetAccountStatus(userDataModel));
    //        user.SetLicense(this.GetLicenseSpecification(licenseDa));
    //        return user;
    //    }

    //    private AccountStatus GetAccountStatus(UserDM userDataModel)
    //    {
    //        if (userDataModel.AccountStatus == "ActiveAccountStatus")
    //        {
    //            return new ActiveAccountStatus();
    //        }

    //        if (userDataModel.AccountStatus == "BlockedAccountStatus")
    //        {
    //            return new BlockedAccountStatus();
    //        }

    //        if (userDataModel.AccountStatus == "InactiveAccountStatus")
    //        {
    //            return new InactiveAccountStatus(userDataModel.AccountStatusReason);
    //        }

    //        return new InactiveAccountStatus("Inactive account");
    //    }

    //    #endregion

    //}
}
