using ProductsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Repositories;
public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    Task<IEnumerable<Product>> GetAllAsync(string? color = null);
    Task<Product?> GetByIdAsync(int id);
}