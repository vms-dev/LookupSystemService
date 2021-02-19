using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Interfaces;
using LookupSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupSystem.DataAccess.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private LookupSystemDbContext db;
        public UserRepository(LookupSystemDbContext contex)
        {
            //db = new UserContext();
            db = contex;
        }
        public IQueryable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
            var user = db.Users                
                .Include(c => c.UserContact)
                .Where(u => u.UserContact.Email.ToUpper() == email.ToUpper())
                .ToList();

            return user;
        } 
        
        public IEnumerable<User> GetUserByPhone(string phone)
        {
            var user = db.Users                
                .Include(c => c.UserContact)
                .Where(u => u.UserContact.Phone.ToUpper() == phone.ToUpper())
                .ToList();

            return user;
        }     
        
        public IEnumerable<User> GetHiredUsers()
        {
            var user = db.Users                
                .Include(c => c.UserContact)
                .Where(u => u.Hired)
                .ToList();

            return user;
        }

        public IEnumerable<User> GetIdledUsers()
        {
            var user = db.Users
                .Where(u => !u.Hired)
                .ToList();

            return user;
        }
    }
}
