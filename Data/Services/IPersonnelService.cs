using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IPersonnelService : IBaseService<Personnel>
    {
        Personnel GetPersonnel(string userName, string password);

        Personnel GetPersonnelByUserName(string userName);

        Enumerations.LoginStatus CheckLogin(string userName, string password);
    }
}