using Data.Entities;
using Data.Repositories;
using System.Linq;

namespace Repository
{
    public static class PersonnelRepository
    {
        public static Personnel GetPersonnel(this IRepository<Personnel> repository ,string userName, string password)
        {
            return repository.Entity.FirstOrDefault(_ => _.UserName == userName && _.PassWord == password);
        }

        public static Personnel GetPersonnelByUserName(this IRepository<Personnel> repository, string userName)
        {
            return repository.Entity.FirstOrDefault(_ => _.UserName == userName);
        }
    }
}