using Data.Entities;
using Data.Repositories;
using System.Linq;

namespace Repository
{
    public static class RolePersonnelRepository
    {
        public static RolePersonnel GetRolePersonnelByPersonnelId(this IRepository<RolePersonnel> repository, int personnelId)
        {
            var rolePersonnel = repository.Entity.FirstOrDefault(_ => _.PersonnelID == personnelId);
            return rolePersonnel;
        }
    }
}