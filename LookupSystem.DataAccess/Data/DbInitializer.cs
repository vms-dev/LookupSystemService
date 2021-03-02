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
                Random randomizer = new Random();

                var users = _dbContext.Users.ToList();
                // Look for any User.
                if (!users.Any())
                {
                    users = GenerateUserData(30);
                    _dbContext.Users.AddRange(users);
                }

                var tags = _dbContext.Tags.ToList();
                if (!tags.Any())
                {
                    // Learning • Leisure activities • Management • Massage • Medicine
                    //
                    var activities = new[] { "Management", "Learning", "Massage", "Medicine", "Publishing" , "Teaching" };
                    foreach (var activity in activities)
                    {
                        tags.Add(FakeTagsData(activity).Generate());
                    }

                    _dbContext.Tags.AddRange(tags);                   
                }

                foreach (var user in users)
                {
                    var randomTags = tags.GetRandom<Tag>(randomizer.Next(1, tags.Count));
                    foreach (var rt in randomTags)
                    {
                        user.Tags.Add(rt);
                    }                   
                }

                _dbContext.SaveChanges();
            }
        }


        private Faker<Tag> FakeTagsData(string activityName)
        {


            var tagContactFaker = new Faker<Tag>()
                    .RuleFor(t => t.Id, f => f.Random.Guid())
                    .RuleFor(t => t.Name, f => activityName);

            return tagContactFaker;
        }

        private List<User> GenerateUserData(int countItems)
        {
            var result = new List<User>();

            //manager 0-lv
            for (int i = 0; i < 2; i++) result.Add(FakeUserData(null).Generate());
            
            //manager 1-lv
            for (int i = 0; i < 2; i++) result.Add(FakeUserData(result[0].Id).Generate());           
            for (int i = 0; i < 3; i++) result.Add(FakeUserData(result[1].Id).Generate());
            
            //manager 2-lv
            for (int i = 0; i < 2; i++) result.Add(FakeUserData(result[2].Id).Generate());            
            for (int i = 0; i < 3; i++) result.Add(FakeUserData(result[3].Id).Generate());            
            for (int i = 0; i < 2; i++) result.Add(FakeUserData(result[4].Id).Generate());            
            for (int i = 0; i < 3; i++) result.Add(FakeUserData(result[5].Id).Generate());            
            for (int i = 0; i < 3; i++) result.Add(FakeUserData(result[6].Id).Generate());
            
            //workers
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[7].Id, true).Generate());
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[8].Id).Generate());
            for (int i = 0; i < 5; i++) result.Add(FakeUserData(result[9].Id).Generate());
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[10].Id, true).Generate());
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[11].Id, true).Generate());
            for (int i = 0; i < 20; i++) result.Add(FakeUserData(result[12].Id, true).Generate());
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[13].Id).Generate());
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[14].Id).Generate());
            for (int i = 0; i < 40; i++) result.Add(FakeUserData(result[15].Id, true).Generate());
            for (int i = 0; i < 20; i++) result.Add(FakeUserData(result[16].Id).Generate());
            for (int i = 0; i < 3; i++) result.Add(FakeUserData(result[17].Id).Generate());
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[18].Id, true).Generate());
            for (int i = 0; i < 10; i++) result.Add(FakeUserData(result[19].Id, true).Generate());
            return result;
        }


        private Faker<User> FakeUserData(Guid? ManagerId, bool isFiredPosible = false)
        {

            var userContactFaker = new Faker<UserContact>()
                    .RuleFor(u => u.Id, f => f.Random.Guid())
                    .RuleFor(u => u.CreatedDate, f => f.Date.Past(15, DateTime.Now.AddDays(-1)))
                    .RuleFor(u => u.DeleteDate, f => isFiredPosible ? f.Date.Past(15, DateTime.Now.AddDays(-1)).OrNull(f, 0.75F) : null)
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
                //.RuleFor(u => u.Fired, f => isFiredPosible ? f.Random.Bool(0.3F) : false)
                //.RuleFor(u => u.ManagerId, (f, u) => u.Fired ? null : f.Random.Guid())
                .RuleFor(u => u.Fired, f => userContact.DeleteDate != null)
                .RuleFor(u => u.ManagerId, ManagerId)
                .RuleFor(u => u.UserContact, f => userContact);
                
            return userFaker;
        }
    }
}
