using Data.Entities;
using Data.Repositories;
using System.Linq;

namespace Repository
{
    public static class RoleEmployeeRepository
    {
        public static RoleEmployee GetRolePersonnelByPersonnelId(this IRepository<RoleEmployee> repository, int employeeId)
        {
            var roleEmployee = repository.Entity.FirstOrDefault(_ => _.EmployeeID == employeeId);
            return roleEmployee;
        }
    }
}