using System.Collections.Generic;
using System.Threading.Tasks;
using LookupSystem.Core;

namespace LookupSystem.Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();
        IEnumerable<UserModel> GetUserByEmail(string email);
    }
}
