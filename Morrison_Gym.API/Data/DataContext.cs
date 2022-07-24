using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Models;

namespace Morrison_Gym.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Coach"
                },
                new Role()
                {
                    Id = 3,
                    Name = "Worker"
                },
                new Role()
                {
                    Id = 4,
                    Name = "Customer"
                }
         );
            
        }
    }
}
