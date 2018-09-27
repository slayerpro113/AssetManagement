using System.Collections.Generic;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;

namespace Service
{
    public class RolePersonnelService : BaseService<RolePersonnel, AssetManagementEntities>, IRolePersonnelService
    {
        private readonly IRepository<RolePersonnel> _rolePersonnelRepository;

        public RolePersonnelService(IUnitOfWork unitOfWork, IRepository<RolePersonnel> rolePersonnelRepository) : base(unitOfWork, rolePersonnelRepository)
        {
            _rolePersonnelRepository = rolePersonnelRepository;
        }


        public RolePersonnel GetRolePersonnelByPersonnelId(int personnelId)
        {
            var rolePersonnel = _rolePersonnelRepository.GetRolePersonnelByPersonnelId(personnelId);
            return rolePersonnel;
        }
    }
}