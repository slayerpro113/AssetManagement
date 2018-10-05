using Data.Entities;
using Data.Repositories;
using System.Linq;

namespace Repository
{
    public static class EmployeeRepository
    {
        public static Employee GetEmployeeByUserName(this IRepository<Employee> repository, string userName)
        {
            return repository.Entity.FirstOrDefault(_ => _.UserName == userName);
        }
    }
}