using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Interfaces;
using LookupSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookupSystem.DataAccess.Repositories
{
    //Будет удалено в понедельник.
    public class UserRepository : IRepository<User>
    {
        private readonly LookupSystemDbContext _db;
        public UserRepository(LookupSystemDbContext context)
        {
            _db = context;
        }
        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public void Create(User item)
        {
            _db.Users.Add(item);
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = _db.Users.Find(id);
            if (user != null)
                _db.Users.Remove(user);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //Client evaluation. 
        public IEnumerable<User> GetUserByEmail(string email)
        {
            var query = _db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .AsEnumerable()
                .Where(u => string.Compare(u.UserContact.Email, email, StringComparison.OrdinalIgnoreCase) == 0);
            
            return query.ToList();
        }

        //Client evaluation. 
        public IEnumerable<User> GetUserByPhone(string phone)
        {
            var query = _db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .AsEnumerable()
                .Where(u => string.Compare(u.UserContact.Phone, phone, StringComparison.OrdinalIgnoreCase) == 0);

            return query.ToList();
        }     


        public IEnumerable<User> GetFiredUsers()
        {
            var query = _db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .Where(u => u.Fired);

            var rerult = query.ToList();
            return rerult;
        }

        public IEnumerable<User> GetHiredUsers()
        {
            var query = _db.Users
                .Where(u => !u.Fired);

            var rerult = query.ToList();
            return rerult;
        }


        //public IEnumerable<User> GetUsersByName(string firstName, string lastName)
        //{
        //    var rawQuery = $"WITH UserCTE\n" +
        //                  "AS\n" +
        //                  "( \n" +
        //                    "SELECT [u].[Id], [u].[CreatedDate], [u].[DeleteDate], [u].[Fired], [u].[ManagerId],  [u0].[Address], [u0].[City], [u0].[Country], [u0].[DriverLicense], [u0].[Email], [u0].[FirstName], [u0].[LastName], [u0].[MobilePhone], [u0].[Phone], [u0].[SSN], [u0].[UserId]\n" +
        //                    "FROM [dbo].[Users] AS [u] LEFT JOIN [dbo].[UserContacts] AS [u0] ON [u0].UserId = [u].Id\n" +
        //                    $"WHERE [u0].FirstName LIKE N'{firstName}' OR [u0].LastName LIKE N'{lastName}'\n" +
        //                    "UNION ALL\n" +
        //                    "SELECT [u].[Id], [u].[CreatedDate], [u].[DeleteDate], [u].[Fired], [u].[ManagerId], [u0].[Address], [u0].[City], [u0].[Country], [u0].[DriverLicense], [u0].[Email], [u0].[FirstName], [u0].[LastName], [u0].[MobilePhone], [u0].[Phone], [u0].[SSN], [u0].[UserId]\n" +
        //                    "FROM UserCTE AS M\n" +
        //                      "JOIN [dbo].[Users] AS [u] ON [u].Id = M.ManagerId\n" +
        //                        "JOIN [dbo].[UserContacts] AS [u0] ON [u0].UserId = [u].Id\n" +
        //                   ")\n" +
        //                  "SELECT * FROM UserCTE\n";
        //    var users = _db.Users.FromSqlRaw(rawQuery);
        //    return users;
        //}


        //На данный момент не получилось вызвать рекурсивный вызов чтоб получить всех менеджеров вверх по графу 
        //через Linq или иетоды расширения. Но мугу это сдлеть или запросом (выше), или методами (нижн).
        //Нашол в одном посте на stackoverflow что EF не поддерживает CTE и тоже предлагали на делать через запрос...
        public IEnumerable<User> GetUsersByName(string firstName, string lastName)
        {
            var result = new List<User>();
            var query = _db.Users.Include(c => c.UserContact)
                .Where(u => EF.Functions.Like(u.UserContact.FirstName, firstName) ||
                            EF.Functions.Like(u.UserContact.LastName, lastName));
            var users = query.ToList();
            foreach (var user in users)
            {
                result.Add(user);
                if (user.ManagerId != null)
                {
                    result.AddRange(GetManager(user.ManagerId.Value));
                }
            }
            return result;
        }


        private IEnumerable<User> GetManager(Guid managerId)
        {
            var users = _db.Users.Where(u => u.Id == managerId).ToList();
            var result = new List<User>();
            result.AddRange(users);
            foreach (var user in users)
            {
                if (user.ManagerId != null)
                {
                    result.AddRange(GetManager(user.ManagerId.Value));
                }
            }
            return result;
        }

    }
}
