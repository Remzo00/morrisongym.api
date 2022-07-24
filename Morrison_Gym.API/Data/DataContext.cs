using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Models;

namespace Morrison_Gym.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

<<<<<<< HEAD:Data/DataContext.cs
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Coach>? Coaches { get; set; }
        public DbSet<Customer>? Customers { get; set; }
=======
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Customer> Customers { get; set; }
>>>>>>> main:Morrison_Gym.API/Data/DataContext.cs

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
<<<<<<< HEAD:Data/DataContext.cs
                    Name = "User"
=======
                    Name = "Worker"
>>>>>>> main:Morrison_Gym.API/Data/DataContext.cs
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
