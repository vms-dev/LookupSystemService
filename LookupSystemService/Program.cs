using LookupSystem.DataAccess.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LookupSystemService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Место вызова возможно не верное. Но согласно примерам вызов в методе OnModelCreating класса LookupSystemDbContext
                    // приводил к ошибке записи данных в базу а также при миграции.                    
                    var dbInitializer = services.GetRequiredService<DbInitializer>();
                    dbInitializer.InitializeFakeData();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<LookupSystemDbContext>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
