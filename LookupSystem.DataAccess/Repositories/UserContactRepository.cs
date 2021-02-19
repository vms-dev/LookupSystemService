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
    public class UserContactRepository : IRepository<UserContact>
    {
        private LookupSystemDbContext db;
        public UserContactRepository(LookupSystemDbContext contex)
        {
            //db = new UserContext();
            db = contex;
        }
        public IQueryable<UserContact> GetAll()
        {
            return db.UserContacts;
        }

        public UserContact Get(int id)
        {
            return db.UserContacts.Find(id);
        }

        public void Create(UserContact item)
        {
            db.UserContacts.Add(item);
        }

        public void Update(UserContact item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserContact user = db.UserContacts.Find(id);
            if (user != null)
                db.UserContacts.Remove(user);
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
    }
}
