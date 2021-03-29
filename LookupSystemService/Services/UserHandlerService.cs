using LookupSystem.DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookupSystemService.Services
{
    public class UserHandlerService: IUserHandlerService
    {
        private IUserRepository _userRepository;
        private ILogger<UserHandlerService> _logger;
        private IConfiguration _configuration;

        public UserHandlerService(ILogger<UserHandlerService> logger, IConfiguration configuration, IUserRepository userRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public bool DeleteOlderData()
        {
            try
            {
                if(int.TryParse(_configuration["CountOfDaysForOlderData"], out var countOfDays))
                {
                    _userRepository.DeleteUserOlderThan(countOfDays);
                }

                return true;
            }
            catch(Exception e)
            {
                _logger?.LogError(e, $"Exception: {e.Message}");
                return false;
            }
        }

    }
}
