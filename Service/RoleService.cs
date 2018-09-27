using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;

namespace Service
{
    public class RoleService : BaseService<Role, AssetManagementEntities>, IRoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRepository<Role> roleRepository) : base(unitOfWork, roleRepository)
        {
            _roleRepository = roleRepository;
        }
    }
}