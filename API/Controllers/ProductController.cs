using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace API.Controllers;

[Route("products")]
public class ProductController : BaseApiController
{
    private readonly IServiceManager _service;

    public ProductController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _service.ProductService.GetAllProductsAsync(false);
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var product = await _service.ProductService.GetProductAsync(id, false);
        return Ok(product);
    }
}