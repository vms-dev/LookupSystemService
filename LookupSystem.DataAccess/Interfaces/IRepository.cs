using System;
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
    }
}
