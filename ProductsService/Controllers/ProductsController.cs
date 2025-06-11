using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductsService.Models;
using ProductsService.Services;

namespace ProductsService.Controllers;
[ApiController]
[Route("products")]
[Authorize(AuthenticationSchemes = "ApiKey")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto dto)
    {
        var product = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? color)
        => Ok(await _service.GetAllAsync(color));


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _service.GetByIdAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }
}