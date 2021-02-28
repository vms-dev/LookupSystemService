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
            dbInitializer.InitializeFakeData();
        }

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("1")]
        public IEnumerable<UserByEmail> GetUserByEmailV1(string email)
        {
            try
            {
                var users = _mapper.Map<List<UserByEmail>>(_userRepo.GetUserByEmail(email));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserByEmail>();
            }
        }

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("2")]
        public IEnumerable<UserByEmail> GetUserByEmailV2(string email)
        {
            try
            {
                var users = _mapper.Map<List<UserByEmail>>(CompileQueries.GetUserByEmail(_context, email).ToList());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserByEmail>();
            }
        }


        [HttpGet("GetUserByPhone/{phone}")]
        [MapToApiVersion("1")]
        public IEnumerable<UserByPhone> GetUserByPhonev1(string phone)
        {
            try
            {
                var users = _mapper.Map<List<UserByPhone>>(_userRepo.GetUserByPhone(phone));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserByPhone>();
            }
        }

        [HttpGet("GetUserByPhone/{phone}")]
        [MapToApiVersion("2")]
        public IEnumerable<UserByPhone> GetUserByPhoneV2(string phone)
        {
            try
            {
                var users = _mapper.Map<List<UserByPhone>>(CompileQueries.GetUserByPhone(_context, phone).ToList());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserByPhone>();
            }
        }


        [HttpGet("GetFiredUsers")]
        [MapToApiVersion("1")]
        public IEnumerable<UserFired> GetFiredUsersV1()
        {
            try
            {
                var users = _mapper.Map<List<UserFired>>(_userRepo.GetFiredUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserFired>();
            }
        }

        [HttpGet("GetFiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<UserFired> GetFiredUsersV2()
        {
            try
            {
                var users = _mapper.Map<List<UserFired>>(CompileQueries.GetFiredUsers(_context).ToList());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserFired>();
            }
        }


        [HttpGet("GetHiredUsers")]
        [MapToApiVersion("1")]
        public IEnumerable<UserFired> GetHiredUsersV1()
        {
            try
            {
                var users = _mapper.Map<List<UserFired>>(_userRepo.GetHiredUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserFired>();
            }
        }


        [HttpGet("GetHiredUsers")]
        [MapToApiVersion("2")]
        public IEnumerable<UserFired> GetHiredUsersV2()
        {
            try
            {
                var users = _mapper.Map<List<UserFired>>(CompileQueries.GetHiredUsers(_context).ToList());
                return users;                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserFired>();
            }
        }

    }
}
