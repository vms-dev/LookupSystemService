using LookupSystem.DataAccess.Models;
using System;
using System.Linq;

namespace LookupSystem.DataAccess.Data
{
    public class DbInitializer
    {
        public static void InitializeV2(LookupSystemDbContext context)
        {
            context.Database.EnsureCreated();
            
            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            
            var users = DataInit.GenerateUserData(10);
            
            context.Users.AddRange(users);
            context.SaveChanges();
            
        }
        
        public static void Initialize(LookupSystemDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();
            var userId3 = Guid.NewGuid();
            var userId4 = Guid.NewGuid();
            var userId5 = Guid.NewGuid();
            var userId6 = Guid.NewGuid();
            var userId7 = Guid.NewGuid();
            var userId8 = Guid.NewGuid();

            var users = new User[]
            {
                new User{
                    Id=userId1,
                    CreatedDate=DateTime.Parse("2019-09-01"),
                    DeleteDate=null,
                    Fired = true,
                    ManagerId = Guid.Parse("1bd725c8-4ea1-4462-9def-f32e027103f9"),
                },
                new User{
                    Id=userId2,
                    CreatedDate=DateTime.Parse("2017-10-12"),
                    DeleteDate=null,
                    Fired = false,
                    ManagerId = null
                },
                new User{
                    Id=userId3,
                    CreatedDate=DateTime.Parse("2018-09-03"),
                    DeleteDate=null,
                    Fired = true,
                    ManagerId = Guid.Parse("1bd725c8-4ea1-4462-9def-f32e027103f9"),

                },
                new User{
                    Id=userId4,
                    CreatedDate=DateTime.Parse("2017-01-06"),
                    DeleteDate=DateTime.Parse("2018-01-06"),
                    Fired = false,
                    ManagerId = null
                },
                new User{
                    Id=userId5,
                    CreatedDate=DateTime.Parse("2017-05-18"),
                    DeleteDate=DateTime.Parse("2020-03-10"),
                    Fired = false,
                    ManagerId = null
                },
                new User{
                    Id=userId6,
                    CreatedDate=DateTime.Parse("2016-09-21"),
                    DeleteDate=null,
                    Fired = true,
                    ManagerId = Guid.Parse("1bd725c8-4ea1-4462-9def-f32e027103f9"),
                },
                new User{
                    Id=userId7,
                    CreatedDate=DateTime.Parse("2018-02-23"),
                    DeleteDate=null,
                    Fired = true,
                    ManagerId = Guid.Parse("e1967daf-cda9-4b86-ac17-e36888177d17"),
                },
                new User{
                    Id=userId8,
                    CreatedDate=DateTime.Parse("2019-08-29"),
                    DeleteDate=null,
                    Fired = false,
                    ManagerId = Guid.Parse("e1967daf-cda9-4b86-ac17-e36888177d17"),
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var userContacts = new UserContact[]
            {
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate = null,
                    FirstName = "Alexander",
                    LastName = "Ivanov",
                    Phone = "0501111111",
                    MobilePhone = "0501111111",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 1",
                    SSN = "SSN999991",
                    DriverLicense = "AAA999991",
                    Email = "a.ivanov@test.com",
                    UserId = userId1
                },
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate = null,
                    FirstName = "Petro",
                    LastName = "Sharapov",
                    Phone = "0501111112",
                    MobilePhone = "0501111112",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 1",
                    SSN = "SSN999992",
                    DriverLicense = "AAA999991",
                    Email = "a.ivanov@test.com",
                    UserId = userId2
                },
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate = null,
                    FirstName = "Gleb",
                    LastName = "Kuzmin",
                    Phone = "0501111113",
                    MobilePhone = "0501111113",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 1",
                    SSN = "SSN999993",
                    DriverLicense = "AAA999993",
                    Email = "kuzmin@test.com",
                    UserId = userId3
                },
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate=DateTime.Parse("2018-01-06"),
                    FirstName = "Petro",
                    LastName = "Tkachuk",
                    Phone = "0501111114",
                    MobilePhone = "0501111114",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 1",
                    SSN = "SSN999994",
                    DriverLicense = "AAA999994",
                    Email = "petro222@test.com",
                    UserId = userId4
                },
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate=DateTime.Parse("2020-03-10"),
                    FirstName = "Irina",
                    LastName = "Petrova",
                    Phone = "0501111115",
                    MobilePhone = "0501111115",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 5",
                    SSN = "SSN999995",
                    DriverLicense = "AAA999995",
                    Email = "i.petrova@test.com",
                    UserId = userId5
                },
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate = null,
                    FirstName = "Tonya",
                    LastName = "Kozak",
                    Phone = "0501111116",
                    MobilePhone = "0501111116",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 6",
                    SSN = "SSN999996",
                    DriverLicense = "AAA999996",
                    Email = "kozak@test2.com",
                    UserId = userId6
                },
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate = null,
                    FirstName = "Taras",
                    LastName = "Suprun",
                    Phone = "0501111117",
                    MobilePhone = "0501111117",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 7",
                    SSN = "SSN999997",
                    DriverLicense = "AAA999997",
                    Email = "t.suprun@test2.com",
                    UserId = userId7
                },
                new UserContact{
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Parse("2019-09-01"),
                    DeleteDate = null,
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Phone = "0501111118",
                    MobilePhone = "0501111118",
                    Country = "Ukraine",
                    City = "Lutsk",
                    Address = "Kravchuka 7",
                    SSN = "SSN999998",
                    DriverLicense = "AAA999998",
                    Email = "ii@test2.com",
                    UserId = userId8
                }
            };

            context.UserContacts.AddRange(userContacts);
            context.SaveChanges();

        }
    }
}
