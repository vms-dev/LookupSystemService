using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookupSystem.DataAccess.CompileQueries
{
    public class UserQueries
    {
        public static Func<LookupSystemDbContext, string, IEnumerable<User>> GetUserByEmail =
            EF.CompileQuery(
                (LookupSystemDbContext db, string email) => db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .Include(t => t.Tags)
                .Where(u => u.UserContact.Email == email)
            );

        public static Func<LookupSystemDbContext, string, IEnumerable<User>> GetUserByPhone =
            EF.CompileQuery(
                (LookupSystemDbContext db, string phone) => db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .Include(t => t.Tags)
                .Where(u => u.UserContact.Phone == phone)
            );

        public static Func<LookupSystemDbContext, IEnumerable<User>> GetFiredUsers =
            EF.CompileQuery(
                (LookupSystemDbContext db) => db.Users
                .AsNoTracking()
                .Include(c => c.UserContact)
                .Include(t => t.Tags)
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
                .Include(t => t.Tags)
                .Where(u => EF.Functions.Like(u.UserContact.FirstName, firstName) ||
                            EF.Functions.Like(u.UserContact.LastName, lastName))
            );
    }
}