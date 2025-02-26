using AMA.Robotat.Entities.Components;
using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Entities.Orders;
using AMA.Robotat.Entities.Robots;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AMA.Robotat.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Component> Components { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
