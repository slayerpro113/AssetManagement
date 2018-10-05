using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        Employee GetEmployeeByUserName(string userName);
        Enumerations.LoginStatus CheckLogin(string userName, string password);
    }
}