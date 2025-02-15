using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyWebApiApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }  // New entity
    }

    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)] 
        public required string Name { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        [MaxLength(50)] 
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}