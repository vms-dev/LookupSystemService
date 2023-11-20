using LookupSystem.DataAccess.Data;
using Microsoft.Extensions.Configuration;

namespace LookupSystem.BusinessLogic.Services
{
    public class UserHandlerService: IUserHandlerService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserHandlerService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public bool DeleteOlderData()
        {
            if (int.TryParse(_configuration["CountOfDaysForOlderData"], out var countOfDays))
            {
                _userRepository.DeleteUserOlderThan(countOfDays);
                return true;
            }
            return false;         
        }

    }
}
