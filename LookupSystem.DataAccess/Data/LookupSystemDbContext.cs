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
        public DbSet<Tag> Tags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserContact)
                .WithOne(uc => uc.User)
                .HasForeignKey<UserContact>(fk => fk.UserId);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Tags)
                .WithMany(u => u.Users)
                .UsingEntity(j => j.ToTable("UsersTags"));

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
