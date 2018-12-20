using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;

namespace Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IUnitOfWork unitOfWork, IRepository<Employee> employeeRepository) : base(unitOfWork, employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee GetEmployeeByUserName(string userName)
        {
            return _employeeRepository.GetEmployeeByUserName(userName);
        }

        public Enumerations.LoginStatus CheckLogin(string userName, string password)
        {
            var user = _employeeRepository.GetEmployeeByUserName(userName);
            if (user == null)
            {
                return Enumerations.LoginStatus.WrongUserName;
            }
            else
            {
                if (!user.Password.Equals(password))
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