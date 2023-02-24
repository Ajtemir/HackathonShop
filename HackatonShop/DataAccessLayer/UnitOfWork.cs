using HackatonShop.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace HackatonShop.DataAccessLayer
{
    public class UnitOfWork : DbContext
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
    
}