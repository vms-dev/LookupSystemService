using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookupSystem.DataAccess.Repositories
{
    public class CompileQueries
    {
        public static Func<LookupSystemDbContext, string, IEnumerable<User>> GetUserByEmail =
            EF.CompileQuery(
                (LookupSystemDbContext db, string email) => db.Users
                .Include(c => c.UserContact)
                .Where(u => string.Compare(u.UserContact.Email, email, StringComparison.OrdinalIgnoreCase) == 0)
            );

        public static Func<LookupSystemDbContext, string, IEnumerable<User>> GetUserByPhone =
            EF.CompileQuery(
                (LookupSystemDbContext db, string phone) => db.Users
                .Include(c => c.UserContact)
                .Where(u => string.Compare(u.UserContact.Phone, phone, StringComparison.OrdinalIgnoreCase) == 0)
            );

        public static Func<LookupSystemDbContext, IEnumerable<User>> GetFiredUsers =
            EF.CompileQuery(
                (LookupSystemDbContext db) => db.Users.Where(u => u.Fired)
            );

        public static Func<LookupSystemDbContext, IEnumerable<User>> GetHiredUsers =
            EF.CompileQuery(
                (LookupSystemDbContext db) => db.Users.Where(u => !u.Fired)
            );

        public static Func<LookupSystemDbContext, string, string, Guid?, IEnumerable<User>> GetUsersByName =
            EF.CompileQuery(
                (LookupSystemDbContext db, string firstName, string lastName, Guid? managerId) => db.Users
                .Include(c => c.UserContact)
                .Where(u => EF.Functions.Like(u.UserContact.FirstName, firstName) ||
                            EF.Functions.Like(u.UserContact.LastName, lastName) ||
                            (u.ManagerId.HasValue && managerId.HasValue && u.ManagerId.Value == managerId.Value))
            );
    }
}