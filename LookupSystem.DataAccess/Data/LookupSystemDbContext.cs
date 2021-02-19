using Microsoft.EntityFrameworkCore;
using LookupSystem.DataAccess.Models;

namespace LookupSystem.DataAccess.Data
{
    public class LookupSystemDbContext : DbContext
    {
        public LookupSystemDbContext(DbContextOptions<LookupSystemDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserContact)
                .WithOne(uc => uc.User)
                .HasForeignKey<UserContact>(fk => fk.UserId);

            //modelBuilder.Entity<User>();
            //modelBuilder.Entity<UserContact>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
