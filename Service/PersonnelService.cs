using System;
using System.Web.UI.WebControls;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;

namespace Service
{
    public class PersonnelService : BaseService<Personnel, AssetManagementEntities>, IPersonnelService
    {
        private readonly  IRepository<Personnel> _personnelRepository;

        public PersonnelService(IUnitOfWork unitOfWork, IRepository<Personnel> personnelRepository) : base(unitOfWork, personnelRepository)
        {
            _personnelRepository = personnelRepository;
        }

        public Personnel GetPersonnel(string userName, string password)
        {
            var personnel = _personnelRepository.GetPersonnel(userName, password);
            return personnel;
        }

        public Personnel GetPersonnelByUserName(string userName)
        {
            var personnel = _personnelRepository.GetPersonnelByUserName(userName);
            return personnel;
        }

        public Enumerations.LoginStatus CheckLogin(string userName, string password)
        {
            var user = _personnelRepository.GetPersonnelByUserName(userName);
            if (user == null)
            {
                return Enumerations.LoginStatus.WrongUserName;
            }
            else 
            {
                if (!user.PassWord.Trim().Equals(password))
                {
                    return Enumerations.LoginStatus.WrongPassword;
                }
                else
                {
                    return Enumerations.LoginStatus.Succsess;
                }
            }
        }
    }
}