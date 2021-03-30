using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Models;
using System;
using System.Linq;

namespace LookupSystem.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LookupSystemDbContext _context;

        public UserRepository(LookupSystemDbContext context)
        {
            _context = context;
        }

        public void DeleteUserOlderThan(int countOfDays)
        {
            var oldUser = FindUserOlderThan(countOfDays);
            if (oldUser.Any())
            {
                _context.Users.RemoveRange(oldUser);
                _context.SaveChanges();
            }
        }

        private IQueryable<User> FindUserOlderThan(int countOfDays)
        {
            var oldDate = DateTime.Now.AddDays(-countOfDays);
            var query = _context.Users.Where(u => u.Fired && u.DeleteDate < oldDate);
            //var queryStr = query.ToQueryString();
            return query;
        }
    }
}