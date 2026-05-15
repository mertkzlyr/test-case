using OmicronTestCase.DTOs;
using OmicronTestCase.Models;
using OmicronTestCase.Repositories;

namespace OmicronTestCase.Services;

public class ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository) : IProductService
{
    public async Task<List<ProductResponse>> GetAllAsync()
    {
        var products = await productRepository.GetAllAsync();
        return products.Select(MapToResponse).ToList();
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return product is null ? null : MapToResponse(product);
    }

    public async Task<List<ProductResponse>> FilterAsync(ProductFilterRequest filter)
    {
        var products = await productRepository.FilterAsync(filter);
        return products.Select(MapToResponse).ToList();
    }

    public async Task<(ProductResponse? product, string? error)> CreateAsync(CreateProductRequest request)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null) return (null, "category_not_found");

        var product = new Product
        {
            Title = request.Title,
            Description = request.Description,
            CategoryId = request.CategoryId,
            StockQuantity = request.StockQuantity,
            IsLive = request.StockQuantity >= category.MinStockQuantity
        };

        var created = await productRepository.CreateAsync(product);
        created.Category = category;
        return (MapToResponse(created), null);
    }

    public async Task<(ProductResponse? product, string? error)> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return (null, "not_found");

        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null) return (null, "category_not_found");

        product.Title = request.Title;
        product.Description = request.Description;
        product.CategoryId = request.CategoryId;
        product.StockQuantity = request.StockQuantity;
        product.IsLive = request.StockQuantity >= category.MinStockQuantity;

        var updated = await productRepository.UpdateAsync(product);
        updated.Category = category;
        return (MapToResponse(updated), null);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return false;

        await productRepository.DeleteAsync(product);
        return true;
    }
    
    private static ProductResponse MapToResponse(Product p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        CategoryId = p.CategoryId,
        CategoryName = p.Category.Name,
        StockQuantity = p.StockQuantity,
        IsLive = p.IsLive
    };
}