using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Interfaces;
using LookupSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookupSystem.DataAccess.Repositories
{
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

        public IEnumerable<User> GetUserByEmail(string email)
        {
            var query = _db.Users.Include(c => c.UserContact)
                .Where(u => string.Compare(u.UserContact.Email, email, StringComparison.OrdinalIgnoreCase) == 0);
            
            return query.ToList();
        } 
        
        public IEnumerable<User> GetUserByPhone(string phone)
        {
            var query = _db.Users.Include(c => c.UserContact)
                .Where(u => string.Compare(u.UserContact.Phone, phone, StringComparison.OrdinalIgnoreCase) == 0);

            return query.ToList();
        }     
        
        public IEnumerable<User> GetFiredUsers()
        {
            var query = _db.Users.Include(c => c.UserContact)
                .Where(u => u.Fired);

            return query.ToList();
        }

        public IEnumerable<User> GetHiredUsers()
        {
            var query = _db.Users
                .Where(u => !u.Fired);

            return query.ToList();
        }

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
