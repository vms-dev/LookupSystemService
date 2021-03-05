using AutoMapper;
using LookupSystem.DataAccess.Interfaces;
using LookupSystem.DataAccess.Models;
using LookupSystemService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using LookupSystem.DataAccess.Data;

namespace LookupSystemService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IMapper mapper, IRepository<User> userRepo, DbInitializer dbInitializer)
        {
            _logger = logger;
            _userRepo = userRepo;
            _mapper = mapper;
            dbInitializer.InitializeFakeData();
        }

        [HttpGet("GetUserByEmail/{email}")]
        public IEnumerable<ShortUserInfo> GetUserByEmail(string email)
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

        [HttpGet("GetUserByPhone/{phone}")]
        public IEnumerable<ShortUserInfo> GetUserByPhone(string phone)
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


        [HttpGet("GetFiredUsers")]
        public IEnumerable<ShortUserInfo> GetFiredUsers()
        {
            try
            {
                var users = _mapper.Map<List<ShortUserInfo>>(_userRepo.GetFiredUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ShortUserInfo>();
            }
        }


        [HttpGet("GetHiredUsers")]
        public IEnumerable<ShortUserInfo> GetHiredUsers()
        {
            try
            {
                var users = _mapper.Map<List<ShortUserInfo>>(_userRepo.GetHiredUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ShortUserInfo>();
            }
        }
    }
}
