using AutoMapper;
using LookupSystemService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using LookupSystem.DataAccess.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using LookupSystem.DataAccess.Models;
using LookupSystem.DataAccess.CompileQueries;

namespace LookupSystemService.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiVersion("3")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly LookupSystemDbContext _context;


        public UserController(ILogger<UserController> logger, IMapper mapper, LookupSystemDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("1")]
        public IEnumerable<UserDto> GetUserByEmailV1(string email)
        {
            try
            {
                var query = _context.Users
                    .AsNoTracking()
                    .Include(c => c.UserContact)
                    .Where(u => u.UserContact.Email == email);

                var queryStr = query.ToQueryString();

                //var users = _mapper.ProjectTo<UserDto>(query).ToList();
                var users = query.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToList();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

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

                ////Server evaluation
                //Expression<Func<UserDto, bool>> predicate = u => u.Email.ToUpper() == email.ToUpper();
                //var query = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate);
                //var queryStr = query.ToQueryString();
                //var users = query.ToList();

                //Server evaluation
                Expression<Func<UserDto, bool>> predicate = u => u.Email.ToUpper() == email.ToUpper();
                var query = _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).Where(predicate);
                var queryStr = query.ToQueryString();
                var users = query.ToList();

                //Client evaluation
                //Expression<Func<UserDto, bool>> predicate = u => u.Email.ToUpper() == email.ToUpper();
                //var query = _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider, predicate);
                //var userStr = query.ToQueryString();
                //var users = query.ToList();

                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("3")]
        public IEnumerable<UserDto> GetUserByEmailV3(string email)
        {
            try
            {
                //Sonya21@yahoo.com
                //Dovie4@hotmail.com
                //Judd_Kilback@yahoo.com
                //Lacy_Larkin@yahoo.com

                var query = UserQueries.GetUserByEmail(_context, email);
                var query2 = query.AsQueryable().ProjectTo<UserDto>(_mapper.ConfigurationProvider);
                var users = query2.ToList();

                //var query = UserQueries.GetUserByEmail(_context, email);
                //var users = _mapper.Map<List<UserDto>>(query);

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
        public IEnumerable<UserDto> GetUserByPhonev1(string phone)
        {
            try
            {
                var query = _context.Users
                  .AsNoTracking()
                  .Include(c => c.UserContact)
                  .Where(u => u.UserContact.Phone.ToUpper() == phone.ToUpper());
                var users = _mapper.ProjectTo<UserDto>(query).ToList();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        [HttpGet("GetUserByPhone/{phone}")]
        [MapToApiVersion("2")]
        public IEnumerable<UserDto> GetUserByPhoneV2(string phone)
        {
            try
            {
                Expression<Func<UserDto, bool>> predicate = u => u.Phone.ToUpper() == phone.ToUpper();
                var users = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate).ToList();
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
        public IEnumerable<UserDto> GetFiredUsersV1()
        {
            try
            {
                var query = _context.Users
                    .AsNoTracking()
                    .Include(c => c.UserContact)
                    .Where(u => u.Fired);
                var users = _mapper.ProjectTo<UserDto>(query).ToList();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        [HttpGet("GetFiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<UserDto> GetFiredUsersV2()
        {
            try
            {
                Expression<Func<UserDto, bool>> predicate = u => u.Fired;
                var users = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate).ToList();
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
        public IEnumerable<UserDto> GetHiredUsersV1()
        {
            try
            {
                var query = _context.Users.Where(u => !u.Fired);
                var users = _mapper.ProjectTo<UserDto>(query).ToList();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        [HttpGet("GetHiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<UserDto> GetHiredUsersV2()
        {
            try
            {
                Expression<Func<UserDto, bool>> predicate = u => !u.Fired;
                var users = _mapper.ProjectTo<UserDto>(_context.Users).Where(predicate).ToList();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }

        [HttpGet("GetUsersByNameBySqlRaw")]
        [MapToApiVersion("1")]
        public IEnumerable<UserDto> GetUsersByNameBySqlRaw([FromQuery] RequestUserByName model)
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

                var query = _context.Users.FromSqlRaw(rawQuery).ToList();
                var users = _mapper.Map<List<UserDto>>(query);
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }


       [HttpGet("GetUsersByNameByTree")]
       [MapToApiVersion("1")]
        public IEnumerable<UserDto> GetUsersByNameByTree([FromQuery] RequestUserByName model)
        {
            try
            {
                var all = _context.Users.AsNoTracking().Include(c => c.UserContact).Include(t => t.Tags).ToList();
                TreeExtensions.ITree<User> virtualRootNode = all.ToTree((parent, child) => child.ManagerId == parent.Id);
                List<TreeExtensions.ITree<User>> rootLevelFoldersWithSubTree = virtualRootNode.Children.ToList();
                List<TreeExtensions.ITree<User>> flattenedListOfFolderNodes = virtualRootNode.Children.Flatten(node => node.Children).ToList();
                TreeExtensions.ITree<User> userNode = flattenedListOfFolderNodes.First(node => node.Data.UserContact.FirstName == model.FirstName || node.Data.UserContact.LastName == model.LastName);
                List<User> usr = new List<User>();
                usr.Add(userNode.Data);
                usr.AddRange(GetParents(userNode));
                //var users = _mapper.ProjectTo<UserDto>(usr.AsQueryable());
                var users = _mapper.Map<List<UserDto>>(usr);
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }



        [HttpGet("GetUsersByNameByRecursion")]
        [MapToApiVersion("1")]
        public IEnumerable<UserDto> GetUsersByNameByRecursion([FromQuery] RequestUserByName model)
        {
            try
            {
                var foundUsers = _context.Users
                    .AsNoTracking()
                    .Include(c => c.UserContact)
                    .Include(t => t.Tags)
                    .Where(u => EF.Functions.Like(u.UserContact.FirstName, model.FirstName) ||
                                EF.Functions.Like(u.UserContact.LastName, model.LastName))
                    .ToList();
                
                var result = new List<User>();
                foreach (var user in foundUsers)
                {
                    result.Add(user);
                    if (user.ManagerId != null)
                    {
                        result.AddRange(GetManager(user.ManagerId.Value));
                    }
                }
                var users = _mapper.Map<List<UserDto>>(result);
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserDto>();
            }
        }


        private static List<T> GetParents<T>(TreeExtensions.ITree<T> node, List<T> parentNodes = null) where T : class
        {
            while (true)
            {
                parentNodes ??= new List<T>();
                if (node?.Parent?.Data == null) return parentNodes;
                parentNodes.Add(node.Parent.Data);
                node = node.Parent;
            }
        }

        private IEnumerable<User> GetManager(Guid managerId)
        {
            var users = _context.Users
                    .AsNoTracking()
                    .Include(c => c.UserContact)
                    .Include(t => t.Tags).Where(u => u.Id == managerId).ToList();
            var result = new List<User>();
            result.AddRange(users);
            foreach (var user in users)
            {
                if (user.ManagerId != null)
                {
                    result.AddRange(GetManager(user.ManagerId.Value));
                }
            }
            return result;
        }

    }
}

