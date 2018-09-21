using System;
using System.Text;
using Data.Entities;

namespace Data.Services
{
    public interface IPersonnelService : IBaseService<Personnel>
    {
        Personnel GetPersonnel(string userName, string password);
        Personnel GetPersonnelByUserName(string userName);

        int CheckLogin(string userName, string password);
    }
}