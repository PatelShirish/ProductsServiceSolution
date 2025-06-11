using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _db;
    public ProductRepository(ProductDbContext db) => _db = db;

    public async Task<Product> CreateAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(string? color = null)
    {
        var query = _db.Products.AsQueryable();
        if (!string.IsNullOrEmpty(color))
            query = query.Where(p => p.Color == color);
        return await query.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
        => await _db.Products.FindAsync(id);
}