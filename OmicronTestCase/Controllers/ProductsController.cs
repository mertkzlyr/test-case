using Microsoft.AspNetCore.Mvc;
using OmicronTestCase.DTOs;
using OmicronTestCase.Services;

namespace OmicronTestCase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] ProductFilterRequest filter) =>
        Ok(await service.FilterAsync(filter));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var (product, error) = await service.CreateAsync(request);
        if (error == "category_not_found")
            return BadRequest(new { message = $"Category with id {request.CategoryId} not found." });
        return CreatedAtAction(nameof(GetById), new { id = product!.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var (product, error) = await service.UpdateAsync(id, request);
        if (error == "not_found") return NotFound();
        if (error == "category_not_found")
            return BadRequest(new { message = $"Category with id {request.CategoryId} not found." });
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}