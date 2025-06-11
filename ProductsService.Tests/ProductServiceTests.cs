using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsService.Repositories;
using ProductsService.Services;
using ProductsService.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ProductsService.Tests;
[TestClass]
public class ProductServiceTests
{
    private Mock<IProductRepository> _mockRepo;
    private IProductService _service;

    [TestInitialize]
    public void Init()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepo.Object);
    }

    [TestMethod]
    public async Task GetAllAsync_PassesFilterThrough()
    {
        // Arrange
        var sample = new[] { new Product { Id = 1, Name = "C", Color = "Green" } };
        _mockRepo.Setup(r => r.GetAllAsync("Green"))
                 .ReturnsAsync(sample);

        // Act
        var result = (await _service.GetAllAsync("Green")).ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Green", result[0].Color);
        _mockRepo.Verify(r => r.GetAllAsync("Green"), Times.Once);
    }

    [TestMethod]
    public async Task Create_CallsRepository()
    {
        var dto = new ProductDto { Name = "X", Color = "Y" };
        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Product>()))
            .ReturnsAsync(new Product { Id=1, Name="X", Color="Y" });

        var result = await _service.CreateAsync(dto);
        Assert.AreEqual(1, result.Id);
        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Product>()), Times.Once);
    }
}