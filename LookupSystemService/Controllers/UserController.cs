using AutoMapper;
using LookupSystem.DataAccess.Interfaces;
using LookupSystem.DataAccess.Models;
using LookupSystemService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using LookupSystem.DataAccess.Data;
using System.Linq;
using LookupSystem.DataAccess.Repositories;

namespace LookupSystemService.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;
        private readonly LookupSystemDbContext _context;


        public UserController(ILogger<UserController> logger, IMapper mapper, IRepository<User> userRepo, DbInitializer dbInitializer, LookupSystemDbContext context)
        {
            _logger = logger;
            _userRepo = userRepo;
            _mapper = mapper;
            _context = context;
#if DEBUG
            dbInitializer.InitializeFakeData();
#endif
        }

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("1")]
        public IEnumerable<ShortUserInfo> GetUserByEmailV1(string email)
        {
            try
            {
                var users = _mapper.Map<List<ShortUserInfo>>(_userRepo.GetUserByEmail(email));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ShortUserInfo>();
            }
        }

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("2")]
        public IEnumerable<ShortUserInfo> GetUserByEmailV2(string email)
        {
            try
            {
                var users = _mapper.Map<List<ShortUserInfo>>(CompileQueries.GetUserByEmail(_context, email).ToList());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ShortUserInfo>();
            }
        }


        [HttpGet("GetUserByPhone/{phone}")]
        [MapToApiVersion("1")]
        public IEnumerable<ShortUserInfo> GetUserByPhonev1(string phone)
        {
            try
            {
                var users = _mapper.Map<List<ShortUserInfo>>(_userRepo.GetUserByPhone(phone));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ShortUserInfo>();
            }
        }

        [HttpGet("GetUserByPhone/{phone}")]
        [MapToApiVersion("2")]
        public IEnumerable<ShortUserInfo> GetUserByPhoneV2(string phone)
        {
            try
            {
                var users = _mapper.Map<List<ShortUserInfo>>(CompileQueries.GetUserByPhone(_context, phone).ToList());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ShortUserInfo>();
            }
        }


        [HttpGet("GetFiredUsers")]
        [MapToApiVersion("1")]
        public IEnumerable<FullUserInfo> GetFiredUsersV1()
        {
            try
            {
                var users = _mapper.Map<List<FullUserInfo>>(_userRepo.GetFiredUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<FullUserInfo>();
            }
        }

        [HttpGet("GetFiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<FullUserInfo> GetFiredUsersV2()
        {
            try
            {
                var users = _mapper.Map<List<FullUserInfo>>(CompileQueries.GetFiredUsers(_context).ToList());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<FullUserInfo>();
            }
        }


        [HttpGet("GetHiredUsers")]
        [MapToApiVersion("1")]
        public IEnumerable<FullUserInfo> GetHiredUsersV1()
        {
            try
            {
                var users = _mapper.Map<List<FullUserInfo>>(_userRepo.GetHiredUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<FullUserInfo>();
            }
        }


        [HttpGet("GetHiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<FullUserInfo> GetHiredUsersV2()
        {
            try
            {
                var users = _mapper.Map<List<FullUserInfo>>(CompileQueries.GetHiredUsers(_context).ToList());
                return users;                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<FullUserInfo>();
            }
        }


        [HttpGet("GetUsersByName")]
        [MapToApiVersion("1")]
        public IEnumerable<ShortUserInfo> GetUsersByNameV1([FromQuery] RequestByNameModel model)
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

                var users = _mapper.Map<List<ShortUserInfo>>(_userRepo.GetUsersByName(model.FirstName, model.LastName));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ShortUserInfo>();
            }
        }


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
