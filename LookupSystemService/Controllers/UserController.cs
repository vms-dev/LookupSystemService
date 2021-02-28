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
        public IEnumerable<UserByEmail> GetUserByEmail(string email)
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

        [HttpGet("GetUserByPhone/{phone}")]
        public IEnumerable<UserByPhone> GetUserByPhone(string phone)
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


        [HttpGet("GetFiredUsers")]
        public IEnumerable<UserFired> GetFiredUsers()
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


        [HttpGet("GetHiredUsers")]
        public IEnumerable<UserFired> GetHiredUsers()
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
    }
}
