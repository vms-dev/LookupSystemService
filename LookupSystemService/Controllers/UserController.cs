using AutoMapper;
using LookupSystemService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using LookupSystem.DataAccess.Data;
using System.Linq;
using LookupSystem.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LookupSystem.DataAccess.Models;

namespace LookupSystemService.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly LookupSystemDbContext _context;


        public UserController(ILogger<UserController> logger, IMapper mapper, DbInitializer dbInitializer, LookupSystemDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
#if DEBUG
            //Место вызова не верное и будет перенесено. Изначально хотел вызвыть в клессе LookupSystemDbContext
            //но я там не получал контекст. Разберусь с этим и переделаю.
            dbInitializer.InitializeFakeData();
#endif
        }

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("1")]
        public async Task<IEnumerable<UserDto>> GetUserByEmailV1(string email)
        {
            try
            {
                var query = _context.Users
                    .AsNoTracking()
                    .Include(c => c.UserContact)
                    .Where(u => u.UserContact.Email.ToUpper() == email.ToUpper());

                var users = await _mapper.ProjectTo<UserDto>(query).ToListAsync();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        //Задача с втрой версией не сделана, я делал  CompileQuery а не Expression
        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("2")]
        public IEnumerable<UserDto> GetUserByEmailV2(string email)
        {
            try
            {
                //Expression<Func<User, bool>> filter = u => u.UserContact.Email.ToUpper() == email.ToUpper();
                //var query = _context.Users.AsNoTracking().Include(c => c.UserContact) .Where(filter);
                //var users = _mapper.ProjectTo<UserDto>(query).ToList();
                //return users;

                Expression<Func<UserDto, bool>> predicate = u => u.Email.ToUpper() == email.ToUpper();
                var users = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate).ToList();
                return users;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }


        [HttpGet("GetUserByPhone/{phone}")]
        [MapToApiVersion("1")]
        public async Task<IEnumerable<UserDto>> GetUserByPhonev1(string phone)
        {
            try
            {
                var query = _context.Users
                  .AsNoTracking()
                  .Include(c => c.UserContact)
                  .Where(u => u.UserContact.Phone.ToUpper() == phone.ToUpper());
                var users = await _mapper.ProjectTo<UserDto>(query).ToListAsync();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        //Задача с втрой версией не сделана, я делал  CompileQuery а не Expression
        [HttpGet("GetUserByPhone/{phone}")]
        [MapToApiVersion("2")]
        public IEnumerable<UserDto> GetUserByPhoneV2(string phone)
        {
            try
            {
                Expression<Func<UserDto, bool>> predicate = u => u.Phone.ToUpper() == phone.ToUpper();
                var users = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate);
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }


        [HttpGet("GetFiredUsers")]
        [MapToApiVersion("1")]
        public async Task<IEnumerable<UserDto>> GetFiredUsersV1()
        {
            try
            {
                var query = _context.Users
                    .AsNoTracking()
                    .Include(c => c.UserContact)
                    .Where(u => u.Fired);
                var users = await _mapper.ProjectTo<UserDto>(query).ToListAsync();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        //Задача с втрой версией не сделана, я делал  CompileQuery а не Expression
        [HttpGet("GetFiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<UserDto> GetFiredUsersV2()
        {
            try
            {
                Expression<Func<UserDto, bool>> predicate = u => u.Fired;
                var users = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate);
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }


        [HttpGet("GetHiredUsers")]
        [MapToApiVersion("1")]
        public async Task<IEnumerable<UserDto>> GetHiredUsersV1()
        {
            try
            {
                var query = _context.Users.Where(u => !u.Fired);
                var users = await _mapper.ProjectTo<UserDto>(query).ToListAsync();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        //Задача с втрой версией не сделана, я делал  CompileQuery а не Expression
        [HttpGet("GetHiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<UserDto> GetHiredUsersV2()
        {
            try
            {
                Expression<Func<UserDto, bool>> predicate = u => !u.Fired;
                var users = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate);
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }


        [HttpGet("GetUsersByName")]
        [MapToApiVersion("1")]
        public IEnumerable<UserDto> GetUsersByNameV1([FromQuery] RequestUserByName model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.FirstName))
                {
                    model.FirstName = $"{model.FirstName}%";
                }
                if (!string.IsNullOrWhiteSpace(model.LastName))
                {
                    model.LastName = $"{model.LastName}%";
                }

                var rawQuery = $"WITH UserCTE\n" +
                              "AS\n" +
                              "( \n" +
                                "SELECT [u].[Id], [u].[CreatedDate], [u].[DeleteDate], [u].[Fired], [u].[ManagerId],  [u0].[Address], [u0].[City], [u0].[Country], [u0].[DriverLicense], [u0].[Email], [u0].[FirstName], [u0].[LastName], [u0].[MobilePhone], [u0].[Phone], [u0].[SSN], [u0].[UserId]\n" +
                                "FROM [dbo].[Users] AS [u] LEFT JOIN [dbo].[UserContacts] AS [u0] ON [u0].UserId = [u].Id\n" +
                                $"WHERE [u0].FirstName LIKE N'{model.FirstName}' OR [u0].LastName LIKE N'{model.LastName}'\n" +
                                "UNION ALL\n" +
                                "SELECT [u].[Id], [u].[CreatedDate], [u].[DeleteDate], [u].[Fired], [u].[ManagerId], [u0].[Address], [u0].[City], [u0].[Country], [u0].[DriverLicense], [u0].[Email], [u0].[FirstName], [u0].[LastName], [u0].[MobilePhone], [u0].[Phone], [u0].[SSN], [u0].[UserId]\n" +
                                "FROM UserCTE AS M\n" +
                                  "JOIN [dbo].[Users] AS [u] ON [u].Id = M.ManagerId\n" +
                                    "JOIN [dbo].[UserContacts] AS [u0] ON [u0].UserId = [u].Id\n" +
                               ")\n" +
                              "SELECT * FROM UserCTE\n";

                var query = _context.Users.FromSqlRaw(rawQuery);
                var users = _mapper.ProjectTo<UserDto>(query).ToList();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        //Задача с втрой версией не сделана, я делал  CompileQuery а не Expression
        //[HttpGet("GetUsersByName")]
        //[MapToApiVersion("2")]
        //public IEnumerable<UserByName> GetUsersByNameV2([FromQuery] RequestByNameModel model)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(model.FirstName))
        //        {
        //            model.FirstName = $"{model.FirstName}%";
        //        }
        //        if (!string.IsNullOrWhiteSpace(model.LastName))
        //        {
        //            model.LastName = $"{model.LastName}%";
        //        }
        //        var users = _mapper.Map<List<UserByName>>(CompileQueries.GetUsersByName(_context, model.FirstName, model.LastName).ToList());
        //        return users;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return new List<UserByName>();
        //    }
        //}
    }
}
