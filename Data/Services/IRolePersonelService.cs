using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Services
{
    public interface IRolePersonnelService : IBaseService<RolePersonnel>
    {
        RolePersonnel GetRolePersonnelByPersonnelId(int personnelId);
    }
}
