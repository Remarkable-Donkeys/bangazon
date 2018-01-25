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
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Product> Product { get; set; }
        // public DbSet<ShoppingCart> ShoppingCart { get; set; }
        // public DbSet<Department> Department { get; set; }
        public DbSet<TrainingProgram> TrainingProgram { get; set; }
        // public DbSet<Computer> Computer { get; set; }
        // public DbSet<Employee> Employee { get; set; }
        // public DbSet<EmployeeComputer> EmployeeComputer { get; set; }
        // public DbSet<OrderedProduct> OrderedProduct {get; set;}
        public DbSet<EmployeeTraining> EmployeeTraining { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        }
    }
}