using System;
using System.Collections.Generic;
using System.Linq;

namespace LookupSystem.DataAccess.Interfaces
{
    public interface IRepository<T> : IDisposable  where T : class
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();

        IEnumerable<T> GetUserByEmail(string email);

        IEnumerable<T> GetUserByPhone(string phone);
        IEnumerable<T> GetFiredUsers();
        IEnumerable<T> GetHiredUsers();
        IEnumerable<T> GetUsersByName(string firstName, string lastName);


    }
}
