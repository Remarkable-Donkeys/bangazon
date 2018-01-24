using Microsoft.EntityFrameworkCore;
using Bangazon.Models;

namespace Bangazon.Data
{
    public class BangazonContext : DbContext
    {
        public BangazonContext(DbContextOptions<BangazonContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customer { get; set; }
        // public DbSet<ProductType> ProductyType { get; set; }
        // public DbSet<PaymentType> PaymentType { get; set; }
        // public DbSet<Products> Products { get; set; }
        // public DbSet<ShoppingCart> ShoppingCart { get; set; }
        // public DbSet<Departments> Departments { get; set; }
        // public DbSet<TrainingPrograms> TrainingPrograms { get; set; }
        // public DbSet<Computer> Computer { get; set; }
        // public DbSet<Employees> Employees { get; set; }
        // public DbSet<EmployeeComputer> EmployeeComputer { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        }
    }
}