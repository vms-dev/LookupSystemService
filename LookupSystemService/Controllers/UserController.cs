using AutoMapper;
using LookupSystem.DataAccess.Data;
using LookupSystem.DataAccess.Interfaces;
using LookupSystem.DataAccess.Models;
using LookupSystem.DataAccess.Repositories;
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
        private readonly IRepository<User> userRepo;

        public UserController(ILogger<UserController> logger, LookupSystemDbContext contex)
        {
            _logger = logger;
            userRepo = new UserRepository(contex);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public IEnumerable<EmailSearchUserViewModel> GetUserByEmail(string email)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, EmailSearchUserViewModel>()
                .ForMember("FirstName", opt => opt.MapFrom(m => m.UserContact.FirstName))
                .ForMember("LastName", opt => opt.MapFrom(m => m.UserContact.LastName))
                .ForMember("Email", opt => opt.MapFrom(m => m.UserContact.Email)));

                var mapper = new Mapper(config);
                var users = mapper.Map<List<EmailSearchUserViewModel>>(userRepo.GetUserByEmail(email));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<EmailSearchUserViewModel>();
            }
        }

        [HttpGet("GetUserByPhone/{phone}")]
        public IEnumerable<PhoneSearchUserViewModel> GetUserByPhone(string phone)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, PhoneSearchUserViewModel>()
                .ForMember("Name", opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember("Phone", opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember("MobilePhone", opt => opt.MapFrom(m => m.UserContact.MobilePhone))
                .ForMember("Country", opt => opt.MapFrom(m => m.UserContact.Country))
                .ForMember("City", opt => opt.MapFrom(m => m.UserContact.City)));

                var mapper = new Mapper(config);
                var users = mapper.Map<List<PhoneSearchUserViewModel>>(userRepo.GetUserByPhone(phone));
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<PhoneSearchUserViewModel>();
            }
        }


        [HttpGet("GetHiredUsers")]
        public IEnumerable<HiredUserViewModel> GetHiredUsers()
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, HiredUserViewModel>()
                .ForMember("Name", opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember("CreatedDate", opt => opt.MapFrom(m => m.CreatedDate.ToShortDateString()))
                .ForMember("DeleteDate", opt => opt.MapFrom(m => m.DeleteDate.HasValue ? m.DeleteDate.Value.ToShortDateString() : null))
                .ForMember("Hired", opt => opt.MapFrom(m => m.Hired))
                .ForMember("ManagerId", opt => opt.MapFrom(m => m.ManagerId))
                .ForMember("Phone", opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember("MobilePhone", opt => opt.MapFrom(m => m.UserContact.MobilePhone))
                .ForMember("Email", opt => opt.MapFrom(m => m.UserContact.Email)));

                var mapper = new Mapper(config);
                var users = mapper.Map<List<HiredUserViewModel>>(userRepo.GetHiredUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<HiredUserViewModel>();
            }
        }


        [HttpGet("GetIdledUsers")]
        public IEnumerable<HiredUserViewModel> GetIdledUsers()
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, HiredUserViewModel>()
                .ForMember("Name", opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember("CreatedDate", opt => opt.MapFrom(m => m.CreatedDate.ToShortDateString()))
                .ForMember("DeleteDate", opt => opt.MapFrom(m => m.DeleteDate.HasValue ? m.DeleteDate.Value.ToShortDateString() : null))
                .ForMember("Hired", opt => opt.MapFrom(m => m.Hired))
                .ForMember("ManagerId", opt => opt.MapFrom(m => m.ManagerId))
                .ForMember("Phone", opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember("MobilePhone", opt => opt.MapFrom(m => m.UserContact.MobilePhone))
                .ForMember("Email", opt => opt.MapFrom(m => m.UserContact.Email)));

                var mapper = new Mapper(config);
                var users = mapper.Map<List<HiredUserViewModel>>(userRepo.GetIdledUsers());
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<HiredUserViewModel>();
            }
        }

    }
}
