using LookupSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupSystem.DataAccess.Data
{
    public interface IUserRepository
    {
        public void DeleteUserOlderThan(int countOfDays);
    }
}
