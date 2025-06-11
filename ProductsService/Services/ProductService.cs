using ProductsService.Models;
using ProductsService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) => _repo = repo;

    public async Task<Product> CreateAsync(ProductDto dto)
    {
        var product = new Product { Name = dto.Name, Color = dto.Color };
        return await _repo.CreateAsync(product);
    }

    public Task<IEnumerable<Product>> GetAllAsync(string? color = null)
        => _repo.GetAllAsync(color);

    public Task<Product?> GetByIdAsync(int id)
        => _repo.GetByIdAsync(id);
}