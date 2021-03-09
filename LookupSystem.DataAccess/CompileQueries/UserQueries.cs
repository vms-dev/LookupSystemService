using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookupSystem.DataAccess.Repositories
{
    public class UserQueries
    {
        public static Func<LookupSystemDbContext, string, IEnumerable<User>> GetUserByEmail =
            EF.CompileQuery(
                (LookupSystemDbContext db, string email) => db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .Where(u => u.UserContact.Email.ToUpper() == email.ToUpper()) // Сравнение строк с преобразованием в ToUpper() сделано в целях теста
            );

        public static Func<LookupSystemDbContext, string, IEnumerable<User>> GetUserByPhone =
            EF.CompileQuery(
                (LookupSystemDbContext db, string phone) => db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .Where(u => string.Compare(u.UserContact.Phone, phone, StringComparison.OrdinalIgnoreCase) == 0)
            );

        public static Func<LookupSystemDbContext, IEnumerable<User>> GetFiredUsers =
            EF.CompileQuery(
                (LookupSystemDbContext db) => db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .Where(u => u.Fired)
            );

        public static Func<LookupSystemDbContext, IEnumerable<User>> GetHiredUsers =
            EF.CompileQuery(
                (LookupSystemDbContext db) => db.Users
                .Where(u => !u.Fired)
            );

        public static Func<LookupSystemDbContext, string, string, IEnumerable<User>> GetUsersByName =
            EF.CompileQuery(
                (LookupSystemDbContext db, string firstName, string lastName) => db.Users
                .Include(c => c.UserContact)
                .Where(u => EF.Functions.Like(u.UserContact.FirstName, firstName) ||
                            EF.Functions.Like(u.UserContact.LastName, lastName))
            );
    }
}