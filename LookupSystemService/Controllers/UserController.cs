using AutoMapper;
using LookupSystem.Business.Interfaces;
using LookupSystem.Core;
using LookupSystemService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookupSystemService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public IEnumerable<UserViewModel> GetAll()
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, UserViewModel>());
                var mapper = new Mapper(config);
                var users = mapper.Map<List<UserViewModel>>(_userService.GetAll());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<UserViewModel>();
            }
        }

        [HttpGet("GetUserByEmail/{email}")]
        public IEnumerable<EmailSearchUserViewModel> GetUserByEmail(string email)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, EmailSearchUserViewModel>());
                var mapper = new Mapper(config);
                var users = mapper.Map<List<EmailSearchUserViewModel>>(_userService.GetUserByEmail(email));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);                
                return new List<EmailSearchUserViewModel>();
            }
        }
    }
}
