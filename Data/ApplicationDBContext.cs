using InventoryAndOrderManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAndOrderManagementAPI.Data
{
    public class ApplicationDBContext : DbContext // It is a giant object which helps working with database
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) 
            : base(dbContextOptions) // passing our context options to DbContext
        {
            
        }

        // accessing the tables from db and storing them in properties
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }


        // building one-to-many relations from order to orderItems and product to orderItems
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasOne(orderItem => orderItem.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(orderItem => orderItem.Product)
                .WithMany(product => product.OrderItems)
                .HasForeignKey(orderItem => orderItem.ProductId);
        }

    }
}
