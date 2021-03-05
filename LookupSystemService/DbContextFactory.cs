using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LookupSystem.DataAccess.Data
{

    public class DbContextFactory : IDesignTimeDbContextFactory<LookupSystemDbContext>
    {
        public LookupSystemDbContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("LookupSystemDbContext");
            var optionsBuilder = new DbContextOptionsBuilder<LookupSystemDbContext>();
            var option = optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)).Options;
            return new LookupSystemDbContext(option);
        }
    }
}