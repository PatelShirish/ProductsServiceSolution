using Microsoft.EntityFrameworkCore;
using ProductsService.Models;

namespace ProductsService.Data;
public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> opts) : base(opts) { }
    public DbSet<Product> Products { get; set; } = null!;
}