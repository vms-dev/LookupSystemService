using LookupSystem.BusinessLogic.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LookupSystemService
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private Timer _timer;

        private readonly IServiceScopeFactory _scopeFactory;

        public TimedHostedService(ILogger<TimedHostedService> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

#if DEBUG
            var dueTime = TimeSpan.FromSeconds(10); //TimeSpan.Zero;
            var period = TimeSpan.FromSeconds(30);
#else
            var startTime = DateTime.Parse(_configuration["StartTimeOfSchedulerForDeleteOldData"]);
            var dueTime = startTime - DateTime.Now;
            if (startTime < DateTime.Now)
            {
                dueTime = startTime.AddDays(1) - DateTime.Now;
            }
            
            var periodInHours = int.Parse(_configuration["PeriodInHoursOfSchedulerForDeleteOldDataIn"]);
            var period = TimeSpan.FromHours(periodInHours);
#endif

            _timer = new Timer(DoWork, null, dueTime, period);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                _logger.LogInformation($"Timed Background Service is working. {DateTime.Now}");

                using (var scope = _scopeFactory.CreateScope())
                {
                    var userHandlerService = scope.ServiceProvider.GetRequiredService<IUserHandlerService>();
                    if (userHandlerService != null)
                    {
                        userHandlerService.DeleteOlderData();
                    }
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, $"Exception: {e.Message}");
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
