using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Repositories;
using ProductsService.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ProductsService.Tests;
[TestClass]
public class ProductRepositoryTests
{
    private ProductDbContext _db;
    private ProductRepository _repo;

    [TestInitialize]
    public void Init()
    {
        var opts = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
       
        _db = new ProductDbContext(opts);

        _db.Database.EnsureDeleted();
        _db.Database.EnsureCreated();

        _repo = new ProductRepository(_db);
    }

    [TestMethod]
    public async Task CreateAndRetrieveProduct()
    {
        var p = new Product { Name = "Test", Color = "Blue" };
        var created = await _repo.CreateAsync(p);
        Assert.IsTrue(created.Id > 0);

        var all = (await _repo.GetAllAsync(null)).ToList();
        Assert.AreEqual(1, all.Count);
        Assert.AreEqual("Blue", all.First().Color);
    }

    [TestMethod]
    public async Task GetAllAsync_NoFilter_ReturnsAllProducts()
    {
        // Arrange: seed three products of different colours
        _db.Products.AddRange(
            new Product { Name = "A", Color = "Red" },
            new Product { Name = "B", Color = "Green" },
            new Product { Name = "C", Color = "Blue" }
        );
        await _db.SaveChangesAsync();

        // Act
        var all = (await _repo.GetAllAsync(null)).ToList();

        // Assert
        Assert.AreEqual(3, all.Count);
        CollectionAssert.AreEquivalent(
            new[] { "Red", "Green", "Blue" },
            all.Select(p => p.Color).ToArray()
        );
    }

    [TestMethod]
    public async Task GetAllAsync_WithColorFilter_ReturnsOnlyMatchingProducts()
    {
        // Arrange: two Reds, one Yellow
        _db.Products.AddRange(
            new Product { Name = "X", Color = "Red" },
            new Product { Name = "Y", Color = "Red" },
            new Product { Name = "Z", Color = "Yellow" }
        );
        await _db.SaveChangesAsync();

        // Act
        var reds = (await _repo.GetAllAsync("Red")).ToList();

        // Assert
        Assert.AreEqual(2, reds.Count);
        Assert.IsTrue(reds.All(p => p.Color == "Red"));
    }

    [TestMethod]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectProduct()
    {
        // Arrange: create one product
        var created = await _repo.CreateAsync(new Product { Name = "Single", Color = "Purple" });

        // Act
        var fetched = await _repo.GetByIdAsync(created.Id);

        // Assert
        Assert.IsNotNull(fetched);
        Assert.AreEqual(created.Id, fetched!.Id);
        Assert.AreEqual("Single", fetched.Name);
    }

}