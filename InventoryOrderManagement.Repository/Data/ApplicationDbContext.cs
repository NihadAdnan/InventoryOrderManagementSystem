using Microsoft.EntityFrameworkCore;
using InventoryOrderManagement.AggregateRoot;
using InventoryOrderManagement.Repository.Data;
using Microsoft.Extensions.Options;


namespace InventoryOrderManagement.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

    }
}


