using LookupSystem.Business.Interfaces;
using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Interfaces;
using LookupSystem.DataAccess.Models;
using LookupSystem.DataAccess.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LookupSystem.Core;

namespace LookupSystem.Business.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> userRepo;
        private readonly IRepository<UserContact> userContactRepo;

        public UserService(LookupSystemDbContext contex) 
        {
            userRepo = new UserRepository(contex);
            userContactRepo = new UserContactRepository(contex);
        }

        public IEnumerable<UserModel> GetAll()
        {
            var users = userRepo.GetAll();
            var contacts = userContactRepo.GetAll();

            var result = from u in users
                         join c in contacts on u.Id equals c.UserId into gj
                         from x in gj.DefaultIfEmpty()
                         select new UserModel
                         {
                             Id = u.Id,
                             CreatedDate = u.CreatedDate,
                             DeleteDate = u.DeleteDate,
                             Hired = u.Hired,
                             ManagerId = u.ManagerId,
                             FirstName = x.FirstName ?? string.Empty,
                             LastName = x.LastName ?? string.Empty,
                             Phone = x.Phone ?? string.Empty,
                             MobilePhone = x.MobilePhone ?? string.Empty,
                             Country = x.Country ?? string.Empty,
                             Address = x.Address ?? string.Empty,
                             SSN = x.SSN ?? string.Empty,
                             DriverLicense = x.DriverLicense ?? string.Empty,
                             Email = x.Email ?? string.Empty
                         };

            return result.ToList();
        }

        public IEnumerable<UserModel> GetUserByEmail(string email)
        {
            var users = userRepo.GetAll();
            var contacts = userContactRepo.GetAll();

            var result = from u in users
                         join c in contacts on u.Id equals c.UserId 
                         where c.Email.ToUpper() == email.ToUpper()
                         select new UserModel
                         {
                             Id = u.Id,
                             CreatedDate = u.CreatedDate,
                             DeleteDate = u.DeleteDate,
                             Hired = u.Hired,
                             ManagerId = u.ManagerId,
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             Phone = c.Phone,
                             MobilePhone = c.MobilePhone,
                             Country = c.Country,
                             Address = c.Address,
                             SSN = c.SSN,
                             DriverLicense = c.DriverLicense,
                             Email = c.Email
                         };

            return result.ToList();
        }
    }
}