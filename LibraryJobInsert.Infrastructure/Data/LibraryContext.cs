using LibraryJobInsert.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryJobInsert.Infrastructure.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public LibraryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
