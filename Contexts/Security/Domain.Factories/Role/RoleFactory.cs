using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Factories.Role
{
    public class RoleFactory: IDomainFactory<Domain.Role.Role, RoleAm>
    {
        public Domain.Role.Role BuildDomainEntityType(RoleAm applicationModel, bool isNew = true)
        {
            applicationModel.Validate();
            return null;
        }

        public RoleAm BuildApplicationModelType(Domain.Role.Role domainEntity)
        {
            RoleAm roleAm = new RoleAm();
            roleAm.Name = domainEntity.TypeName;
            roleAm.Id = domainEntity.Id;

            return roleAm;
        }

        public IEnumerable<RoleAm> BuildApplicationModelTypes(IEnumerable<Domain.Role.Role> domainEntities)
        {
            List<RoleAm> applicationModels = new List<RoleAm>();

            foreach (Domain.Role.Role role in domainEntities)
            {
                applicationModels.Add(this.BuildApplicationModelType(role));
            }

            return applicationModels;
        }
    }
}
