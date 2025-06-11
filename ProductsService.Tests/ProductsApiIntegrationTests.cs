using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductsService.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProductsService.Tests;
[TestClass]
public class ProductsApiIntegrationTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [TestInitialize]
    public void Init()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TestMethod]
    public async Task CreateProduct_ThenGetById_ReturnsCreatedProduct()
    {
        _client.DefaultRequestHeaders.Add("X-API-KEY", "super-secret-key");
        var dto = new { Name = "Orange", Color = "Orange" };
        var content = new StringContent(
            JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

        // Create
        var post = await _client.PostAsync("/products", content);
        Assert.AreEqual(HttpStatusCode.Created, post.StatusCode);
        //var location = post.Headers.Location!;

        // Retrieve
        var res = await _client.GetAsync("/products?color=Orange");
        Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        var prod = JsonSerializer.Deserialize<List<Product>>(await res.Content.ReadAsStringAsync());
        Assert.AreEqual("Orange", prod[0].Name);
    }

}