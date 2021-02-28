using LookupSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Bogus;
using Bogus.Extensions;

namespace LookupSystem.DataAccess.Data
{
    public class DbInitializer
    {
        private readonly LookupSystemDbContext _dbContext;
        
        private readonly IConfiguration _configuration;
        
        public DbInitializer(LookupSystemDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;

        }

        public void InitializeFakeData()
        {
            if (bool.TryParse(_configuration["AddFakeDataToDatabase"], out var result) && result)
            {
                // Look for any User.
                if (_dbContext.Users.Any())
                {
                    return;  
                }
                
                var users = GenerateUserData(10);
                _dbContext.Users.AddRange(users);
                _dbContext.SaveChanges();
            }
        }


        private List<User> GenerateUserData(int countItems)
        {
            var result = new List<User>();
            for (int i = 0; i < countItems; i++)
            {
                result.Add(FakeUserData().Generate());
            }

            return result;
        }


        private Faker<User> FakeUserData()
        {

            var userContactFaker = new Faker<UserContact>()
                    .RuleFor(u => u.Id, f => f.Random.Guid())
                    .RuleFor(u => u.CreatedDate, f => f.Date.Past(15, DateTime.Now.AddDays(-1)))
                    .RuleFor(u => u.DeleteDate, f => f.Date.Past(15, DateTime.Now.AddDays(-1)).OrNull(f, 0.2F))
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Phone, f => f.Person.Phone)
                    .RuleFor(u => u.MobilePhone, f => f.Person.Phone)
                    .RuleFor(u => u.Country, f => f.Address.Country())
                    .RuleFor(u => u.City, f => f.Address.City())
                    .RuleFor(u => u.Address, f => f.Address.FullAddress())
                    .RuleFor(u => u.SSN, f => f.Random.Replace("###-##-####"))
                    .RuleFor(u => u.DriverLicense, f => f.Random.Replace("#########"))
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                    .RuleFor(u => u.UserId, f => f.Random.Guid());
            var userContact = userContactFaker.Generate();
            
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => userContact.UserId)
                .RuleFor(u => u.CreatedDate, f => userContact.CreatedDate)
                .RuleFor(u => u.DeleteDate, f => userContact.DeleteDate)
                .RuleFor(u => u.Fired, f => f.Random.Bool(0.5F))
                .RuleFor(u => u.ManagerId, (f, u) => u.Fired ? null : f.Random.Guid())
                .RuleFor(u => u.UserContact, f => userContact);
                
            return userFaker;
        }
    }
}
