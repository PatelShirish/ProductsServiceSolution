using ProductsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Services;
public interface IProductService
{
    Task<Product> CreateAsync(ProductDto dto);
    Task<IEnumerable<Product>> GetAllAsync(string? color = null);
    Task<Product?> GetByIdAsync(int id);
}