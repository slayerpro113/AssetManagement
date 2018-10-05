using Data.Entities;

namespace Data.Services
{
    public interface IRoleEmployeeService : IBaseService<RoleEmployee>
    {
        RoleEmployee GetRoleEmployeeByEmployeeId(int employeeId);
    }
}