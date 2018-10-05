using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;

namespace Service
{
    public class RoleEmployeeService : BaseService<RoleEmployee>, IRoleEmployeeService
    {
        private readonly IRepository<RoleEmployee> _roleEmployeeRepository;

        public RoleEmployeeService(IUnitOfWork unitOfWork, IRepository<RoleEmployee> roleEmployeeRepository) : base(unitOfWork, roleEmployeeRepository)
        {
            _roleEmployeeRepository = roleEmployeeRepository;
        }

        public RoleEmployee GetRoleEmployeeByEmployeeId(int employeeId)
        {
            var roleEmployee = _roleEmployeeRepository.GetRolePersonnelByPersonnelId(employeeId);
            return roleEmployee;
        }
    }
}