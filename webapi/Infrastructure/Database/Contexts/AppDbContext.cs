using Microsoft.EntityFrameworkCore;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Infrastructure.Database.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {


        }

        public DbSet<User> Users { get; set; }

        //added 4:00pm 1/24/2023
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Admin> Admins { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=(localdb)\\\\ProjectModels;Initial Catalog=WebApiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
